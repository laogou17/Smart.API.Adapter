using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    public  class TotalCountReq
    {
        public string parkLotCode { get; set; }

        public string totalCount { get; set; }

        public List<TotalInfo> data { get; set; } 
    }
}
