using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

namespace Smart.API.Adapter.Web.Api {

	/// <summary>
	/// API接口执行上下文。
	/// </summary>
	public class ApiContext {
		HttpActionContext actionContext;
		ApiRequestBase apiRequest;
		Dictionary<string, string> requestParameters;

		public ApiContext(HttpActionContext actionContext) {
			if(actionContext == null) {
				throw new ArgumentNullException("actionContext");
			}

			this.actionContext = actionContext;
			this.BuildApiRequest();
			actionContext.Request.Properties[ApiConstants.ContextKey] = this;
		}

		/// <summary>
		/// 包含正在执行的操作的信息。
		/// </summary>
		public HttpActionContext ActionContext {
			get {
				return this.actionContext;
			}
		}

		/// <summary>
		/// 获取当前API接口请求参数。
		/// </summary>
		public ApiRequestBase Request {
			get {
				return this.apiRequest;
			}
		}

		/// <summary>
		/// 获取原始请求参数列表。
		/// </summary>
		public Dictionary<string, string> Parameters {
			get {
				return this.requestParameters;
			}
		}

		protected void BuildApiRequest() {
			this.requestParameters = GetRequestHeaders(actionContext.Request);// GetRequestParameters(actionContext.Request);
			string accessId="", sign="", version="", random="", timestamp="";
			this.requestParameters.TryGetRequireValue(ApiConstants.ParamAppId, out accessId);
			this.requestParameters.TryGetRequireValue(ApiConstants.ParamRandom, out random);
			this.requestParameters.TryGetRequireValue(ApiConstants.ParamTimestamp, out timestamp);
			this.requestParameters.TryGetRequireValue(ApiConstants.ParamSignature, out sign);
			this.requestParameters.TryGetRequireValue(ApiConstants.ParamVersion, out version);
			//this.requestParameters = GetRequestParameters(actionContext.Request);

			this.apiRequest = new ApiRequestModel() {
				appId = accessId,
				random = random,
				timestamp = timestamp,
				sign = sign,
				v = version
			};
		}

		/// <summary>
		/// 根据HTTP Method提取HTTP请求参数列表。
		/// </summary>
		/// <param name="request">HTTP请求消息。</param>
		/// <returns>返回客户端请求参数的字典列表。</returns>
		/// <remarks>
		/// 对于GET请求，所有参数均应该来自Query String。
		/// 对于POST请求，所有参数均应该来自Form表单。
		/// 对于重复键名的参数以最后出现的为准。
		/// </remarks>
		Dictionary<string, string> GetRequestHeaders(HttpRequestMessage request) {
			var requestParams = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

			foreach(var pair in request.Headers) {
				var pairvalue = pair.Value;
				if(pairvalue == null) { continue; }
				if(requestParams.ContainsKey(pair.Key)) {
					requestParams[pair.Key] = pairvalue.FirstOrDefault<string>();
				}
				else {
					requestParams.Add(pair.Key, pairvalue.FirstOrDefault<string>());
				}
			}
			return requestParams;
		}

		/// <summary>
		/// 根据HTTP Method提取HTTP请求参数列表。
		/// </summary>
		/// <param name="request">HTTP请求消息。</param>
		/// <returns>返回客户端请求参数的字典列表。</returns>
		/// <remarks>
		/// 对于GET请求，所有参数均应该来自Query String。
		/// 对于POST请求，所有参数均应该来自Form表单。
		/// 对于重复键名的参数以最后出现的为准。
		/// </remarks>
		Dictionary<string, string> GetRequestParameters(HttpRequestMessage request) {

			var requestParams = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			IEnumerable<KeyValuePair<string, string>> orgionParams;

			if(request.IsMessageFromBody()) {
				var formData = HttpContext.Current.Request.Form;
				orgionParams = formData.AllKeys.SelectMany(formData.GetValues,
					(k, v) => new KeyValuePair<string, string>(k, v));
			}
			else {
				orgionParams = request.GetQueryNameValuePairs();
			}

			foreach(var pair in orgionParams) {
				if(pair.Key == null) continue;
				if(requestParams.ContainsKey(pair.Key)) {
					requestParams[pair.Key] = pair.Value;
				}
				else {
					requestParams.Add(pair.Key, pair.Value);
				}
			}

			return requestParams;
		}
	}

	public static class DictionaryExtentions {
		public static void TryGetRequireValue(this Dictionary<string, string> source, string argument, out string value) {
			source.TryGetValue(argument, out value);
			//Contract.Requires(!string.IsNullOrEmpty(value),
				//string.Format(Resources.MissingRequiredArgument, argument));
		}
	}
}
