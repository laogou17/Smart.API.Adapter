using System;

namespace Smart.API.Adapter.Common
{
    #region 扩展方法

    public static class Expend
    {
        /// <summary>
        /// 密文显示(左右两边显示明文，中间显示密文)
        /// </summary>
        /// <param name="num"></param>
        /// <param name="start">左边几位</param>
        /// <param name="end">右边几位</param>
        /// <returns></returns>
        public static string SetMask(this String num, int start, int end)
        {
            if (string.IsNullOrEmpty(num))
                return string.Empty;
            int len = num.Length;
            string result;
            var all = start + end;
            if (len > all)
            {
                result = num.Substring(0, start) + num.Substring(len - end, end).PadLeft(len - end, '*');
            }
            else
                result = num;
            return result;
        }

        /// <summary>
        /// 字符串转int?,空字符串为null,,转换失败为null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ToInt(this String str)
        {
            int temp;
            if (str == "" || !int.TryParse(str, out temp)) return null;
            return temp;
        }

        /// <summary>
        /// 字符串转long?,空字符串为null,转换失败为null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long? ToLong(this String str)
        {

            long temp;
            if (str == "" || !long.TryParse(str, out temp)) return null;
            return temp;
        }

        /// <summary>
        /// 字符串转时区日期字符串,空字符串为null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToDateTimeOffsetStr(this String str)
        {
            DateTime dt;
            if (str == "" || !DateTime.TryParse(str, out dt)) return null;
            return dt.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        /// <summary>
        /// 字符串转bool?,空字符串为null,转换失败为null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool? Tobool(this String str)
        {
            bool b;
            if (str == "" || !bool.TryParse(str, out b)) return null;
            return b;
        }
        /// <summary>
        /// 空字符串为null,只有一个人换行符为null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EmptyStringToNull(this String str)
        {
            return str == "" ? null : str;
        }

        /// <summary>
        /// 字符串转double?空字符串为null,转换失败为null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double? ToDouble(this String str)
        {
            double temp;
            if (str == "" || !double.TryParse(str, out temp)) return null;
            return temp;
        }
        /// <summary>
        /// 字符串转decimal?空字符串为null,转换失败为null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal? ToDecimal(this String str)
        {
            decimal temp;
            if (str == "" || !decimal.TryParse(str, out temp)) return null;
            return temp;
        }
    }

    #endregion
}
