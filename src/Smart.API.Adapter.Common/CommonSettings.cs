using Smart.API.Adapter.Models;
using Smart.API.Adapter.Models.DTO.JD;
using System;
using System.Configuration;
namespace Smart.API.Adapter.Common
{
    public class CommonSettings
    {
        /// <summary>
        /// 获取应用程序名称。
        /// </summary>
        public static string ApplicationName
        {
            get
            {
                string cfgAppName = ConfigurationManager.AppSettings["ApplicationName"];
                if (string.IsNullOrWhiteSpace(cfgAppName))
                {
                    cfgAppName = AppDomain.CurrentDomain.SetupInformation.ApplicationName;
                }
                return cfgAppName;
            }
        }
        /// <summary>
        /// 获取是否开发调试模式。
        /// </summary>
        public static bool IsDev
        {
            get
            {
                bool isDev = false;
                string dev = ConfigurationManager.AppSettings["isdev"];
                return bool.TryParse(dev, out isDev) && isDev;
            }
        }


        public static string LogType
        {
            get
            {
                string cfgLogType = ConfigurationManager.AppSettings["LogType"];
                if (string.IsNullOrWhiteSpace(cfgLogType))
                {
                    cfgLogType = "0";
                }
                return cfgLogType;
            }
        }

        /// <summary>
        /// JieLink接口地址
        /// </summary>
        public static string BaseAddressJS
        {
            get
            {
                string BaseAddressJS = ConfigurationManager.AppSettings["BaseAddressJS"];
                if (string.IsNullOrWhiteSpace(BaseAddressJS))
                {
                    BaseAddressJS = "http://test.spl.jd.com/external/";
                }
                return BaseAddressJS;
            }
        }

        /// <summary>
        /// 心跳检查时间间隔
        /// </summary>
        public static int HeartInterval
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["HeartInterval"]))
                {
                    return Convert.ToInt32(ConfigurationManager.AppSettings["HeartInterval"]);
                }
                return 5000;
            }
        }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public static string EmailFrom
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EmailFrom"]))
                {
                    return ConfigurationManager.AppSettings["EmailFrom"];
                }
                return "";
            }
        }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public static string EmailTo
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EmailTo"]))
                {
                    return ConfigurationManager.AppSettings["EmailTo"];
                }
                return "";
            }
        }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public static string EmailSMTP
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EmailSMTP"]))
                {
                    return ConfigurationManager.AppSettings["EmailSMTP"];
                }
                return "";
            }
        }

        /// <summary>
        /// 邮件端口
        /// </summary>
        public static int EmailPort
        {
            get
            {
                int iPort = 0;
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EmailPort"]))
                {
                    int.TryParse(ConfigurationManager.AppSettings["EmailPort"], out iPort);
                }
                return iPort;
            }
        }


        /// <summary>
        /// 邮件账户
        /// </summary>
        public static string EmailUserName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EmailUserName"]))
                {
                    return ConfigurationManager.AppSettings["EmailUserName"];
                }
                return "";
            }
        }


        /// <summary>
        /// 邮件密码
        /// </summary>
        public static string EmailPassword
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EmailPassword"]))
                {
                    return ConfigurationManager.AppSettings["EmailPassword"];
                }
                return "";
            }
        }

        /// <summary>
        /// 邮件启用SSL
        /// </summary>
        public static bool EmailSSL
        {
            get
            {
                bool enableSSL = false;
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["EmailSSL"]))
                {
                    bool.TryParse(ConfigurationManager.AppSettings["EmailSSL"], out enableSSL);
                }
                return enableSSL;
            }
        }

        /// <summary>
        /// 京东定义的客户端系统编码
        /// </summary>
        public static string BaseAddressJd
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["BaseAddressJd"]))
                {
                    return ConfigurationManager.AppSettings["BaseAddressJd"].TrimEnd('/')+"/";
                }
                return "http://test.spl.jd.com/external/";
            }
        }

        /// <summary>
        /// Vesion xml地址
        /// </summary>
        public static string ParkXmlAddress
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["JdParkXml"]))
                {
                    return ConfigurationManager.AppSettings["JdParkXml"];
                }
                return "/Config/ParkVersion.xml";
            }
        }

        /// <summary>
        /// 京东定义的客户端系统编码
        /// </summary>
        public static string SysId
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["SysId"]))
                {
                    return ConfigurationManager.AppSettings["SysId"];
                }
                return "0";
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

        /// <summary>
        /// 请求第三方超时时间，默认5秒
        /// </summary>
        public static int PostTimeOut
        {
            get
            {
                string PostTimeOut = ConfigurationManager.AppSettings["PostTimeOut"];
                int iPostTimeOut = 0;
                int.TryParse(PostTimeOut, out iPostTimeOut);
                if (iPostTimeOut <= 0)
                {
                    iPostTimeOut = 5;
                }
                return iPostTimeOut;
            }
        }

        private static JDParkConfig _JDParkConfig;
        public static JDParkConfig JDParkConfigInfo
        {
            get
            {
                if (_JDParkConfig == null)
                {
                    _JDParkConfig = XMLHelper.FromXMLToObject<JDParkConfig>(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\JDParkXML.xml");
                }
                return _JDParkConfig;
            }
        }

        /// <summary>
        /// JD配置参数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static JDTimer JDTimerInfo(enumJDBusinessType type)
        {
            JDParkConfig jdConfig = JDParkConfigInfo;
            if (jdConfig != null && jdConfig.LJDTime != null
                && jdConfig.LJDTime.Count > 0)
            {
                //通过xml配置文件获取 Timer配置
                return jdConfig.LJDTime[(int)type - 1];

            }
            return null;
        }

        /// <summary>
        /// 请求第三方计费错误，默认返回码
        /// </summary>
        public static string ThirdChargingFailCode
        {
            get
            {
                string thirdChargingFailCode = ConfigurationManager.AppSettings["ThirdChargingFailCode"];
                if (string.IsNullOrWhiteSpace(thirdChargingFailCode))
                {
                    thirdChargingFailCode = "1";
                }
                return thirdChargingFailCode;
            }
        }

        /// <summary>
        /// 请求计费错误，默认返回开闸标识
        /// </summary>
        public static int ThirdChargingIsOpenGate
        {
            get
            {
                string ThirdChargingIsOpenGate = ConfigurationManager.AppSettings["ThirdChargingIsOpenGate"];
                int iThirdChargingIsOpenGate = 0;
                int.TryParse(ThirdChargingIsOpenGate, out iThirdChargingIsOpenGate);
                return iThirdChargingIsOpenGate;
            }
        }
    }
}
