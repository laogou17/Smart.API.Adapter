
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    /// <summary>
    /// 剩余车位数
    /// </summary>
    public  class RemainInfo
    {
        public string RegionCode { get; set; }
        public int RemainCount { get; set; }
    }

    public class TotalInfo
    {
        public string RegionCode { get; set; }

        public int TotalCount { get; set; }
 
    }

   

}
