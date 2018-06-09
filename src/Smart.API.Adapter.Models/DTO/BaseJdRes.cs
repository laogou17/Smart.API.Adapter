using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    /// <summary>
    /// 响应基类
    /// </summary>
    public  class BaseJdRes
    {
        
        public string returnCode { get; set; }
        public string description { get; set; }
    }
}
