using System;
using System.Net.Http;
using System.Web.Http;
using Smart.API.Adapter.Common;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.Web.Api;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

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
        [HttpGet, WriteLog, ActionName("demo-test")]
        public HttpResponseMessage demotest()
        {
            //签名规则

            string random = "123456";
            long timestamp = Smart.API.Adapter.Common.StringHelper.ConvertDateTimeInt(DateTime.Now);
            string key = "B018D3F4-9029-4C38-BFB2-3477358C6FF6";
            string sn = "random" + random + "timestamp" + timestamp + "key" + key.ToLower();
            MD5 md5 = MD5.Create();
            string serverSign = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(sn))).Replace("-", "");

            return Request.CreateResponse(new { sn = sn, sign = serverSign });
        }
    }
}
