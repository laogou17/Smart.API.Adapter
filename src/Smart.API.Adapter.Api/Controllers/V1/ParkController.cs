using System;
using System.Net.Http;
using System.Web.Http;
using Smart.API.Adapter.Common;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.Web.Api;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Smart.API.Adapter.Biz;
using System.Threading.Tasks;
using Smart.API.Adapter.Models.Core;

namespace Smart.API.Adapter.Api.Controllers.V1
{

    /// <summary>
    /// Smart.API.Adapter Open Api
    /// </summary>

    public class ParkController : ApiControllerBase
    {    
        /// <summary>
        /// 同步设备状态，jielink+调用此接口
        /// </summary>
        /// <param name="LEquipmentStatus"></param>
        /// <returns></returns>
        [HttpPost, WriteLog, ActionName("equipmentstatus")]
        public HttpResponseMessage equipmentstatus(List<EquipmentStatus> LEquipmentStatus)
        {       
            APIResultBase result = new JDParkBiz().PostEquipmentStatus(LEquipmentStatus);
            return Request.CreateResponse(result);
        }

        /// <summary>
        /// 接收车辆入场识别记录，jielink+调用此接口
        /// </summary>
        /// <param name="LEquipmentStatus"></param>
        /// <returns></returns>
        [HttpPost, WriteLog, ActionName("inrecognition")]
        public HttpResponseMessage inrecognition(InRecognitionRecord requestdata)
        {
            APIResultBase result = new JDParkBiz().PostInRecognition(requestdata);
            return Request.CreateResponse(result);
        }
    }
}
