using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.Core
{
    public class InRecognitionRecord
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
        /// 车场ID
        /// </summary>
        public string parkId
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
        /// 入场识别时间
        /// </summary>
        public string recognitionTime
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
    }
}
