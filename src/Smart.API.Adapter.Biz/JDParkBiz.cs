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
        /// <summary>
        /// key: 1表示到达入口
        /// 2：车辆入场
        /// 3：到达出口
        /// 4：车辆出场
        /// </summary>
        static Dictionary<int, JDPostInfo> dicReConnectInfo = new Dictionary<int, JDPostInfo>();
        static Dictionary<string, string> dicDevStatus = new Dictionary<string, string>();
        /// <summary>
        /// 调用京东接口获取白名单数据版本
        /// </summary>
        /// <returns></returns>
        public HeartVersion HeartBeatCheckJd()
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
                HeartVersion heartJd = result.Content.ReadAsStringAsync().Result.FromJson<HeartVersion>();
                LogHelper.Info("PostResponse:heartbeatCheck" + result.Content.ReadAsStringAsync().Result);//记录日志
                return heartJd;
            }
        }
        public HeartVersion HeartBeatCheckJd2()
        {
            InterfaceHttpProxyApi requestApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);

            HeartReq req = new HeartReq()
            {
                sysId = CommonSettings.SysId,
                parkLotCode = CommonSettings.ParkLotCode,
                token = CommonSettings.Token
            };

            ApiResult<HeartVersion> result = requestApi.PostRaw<HeartVersion>("heartbeatCheck", req);

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
        public VehicleLegality QueryVehicleLegalityJd(string version)
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
                var result = client.PostAsync("queryVehicleLegality", content).Result;

                VehicleLegality vehicleJd = result.Content.ReadAsStringAsync().Result.FromJson<VehicleLegality>();
                LogHelper.Info("PostResponse:queryVehicleLegality" + result.Content.ReadAsStringAsync().Result);//记录日志
                return vehicleJd;
            }
        }

        /// <summary>
        /// 调用京东接口获取白名单
        /// </summary>
        /// <returns></returns>
        public VehicleLegality QueryVehicleLegalityJd2(string version)
        {
            InterfaceHttpProxyApi requestApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);
            WhiteListReq req = new WhiteListReq();
            req.version = version;
            ApiResult<VehicleLegality> result = requestApi.PostRaw<VehicleLegality>("queryVehicleLegality", req);
            if (result.data == null)
            {
                throw new Exception(result.message);
            }
            return result.data;
        }

        public async Task<BaseJdRes> ModifyParkRemainCount(RemainCountReq remainCountReq)
        {

            //InterfaceHttpProxyApi requestApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);
            //ParkCountReq req = new ParkCountReq();
            //req.Param = remainCountReq.ToJson();
            //ApiResult<BaseJdRes> result = await requestApi.PostAsync<BaseJdRes>("ModifyParkLotTotalCount", req);
            //if (result.data == null)
            //{
            //    throw new Exception(result.message);
            //}
            //return result.data;

            using (HttpClient client = new HttpClient())
            {
                LogHelper.Info("PostRequest:modifyParkLotRemainCount" + remainCountReq.ToJson());//记录日志

                client.BaseAddress = new Uri(CommonSettings.BaseAddressJd);
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"param", remainCountReq.ToJson()},  
                    {"token", CommonSettings.Token}                 
                });
                var result = await client.PostAsync("modifyParkLotRemainCount", content);

                BaseJdRes resJd = result.Content.ReadAsStringAsync().Result.FromJson<BaseJdRes>();
                LogHelper.Info("PostResponse:modifyParkLotRemainCount" + result.Content.ReadAsStringAsync().Result);//记录日志
                return resJd;
            }
        }

        public async Task<BaseJdRes> ModifyParkTotalCount(TotalCountReq totalCountReq)
        {
            using (HttpClient client = new HttpClient())
            {
                LogHelper.Info("PostRequest:modifyParkLotTotalCount" + totalCountReq.ToJson());//记录日志

                client.BaseAddress = new Uri(CommonSettings.BaseAddressJd);
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"param", totalCountReq.ToJson()},  
                    {"token", CommonSettings.Token}                 
                });
                var result = await client.PostAsync("modifyParkLotTotalCount", content);

                BaseJdRes resJd = result.Content.ReadAsStringAsync().Result.FromJson<BaseJdRes>();
                LogHelper.Info("PostResponse:modifyParkLotTotalCount" + result.Content.ReadAsStringAsync().Result);//记录日志
                return resJd;
            }

            //InterfaceHttpProxyApi requestApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);
            //ParkCountReq req = new ParkCountReq();
            //req.Param = totalCountReq.ToJson();
            //ApiResult<BaseJdRes> result = await  requestApi.PostAsync<BaseJdRes>("modifyParkLotTotalCount", req);
            //if (result.data == null)
            //{
            //    throw new Exception(result.message);
            //}
            //return result.data;
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
                bool bReTry = true;
                string sReType = "unavailable";
                JDTimer jdTimer = CommonSettings.JDTimerInfo(enumJDBusinessType.EquipmentStatus);
                if (dicReConnectInfo.ContainsKey((int)enumJDBusinessType.EquipmentStatus))
                {
                    bReTry = dicReConnectInfo[(int)enumJDBusinessType.EquipmentStatus].IsReTry;
                    sReType = dicReConnectInfo[(int)enumJDBusinessType.EquipmentStatus].ReType;
                }
                if (!bReTry)
                {
                    JDRePostUpdatePostTime(enumJDBusinessType.EquipmentStatus, sReType);
                    apiBaseResult.code = "1";
                    apiBaseResult.msg = "等待第三方重试的时间间隔";
                    return apiBaseResult;
                }


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
                    ApiResult<BaseJdRes> apiResult = httpApi.PostUrl<BaseJdRes>("checkEquipment", requestEquipmentInfo);
                    if (!apiResult.successed)//请求JD接口失败
                    {
                        apiBaseResult.code = "1";
                        if (apiResult.data != null)
                        {
                            apiBaseResult.msg = apiResult.data.description;
                        }
                        else
                        {
                            apiBaseResult.msg = apiResult.message;
                        }
                        //处理失败超过次数，则发送邮件
                        JDRePostAndEail(enumJDBusinessType.EquipmentStatus, "unavailable");
                    }
                    else
                    {
                        if (apiResult.data != null)
                        {
                            if (apiResult.data.returnCode == "success")
                            {
                                if (dicReConnectInfo.ContainsKey((int)enumJDBusinessType.EquipmentStatus))
                                {
                                    dicReConnectInfo.Remove((int)enumJDBusinessType.EquipmentStatus);
                                }
                            }
                            else if (apiResult.data.returnCode == "fail")
                            {

                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                apiBaseResult.code = "1";
                apiBaseResult.msg = "请求第三方失败，" + ex.Message;
                LogHelper.Error("请求设备状态错误:", ex);
                //处理失败超过次数，则发送邮件
                JDRePostAndEail(enumJDBusinessType.EquipmentStatus, "unavailable");
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


        /// <summary>
        /// 到达入口
        /// </summary>
        /// <param name="inRecognitionRecord"></param>
        /// <returns></returns>
        public APIResultBase PostInRecognition(InRecognitionRecord inRecognitionRecord)
        {
            APIResultBase apiBaseResult = new APIResultBase();
            try
            {
                InterfaceHttpProxyApi httpApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);
                RequestVehicleLog reqVehicleLog = new RequestVehicleLog();
                reqVehicleLog.logNo = inRecognitionRecord.inRecordId;
                reqVehicleLog.actionDescId = "100";
                reqVehicleLog.vehicleNo = inRecognitionRecord.plateNumber;
                reqVehicleLog.actionTime = inRecognitionRecord.recognitionTime;
                reqVehicleLog.actionPositionCode = inRecognitionRecord.inDeviceId;
                reqVehicleLog.actionPosition = inRecognitionRecord.inDeviceName;
                string fileName = "";
                reqVehicleLog.photoStr = StringHelper.GetPicStringByUrl(inRecognitionRecord.inImage, out fileName);
                reqVehicleLog.photoName = fileName;
                reqVehicleLog.resend = "1";
                bool bReTry = true;
                JDTimer jdTimer = CommonSettings.JDTimerInfo(enumJDBusinessType.InRecognition);
                string sReType = "unavailable";
                if (dicReConnectInfo.ContainsKey((int)enumJDBusinessType.InRecognition))
                {
                    if (dicReConnectInfo[(int)enumJDBusinessType.InRecognition].ReCount > jdTimer.ReConnectCount)
                    {
                        reqVehicleLog.resend = "0";
                        bReTry = dicReConnectInfo[(int)enumJDBusinessType.InRecognition].IsReTry;
                        sReType = dicReConnectInfo[(int)enumJDBusinessType.InRecognition].ReType;
                    }
                }
                if (!bReTry)
                {
                    JDRePostUpdatePostTime(enumJDBusinessType.InRecognition, sReType);
                    apiBaseResult.code = "99";//99代表数据需要重传 ,由jielink+ 会发请重试，频率是每5秒重试一次。
                    apiBaseResult.msg = "等待第三方重试的时间间隔";
                    return apiBaseResult;
                }

                ApiResult<BaseJdRes> apiResult = httpApi.PostUrl<BaseJdRes>("createVehicleLogDetail", reqVehicleLog);
                if (!apiResult.successed)
                {
                    apiBaseResult.code = "99";//99代表数据需要重传 ,由jielink+ 会发请重试，频率是每5秒重试一次。
                    apiBaseResult.msg = "请求第三方失败，" + apiResult.message;
                    JDRePostAndEail(enumJDBusinessType.InRecognition, "unavailable");//重试计数和发送邮件
                }
                else
                {
                    if (apiResult.data != null)
                    {
                        if (apiResult.data.returnCode == "success")
                        {
                            if (dicReConnectInfo.ContainsKey((int)enumJDBusinessType.InRecognition))
                            {
                                dicReConnectInfo.Remove((int)enumJDBusinessType.InRecognition);
                            }
                        }
                        else if (apiResult.data.returnCode == "fail")
                        {
                            JDRePostAndEail(enumJDBusinessType.InRecognition, "fail");//重试计数和发送邮件
                        }
                        else
                        {
                            JDRePostAndEail(enumJDBusinessType.InRecognition, "exception");//重试计数和发送邮件
                        }
                    }
                    else
                    {
                        reqVehicleLog.resend = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                apiBaseResult.code = "99";//99代表数据需要重传
                apiBaseResult.msg = "请求第三方失败，" + ex.Message;
                LogHelper.Error("请求设备状态错误:", ex);
                JDRePostAndEail(enumJDBusinessType.InRecognition, "unavailable");//重试计数和发送邮件
            }
            return apiBaseResult;
        }

        /// <summary>
        /// 重试计数,间隔时间再次重试和发送邮件
        /// </summary>
        /// <param name="type"></param>
        private void JDRePostAndEail(enumJDBusinessType type, string failType)
        {
            try
            {
                int ReConnectCount = 5;
                bool bSendEmail = false;
                JDTimer jdTimer = CommonSettings.JDTimerInfo(type);

                //通过xml配置文件获取重试的次数
                ReConnectCount = jdTimer.ReConnectCount;
                //是否发送邮件
                bSendEmail = jdTimer.SendEmail;

                if (!dicReConnectInfo.ContainsKey((int)type))
                {
                    JDPostInfo jdPostInfo = new JDPostInfo();
                    jdPostInfo.ReCount++;
                    jdPostInfo.ReTime = DateTime.Now;
                    jdPostInfo.IsReTry = true;
                    jdPostInfo.ReType = failType;
                    dicReConnectInfo.Add((int)type, jdPostInfo);
                }
                else
                {
                    dicReConnectInfo[(int)type].ReType = failType;
                    if (dicReConnectInfo[(int)type].ReCount > ReConnectCount)
                    {
                        JDRePostUpdatePostTime(type, failType);
                        return;
                    }
                    dicReConnectInfo[(int)type].ReCount++;
                    dicReConnectInfo[(int)type].ReTime = DateTime.Now;
                }

                if (dicReConnectInfo[(int)type].ReCount > ReConnectCount)
                {
                    //超过重试最大次数后，不再计数增加，防止溢出
                    dicReConnectInfo[(int)type].ReCount = ReConnectCount + 1;
                    //超过5次失败发送邮件
                    if (bSendEmail)
                    {
                        //发送邮件
                        SendMailHelper mail = new SendMailHelper();
                        mail.SendMail();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("重试请求错误：", ex);
            }
        }


        /// <summary>
        /// 更新请求时间
        /// </summary>
        /// <param name="type"></param>
        /// <param name="failType"></param>
        private void JDRePostUpdatePostTime(enumJDBusinessType type, string failType)
        {
            JDTimer jdTimer = CommonSettings.JDTimerInfo(type);
            if (failType == "fail")
            {
                if (DateTime.Now.Subtract(dicReConnectInfo[(int)type].ReTime).TotalSeconds > jdTimer.FailTimeSpan)
                {
                    dicReConnectInfo[(int)type].IsReTry = true;
                    dicReConnectInfo[(int)type].ReTime = DateTime.Now;
                }
                else
                {
                    dicReConnectInfo[(int)type].IsReTry = false;
                }
            }
            else if (failType == "exception")
            {
                if (DateTime.Now.Subtract(dicReConnectInfo[(int)type].ReTime).TotalSeconds > jdTimer.ExceptionTimeSpan)
                {
                    dicReConnectInfo[(int)type].IsReTry = true;
                    dicReConnectInfo[(int)type].ReTime = DateTime.Now;
                }
                else
                {
                    dicReConnectInfo[(int)type].IsReTry = false;
                }
            }
            else
            {
                if (DateTime.Now.Subtract(dicReConnectInfo[(int)type].ReTime).TotalSeconds > jdTimer.UnavailableTimeSpan)
                {
                    dicReConnectInfo[(int)type].IsReTry = true;
                    dicReConnectInfo[(int)type].ReTime = DateTime.Now;
                }
                else
                {
                    dicReConnectInfo[(int)type].IsReTry = false;
                }
            }
        }
    }
}
