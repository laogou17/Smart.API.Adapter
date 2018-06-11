using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.Core
{
    public class RequestThirdCharging
    {
      
        /// <summary>
        /// 入场记录唯一标识
        /// </summary>
        public string inRecordId
        {
            get;
            set;
        }

        /// <summary>
        /// 车牌
        /// </summary>
        public string plateNumber
        {
            get;
            set;
        }
    }
}
