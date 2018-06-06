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

        [HttpGet]
        public async Task<HttpResponseMessage> QueryVehicleLegality(object jsonObj)
        {
            ParkBiz parkBiz = new ParkBiz();
            VehicleLegality vehicleJd =await parkBiz.QueryVehicleLegalityJd("1");

            //服务端不可用，每隔 5s 进行重试， 5次后如仍不行， 客户端 应用 需邮件 通知 服务端 人
            //服务端处理失败,一般是校验问题
            if (vehicleJd.ReturnCode=="fail")
            {
            }

            //服务端异常
            if (vehicleJd.ReturnCode == "exception")
            { 
            }
          
          //VehicleLegality test =  result.ToJson().FromJson<VehicleLegality>();

            return Request.CreateResponse(vehicleJd);
        }

        [HttpGet]
        public void Test()
        {
            ParkBiz parkBiz = new ParkBiz();
            parkBiz.Test();

 
        }
        






    }
}
