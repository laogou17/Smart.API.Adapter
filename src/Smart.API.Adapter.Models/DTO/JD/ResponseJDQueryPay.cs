using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.DTO.JD
{
    public class ResponseJDQueryPay : BaseJdRes
    {
        public string resultCode
        {
            get;
            set;
        }

        public string qrCode
        {
            get;
            set;
        }

        public string cost
        {
            get;
            set;
        }
    }
}
