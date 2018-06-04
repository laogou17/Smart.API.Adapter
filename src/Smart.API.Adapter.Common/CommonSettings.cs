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

        /// <summary>
        /// 京东接口地址
        /// </summary>
        public static string BaseAddress
        {
            get
            {
                string baseAddress = ConfigurationManager.AppSettings["BaseAddress"];
                if (string.IsNullOrWhiteSpace(baseAddress))
                {
                    baseAddress = "http://test.spl.jd.com/external/";
                }
                return baseAddress;
            }
        }
        /// <summary>
        /// 白名单版本号
        /// </summary>
        public static string Version
        {
            get
            {
                string version = ConfigurationManager.AppSettings["Version"];
                if (string.IsNullOrWhiteSpace(version))
                {
                    version = "1";
                }
                return version;
            }
        }

        /// <summary>
        /// 京东车场Code
        /// </summary>
        public static string ParkLotCode
        {
            get
            {
                string parkLotCode = ConfigurationManager.AppSettings["ParkLotCode"];
                if (string.IsNullOrWhiteSpace(parkLotCode))
                {
                    parkLotCode = "1";
                }
                return parkLotCode;
            }
        }

        /// <summary>
        /// 访问京东接口Token
        /// </summary>
        public static string Token
        {
            get
            {
                string token = ConfigurationManager.AppSettings["Token"];
                if (string.IsNullOrWhiteSpace(token))
                {
                    token = "1";
                }
                return token;
            }
        }


    }
}
