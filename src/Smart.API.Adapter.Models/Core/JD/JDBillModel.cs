using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.Core.JD
{
    public class JDBillModel
    {
        /// <summary>
        /// 车辆进出的唯一标识，采用Jielink+的 inReocrdId
        /// </summary>
        public string LogNo
        {
            get;
            set;
        }

        /// <summary>
        /// 结果码
        /// 0：未结算
        /// 1：无需缴费
        /// 2：等待签约代扣结果
        /// </summary>
        public string ResultCode
        {
            get;
            set;
        }

        /// <summary>
        /// 聚合支付二维码url
        /// </summary>
        public string QrCode
        {
            get;
            set;
        }

        /// <summary>
        /// 应收金额
        /// </summary>
        public string Cost
        {
            get;
            set;
        }

        /// <summary>
        /// 0:未支付
        /// 1：已支付
        /// </summary>
        public int PayResult
        {
            get;
            set;
        }

        /// <summary>
        /// 原因代码
        /// </summary>
        public string ReasonCode
        {
            get;
            set;
        }

        /// <summary>
        /// 原因
        /// </summary>
        public string Reason
        {
            get;
            set;
        }
        

        public DateTime CreatTime
        {
            get;
            set;
        }
    }
}
