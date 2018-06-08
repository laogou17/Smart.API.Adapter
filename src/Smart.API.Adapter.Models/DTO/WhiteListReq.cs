using Smart.API.Adapter.Models.DTO.JD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models
{
    public class WhiteListReq : RequestJDBase
    {
        public string Version { get; set; }
    }
}
