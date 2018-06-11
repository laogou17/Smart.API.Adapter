using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.DTO.JD
{
    public class RequsetJDQueryPay : RequestJDBase
    {
        public string logNo
        {
            get;
            set;
        }

        public string payType
        {
            get;
            set;
        }
    }
}
