using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    public  class ParkPlaceRes
    {
        public string Code { get; set; }
        public string Msg { get; set; }

        public ParkPlaceCount Data { get; set; }
    }

    public class ParkPlaceCount
    {
        public string ParkAreaNo { get; set; }
        public string ParkAreaName { get; set; }

        public string ParkCount { get; set; }

        public string ParkRemainCount { get; set; }

        public List<ParkRegionCount> AreaParkList { get; set; }

    }

    public class ParkRegionCount
    {
        public string AreaNo { get; set; }

        public string AreaName { get; set; }

        public string AreaParkCount { get; set; }

        public string AreaParkRemainCount { get; set; }
    }
}
