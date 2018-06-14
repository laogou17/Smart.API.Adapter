using Smart.API.Adapter.BizCore.JD;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Common.JD
{
    public class JDCommonSettings
    {
        private static ICollection<VehicleInfo> _ParkWhiteList;

        /// <summary>
        /// JD白名单
        /// </summary>
        public static ICollection<VehicleInfo> ParkWhiteList
        {
            get
            {
                if (_ParkWhiteList == null)
                {
                    _ParkWhiteList = new ParkWhiteListBLL().GetParkWhiteList();
                }

                return _ParkWhiteList;
            }
        }

        /// <summary>
        /// 白名单更新，重新加载
        /// </summary>
        public static void ReLoadWhiteList()
        {
            _ParkWhiteList = null;
        }


        private static int remainTotalCount = -1;

        /// <summary>
        /// 剩余车位数
        /// </summary>
        public static int RemainTotalCount
        {
            get
            {
                if (remainTotalCount < 0)
                {
                    try
                    {
                        InterfaceHttpProxyApi requestApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJS);
                        var res = requestApi.PostRaw<ParkPlaceRes>("park/parkingplace", "");
                        if (!res.successed)
                        {
                            LogHelper.Error("请求JieLink剩余车位出错" + res.code);
                        }
                        else
                        {
                            if (res.data != null && res.data.data != null)
                            {
                                remainTotalCount = res.data.data.parkRemainCount;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error("请求JieLink剩余车位出错", ex);
                    }
                }
                return remainTotalCount;
            }
            set { remainTotalCount = value; }
        }


        private static int parkTotalCount = -1;

        /// <summary>
        /// 总车位数
        /// </summary>
        public static int ParkTotalCount
        {
            get
            {
                if (parkTotalCount < 0)
                {
                    try
                    {
                        InterfaceHttpProxyApi requestApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJS);
                        var res = requestApi.PostRaw<ParkPlaceRes>("park/parkingplace", "");
                        if (!res.successed)
                        {
                            LogHelper.Error("请求JieLink剩余车位出错" + res.code);
                        }
                        else
                        {
                            if (res.data != null && res.data.data != null)
                            {
                                parkTotalCount = res.data.data.parkCount;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error("请求JieLink剩余车位出错", ex);
                    }
                }

                return parkTotalCount;
            }
            set { parkTotalCount = value; }
        }

        private static int inParkCount = 0;

        /// <summary>
        /// 场内车辆数
        /// </summary>
        public static int InParkCount
        {
            get
            {
                try
                {
                    InterfaceHttpProxyApi requestApi = new InterfaceHttpProxyApi(CommonSettings.BaseAddressJS);
                    RequestInparkingRecord requestParmters = new RequestInparkingRecord();
                    requestParmters.pageIndex = 1;
                    requestParmters.pageSize = 10;
                    var res = requestApi.PostRaw<APIResultBase<ResponseInparkIngRecord>>("park/inparkingrecord", requestParmters);
                    if (!res.successed)
                    {
                        LogHelper.Error("请求JieLink查询场内记录出错" + res.code);
                    }
                    else
                    {
                        if (res.data != null && res.data.data != null)
                        {
                            inParkCount = res.data.data.totalCount;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error("请求JieLink剩余车位出错", ex);
                }

                return inParkCount;
            }
        }

    }
}
