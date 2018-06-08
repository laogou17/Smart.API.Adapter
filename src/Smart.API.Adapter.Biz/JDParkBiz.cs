using Smart.API.Adapter.Common;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.Models.Core;
using Smart.API.Adapter.Models.DTO.JD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Biz
{
    public class JDParkBiz
    {
        static int PostEquipmentStatusCount = 0;
        static Dictionary<string, string> dicDevStatus = new Dictionary<string, string>();
        /// <summary>
        /// 调用京东接口获取白名单数据版本
        /// </summary>
        /// <returns></returns>
        public  HeartVersion HeartBeatCheckJd()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonSettings.BaseAddressJd);
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"sysId", CommonSettings.SysId},  
                    {"parkLotCode", CommonSettings.ParkLotCode}  ,
                    {"token", CommonSettings.Token}                 
                });
                var result = client.PostAsync("heartbeatCheck", content).Result;
                HeartVersion  heartJd= result.Content.ReadAsStringAsync().Result.FromJson<HeartVersion>();

                return heartJd;
            }
        }
        public HeartVersion HeartBeatCheckJd2()
        {
            InterfaceHttpProxyApi requestApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);

            HeartReq req = new HeartReq()
            {
                parkLotCode = CommonSettings.ParkLotCode,
                SysId = CommonSettings.SysId,
                token = CommonSettings.Token
            };

            ApiResult<HeartVersion> result =  requestApi.PostRaw<HeartVersion>("HeartBeatCheck",req.ToJson());
            if (result.data == null)
            {
                throw new Exception(result.message);
            }
            return result.data;
        }

        /// <summary>
        /// 调用京东接口获取白名单
        /// </summary>
        /// <returns></returns>
        public async Task<VehicleLegality> QueryVehicleLegalityJd(string version)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonSettings.BaseAddressJd);
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"parkLotCode", CommonSettings.ParkLotCode},  
                    {"version", version}  ,
                    {"token", CommonSettings.Token}                 
                });
                var result = await client.PostAsync("QueryVehicleLegality", content);

                VehicleLegality vehicleJd = result.Content.ToJson().FromJson<VehicleLegality>();
                return vehicleJd;
            }
        }

        /// <summary>
        /// 调用京东接口获取白名单
        /// </summary>
        /// <returns></returns>
        public  VehicleLegality QueryVehicleLegalityJd2(string version)
        {
            InterfaceHttpProxyApi requestApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);
            WhiteListReq req = new WhiteListReq();
            req.Version = version;
            ApiResult<VehicleLegality> result = requestApi.PostRaw<VehicleLegality>("QueryVehicleLegality", req);
            if (result.data == null)
            {
                throw new Exception(result.message);
            }
            return result.data;
        }

        public async Task<BaseJdRes> ModifyParkRemainCount(RemainCountReq remainCountReq)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonSettings.BaseAddressJd);
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"param", remainCountReq.ToJson()},  
                    {"token", CommonSettings.Token}                 
                });
                var result = await client.PostAsync("ModifyParkLotRemainCount", content);

                BaseJdRes resJd = result.Content.ToJson().FromJson<BaseJdRes>();
                return resJd;
            }
        }

        public async Task<BaseJdRes> ModifyParkTotalCount(TotalCountReq totalCountReq)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonSettings.BaseAddressJd);
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"param", totalCountReq.ToJson()},  
                    {"token", CommonSettings.Token}                 
                });
                var result = await client.PostAsync("ModifyParkLotRemainCount", content);

                BaseJdRes resJd = result.Content.ToJson().FromJson<BaseJdRes>();
                return resJd;
            }
        }

        /// <summary>
        /// 同步设备状态
        /// </summary>
        /// <param name="LEquipmentStatus"></param>
        /// <returns></returns>
        public APIResultBase PostEquipmentStatus(List<EquipmentStatus> LEquipmentStatus)
        {
            APIResultBase apiBaseResult = new APIResultBase();
            InterfaceHttpProxyApi httpApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);
            RequestEquipmentInfo requestEquipmentInfo = new RequestEquipmentInfo();

            try
            {
                List<JDEquipmentInfo> LjdEquipment = new List<JDEquipmentInfo>();
                if (LEquipmentStatus != null && LEquipmentStatus.Count > 0)
                {
                    foreach (EquipmentStatus item in LEquipmentStatus)
                    {
                        bool flag = false;//同设备状态未改变，不用上传信息
                        if (dicDevStatus.ContainsKey(item.deviceGuid))
                        {
                            if (dicDevStatus[item.deviceGuid] == item.deviceStatus)
                            {
                                flag = true;
                            }
                        }
                        if (!flag)
                        {
                            JDEquipmentInfo jdEquipment = new JDEquipmentInfo();
                            jdEquipment.code = item.deviceGuid;
                            jdEquipment.name = item.deviceName;
                            jdEquipment.position = GetDevPositonByIoType(item.deviceIoType);
                            jdEquipment.status = GetDevStatus(item.deviceStatus);
                            jdEquipment.sysTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                            LjdEquipment.Add(jdEquipment);
                            if (!dicDevStatus.ContainsKey(item.deviceGuid))
                            {
                                dicDevStatus.Add(item.deviceGuid, item.deviceStatus);
                            }
                        }

                    }
                }
                if (LjdEquipment.Count > 0)
                {
                    requestEquipmentInfo.device = LjdEquipment.ToJson();

                    ApiResult<BaseJdRes> apiResult = httpApi.PostRaw<BaseJdRes>("checkEquipment", requestEquipmentInfo);
                    if (!apiResult.successed)//请求JD接口失败
                    {
                        PostEquipmentStatusCount++;
                        apiBaseResult.code = "1";
                        if (apiResult.data != null)
                        {
                            apiBaseResult.msg = apiResult.data.Description;
                        }
                    }
                    else
                    {
                        PostEquipmentStatusCount = 0;
                    }
                }

            }
            catch (Exception ex)
            {
                apiBaseResult.code = "1";
                apiBaseResult.msg = ex.Message;
                PostEquipmentStatusCount++;
                LogHelper.Error("请求设备状态错误:", ex);
            }
            if (PostEquipmentStatusCount > 5)
            {
                //超过5次失败发送邮件
                PostEquipmentStatusCount = 0;
                //发送邮件
                new MailManager().SendMail();

            }
            return apiBaseResult;
        }

        /// <summary>
        /// 转换设备进出口类型
        /// </summary>
        /// <param name="ioType"></param>
        /// <returns></returns>
        string GetDevPositonByIoType(int ioType)
        {
            string sPosition = "";
            switch (ioType)
            {
                case 0:
                    sPosition = "未启用";
                    break;
                case 1:
                    sPosition = "大车场入口";
                    break;
                case 2:
                    sPosition = "大车场出口";
                    break;
                case 3:
                    sPosition = "小车场入口";
                    break;
                case 4:
                    sPosition = "小车场出口";
                    break;
                case 5:
                    sPosition = "中央收费机";
                    break;
                case 6:
                    sPosition = "中央收费机(带吞卡机的出口)";
                    break;
                default:
                    break;
            }
            return sPosition;
        }

        string GetDevStatus(string status)
        {
            string sStatus = "";
            switch (status)
            {
                case "01":
                    sStatus = "0";
                    break;
                case "02":
                    sStatus = "1";
                    break;
                case "05":
                    sStatus = "1";
                    break;
                default:
                    sStatus = "0";
                    break;
            }
            return sStatus;
        }
    }
}
