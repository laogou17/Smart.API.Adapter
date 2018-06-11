using Smart.API.Adapter.BizCore.JD;
using Smart.API.Adapter.Common;
using Smart.API.Adapter.Common.JD;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.Models.Core;
using Smart.API.Adapter.Models.Core.JD;
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
                var result = client.PostAsync("external/heartbeatCheck", content).Result;
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

            ApiResult<HeartVersion> result = requestApi.PostRaw<HeartVersion>("external/heartbeatCheck", req);

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
                var result = client.PostAsync("external/queryVehicleLegality", content).Result;

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
            ApiResult<VehicleLegality> result = requestApi.PostRaw<VehicleLegality>("external/queryVehicleLegality", req);
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
                var result = await client.PostAsync("external/modifyParkLotRemainCount", content);

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
                var result = await client.PostAsync("external/modifyParkLotTotalCount", content);

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
                    ApiResult<BaseJdRes> apiResult = httpApi.PostUrl<BaseJdRes>("external/checkEquipment", requestEquipmentInfo);
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
                                apiBaseResult.code = "0";
                                if (dicReConnectInfo.ContainsKey((int)enumJDBusinessType.EquipmentStatus))
                                {
                                    dicReConnectInfo.Remove((int)enumJDBusinessType.EquipmentStatus);
                                }
                            }
                            else if (apiResult.data.returnCode == "fail")
                            {
                                apiBaseResult.code = "1";
                                apiBaseResult.msg = "请求第三方失败，返回[fail]:" + apiResult.data.description;
                            }
                            else
                            {
                                apiBaseResult.code = "1";
                                apiBaseResult.msg = "请求第三方失败，返回[exception]:" + apiResult.data.description;
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
        /// 检查是否白名单(第三方鉴权)
        /// </summary>
        /// <param name="inRecognitionRecord"></param>
        /// <returns></returns>
        public APIResultBase CheckWhiteList(InRecognitionRecord inRecognitionRecord, enumJDBusinessType businessType)
        {
            APIResultBase apiBaseResult = new APIResultBase();
            apiBaseResult.code = "0";
            apiBaseResult.msg = "";
            try
            {
                ICollection<VehicleInfo> VehicleInfoCollection = JDCommonSettings.ParkWhiteList;
                bool bIsWhiteList = true;
                var query = VehicleInfoCollection.Where(p => p.vehicleNo == inRecognitionRecord.plateNumber).FirstOrDefault();
                if (query == null)
                {
                    bIsWhiteList = false;
                }
                apiBaseResult = PostInRecognition(inRecognitionRecord, businessType);
                if (apiBaseResult.code != "0")//请求第三方接口失败
                {
                    //推送识别记录失败
                    if (!bIsWhiteList)//非法车辆，补推记录，但不开闸
                    {
                        apiBaseResult.code = "98";// 约定jielink+ api code="98" ，不开闸，但补推记录
                        apiBaseResult.msg = apiBaseResult.msg + ",非法车辆不开闸，补推记录";
                    }
                    else
                    {
                        //白名单，推送记录失败，开闸，补推记录。
                        apiBaseResult.code = "99";// 约定jielink+ api code="99" ，开闸，补推记录
                        apiBaseResult.msg = apiBaseResult.msg + ",白名单开闸，补推记录";
                    }
                }
                else  //请求第三方成功
                {
                    if (bIsWhiteList)//白名单
                    {
                        apiBaseResult.code = "0";
                        apiBaseResult.msg = "";
                    }
                    else
                    {
                        apiBaseResult.code = "1";
                        apiBaseResult.msg = "非法车辆";
                    }
                }
            }
            catch (Exception ex)
            {
                apiBaseResult.msg = "检查白名单失败";
                LogHelper.Error("检查白名单错误:", ex);
            }
            return apiBaseResult;
        }

        /// <summary>
        /// 到达入口
        /// </summary>
        /// <param name="inRecognitionRecord"></param>
        /// <returns></returns>
        public APIResultBase PostInRecognition(InRecognitionRecord inRecognitionRecord, enumJDBusinessType businessType)
        {
            APIResultBase apiBaseResult = new APIResultBase();
            apiBaseResult.code = "99";//99代表数据需要重传 ,发请重试，频率是每5秒重试一次。
            apiBaseResult.msg = "";
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
                if (inRecognitionRecord.reTrySend == "1")
                {
                    reqVehicleLog.resend = "0";//补发的记录
                }
                bool bReTry = true;
                JDTimer jdTimer = CommonSettings.JDTimerInfo(businessType);
                string sReType = "unavailable";
                if (dicReConnectInfo.ContainsKey((int)businessType))
                {
                    if (dicReConnectInfo[(int)businessType].ReCount > jdTimer.ReConnectCount)
                    {
                        reqVehicleLog.resend = "0";
                        bReTry = dicReConnectInfo[(int)businessType].IsReTry;
                        sReType = dicReConnectInfo[(int)businessType].ReType;
                    }
                }

                if (!bReTry)
                {
                    JDRePostUpdatePostTime(businessType, sReType);
                    apiBaseResult.msg = "等待第三方重试的时间间隔";
                    return apiBaseResult;
                }

                ApiResult<BaseJdRes> apiResult = httpApi.PostUrl<BaseJdRes>("external/createVehicleLogDetail", reqVehicleLog);
                if (!apiResult.successed)
                {
                    apiBaseResult.msg = "请求第三方失败，" + apiResult.message;
                    JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
                }
                else
                {
                    if (apiResult.data != null)
                    {
                        if (apiResult.data.returnCode == "success")
                        {
                            apiBaseResult.code = "0";//请求成功
                            if (dicReConnectInfo.ContainsKey((int)businessType))
                            {
                                dicReConnectInfo.Remove((int)businessType);
                            }
                        }
                        else if (apiResult.data.returnCode == "fail")
                        {
                            JDRePostAndEail(businessType, "fail");//重试计数和发送邮件
                            apiBaseResult.msg = "请求第三方失败，返回[fail]:" + apiResult.data.description;
                        }
                        else
                        {
                            JDRePostAndEail(businessType, "exception");//重试计数和发送邮件
                            apiBaseResult.msg = "请求第三方失败，返回[exception]:" + apiResult.data.description;
                        }
                    }
                    else
                    {
                        apiBaseResult.msg = "请求第三方失败，返回的data为null";
                        JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
                    }
                }
            }
            catch (Exception ex)
            {
                apiBaseResult.msg = "请求第三方失败，" + ex.Message;
                LogHelper.Error("请求第三方入场识别错误:", ex);
                JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
            }

            return apiBaseResult;
            //TODO:如果推送记录失败，写入任务表，进行定时推送（不采用）
            //或者不写任务表，修改Jielink+ api  写入SDK失败记录，让中心重新推送（采用此方式）

        }


        /// <summary>
        /// 进入停车场
        /// </summary>
        /// <param name="inRecognitionRecord"></param>
        /// <returns></returns>
        public APIResultBase PostCarIn(InCrossRecord inCrossRecord, enumJDBusinessType businessType)
        {
            APIResultBase apiBaseResult = new APIResultBase();
            apiBaseResult.code = "99";//99代表数据需要重传 ,jielink+中心发请重试，频率是每5秒重试一次。
            apiBaseResult.msg = "";
            try
            {
                InterfaceHttpProxyApi httpApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);
                RequestVehicleLog reqVehicleLog = new RequestVehicleLog();
                reqVehicleLog.logNo = inCrossRecord.inRecordId;
                reqVehicleLog.actionDescId = "1";//自动抬杆进入停车场
              
                if (inCrossRecord.parkEventType.ToUpper() == "BRUSHCARD" || inCrossRecord.parkEventType.ToUpper() == "OVERTIME")
                {
                    reqVehicleLog.actionDescId = "1";
                }
                else
                {
                    //TODO:需要添加手动抬杆的原因。
                    reqVehicleLog.actionDescId = "2";
                }
               
               

                reqVehicleLog.vehicleNo = inCrossRecord.plateNumber;
                reqVehicleLog.actionTime = inCrossRecord.inTime;
                reqVehicleLog.actionPositionCode = inCrossRecord.inDeviceId;
                reqVehicleLog.actionPosition = inCrossRecord.inDeviceName;
                reqVehicleLog.resend = "1";
                if (inCrossRecord.reTrySend == "1")
                {
                    reqVehicleLog.resend = "0";//补发的记录
                }
                bool bReTry = true;
                JDTimer jdTimer = CommonSettings.JDTimerInfo(businessType);
                string sReType = "unavailable";
                if (dicReConnectInfo.ContainsKey((int)businessType))
                {
                    if (dicReConnectInfo[(int)businessType].ReCount > jdTimer.ReConnectCount)
                    {
                        reqVehicleLog.resend = "0";
                        bReTry = dicReConnectInfo[(int)businessType].IsReTry;
                        sReType = dicReConnectInfo[(int)businessType].ReType;
                    }
                }
                if (!bReTry)
                {
                    JDRePostUpdatePostTime(businessType, sReType);
                    apiBaseResult.code = "99";//99代表数据需要重传 ,jielink+中心会发起重试，频率是每5秒重试一次。
                    apiBaseResult.msg = "等待第三方重试的时间间隔";
                    return apiBaseResult;
                }

                ApiResult<BaseJdRes> apiResult = httpApi.PostUrl<BaseJdRes>("external/createVehicleLogDetail", reqVehicleLog);
                if (!apiResult.successed)
                {
                    apiBaseResult.msg = "请求第三方失败，" + apiResult.message;
                    JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
                }
                else
                {
                    if (apiResult.data != null)
                    {
                        if (apiResult.data.returnCode == "success")
                        {
                            apiBaseResult.code = "0";//请求成功
                            if (dicReConnectInfo.ContainsKey((int)businessType))
                            {
                                dicReConnectInfo.Remove((int)businessType);
                            }
                        }
                        else if (apiResult.data.returnCode == "fail")
                        {
                            JDRePostAndEail(businessType, "fail");//重试计数和发送邮件
                            apiBaseResult.msg = "请求第三方失败，返回[fail]:" + apiResult.data.description;
                        }
                        else
                        {
                            JDRePostAndEail(businessType, "exception");//重试计数和发送邮件
                            apiBaseResult.msg = "请求第三方失败，返回[exception]:" + apiResult.data.description;
                        }
                    }
                    else
                    {
                        apiBaseResult.msg = "请求第三方失败，返回的data为null";
                        JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
                    }
                }
            }
            catch (Exception ex)
            {
                apiBaseResult.msg = "请求第三方失败，" + ex.Message;
                LogHelper.Error("请求第三方入场过闸错误:", ex);
                JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
            }

            return apiBaseResult;
        }


        /// <summary>
        /// 到达出口
        /// </summary>
        /// <param name="inRecognitionRecord"></param>
        /// <returns></returns>
        public APIResultBase PostOutRecognition(OutRecognitionRecord outRecognitionRecord, enumJDBusinessType businessType)
        {
            APIResultBase apiBaseResult = new APIResultBase();
            apiBaseResult.code = "99";//99代表数据需要重传 ,jielink+中心发请重试，频率是每5秒重试一次。
            apiBaseResult.msg = "";
            try
            {
                InterfaceHttpProxyApi httpApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);
                RequestVehicleLog reqVehicleLog = new RequestVehicleLog();
                reqVehicleLog.logNo = string.IsNullOrWhiteSpace(outRecognitionRecord.inRecordId) ? outRecognitionRecord.outRecordId : outRecognitionRecord.inRecordId;
                reqVehicleLog.actionDescId = "101";
                reqVehicleLog.entryTime = outRecognitionRecord.inTime;
                reqVehicleLog.vehicleNo = outRecognitionRecord.plateNumber;
                reqVehicleLog.actionTime = outRecognitionRecord.recognitionTime;
                reqVehicleLog.actionPositionCode = outRecognitionRecord.outDeviceId;
                reqVehicleLog.actionPosition = outRecognitionRecord.outDeviceName;
                reqVehicleLog.resend = "1";
                if (outRecognitionRecord.reTrySend == "1")
                {
                    reqVehicleLog.resend = "0";//补发的记录
                }

                string fileName = "";
                reqVehicleLog.photoStr = StringHelper.GetPicStringByUrl(outRecognitionRecord.outImage, out fileName);
                reqVehicleLog.photoName = fileName;

                bool bReTry = true;
                JDTimer jdTimer = CommonSettings.JDTimerInfo(businessType);
                string sReType = "unavailable";
                if (dicReConnectInfo.ContainsKey((int)businessType))
                {
                    if (dicReConnectInfo[(int)businessType].ReCount > jdTimer.ReConnectCount)
                    {
                        reqVehicleLog.resend = "0";
                        bReTry = dicReConnectInfo[(int)businessType].IsReTry;
                        sReType = dicReConnectInfo[(int)businessType].ReType;
                    }
                }
                if (!bReTry)
                {
                    JDRePostUpdatePostTime(businessType, sReType);
                    apiBaseResult.msg = "等待第三方重试的时间间隔";
                    return apiBaseResult;
                }

                ApiResult<ResponseOutRecognition> apiResult = httpApi.PostUrl<ResponseOutRecognition>("external/createVehicleLogDetail", reqVehicleLog);
                if (!apiResult.successed)
                {
                    apiBaseResult.msg = "请求第三方失败，" + apiResult.message;
                    JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
                }
                else
                {
                    if (apiResult.data != null)
                    {
                        if (apiResult.data.returnCode == "success")
                        {
                            apiBaseResult.code = "0";//请求成功
                            if (dicReConnectInfo.ContainsKey((int)businessType))
                            {
                                dicReConnectInfo.Remove((int)businessType);
                            }

                            //保存JD账单
                            if (apiResult.data.resultCode != "1")//需要缴费
                            {
                                if (outRecognitionRecord.reTrySend != "1")//并且不是补发的记录
                                {
                                    JDBillModel model = new JDBillModel();
                                    model.LogNo = reqVehicleLog.logNo;
                                    model.ResultCode = apiResult.data.resultCode;
                                    model.QrCode = apiResult.data.qrCode;
                                    model.Cost = apiResult.data.cost;

                                    new JDBillBLL().Insert(model);
                                }
                                else
                                {
                                    //补发的记录处理，是否进行账单归档？ 暂不做处理
                                }
                            }
                        }
                        else if (apiResult.data.returnCode == "fail")
                        {
                            JDRePostAndEail(businessType, "fail");//重试计数和发送邮件
                            apiBaseResult.msg = "请求第三方失败，返回[fail]:" + apiResult.data.description;
                        }
                        else
                        {
                            JDRePostAndEail(businessType, "exception");//重试计数和发送邮件
                            apiBaseResult.msg = "请求第三方失败，返回[exception]:" + apiResult.data.description;
                        }
                    }
                    else
                    {
                        apiBaseResult.msg = "请求第三方失败，返回的data为null";
                        JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
                    }
                }
            }
            catch (Exception ex)
            {
                apiBaseResult.msg = "请求第三方失败，" + ex.Message;
                LogHelper.Error("请求第三方出场识别错误:", ex);
                JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
            }

            return apiBaseResult;
        }


        /// <summary>
        /// 离开停车场
        /// </summary>
        /// <param name="inRecognitionRecord"></param>
        /// <returns></returns>
        public APIResultBase PostCarOut(OutCrossRecord outCrossRecord, enumJDBusinessType businessType)
        {
            APIResultBase apiBaseResult = new APIResultBase();
            apiBaseResult.code = "99";//99代表数据需要重传 ,jielink+中心发请重试，频率是每5秒重试一次。
            apiBaseResult.msg = "";
            try
            {
                InterfaceHttpProxyApi httpApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);
                RequestVehicleLog reqVehicleLog = new RequestVehicleLog();
                reqVehicleLog.logNo = string.IsNullOrWhiteSpace(outCrossRecord.inRecordId) ? outCrossRecord.outRecordId : outCrossRecord.inRecordId;
                reqVehicleLog.actionDescId = "5";
                if (outCrossRecord.parkEventType.ToUpper() == "BRUSHCARD" || outCrossRecord.parkEventType.ToUpper() == "OVERTIME")
                {
                    reqVehicleLog.actionDescId = "5";
                }
                else
                {
                    reqVehicleLog.actionDescId = "4";
                }
                reqVehicleLog.entryTime = outCrossRecord.inTime;
                reqVehicleLog.vehicleNo = outCrossRecord.plateNumber;
                reqVehicleLog.actionTime = outCrossRecord.outTime;
                reqVehicleLog.actionPositionCode = outCrossRecord.outDeviceId;
                reqVehicleLog.actionPosition = outCrossRecord.outDeviceName;
                reqVehicleLog.resend = "1";

                if (outCrossRecord.reTrySend == "1")
                {
                    reqVehicleLog.resend = "0";//补发的记录
                }

                bool bReTry = true;
                JDTimer jdTimer = CommonSettings.JDTimerInfo(businessType);
                string sReType = "unavailable";
                if (dicReConnectInfo.ContainsKey((int)businessType))
                {
                    if (dicReConnectInfo[(int)businessType].ReCount > jdTimer.ReConnectCount)
                    {
                        reqVehicleLog.resend = "0";
                        bReTry = dicReConnectInfo[(int)businessType].IsReTry;
                        sReType = dicReConnectInfo[(int)businessType].ReType;
                    }
                }
                if (!bReTry)
                {
                    JDRePostUpdatePostTime(businessType, sReType);
                    apiBaseResult.msg = "等待第三方重试的时间间隔";
                    return apiBaseResult;
                }
                //TODO:出场成功，先查询reasonCode和reason ，进行赋值，并将JD账单记录进行归档,
                reqVehicleLog.reasonCode = "";
                reqVehicleLog.reason = "";


                ApiResult<ResponseOutRecognition> apiResult = httpApi.PostUrl<ResponseOutRecognition>("external/createVehicleLogDetail", reqVehicleLog);
                if (!apiResult.successed)
                {
                    apiBaseResult.msg = "请求第三方失败，" + apiResult.message;
                    JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
                }
                else
                {
                    if (apiResult.data != null)
                    {
                        if (apiResult.data.returnCode == "success")
                        {
                            apiBaseResult.code = "0";//请求成功
                            if (dicReConnectInfo.ContainsKey((int)businessType))
                            {
                                dicReConnectInfo.Remove((int)businessType);
                            }



                        }
                        else if (apiResult.data.returnCode == "fail")
                        {
                            JDRePostAndEail(businessType, "fail");//重试计数和发送邮件
                            apiBaseResult.msg = "请求第三方失败，返回[fail]:" + apiResult.data.description;
                        }
                        else
                        {
                            JDRePostAndEail(businessType, "exception");//重试计数和发送邮件
                            apiBaseResult.msg = "请求第三方失败，返回[exception]:" + apiResult.data.description;
                        }
                    }
                    else
                    {
                        apiBaseResult.msg = "请求第三方失败，返回的data为null";
                        JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
                    }
                }
            }
            catch (Exception ex)
            {
                apiBaseResult.msg = "请求第三方失败，" + ex.Message;
                LogHelper.Error("请求第三方出场过闸错误:", ex);
                JDRePostAndEail(businessType, "unavailable");//重试计数和发送邮件
            }

            return apiBaseResult;
        }

        /// <summary>
        /// 请求第三方计费（读取JD的账单表）
        /// </summary>
        /// <param name="inRecognitionRecord"></param>
        /// <returns></returns>
        public APIResultBase<ResponseThirdCharging> ThirdCharging(RequestThirdCharging requestThirdCharging)
        {
            APIResultBase<ResponseThirdCharging> apiBaseResult = new APIResultBase<ResponseThirdCharging>();
            apiBaseResult.code = "0";
            apiBaseResult.msg = "";
            ResponseThirdCharging thirdCharging = new ResponseThirdCharging();
            thirdCharging.isOpenGate = 1;
            try
            {
                //查询JD账单表
                JDBillModel model = new JDBillBLL().GetJDBillByLogNo(requestThirdCharging.inRecordId);
                if (model != null)
                {
                    float fCharge = 0;
                    float.TryParse(model.Cost, out fCharge);
                    thirdCharging.charge = (int)fCharge * 100;
                    thirdCharging.chargeTotal = (int)fCharge * 100;
                    thirdCharging.discountAmount = 0;
                    if (fCharge <= 0)
                    {
                        thirdCharging.isOpenGate = 1;
                    }
                    else
                    {
                        thirdCharging.isOpenGate = 0;
                    }
                    thirdCharging.paid = 0;
                    thirdCharging.payQrcodeLink = model.QrCode;
                    thirdCharging.payType = "OTHER";
                }
            }
            catch (Exception ex)
            {
                apiBaseResult.code = CommonSettings.ThirdChargingFailCode;
                thirdCharging.isOpenGate = CommonSettings.ThirdChargingIsOpenGate;
                apiBaseResult.msg = "请求第三方计费失败，" + ex.Message;
                LogHelper.Error("请求第三方计费失败:", ex);
            }
            apiBaseResult.data = thirdCharging;
            return apiBaseResult;
        }


        /// <summary>
        /// 支付反查
        /// </summary>
        /// <param name="inRecognitionRecord"></param>
        /// <returns></returns>
        public APIResultBase<ResponsePayCheck> PayCheck(RequestPayCheck requesPayCheck)
        {
            APIResultBase<ResponsePayCheck> apiBaseResult = new APIResultBase<ResponsePayCheck>();
            apiBaseResult.code = "0";
            apiBaseResult.msg = "";
            ResponsePayCheck responsePayCheck = new ResponsePayCheck();

            try
            {
                RequsetJDQueryPay queryPay = new RequsetJDQueryPay();
                queryPay.logNo = requesPayCheck.payNo;

                //查询JD账单表
                JDBillModel model = new JDBillBLL().GetJDBillByLogNo(queryPay.logNo);
                if (model != null)
                {
                    queryPay.payType = model.ResultCode;
                }
                InterfaceHttpProxyApi httpApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);
                ApiResult<ResponseJDQueryPay> apiResult = new ApiResult<ResponseJDQueryPay>();
                try
                {
                    apiResult = httpApi.PostUrl<ResponseJDQueryPay>("external/queryPay", queryPay);
                }
                catch (Exception ex)
                {
                    apiBaseResult.msg = "请求第三方失败，" + apiResult.message;
                    responsePayCheck.chargeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    responsePayCheck.payStatus = 1;
                    responsePayCheck.payType = "OTHER";
                    responsePayCheck.transactionId = queryPay.logNo;
                    //TODO: 更新JD账单，将失败原因写入账单记录 reasonCode 和 reason,出场时需要带上推送

                    apiBaseResult.data = responsePayCheck;
                    return apiBaseResult;
                }

                if (!apiResult.successed)
                {
                    apiBaseResult.msg = "请求第三方失败，" + apiResult.message;
                    responsePayCheck.chargeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    responsePayCheck.payStatus = 1;
                    responsePayCheck.payType = "OTHER";
                    responsePayCheck.transactionId = queryPay.logNo;
                    //TODO: 更新JD账单，将失败原因写入账单记录 reasonCode 和 reason,出场时需要带上推送
                }
                else
                {
                    if (apiResult.data != null)
                    {
                        if (apiResult.data.returnCode == "success")
                        {
                            apiBaseResult.code = "0";//请求成功
                            //完成缴费，开闸
                            responsePayCheck.chargeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            responsePayCheck.payStatus = 1;
                            responsePayCheck.payType = "OTHER";
                            responsePayCheck.transactionId = queryPay.logNo;
                        }
                        else if (apiResult.data.returnCode == "fail")
                        {
                            apiBaseResult.msg = "请求第三方失败，返回[fail]:" + apiResult.data.description;

                            responsePayCheck.chargeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            responsePayCheck.payStatus = 0;
                            responsePayCheck.payType = "OTHER";
                            responsePayCheck.transactionId = queryPay.logNo;
                            if (apiResult.data.resultCode == null)
                            {
                                responsePayCheck.payStatus = 0;
                                //TODO: 更新JD账单，将失败原因写入账单记录 reasonCode 和 reason,出场时需要带上推送
                            }
                            else if (apiResult.data.returnCode == "2")
                            {
                                //TODO:每隔3秒重试，重试3次后，更新JD账单，将失败原因写入账单记录 reasonCode 和 reason,出场时需要带上推送
                            }
                            else if (apiResult.data.returnCode == "0")
                            {
                                //TODO:等待20秒，等待二维码支付，重试3次后不再重试，开闸出场，更新JD账单，将失败原因写入账单记录 reasonCode 和 reason,出场时需要带上推送
                            }
                        }
                        else
                        {
                            apiBaseResult.msg = "请求第三方失败，返回[exception]:" + apiResult.data.description;
                            responsePayCheck.chargeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            responsePayCheck.payStatus = 1;
                            responsePayCheck.payType = "OTHER";
                            responsePayCheck.transactionId = queryPay.logNo;
                            //TODO: 更新JD账单，将失败原因写入账单记录 reasonCode 和 reason,出场时需要带上推送
                        }
                    }
                    else
                    {
                        apiBaseResult.msg = "请求第三方失败，返回的data为null";
                        responsePayCheck.chargeTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        responsePayCheck.payStatus = 1;
                        responsePayCheck.payType = "OTHER";
                        responsePayCheck.transactionId = queryPay.logNo;
                        //TODO: 更新JD账单，将失败原因写入账单记录 reasonCode 和 reason,出场时需要带上推送
                    }
                }
            }
            catch (Exception ex)//TODO: 重试3次后 ，服务端错误，发送邮件
            {
                apiBaseResult.msg = "请求第三方支付反查失败，" + ex.Message;
                LogHelper.Error("请求第三方支付反查失败:", ex);
            }


            apiBaseResult.data = responsePayCheck;
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
