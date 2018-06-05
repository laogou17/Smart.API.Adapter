using Smart.API.Adapter.Common;
using System.Web.Http;
using System.Web.Routing;

namespace Smart.API.Adapter.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            LogHelper.RegisterLog4Config(System.Web.HttpContext.Current.Server.MapPath("Config\\Log4net.config"));
			GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
