using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    /// <summary>
    /// 停车场剩余车位数请求类
    /// </summary>
    public  class RemainCountReq
    {
        public string parkLotCode { get; set; }

        public int remainTotalCount { get; set; }

        public List<RemainInfo> data { get; set; } 

    }
}
