using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.DTO.JD
{
    /// <summary>
    /// 请求设备状态信息
    /// </summary>
    public class RequestEquipmentInfo : RequestJDBase
    {
        public RequestEquipmentInfo()
            : base()
        {
        
        }

        /// <summary>
        /// 设备状态信息 json
        /// </summary>
        public string device { get; set; }

    }

    /// <summary>
    /// 设备状态信息
    /// </summary>
    public class JDEquipmentInfo
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 设备代码
        /// </summary>

        public string code { get; set; }

        /// <summary>
        /// 设备位置
        /// </summary>

        public string position { get; set; }

        /// <summary>
        /// 0在线 1离线
        /// </summary>

        public string status { get; set; }

        /// <summary>
        /// 同步时间
        /// </summary>
        public string sysTime { get; set; }
    }
}
