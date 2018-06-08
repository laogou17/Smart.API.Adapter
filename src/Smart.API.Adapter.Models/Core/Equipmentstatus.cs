using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.Core
{
    public class EquipmentStatus
    {
        /// <summary>
        /// 设备guid
        /// </summary>
        public string deviceGuid
        {
            get;
            set;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string deviceName
        {
            get;
            set;
        }

        /// <summary>
        /// 出入口类型
        /// </summary>
        public int deviceIoType
        {
            get;
            set;
        }


        /// <summary>
        /// 设备状态
        /// </summary>
        public string deviceStatus
        {
            get;
            set;
        }
    }
}
