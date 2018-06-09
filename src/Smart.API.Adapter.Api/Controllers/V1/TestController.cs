using System;
using System.Net.Http;
using System.Web.Http;
using Smart.API.Adapter.Common;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.Web.Api;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Smart.API.Adapter.Models.DTO.JD;

namespace Smart.API.Adapter.Api.Controllers.V1
{

    /// <summary>
    /// Smart.API.Adapter Open Api
    /// </summary>

    public class TestController : ApiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, WriteLog, ActionName("checkEquipment")]
        public HttpResponseMessage demotest(RequestEquipmentInfo test)
        {
            if (test != null)
            {
                LogHelper.Info("JD接收:" + test.ToJson());
            }
            BaseJdRes jdres = new BaseJdRes();
            jdres.ReturnCode = "0";
            //签名规则

            //string random = "123456";
            //long timestamp = Smart.API.Adapter.Common.StringHelper.ConvertDateTimeInt(DateTime.Now);
            //string key = "B018D3F4-9029-4C38-BFB2-3477358C6FF6";
            //string sn = "random" + random + "timestamp" + timestamp + "key" + key.ToLower();
            //MD5 md5 = MD5.Create();
            //string serverSign = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(sn))).Replace("-", "");

            return Request.CreateResponse(jdres);
        }
    }
}
