namespace Smart.API.Adapter.Models
{
    /// <summary>
    /// 黑白名单
    /// </summary>
    public enum BlackWhiteType
    { 
        BlackList = 1,
        GrayList=2,
        WhiteList=3,
    }

    /// <summary>
    /// 京东请求失败需重试的业务类型
    /// </summary>
    public enum enumJDBusinessType
    {
        /// <summary>
        /// 到达入口
        /// </summary>

        InRecognition = 1,

       /// <summary>
        /// 车辆入场
       /// </summary>
        InCross =2 ,

        /// <summary>
        /// 到达出口
        /// </summary>
        OutRecognition = 3,

        /// <summary>
        /// 车辆出场
        /// </summary>
        OutCross = 4,

        /// <summary>
        /// 设备状态
        /// </summary>
        EquipmentStatus = 5,

        /// <summary>
        /// 支付反查
        /// </summary>
        PayCheck = 6,
    }

}