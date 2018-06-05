using System;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Smart.API.Adapter.Web.Api.Filters {
	/// <summary>
	/// API接口异常处理过滤器。
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class ApiExceptionFilterAttribute : ExceptionFilterAttribute {
		/// <summary>
		/// 初始化 Smart.API.Adapter.Web.Api.Filters.ApiExceptionFilterAttribute 类的新实例。
		/// </summary>
		public ApiExceptionFilterAttribute() { }

		/// <summary>
		/// 处理请求过程中发生的异常。
		/// </summary>
		/// <param name="actionExecutedContext"></param>
		public override void OnException(HttpActionExecutedContext actionExecutedContext) {
			Exception exception = actionExecutedContext.Exception;

			HttpRequestMessage request = actionExecutedContext.Request;

			actionExecutedContext.Response = request.CreateErrorResponse(exception);

			// log the unknown exception
			if(!(exception is ApiException)) {
				// note: before call this method, 
				//       must be ensure the actionExecutedContext.Response has an value.
				actionExecutedContext.BuildResponseLog();
			}
		}
	}
}
