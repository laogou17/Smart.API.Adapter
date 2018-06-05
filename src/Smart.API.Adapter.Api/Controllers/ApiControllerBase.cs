// -----------------------------------------------------------------
// 版权所有：版权所有(C) 2016，Microsoft(China) Co.,LTD
// 系统名称：Smart.API.Adapter
// 文件名称：ApiControllerBase.cs
// 模块名称：Smart.API.Adapter.Api
// 模块编号：
// 作　　者：lixin
// 完成日期：2016-08-01
// 功能说明：定义API控制器的基类。
// -----------------------------------------------------------------
using Smart.API.Adapter.Web.Api;
using System.Web.Http;

namespace Smart.API.Adapter.Api.Controllers
{
    /// <summary>
    /// 定义API控制器的基类。
    /// </summary>
    public abstract class ApiControllerBase : ApiController
    {
        /// <summary>
        /// 自定义API请求上下文。
        /// </summary>
        protected ApiContext Context
        {
            get
            {
                return (ApiContext)Request.Properties[ApiConstants.ContextKey];
            }
        }

        // public methods of api controllers may be over here.

        #region Help Methods


        #endregion
    }
}