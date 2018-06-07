using Smart.API.Adapter.Common;
using Smart.API.Adapter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Biz
{
    public  class JDParkBiz
    {
        /// <summary>
        /// 调用京东接口获取白名单数据版本
        /// </summary>
        /// <returns></returns>
        public async Task<HeartVersion> HeartBeatCheckJd()
        {
            InterfaceHttpProxyApi requestApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJd);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(CommonSettings.BaseAddressJd);
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"sysId", CommonSettings.SysId},  
                    {"parkLotCode", CommonSettings.ParkLotCode}  ,
                    {"token", CommonSettings.Token}                 
                });
                var result = await client.PostAsync("/HeartBeatCheck", content);

                HeartVersion heartJd = result.Content.ToJson().FromJson<HeartVersion>();
                return heartJd;
            }
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
                var result = await client.PostAsync("/QueryVehicleLegality", content);

                VehicleLegality vehicleJd = result.Content.ToJson().FromJson<VehicleLegality>();
                return vehicleJd;
            }
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
                var result = await client.PostAsync("/ModifyParkLotRemainCount", content);

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
                var result = await client.PostAsync("/ModifyParkLotRemainCount", content);

                BaseJdRes resJd = result.Content.ToJson().FromJson<BaseJdRes>();
                return resJd;
            }
        }
    }
}
