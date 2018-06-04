using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.DTO
{
    public  class VehicleLegality : BaseJdRes
    {
        public string Version { get; set; }

        public List<VehicleInfo> Data { get; set; }
    }
}
