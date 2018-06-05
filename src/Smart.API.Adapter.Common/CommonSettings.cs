using System;
using System.Configuration;
namespace Smart.API.Adapter.Common {
	public class CommonSettings {
		/// <summary>
		/// 获取应用程序名称。
		/// </summary>
		public static string ApplicationName {
			get {
				string cfgAppName = ConfigurationManager.AppSettings["ApplicationName"];
				if(string.IsNullOrWhiteSpace(cfgAppName)) {
					cfgAppName = AppDomain.CurrentDomain.SetupInformation.ApplicationName;
				}
				return cfgAppName;
			}
		}
		/// <summary>
		/// 获取是否开发调试模式。
		/// </summary>
		public static bool IsDev {
			get {
				bool isDev = false;
				string dev = ConfigurationManager.AppSettings["isdev"];
				return bool.TryParse(dev, out isDev) && isDev;
			}
		}
    }
}
