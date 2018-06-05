using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Smart.API.Adapter.Web.Api.Filters {

	/// <summary>
	/// API接口访问控制过滤器。
	/// </summary>
	/// <remarks>
	/// 1、负责记录请求响应报文。
	/// 2、负责校验请求参数签名。
	/// 3、负责接口访问频率控制。
	/// </remarks>
	public class AccessControlFilterAttribute : ActionFilterAttribute, IActionFilter, IFilter {
		/// <summary>
		/// 接口访问控制上下文。
		/// </summary>
		private class AccessContext : ApiContext {
			public AccessContext(HttpActionContext actionContext)
				: base(actionContext) {

			}

			private string signature = string.Empty;

			/// <summary>
			/// 待签名的文本字符串。
			/// </summary>
			public string SignaturePlainText {
				get {
					if(signature == string.Empty) {
						// 排除 sign 字段
						Parameters.Remove(ApiConstants.ParamSignature);
						// 组装待签名字符串
						signature = string.Join("&",
						   Parameters.OrderBy(pair => pair.Key)
										.Select(pair => pair.Key + "=" + pair.Value));
					}

					return signature;
				}
			}
		}
		/// <summary>
		/// 接口访问控制验证器。
		/// </summary>
		private IApiAccessVerifier apiAccessor;
		/// <summary>
		/// 获取一个值，该值表示是否启用接口请求参数签名校验
		/// </summary>
		public bool AccessSignEnaled {
			get;
			private set;
		}
		/// <summary>
		/// 获取一个值，该值表示是否启用接口访问权限校验
		/// </summary>
		public bool AccessRightsEnabled {
			get;
			private set;
		}
		/// <summary>
		/// 获取一个值，该值表示是否启用接口访问频率校验
		/// </summary>
		public bool AccessFrequencyEnaled {
			get;
			private set;
		}

		/// <summary>
		/// 初始化<see cref="Smart.API.Adapter.Web.Api.Filters.AccessControlFilterAttribute"/>类的新实例。
		/// </summary>
		/// <param name="apiAccessor">IApiAccessVerifier</param>
		public AccessControlFilterAttribute(IApiAccessVerifier apiAccessor) {
			if(apiAccessor == null) {
				throw new ArgumentNullException("apiAccessor");
			}
			this.apiAccessor = apiAccessor;
			InitializeConfigs();
		}

		/// <summary>
		/// 读取系统配置
		/// </summary>
		private void InitializeConfigs() {
			bool apiAccessSignEnabled = true;
			bool apiAccessRightsEnabled = false;
			bool apiAccessFrequencyEnabled = false;

			string apiAccessSignEnabledCfg = ConfigurationManager.
				AppSettings["webapi:SignEnabled"];
			string apiAccessRightsEnabledCfg = ConfigurationManager.
				AppSettings["webapi:RightsEnabled"];
			string apiAccessFrequencyEnabledCfg = ConfigurationManager.
				AppSettings["webapi:FrequencyEnabled"];

			if(!Boolean.TryParse(apiAccessSignEnabledCfg,
				out apiAccessSignEnabled)) {
				apiAccessSignEnabled = true;
			}

			if(!Boolean.TryParse(apiAccessRightsEnabledCfg,
				out apiAccessRightsEnabled)) {
				apiAccessRightsEnabled = false;
			}

			if(!Boolean.TryParse(apiAccessFrequencyEnabledCfg,
				out apiAccessFrequencyEnabled)) {
				apiAccessFrequencyEnabled = false;
			}

			this.AccessSignEnaled = apiAccessSignEnabled;
			this.AccessRightsEnabled = apiAccessRightsEnabled;
			this.AccessFrequencyEnaled = apiAccessFrequencyEnabled;
		}

		/// <summary>
		/// 在调用操作方法之前发生。
		/// </summary>
		/// <param name="actionContext">操作上下文。</param>
		public override void OnActionExecuting(HttpActionContext actionContext) {
			// log the request message.
			actionContext.BuildRequestLog();

			var attr = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();
			bool isAnonymous = attr.Any(a => a is AllowAnonymousAttribute);

			if(isAnonymous) {
				base.OnActionExecuting(actionContext);
				return;
			}

			var accessContext = new AccessContext(actionContext);

			// 校验接口请求参数签名。
			if(AccessSignEnaled) {
				VerifyAccessSign(accessContext);
			}

			// 校验接口访问权限。
			if(AccessRightsEnabled) {
				VerifyAccessRights(accessContext);
			}

			// 判断接口访问频率是否超过配置值。
			if(AccessFrequencyEnaled) {
				VerifyAccessFrequency(accessContext);
			}

			base.OnActionExecuting(actionContext);
		}

		/// <summary>
		/// 在调用操作方法之后发生。
		/// </summary>
		/// <param name="actionExecutedContext"></param>
		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext) {
			base.OnActionExecuted(actionExecutedContext);

			// log the response message.
			actionExecutedContext.BuildResponseLog();
		}

		/// <summary>
		/// 校验请求参数签名是否合法。
		/// </summary>
		/// <param name="accessContext">接口访问请求上下文。</param>
		void VerifyAccessSign(AccessContext accessContext) {
			var verifyResult = apiAccessor.VerifyAccessSign(
					accessContext.Request.appId,
					accessContext.Request.random,
					accessContext.Request.timestamp,
					accessContext.Request.sign,
					accessContext.SignaturePlainText);

			if(!verifyResult.Successed) {
				Error.ThrowInvalidSignException(verifyResult.Message, verifyResult.Code);
			}
		}

		/// <summary>
		/// 校验接口访问权限。
		/// </summary>
		/// <param name="accessContext">接口访问请求上下文。</param>
		void VerifyAccessRights(AccessContext accessContext) {
			string interfaceName = accessContext.ActionContext.GetApiName();

			var verifyResult = apiAccessor.VerifyAccessRights(
					accessContext.Request.appId,
					interfaceName);

			if(!verifyResult.Successed) {
				Error.ThrowNoAccessRightException(verifyResult.Message, verifyResult.Code);
			}
		}

		/// <summary>
		/// 校验接口访问频率是否超过配置值。
		/// </summary>
		/// <param name="accessContext">接口访问请求上下文。</param>
		void VerifyAccessFrequency(AccessContext accessContext) {
			string interfaceName = accessContext.ActionContext.GetApiName();

			var verifyResult = apiAccessor.VerifyAccessFrequency(
				accessContext.Request.appId,
				interfaceName);

			if(verifyResult.Successed) {
				Error.ThrowAccessFrequencyException();
			}
		}
	}
}
