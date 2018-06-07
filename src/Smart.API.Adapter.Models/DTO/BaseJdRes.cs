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
        
        public string ReturnCode { get; set; }
        public string Description { get; set; }
    }
}
