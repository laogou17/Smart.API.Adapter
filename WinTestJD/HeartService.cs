using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Smart.API.Adapter.Biz;
using Smart.API.Adapter.Common;

namespace WinTestJD
{
    class HeartService
    {
        private ParkBiz parkBiz = new ParkBiz();
        private MailManager emailManager = new MailManager();
        private Timer timerHeart;
        private Timer timerUpdateParkTotalCount;
        private Timer timerUpdateParkRemainCount;
        private int faliTimes = 0;
        private int faliTimesUpdateParkTotalCount = 0;
        private int faliTimesUpdateParkRemainCount = 0;

        public void Start()
        {
            //初始化版本：

            timerHeart = new Timer(new TimerCallback(HeartCheck), null, 0, Timeout.Infinite);

        }

        /// <summary>
        /// 定时任务：包含心跳检测和白名单
        /// </summary>
        /// <param name="obj"></param>
        private  void HeartCheck(object obj)
        {
            bool result =  parkBiz.HeartCheck();
            if (!result)
            {
                LogHelper.Error(string.Format("{0}:心跳检测失败，服务端出错", DateTime.Now.ToString()));                
                faliTimes++;
            }
            //5次不行则发邮件通知 ,在没有任何反馈时
            if (faliTimes >= 5)
            {
                LogHelper.Error(string.Format("{0}:超过5次,停止检测", DateTime.Now.ToString()));
                faliTimes = 0;
                return;    
                //emailManager.SendMail();
            }
            timerHeart.Change(parkBiz.HeartInterval, Timeout.Infinite);
        }

        public void UpdateParkTotalCount()
        {
            timerUpdateParkTotalCount = new Timer(new TimerCallback(UpdateParkTotalCountCallBack), null, 0, Timeout.Infinite);

        }
        private async void UpdateParkTotalCountCallBack(object obj)
        {
            bool result = await parkBiz.UpdateToltalCount();
            if (!result)
            {
                faliTimesUpdateParkTotalCount++;
                LogHelper.Error(string.Format("{0}:更新车位数量出错{1}次", DateTime.Now.ToString(), faliTimesUpdateParkTotalCount));
                //如果出错5s后重试
                timerUpdateParkTotalCount.Change(parkBiz.HeartInterval, Timeout.Infinite);
            }
            //5次不行则发邮件通知
            if (faliTimesUpdateParkTotalCount >= 5)
            {
                LogHelper.Error(string.Format("{0}::更新车位数量出错超过5次,停止重试", DateTime.Now.ToString()));
                //emailManager.SendMail();
            }
        }
        public void UpdateParkRemainCount()
        {
            timerUpdateParkRemainCount = new Timer(new TimerCallback(UpdateParkRemainCountCallBack), null, 0, Timeout.Infinite);

        }
        private async void UpdateParkRemainCountCallBack(object obj)
        {
            bool result = await parkBiz.UpdateToltalCount();
            if (!result)
            {
                faliTimesUpdateParkRemainCount++;
                LogHelper.Error(string.Format("{0}:更新车位数量出错{1}次", DateTime.Now.ToString(), faliTimesUpdateParkRemainCount));
                //如果出错5s后重试
                timerUpdateParkRemainCount.Change(parkBiz.HeartInterval, Timeout.Infinite);
            }
            //5次不行则发邮件通知
            if (faliTimesUpdateParkRemainCount >= 5)
            {
                LogHelper.Error(string.Format("{0}::更新车位数量出错超过5次,停止重试", DateTime.Now.ToString()));
                //emailManager.SendMail();
            }
        }



    }
}
