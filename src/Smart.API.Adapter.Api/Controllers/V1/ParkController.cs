using System;
using System.Net.Http;
using System.Web.Http;
using Smart.API.Adapter.Common;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.Web.Api;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Smart.API.Adapter.Models.DTO;
using Smart.API.Adapter.Biz;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Api.Controllers.V1
{

    /// <summary>
    /// Smart.API.Adapter Open Api
    /// </summary>

    public class ParkController : ApiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        //[HttpGet, WriteLog, ActionName("demo-test")]
        public HttpResponseMessage demotest()
        {

            return Request.CreateResponse(new { sn = 123 }); 
        }


        public async Task<HttpResponseMessage> QueryVehicleLegality(object jsonObj)
        {
            ParkBiz parkBiz = new ParkBiz();
            VehicleLegality result =await parkBiz.QueryVehicleLegality();

          // string json1=  result.ToJson();
          //VehicleLegality test =  result.ToJson().FromJson<VehicleLegality>();

            return Request.CreateResponse(result );
        }




    }
}
