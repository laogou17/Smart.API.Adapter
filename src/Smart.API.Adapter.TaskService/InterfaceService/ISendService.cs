using Smart.API.Adapter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.TaskService.InterfaceService
{
    public interface ISendService
    {
        /// <summary>
        /// 定时推送
        /// </summary>
        /// <param name="ThreadId"></param>
        void TimerSend(int ThreadId);


        /// <summary>
        /// 即时推送
        /// </summary>
        /// <param name="task"></param>
        void Send(TaskQueueEntity task);
    }
}
