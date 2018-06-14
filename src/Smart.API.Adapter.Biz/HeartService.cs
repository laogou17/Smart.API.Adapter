using Smart.API.Adapter.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Biz
{
    public  class HeartService
    {
        private ParkBiz parkBiz = new ParkBiz();
        private Timer timerHeart;
        private Timer timerUpdateParkTotalCount;
        private Timer timerUpdateParkRemainCount;
        private Timer timerUpdateEquipmentStatus;
        private Timer timerCheckNowTime;
        private int faliTimes = 0;
        private int faliTimesUpdateParkTotalCount = 0;
        private int faliTimesUpdateParkRemainCount = 0;
        private int faliTimesUpdateEquipmentStatus = 0;
        private bool updateParkTotalByTimeFlag = false;

        private Timer timerUpdateTotal;

        private SendMailHelper mail = new SendMailHelper();

        private static HeartService _heartService;
        private static readonly object  objLock=new object();
        private HeartService()
        { 
        }

        public static HeartService GetInstance()
        {
            if(_heartService==null)
            {
                lock (objLock)
                {
                    if (_heartService == null)
                    {
                        _heartService = new HeartService();
 
                    }
 
                }
            }
            return _heartService;
        
        }

        public void Start()
        {

            //客户端初始化时 每隔 5s向京东服务端调用心跳接口，以检测是否存活
            //客户端应用 初始化时获取白名单，在第一次心跳处理
            string message = string.Format("{0}:服务启动，心跳检测开始,同步车位总数.", DateTime.Now.ToString());
            if (CommonSettings.IsDev)
            {
                Console.WriteLine(message);
            }

            LogHelper.Info(message);
            timerHeart = new Timer(new TimerCallback(HeartCheck), null, 0, Timeout.Infinite);

            //客户端应用初始化时同步停车场总车位数，之后每天0点同步一次
            timerUpdateTotal = new Timer(new TimerCallback(UpdateParkTotalByTime), null, 0, Timeout.Infinite);


        }

        /// <summary>
        /// 定时任务：包含心跳检测和白名单
        /// </summary>
        /// <param name="obj"></param>
        private void HeartCheck(object obj)
        {
            string message = string.Format("{0}:心跳检测：", DateTime.Now.ToString());
            if (CommonSettings.IsDev)
            {
                Console.WriteLine(message);
            }

            LogHelper.Info(message);
            bool result = parkBiz.HeartCheck();
            if (!result)
            {
                faliTimes++;
                string messageError = string.Format("{0}:心跳检测失败，服务端出错：", DateTime.Now.ToString());
                if (CommonSettings.IsDev)
                {
                    Console.WriteLine(messageError);
                }
                LogHelper.Error(messageError);
                if (faliTimes >= 5)
                {
                    messageError = string.Format("{0}:超过5次,停止检测", DateTime.Now.ToString());
                    if (CommonSettings.IsDev)
                    {
                        Console.WriteLine(messageError);
                    }
                    LogHelper.Error(messageError);
                    faliTimes = 0;
                    mail.SendMail();
                }
                //timerHeart.Change(parkBiz.HeartInterval, Timeout.Infinite);
            }
            //5次不行则发邮件通知 ,在没有任何反馈时            
            timerHeart.Change(parkBiz.HeartInterval, Timeout.Infinite);
        }

        private void UpdateParkTotalByTime(object obj)
        {
            UpdateParkTotalCount();
            //一分钟后每隔一分钟检测一次
            timerCheckNowTime = new Timer(new TimerCallback(CheckTime), null, 1000 * 60, 1000 * 60);
        }

        private void CheckTime(object obj)
        {
            //00:00执行
            if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
            {
                timerUpdateTotal.Change(0, Timeout.Infinite);
            }

        }
        //一下注释为00:00执行后，后面每24小时执行一次，上面为每分钟检测一次
        //private void UpdateParkTotalByTime(object obj)
        //{
        //    UpdateParkTotalCount();
        //    //执行过后，每隔一分钟检查时间
        //    if (!updateParkTotalByTimeFlag)
        //    {
        //        timerCheckNowTime = new Timer(new TimerCallback(CheckTime), null, 0, 1000 * 60);
        //    }
        //    updateParkTotalByTimeFlag = true;
        //}

        //private void CheckTime(object obj)
        //{ 
        //    //00:00执行后，后面每24小时执行一次
        //    if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
        //    {
        //        //不再检测时间
        //        timerCheckNowTime.Dispose();
        //        timerUpdateTotal.Change(0, 1000 * 60 * 60 * 24);
        //    }

        //}

        //更新总车位
        public void UpdateParkTotalCount()
        {
            string message = string.Format("{0}:更新车场车位总数", DateTime.Now.ToString());
            if (CommonSettings.IsDev)
            {
                Console.WriteLine(message);
            }
            LogHelper.Info(message);
            timerUpdateParkTotalCount = new Timer(new TimerCallback(UpdateParkTotalCountCallBack), null, 0, Timeout.Infinite);
            //更新总车位后，紧跟着要更新剩余车位
            UpdateParkRemainCount();

        }
        private async void UpdateParkTotalCountCallBack(object obj)
        {
            bool result = await parkBiz.UpdateToltalCount();
            ReTryAndEmail(result, ref faliTimesUpdateParkTotalCount, timerUpdateParkTotalCount, "更新总车位数量");
            //if (!result)
            //{
            //    faliTimesUpdateParkTotalCount++;
            //    LogHelper.Error(string.Format("{0}:更新车位数量出错{1}次", DateTime.Now.ToString(), faliTimesUpdateParkTotalCount));
            //    //如果出错5s后重试
            //    timerUpdateParkTotalCount.Change(parkBiz.HeartInterval, Timeout.Infinite);
            //}
            ////5次不行则发邮件通知
            //if (faliTimesUpdateParkTotalCount >= 5)
            //{
            //    LogHelper.Error(string.Format("{0}::更新车位数量出错超过5次,停止重试", DateTime.Now.ToString()));
            //    faliTimesUpdateParkTotalCount = 0;
            //    mail.SendMail();
            //}
        }
        public void UpdateParkRemainCount()
        {
            LogHelper.Info(string.Format("{0}:更新车场剩余车位数", DateTime.Now.ToString()));
            timerUpdateParkRemainCount = new Timer(new TimerCallback(UpdateParkRemainCountCallBack), null, 0, Timeout.Infinite);

        }
        private async void UpdateParkRemainCountCallBack(object obj)
        {
            bool result = await parkBiz.UpdateRemainCount();
            ReTryAndEmail(result, ref faliTimesUpdateParkRemainCount, timerUpdateParkRemainCount, "更新剩余车位数量");
            //if (!result)
            //{
            //    faliTimesUpdateParkRemainCount++;
            //    LogHelper.Error(string.Format("{0}:更新车位数量出错{1}次", DateTime.Now.ToString(), faliTimesUpdateParkRemainCount));
            //    //如果出错5s后重试
            //    timerUpdateParkRemainCount.Change(parkBiz.HeartInterval, Timeout.Infinite);
            //}
            ////5次不行则发邮件通知
            //if (faliTimesUpdateParkRemainCount >= 5)
            //{
            //    LogHelper.Error(string.Format("{0}::更新车位数量出错超过5次,停止重试", DateTime.Now.ToString()));
            //    faliTimesUpdateParkRemainCount = 0;
            //    mail.SendMail();
            //}
        }

        public void UpdateEquipmentStatus()
        {
            LogHelper.Info(string.Format("{0}:更新设备状态", DateTime.Now.ToString()));
            timerUpdateEquipmentStatus = new Timer(new TimerCallback(UpdateEquipmentStatusCallBack), null, 0, Timeout.Infinite);

        }
        private void UpdateEquipmentStatusCallBack(object obj)
        {
            bool result = parkBiz.UpdateEquipmentStatus();
            ReTryAndEmail(result, ref faliTimesUpdateEquipmentStatus, timerUpdateEquipmentStatus, "更新设备状态");
            //if (!result)
            //{
            //    faliTimesUpdateEquipmentStatus++;
            //    LogHelper.Error(string.Format("{0}:更新设备状态出错{1}次", DateTime.Now.ToString(), faliTimesUpdateParkRemainCount));
            //    //如果出错5s后重试
            //    timerUpdateEquipmentStatus.Change(parkBiz.HeartInterval, Timeout.Infinite);
            //}
            ////5次不行则发邮件通知
            //if (faliTimesUpdateEquipmentStatus >= 5)
            //{
            //    LogHelper.Error(string.Format("{0}::更新设备状态出错超过5次,停止重试", DateTime.Now.ToString()));
            //    faliTimesUpdateEquipmentStatus = 0;
            //    mail.SendMail();
            //}
        }

        private void ReTryAndEmail(bool result, ref int tryCount, Timer timer, string eventStr)
        {
            if (!result)
            {
                tryCount++;
                string message = string.Format("{0}:{2}出错{1}次", DateTime.Now.ToString(), tryCount, eventStr);
                if (CommonSettings.IsDev)
                {
                    Console.WriteLine(message);
                }
                LogHelper.Error(message);
                //超过5次后不再重试，发邮件
                if (tryCount >= 5)
                {
                    message = string.Format("{0}:{1}出错超过5次,停止重试", DateTime.Now.ToString(), eventStr);
                    LogHelper.Error(message);
                    tryCount = 0;
                    mail.SendMail();
                }
                //如果出错5s后重试
                timer.Change(parkBiz.HeartInterval, Timeout.Infinite);
            }
            //5次不行则发邮件通知
        }
    }
}
