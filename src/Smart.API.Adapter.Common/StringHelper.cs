using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Common {
	public class StringHelper {
		/// <summary>
		/// 替换手机号中间四位为*
		/// </summary>
		/// <param name="phoneNo"></param>
		/// <returns></returns>
		public static string ReturnPhoneNO(string phoneNo) {
			Regex re = new Regex("(\\d{3})(\\d{4})(\\d{4})", RegexOptions.None);
			phoneNo = re.Replace(phoneNo, "$1****$3");
			return phoneNo;
		}

		/// <summary>
		/// md532位
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string UserMd5(string str) {
			string cl = str;
			string pwd = "";
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();

			// 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
			byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
			// 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
			for(int i = 0; i < s.Length; i++) {
				// 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 

				pwd = pwd + s[i].ToString("x2");

			}
			return pwd;
		}


		/// <summary>  
		/// 时间戳转为C#格式时间  
		/// </summary>  
		/// <param name="timeStamp">Unix时间戳格式</param>  
		/// <returns>C#格式时间</returns>  
		public static DateTime GetTime(string timeStamp) {
			DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long lTime = long.Parse(timeStamp + "0000");
			TimeSpan toNow = new TimeSpan(lTime);
			return dtStart.Add(toNow);
		}


		/// <summary>  
		/// DateTime时间格式转换为Unix时间戳格式  
		/// </summary>  
		/// <param name="time"> DateTime时间格式</param>  
		/// <returns>Unix时间戳格式</returns>  
		public static long ConvertDateTimeInt(System.DateTime time) {
			System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
			return (long)(time - startTime).TotalSeconds;
		}

        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static long ConvertDateTimeTotalMilliseconds(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalMilliseconds;
        }


		public static List<string> GetStringWithRegex(string content, string s, string e) {
			List<string> temp = new List<string>();
			try {
				Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
				MatchCollection matches = rg.Matches(content);
				foreach(Match match in matches) {
					temp.Add(match.Groups[0].Value);
				}
			}
			catch { }
			return temp;
		}

		public static string GetStringWithRegexReturnOne(string content, string s, string e) {
			try {
				Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
				return rg.Match(content).Value;
			}
			catch {
				return "";
			}
		}

        /// <summary>
        /// 通过图片Url地址获取图片字节字符串
        /// </summary>
        /// <param name="picUrl"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetPicStringByUrl(string picUrl, out string fileName)
        {
            string fileBase64 = "";
            fileName = "";
            try
            {
                using (var client = new HttpClient())
                {
                    picUrl.TrimEnd('/');
                    if (picUrl.Contains('/'))
                    {
                        fileName = picUrl.Substring(picUrl.LastIndexOf('/') + 1);
                    }
                    byte[] imgByte = client.GetByteArrayAsync(picUrl).Result;
                    if (imgByte != null)
                    {
                        fileBase64 = Convert.ToBase64String(imgByte);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("获取图片：" + picUrl + "错误：", ex);
            }

            return fileBase64;
        }
	}
}
