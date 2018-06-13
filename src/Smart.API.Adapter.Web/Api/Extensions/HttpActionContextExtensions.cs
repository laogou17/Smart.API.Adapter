using Infrastructure.Logging;
using Infrastructure.Logging.Entity;
using Smart.API.Adapter.Common;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Smart.API.Adapter.Web.Api {

	/// <summary>
	/// HttpActionContextExtensions
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpActionContextExtensions {

		/// <summary>
		/// 返回当前请求接口带版本号的名称。
		/// </summary>
		/// <param name="actionContext"><see cref="System.Web.Http.Controllers.HttpActionContext"/></param>
		/// <returns>返回当前请求接口带版本号的名称。</returns>
		public static string GetApiName(this HttpActionContext actionContext) {

			var controllerType = actionContext.ActionDescriptor.ControllerDescriptor.ControllerType;

			var segments = controllerType.Namespace.Split(Type.Delimiter);

			// strip "Controller" from the end of the type name.
			var controllerName = controllerType.Name.Remove(controllerType.Name.Length - QueryStringVersionControllerSelector.ControllerSuffix.Length);

			var versionControllerName = String.Format(CultureInfo.InvariantCulture, "{0}.{1}",
				segments[segments.Length - 1], controllerName);

			return String.Format(CultureInfo.InvariantCulture, "{0}{1}.{2}",
					ApiConstants.NamePrefix,
					versionControllerName,
					actionContext.ActionDescriptor.ActionName);
		}

		/// <summary>
		/// 构造请求消息日志
		/// </summary>
		/// <param name="actionContext"></param>
		public static void BuildRequestLog(this HttpActionContext actionContext) {
			var logFlag = actionContext.ActionDescriptor.GetCustomAttributes<WriteLogAttribute>();
			if(logFlag != null && logFlag.Count > 0) {
				actionContext.Request.Properties[ApiConstants.StartInterfaceTime] = DateTime.Now;
			}
		}

		/// <summary>
		/// 构造响应消息日志
		/// </summary>
		/// <param name="actionExecutedContext"></param>
		public static void BuildResponseLog(this HttpActionExecutedContext actionExecutedContext) {

			var logFlag = actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<WriteLogAttribute>();
			if(logFlag == null || logFlag.Count == 0) {
				return;
			}
			var request = actionExecutedContext.Request;
			var response = actionExecutedContext.Response;

			if(response == null) {
				// not log when the response is null.
				return;
			}

			DateTime startTime;

			if(!request.TryGetPropertyValue<DateTime>(
				ApiConstants.StartInterfaceTime, out startTime)) {
				return;
			}

			// 接口调用日志
			InterfaceLog log = new InterfaceLog();
			log.ApplicationName = "Smart.API.Adapter.Api";
			log.ServerName = Environment.MachineName;
			log.IPAddress = HttpContext.Current.Request.UserHostAddress;
			log.InterfaceName = actionExecutedContext.ActionContext.GetApiName();
			log.MethodName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
			log.RequestTime = startTime;
			log.ResponseTime = DateTime.Now;

			string requestContent = "",
				responseContent = "";

			if(request.Content != null) {
				if(request.Content.IsMimeMultipartContent()) {
					requestContent = "---request with multipart/form-data only log the form data---"
										+ "\r\n\r\n"
										+ HttpContext.Current.Request.Form.ToString();
				}
				else {
					using(var stream = request.Content.ReadAsStreamAsync().Result) {
						stream.Seek(0, System.IO.SeekOrigin.Begin);
						using(var reader = new System.IO.StreamReader(stream)) {
							requestContent = reader.ReadToEnd();
						}
					}
				}
			}
			if(actionExecutedContext.Response.Content != null) {
				responseContent = response.Content.ReadAsStringAsync().Result;
			}

			log.RequestContent = String.Format(CultureInfo.InvariantCulture, "HTTP {0} {1}{2}",
						 request.Method.Method,
						 request.RequestUri.ToString(),
						 string.IsNullOrEmpty(requestContent) ? "" : "\r\n" + requestContent);

			log.ResponseContent = String.Format(CultureInfo.InvariantCulture, "{0} {1}\r\n{2}",
						((int)response.StatusCode).ToString(),
						response.StatusCode.ToString(),
						responseContent);

			//Logger.TryLog(log);
            //写日志
            if (CommonSettings.LogType == "1")
            {
                LogHelper.Info(log.ToJson());
            }
		}
	}
}
