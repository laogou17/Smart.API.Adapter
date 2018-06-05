using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.TaskService.InterfaceService
{
    public class SendServiceCreate
    {
        public static ISendService Instance
        {
            get
            {
                string cfgSendServiceName = ConfigurationManager.AppSettings["SendServiceName"];
                if (string.IsNullOrWhiteSpace(cfgSendServiceName))
                {
                    cfgSendServiceName = "";
                }
                switch (cfgSendServiceName)
                {
                    default:
                        {
                            return new SendService();
                        }
                }
            }
        }
    }
}
