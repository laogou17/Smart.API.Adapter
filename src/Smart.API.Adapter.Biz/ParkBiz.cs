using Smart.API.Adapter.Common;
using Smart.API.Adapter.DataAccess;
using Smart.API.Adapter.Models;
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
        public async Task<bool> HeartCheck()
        {
            try
            {
                HeartVersion heartJd = await jdParkBiz.HeartBeatCheckJd();
                if (heartJd.ReturnCode == "Fail")
                {
                    //客户端未验证
                    return false;
                }
                if (heartJd.ReturnCode == "exception")
                {
                    //服务端异常
                    return false;
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
                return true;

            }
        }

        /// <summary>
        /// 更新白名单到本地
        /// </summary>
        public async void UpdateWhiteList(string version)
        {
            try
            {
                VehicleLegality vehicleJd = await jdParkBiz.QueryVehicleLegalityJd(version);
                //服务端不可用，每隔 5s 进行重试， 5次后如仍不行， 客户端 应用 需邮件 通知 服务端 人
                //服务端处理失败,一般是校验问题
                if (vehicleJd.ReturnCode == "fail")
                {
                    return;
                }

                //服务端异常
                if (vehicleJd.ReturnCode == "exception")
                {
                    return;
                }
                //更新到数据库
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
            catch(Exception ex)
            { 
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
            XDocument xDoc = XDocument.Load(xmlAddr);
            xDoc.Root.SetElementValue("Version", heartJd.Version);
            xDoc.Root.SetElementValue("OverFlowCount", heartJd.OverFlowCount);
            xDoc.Save(xmlAddr);
        }

        /// <summary>
        /// 同步白名单信息到JieLink
        /// </summary>
        /// <param name="vehicleList">京东方的白名单信息</param>
        public void UpdateWhiteList(VehicleLegality vehicleJd)
        {

            foreach (var vehicle in vehicleJd.Data)
            {
                //0：合法,1:不合法
                if (vehicle.Yn == "0")
                {
 
                }
            }
            
        }







   






    }
}
