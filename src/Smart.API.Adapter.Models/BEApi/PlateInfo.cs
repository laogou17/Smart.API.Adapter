using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    public  class PlateInfo
    {
        public string PlateNumber { get; set; }
        public BlackWhiteType BlackWhiteType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        
    }
}
