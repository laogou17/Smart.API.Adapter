using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Models.DTO.JD
{
    public class RequestJDBase
    {
        public RequestJDBase()
        { }
        public string parkLotCode
        {
            get
            {
                string parkLotCode = ConfigurationManager.AppSettings["ParkLotCode"];
                if (string.IsNullOrWhiteSpace(parkLotCode))
                {
                    parkLotCode = "1";
                }
                return parkLotCode;
            }
        }

        public string token
        {
            get
            {
                string token = ConfigurationManager.AppSettings["Token"];
                if (string.IsNullOrWhiteSpace(token))
                {
                    token = "1";
                }
                return token;
            }
        }
    }
}
