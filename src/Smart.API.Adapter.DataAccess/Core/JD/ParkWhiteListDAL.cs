using Smart.API.Adapter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.DataAccess.Core.JD
{
    public class ParkWhiteListDAL : DataBase
    {
        public ParkWhiteListDAL()
            : base(DbName.SmartAPIAdapterCore, "ParkWhiteList", "VehicleNo", false)
        {

        }

        /// <summary>
        /// 获取合法白名单
        /// </summary>
        /// <returns></returns>
        public ICollection<VehicleInfo> GetParkWhiteList()
        {
            string sql = "select * from ParkWhiteList where yn = '0' ";
            return base.GetEnityListBySqlString<VehicleInfo>(sql, null);
        }
    }
}
