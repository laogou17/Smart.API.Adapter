using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    public  class VehicleInfoDb:VehicleInfo
    {
        public VehicleInfoDb()
        { 
        }
        public VehicleInfoDb(VehicleInfo v)
        {
            vehicleNo = v.vehicleNo;
            parkLotCode = v.parkLotCode;
            yn = v.yn;
        }
        public DateTime CreateTime { get;set;}
        public DateTime UpdateTime { get; set; }
    }
}
