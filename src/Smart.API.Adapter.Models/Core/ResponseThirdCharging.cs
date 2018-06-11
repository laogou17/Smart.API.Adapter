using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.Core
{
    public class ResponseThirdCharging
    {
        /// <summary>
        /// 应收金额，单位分
        /// </summary>
        public int chargeTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 打折金额，单位分
        /// </summary>
        public int discountAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 实收金额，单位分
        /// </summary>
        public int charge
        {
            get;
            set;
        }

        /// <summary>
        /// 已支付金额，单位分
        /// </summary>
        public int paid
        {
            get;
            set;
        }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string payType
        {
            get;
            set;
        }

        /// <summary>
        /// 开闸标识
        /// 1：开闸
        /// 0：不开，等待收费
        /// </summary>
        public int isOpenGate
        {
            get;
            set;
        }

        /// <summary>
        /// 支付二维码链接
        /// </summary>
        public string payQrcodeLink
        {
            get;
            set;
        }
    }
}
