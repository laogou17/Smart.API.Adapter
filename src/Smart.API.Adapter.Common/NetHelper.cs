using System.Net;
using System.Web;

namespace Smart.API.Adapter.Common {
	public class NetHelper {
		/// <summary>
		/// 获取当前请求客户端的IP地址。
		/// </summary>
		/// <returns></returns>
		public static string GetIPAddress() 
        {
            try
            {
                var httpContext = HttpContext.Current;
                if (httpContext == null || httpContext.Request == null)
                {
                    IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress ipaddress = ipHost.AddressList[0];
                    foreach (IPAddress ipa in ipHost.AddressList)
                    {
                        if (ipa.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipaddress = ipa;
                            break;
                        }
                    }
                    return ipaddress.ToString();
                }
                else
                {
                    return httpContext.Request.UserHostAddress;
                }
            }
            catch { }
            return string.Empty;
		}
	}
}
