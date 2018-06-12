using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    public class ParkPlaceRes
    {
        public string code { get; set; }
        public string msg { get; set; }

        public ParkPlaceCount Data { get; set; }
    }

    public class ParkPlaceCount
    {
        public string parkAreaNo { get; set; }
        public string parkAreaName { get; set; }

        public string parkCount { get; set; }

        public string parkRemainCount { get; set; }

        public List<ParkRegionCount> areaParkList { get; set; }

    }

    public class ParkRegionCount
    {
        public string areaNo { get; set; }

        public string areaName { get; set; }

        public string areaParkCount { get; set; }

        public string areaParkRemainCount { get; set; }
    }
}
