using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Smart.API.Adapter.Biz;

namespace ConsoleParkService
{
    class HeartService
    {
        private ParkBiz parkBiz = new ParkBiz();
        private MailManager emailManager = new MailManager();
        private Timer timerHeart;
        private int faliTimes = 0;
        public void Start()
        {
            //初始化版本：

            timerHeart = new Timer(new TimerCallback(HeartCheck), null, 0, Timeout.Infinite);

        }

        /// <summary>
        /// 定时任务
        /// </summary>
        /// <param name="obj"></param>
        private async void HeartCheck(object obj)
        {
            bool result = await parkBiz.HeartCheck();
            if (!result)
            {
                faliTimes++;
            }
            //5次不行则发邮件通知
            if (faliTimes >= 5)
            {
                emailManager.SendMail();
            }
            timerHeart.Change(parkBiz.HeartInterval, Timeout.Infinite);
        }
    }
}
