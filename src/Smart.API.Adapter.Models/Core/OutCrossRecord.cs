using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.Core
{
    public class OutCrossRecord
    {
        /// <summary>
        /// 出场id
        /// </summary>
        public string outRecordId
        {
            get;
            set;
        }

        /// <summary>
        /// 车场ID
        /// </summary>
        public string parkId
        {
            get;
            set;
        }

        /// <summary>
        /// 出场设备ID
        /// </summary>
        public string outDeviceId
        {
            get;
            set;
        }

        /// <summary>
        /// 出场设备名称
        /// </summary>
        public string outDeviceName
        {
            get;
            set;
        }


        /// <summary>
        /// 出场识别时间
        /// </summary>
        public string outTime
        {
            get;
            set;
        }

        /// <summary>
        /// 入场记录唯一标识
        /// </summary>
        public string inRecordId
        {
            get;
            set;
        }

        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string inDeviceId
        {
            get;
            set;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string inDeviceName
        {
            get;
            set;
        }

        /// <summary>
        /// 入场时间
        /// </summary>
        public string inTime
        {
            get;
            set;
        }

        /// <summary>
        /// 车牌图片地址
        /// </summary>
        public string inImage
        {
            get;
            set;
        }

        /// <summary>
        /// 车牌图片地址
        /// </summary>
        public string outImage
        {
            get;
            set;
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string plateNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 车牌颜色
        /// </summary>
        public string plateColor
        {
            get;
            set;
        }

        /// <summary>
        /// 操作员
        /// </summary>
        public string stationOperator
        {
            get;
            set;
        }

        /// <summary>
        /// 应收金额
        /// </summary>
        public int chargeTotal
        {
            get;
            set;
        }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public int discountAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 实收金额
        /// </summary>
        public int charge
        {
            get;
            set;
        }


        /// <summary>
        /// 套餐名
        /// </summary>
        public string sealName
        {
            get;
            set;
        }

        /// <summary>
        /// 重试标识
        /// 1代表为补发的记录   
        /// </summary>
        public string reTrySend
        {
            get;
            set;
        }

        /// <summary>
        /// 发送记录的时间
        /// </summary>
        public string sendTime
        {
            get;
            set;
        }


        /// <summary>
        /// 事件类型
        /// </summary>
        public string parkEventType
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            get;
            set;
        }
    }
}
