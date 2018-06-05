using Smart.API.Adapter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smart.API.Adapter.Common;

namespace Smart.API.Adapter.TaskService.InterfaceService
{
    public class SendService : ISendService
    {
        #region 实时推送
        public void Send(TaskQueueEntity task)
        {
            switch (task.TaskType)
            { 
                case (int)TaskType.InRecognizingData:
                    SendInRecognizingData(task);
                    break;
                default:
                    break;
            }
        }

        private void SendInRecognizingData(TaskQueueEntity task)
        {

            Api_Channel content = task.Content.FromXML<Api_Channel>();
            //推送至第三方


        }
        #endregion 



    }
}
