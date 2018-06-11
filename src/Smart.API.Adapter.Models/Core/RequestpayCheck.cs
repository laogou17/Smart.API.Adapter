using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.Core
{
    public class RequestPayCheck
    {
        public string parkId
        {
            get;
            set;
        }

        /// <summary>
        /// 订单号，第三方计费的则为inRecordId,入场的id
        /// </summary>
        public string payNo
        {
            get;
            set;
        }
    }
}
