using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    public  class TotalCountReq
    {
        public string ParkLotCode { get; set; }

        public int TotalCount { get; set; }

        public List<RemainInfo> Data { get; set; } 
    }
}
