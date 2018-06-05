using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Common {
	public class DateTimeHelper {

		// <summary>  
		/// 得到本周第一天(以星期一为第一天)  
		/// </summary>  
		/// <param name="datetime"></param>  
		/// <returns></returns>  
		public static DateTime GetWeekFirstDayMon(DateTime datetime) {
			//星期一为第一天  
			int weeknow = Convert.ToInt32(datetime.DayOfWeek);

			//因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
			weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
			int daydiff = (-1) * weeknow;

			//本周第一天  
			string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
			return Convert.ToDateTime(FirstDay);
		}


		/// <summary>  
		/// 得到本周第一天(以星期天为第一天)  
		/// </summary>  
		/// <param name="datetime"></param>  
		/// <returns></returns>  
		public static DateTime GetWeekFirstDaySun(DateTime datetime) {
			//星期天为第一天  
			int weeknow = Convert.ToInt32(datetime.DayOfWeek);
			int daydiff = (-1) * weeknow;

			//本周第一天  
			string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
			return Convert.ToDateTime(FirstDay);
		}

		/// <summary>
		/// 得到本月第一天
		/// </summary>
		/// <param name="datetime"></param>
		/// <returns></returns>
		public static DateTime GetMothFirstDay(DateTime datetime) {
			string FirstDay = datetime.Year + "-" + datetime.Month + "-1";
			return Convert.ToDateTime(FirstDay);
		}
	}
}
