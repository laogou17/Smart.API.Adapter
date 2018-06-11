using Smart.API.Adapter.BizCore.JD;
using Smart.API.Adapter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Common.JD
{
    public class JDCommonSettings
    {
        private static ICollection<VehicleInfo> _ParkWhiteList;

        /// <summary>
        /// JD白名单
        /// </summary>
        public static ICollection<VehicleInfo> ParkWhiteList
        {
            get
            {
                if (_ParkWhiteList == null)
                {
                    _ParkWhiteList = new ParkWhiteListBLL().GetParkWhiteList();
                }

                return _ParkWhiteList;
            }
        }

        /// <summary>
        /// 白名单更新，重新加载
        /// </summary>
        public static void ReLoadWhiteList()
        {
            _ParkWhiteList = null;
        }
    }
}
