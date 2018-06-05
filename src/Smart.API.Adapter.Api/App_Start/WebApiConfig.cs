using Smart.API.Adapter.Api.Controllers;
using Smart.API.Adapter.Web.Api;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace Smart.API.Adapter.Api {
	public static class WebApiConfig {
		public static void Register(HttpConfiguration config) {
			// Web API 配置和服务

			// Web API 路由
			//config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver
				= new DefaultContractResolver { IgnoreSerializableAttribute = true };
			config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
				new Newtonsoft.Json.Converters.IsoDateTimeConverter() {
					DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
				}
			);

			// 启用版本控制
			config.EnableQueryStringVersion();

			// 注册API接口异常处理过滤器。
			config.HandleApiException();

			// 注册API接口访问控制过滤器。
			config.EnableAccessControl(new ApiAccessor());
		}
	}
}
