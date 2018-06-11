using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.DataAccess.Core.JD;


namespace Smart.API.Adapter.BizCore.JD
{
    public class ParkWhiteListBLL
    {
        ParkWhiteListDAL dal = new ParkWhiteListDAL();
        public ICollection<VehicleInfo> GetParkWhiteList()
        {
            return dal.FindAll<VehicleInfo>();
        }
    }
}
