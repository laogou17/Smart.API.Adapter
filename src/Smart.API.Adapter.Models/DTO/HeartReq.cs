using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    public  class HeartReq
    {
        public string sysId { get; set; }
        public string parkLotCode { get; set; }

        public string token { get; set; }
    }
}
