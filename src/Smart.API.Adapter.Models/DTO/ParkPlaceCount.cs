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

        public ParkPlaceCount data { get; set; }
    }

    public class ParkPlaceCount
    {
        public string parkAreaNo { get; set; }
        public string parkAreaName { get; set; }

        public int parkCount { get; set; }

        public int parkRemainCount { get; set; }

        public List<ParkRegionCount> areaParkList { get; set; }

    }

    public class ParkRegionCount
    {
        public string areaNo { get; set; }

        public string areaName { get; set; }

        public int areaParkCount { get; set; }

        public int areaParkRemainCount { get; set; }
    }
}
