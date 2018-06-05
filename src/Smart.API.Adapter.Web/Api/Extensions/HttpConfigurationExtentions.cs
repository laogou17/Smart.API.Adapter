using Smart.API.Adapter.Web.Api.Filters;
using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
namespace Smart.API.Adapter.Web.Api {
	public static class HttpConfigurationExtentions {
		/// <summary>
		/// 启用API接口访问控制。
		/// </summary>
		/// <param name="config"></param>
		/// <param name="verifier"></param>
		public static void HandleApiException(this HttpConfiguration config) {
			if(config == null) {
				Error.ThrowArgumentNullException("config");
			}
			config.Filters.Add(new ApiExceptionFilterAttribute());
		}

		/// <summary>
		/// 启用API接口访问控制。
		/// </summary>
		/// <param name="config"></param>
		/// <param name="verifier"></param>
		public static void EnableAccessControl(this HttpConfiguration config, IApiAccessVerifier verifier) {
			if(config == null) {
				Error.ThrowArgumentNullException("config");
			}
			config.Filters.Add(new AccessControlFilterAttribute(verifier));
		}

		/// <summary>
		/// 启用版本控制。
		/// </summary>
		/// <param name="config"></param>
		public static void EnableQueryStringVersion(this HttpConfiguration config) {
			if(config == null) {
				Error.ThrowArgumentNullException("config");
			}
			config.Services.Replace(typeof(IHttpControllerSelector),
				new QueryStringVersionControllerSelector(config, namespaceResolver));

		}

		// Get api version from the http request
		static Func<HttpRequestMessage, string> namespaceResolver =
				new Func<HttpRequestMessage, string>(request => {
					string version = "";

					if(request.Method == HttpMethod.Post) {
						if(request.Content.IsFormData() || request.Content.IsMimeMultipartContent()) {
							var requestFormData = HttpContext.Current.Request.Form;
							if(requestFormData != null) {
								version = requestFormData[ApiConstants.ParamVersion];
							}
							//throw new ArgumentException("POST提交数据时，ContentType应该为“application/x-www-form-urlencoded”或“multipart/form-data”。");
						}
					}
					else {
						request.GetQueryNameValuePairs()
								.ToDictionary(pair => pair.Key, pair => pair.Value)
								.TryGetValue(ApiConstants.ParamVersion, out version);
					}
					switch(version) {
						case "1":
						case "1.0": return "v1";
					}

					return "v1"; // default namespace, return null to throw 404 when namespace not given
				});
	}
}
