using Smart.API.Adapter.Common;
using Smart.API.Adapter.DataAccess;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Smart.API.Adapter.Biz
{
    public class ParkBiz
    {
        public  static string version = "1";
        public  static int overFlowCount = 100;
        private DataBase dataBase ;
        private string xmlAddr;
        private JDParkBiz jdParkBiz;

        public int HeartInterval
        {
            get
            {
                return  CommonSettings.HeartInterval;
            }
        }
        public ParkBiz()
        {
            xmlAddr =System.IO.Directory.GetParent(System.IO.Directory.GetParent( Environment.CurrentDirectory).ToString()) + CommonSettings.ParkXmlAddress;
            dataBase = new DataBase(DataBase.DbName.SmartAPIAdapterCore, "ParkWhiteList", "VehicleNo", false);
            jdParkBiz = new JDParkBiz();
            InitVersion(); 
        }

        private void InitVersion()
        {
            XDocument xDoc = XDocument.Load(xmlAddr);
            version = xDoc.Root.Element("Version").Value;
            overFlowCount = Convert.ToInt32(xDoc.Root.Element("OverFlowCount").Value);
 
        }

        /// <summary>
        /// 定时执行心跳任务
        /// </summary>
        public  bool HeartCheck()
        {
            try
            {
                HeartVersion heartJd = jdParkBiz.HeartBeatCheckJd2();
                
                if (heartJd.ReturnCode == "Fail")
                {
                    //客户端未验证
                    LogHelper.Error(string.Format("{0}:心跳检测响应Fail:{1}", DateTime.Now.ToString(), heartJd.Description));
                    return true;
                }
                if (heartJd.ReturnCode == "exception")
                {
                    //服务端异常
                    LogHelper.Error(string.Format("{0}:心跳检测响应Fail:{1}", DateTime.Now.ToString(), heartJd.Description));
                    return true;
                }
                if (heartJd.Version != ParkBiz.version)
                {
                    ParkBiz.version = heartJd.Version;
                    ParkBiz.overFlowCount = heartJd.OverFlowCount;
                    UpdateHeartVersion(heartJd);
                    //版本号不一致需要同步白名单
                    UpdateWhiteList(heartJd.Version);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(string.Format("{0}:心跳检测出错:{1}", DateTime.Now.ToString(), ex.Message));
                return false;
            }
        }

        /// <summary>
        /// 更新白名单到本地
        /// </summary>
        public void UpdateWhiteList(string version)
        {
            try
            {
                VehicleLegality vehicleJd = jdParkBiz.QueryVehicleLegalityJd2(version);
                //服务端不可用，每隔 5s 进行重试， 5次后如仍不行， 客户端 应用 需邮件 通知 服务端 人
                //服务端处理失败,一般是校验问题
                if (vehicleJd.ReturnCode == "fail")
                {
                    LogHelper.Error(string.Format("{0}:获取白名单Fail:{1}", DateTime.Now.ToString(), vehicleJd.Description));
                    return;
                }

                //服务端异常
                if (vehicleJd.ReturnCode == "exception")
                {
                    LogHelper.Error(string.Format("{0}:获取白名单exception:{1}", DateTime.Now.ToString(), vehicleJd.Description));
                    return;
                }
                //更新到数据库
                try
                {
                    foreach (VehicleInfo v in vehicleJd.Data)
                    {
                        if (dataBase.IsExist(v.VehicleNo))
                        {
                            dataBase.Update<VehicleInfo>(v, v.VehicleNo);
                        }
                        else
                        {
                            dataBase.Insert<VehicleInfo>(v);
                        }
                    }         
                }
                catch (Exception ex)
                {
                    LogHelper.Error(string.Format("{0}:更新数据库出错:{1}", DateTime.Now.ToString(), ex.Message));
                    throw ex;
                } 
            }
            catch(Exception ex)
            {
                LogHelper.Error(string.Format("{0}:获取京东白名单出错:{1}", DateTime.Now.ToString(), ex.Message));                
            } 
        }

        public void Test()
        {
            VehicleLegality res = new VehicleLegality();
            res.ReturnCode = "OK";
            res.Version = "1";
            res.Description = "Test";
            res.Data = new List<VehicleInfo>();
            res.Data.Add(new VehicleInfo() { ParkLotCode = "1", VehicleNo = "001", Yn = "9" });
            res.Data.Add(new VehicleInfo() { ParkLotCode = "1", VehicleNo = "002", Yn = "6" });
            res.Data.Add(new VehicleInfo() { ParkLotCode = "1", VehicleNo = "004", Yn = "0" });
            foreach (VehicleInfo v in res.Data)
            {
                if (dataBase.IsExist(v.VehicleNo))
                {
                    dataBase.Update<VehicleInfo>(v, v.VehicleNo);
                }
                else
                {
                    dataBase.Insert<VehicleInfo>(v);
                }
            }          
 
        }


        /// <summary>
        /// 更新xml内容
        /// </summary>
        /// <param name="heartJd"></param>
        public void UpdateHeartVersion(HeartVersion heartJd)
        {
            try
            {
                XDocument xDoc = XDocument.Load(xmlAddr);
                xDoc.Root.SetElementValue("Version", heartJd.Version);
                xDoc.Root.SetElementValue("OverFlowCount", heartJd.OverFlowCount);
                xDoc.Save(xmlAddr);
            }
            catch(Exception ex)
            {
                LogHelper.Error(string.Format("{0}:更新xml出错:{1}", DateTime.Now.ToString(), ex.Message));
                throw ex;
            }
        }
      
        /// <summary>
        /// 定时查车位总数并更新
        /// </summary>
        public async Task<bool> UpdateToltalCount()
        {
            try
            {
                TotalCountReq totalReq = new TotalCountReq();
                //try
                //{
                //    //调用Jielink获取车场车位数据
                //    ParkPlaceRes parkPlaceRes = GetParkPlaceCount();

                //    //转换为京东车位数据
                //    totalReq = new TotalCountReq();
                //    totalReq.ParkLotCode = CommonSettings.ParkLotCode;
                //    totalReq.TotalCount = parkPlaceRes.Data.ParkCount;
                //    totalReq.Data = new List<TotalInfo>();

                //    parkPlaceRes.Data.AreaParkList.ForEach(x =>
                //        {
                //            totalReq.Data.Add(new TotalInfo() { RegionCode = x.AreaNo, TotalCount = x.AreaParkCount });
                //        });
                //}
                //catch (Exception ex)
                //{
                //    LogHelper.Error(string.Format("{0}:获取jieLink车场数据出错:{1}", DateTime.Now.ToString(), ex.Message));
 
                //}

                //Demo数据
                totalReq = new TotalCountReq();
                totalReq.ParkLotCode = CommonSettings.ParkLotCode;
                totalReq.TotalCount = 1300;
                totalReq.Data = new List<TotalInfo>();
                totalReq.Data.Add(new TotalInfo() { RegionCode = "A1", TotalCount = 100 });
                totalReq.Data.Add(new TotalInfo() { RegionCode = "A2", TotalCount = 150 });
                totalReq.Data.Add(new TotalInfo() { RegionCode = "B1", TotalCount = 200 });
                totalReq.Data.Add(new TotalInfo() { RegionCode = "B2", TotalCount = 200 });
                totalReq.Data.Add(new TotalInfo() { RegionCode = "C1", TotalCount = 200 });
                totalReq.Data.Add(new TotalInfo() { RegionCode = "C2", TotalCount = 200 });
                totalReq.Data.Add(new TotalInfo() { RegionCode = "C3", TotalCount = 250 });

                //数据推给京东
                BaseJdRes jdRes =await  jdParkBiz.ModifyParkTotalCount(totalReq);
                if (jdRes.ReturnCode == "Fail")
                {
                    LogHelper.Error(string.Format("{0}:更新车位总数响应Fail:{1}",DateTime.Now.ToString(), jdRes.Description));
                    //客户端未验证
                }
                if (jdRes.ReturnCode == "exception")
                {
                    LogHelper.Error(string.Format("{0}:更新车位总数响应Exception:{1}", DateTime.Now.ToString(), jdRes.Description));
                    //服务端异常
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(string.Format("{0}:更新车位总数出错:{1}", DateTime.Now.ToString(), ex.Message));
                return false;

            }
        }

        /// <summary>
        /// 定时查剩余车位数并更新
        /// </summary>
        public async Task<bool> UpdateRemainCount()
        {
            try
            {
                RemainCountReq totalReq = new RemainCountReq();
                //try
                //{
                //    //调用Jielink获取车场车位数据
                //    ParkPlaceRes parkPlaceRes = GetParkPlaceCount();

                //    //转换为京东车位数据
                //    totalReq = new RemainCountReq();
                //    totalReq.ParkLotCode = CommonSettings.ParkLotCode;
                //    totalReq.RemainTotalCount = parkPlaceRes.Data.ParkRemainCount;
                //    totalReq.Data = new List<RemainInfo>();

                //    parkPlaceRes.Data.AreaParkList.ForEach(x =>
                //    {
                //        totalReq.Data.Add(new RemainInfo() { RegionCode = x.AreaNo, RemainCount = x.AreaParkRemainCount });
                //    });
                //}
                //catch (Exception ex)
                //{
                //    LogHelper.Error(string.Format("{0}:获取jieLink车场数据出错:{1}", DateTime.Now.ToString(), ex.Message));
                //}

                //Demo数据
                totalReq = new RemainCountReq();
                totalReq.ParkLotCode = CommonSettings.ParkLotCode;
                totalReq.RemainTotalCount = 500;
                totalReq.Data = new List<RemainInfo>();
                totalReq.Data.Add(new RemainInfo() { RegionCode = "A1", RemainCount = 50 });
                totalReq.Data.Add(new RemainInfo() { RegionCode = "A2", RemainCount = 100 });
                totalReq.Data.Add(new RemainInfo() { RegionCode = "B1", RemainCount = 50 });
                totalReq.Data.Add(new RemainInfo() { RegionCode = "B2", RemainCount = 50 });
                totalReq.Data.Add(new RemainInfo() { RegionCode = "C1", RemainCount = 50 });
                totalReq.Data.Add(new RemainInfo() { RegionCode = "C2", RemainCount = 100 });
                totalReq.Data.Add(new RemainInfo() { RegionCode = "C3", RemainCount = 100 });

                //数据推给京东
                BaseJdRes jdRes = await jdParkBiz.ModifyParkRemainCount(totalReq);
                if (jdRes.ReturnCode == "Fail")
                {
                    LogHelper.Error(string.Format("{0}:更新车位剩余数响应Fail:{1}", DateTime.Now.ToString(), jdRes.Description));
                    //客户端未验证
                }
                if (jdRes.ReturnCode == "exception")
                {
                    LogHelper.Error(string.Format("{0}:更新车位剩余数响应Exception:{1}", DateTime.Now.ToString(), jdRes.Description));
                    //服务端异常
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(string.Format("{0}:更新车位剩余数出错:{1}", DateTime.Now.ToString(), ex.Message));
                return false;

            }
        }

        public  ParkPlaceRes GetParkPlaceCount()
        {            
            string parkId = CommonSettings.ParkLotCode; ;
            InterfaceHttpProxyApi requestApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJS);
            var res =  requestApi.PostRaw<ParkPlaceRes>("/parking/place", parkId);
            if (!res.successed)
            {
                LogHelper.Error("请求JieLink出错" + res.code); 
            }
            return res.data; 
        }

        public  bool UpdateEquipmentStatus()
        {
            try
            {
                List<EquipmentStatus> equipmentStatusList = new List<EquipmentStatus>();
                //try
                //{
                //    //调用Jielink获取车场车位数据
                //    ParkPlaceRes parkPlaceRes = GetParkPlaceCount();

                //    //转换为京东车位数据
                //    totalReq = new RemainCountReq();
                //    totalReq.ParkLotCode = CommonSettings.ParkLotCode;
                //    totalReq.RemainTotalCount = parkPlaceRes.Data.ParkRemainCount;
                //    totalReq.Data = new List<RemainInfo>();

                //    parkPlaceRes.Data.AreaParkList.ForEach(x =>
                //    {
                //        totalReq.Data.Add(new RemainInfo() { RegionCode = x.AreaNo, RemainCount = x.AreaParkRemainCount });
                //    });
                //}
                //catch (Exception ex)
                //{
                //    LogHelper.Error(string.Format("{0}:获取jieLink车场数据出错:{1}", DateTime.Now.ToString(), ex.Message));
                //}

                //Demo数据
                equipmentStatusList.Add(new EquipmentStatus() { deviceGuid = "0001", deviceIoType = 1, deviceName = "闸机1", deviceStatus = "1" });
                equipmentStatusList.Add(new EquipmentStatus() { deviceGuid = "0002", deviceIoType = 2, deviceName = "闸机2", deviceStatus = "0" });
                equipmentStatusList.Add(new EquipmentStatus() { deviceGuid = "0003", deviceIoType = 1, deviceName = "闸机3", deviceStatus = "1" });
                equipmentStatusList.Add(new EquipmentStatus() { deviceGuid = "0004", deviceIoType = 3, deviceName = "闸机4", deviceStatus = "0" });

                //数据推给京东
                var jdRes = jdParkBiz.PostEquipmentStatus(equipmentStatusList);

                //if (jdRes.ReturnCode == "Fail")
                //{
                //    LogHelper.Error(string.Format("{0}:更新车位剩余数响应Fail:{1}", DateTime.Now.ToString(), jdRes.Description));
                //    //客户端未验证
                //}
                //if (jdRes.ReturnCode == "exception")
                //{
                //    LogHelper.Error(string.Format("{0}:更新车位剩余数响应Exception:{1}", DateTime.Now.ToString(), jdRes.Description));
                //    //服务端异常
                //}
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(string.Format("{0}:更新车位剩余数出错:{1}", DateTime.Now.ToString(), ex.Message));
                return false;

            }
        }










   






    }
}
