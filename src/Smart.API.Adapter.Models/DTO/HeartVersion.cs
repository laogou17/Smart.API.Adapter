using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    /// <summary>
    /// 心跳响应
    /// </summary>
    public class HeartVersion : BaseJdRes
    {
        public string Version { get; set; }

        public int OverFlowCount { get; set; }
    }
}
