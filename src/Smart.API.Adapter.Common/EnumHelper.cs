using System;
using System.ComponentModel;
using System.Reflection;
namespace Smart.API.Adapter.Common
{

    public static class EnumHelper
    {
        /// <summary>
        /// TEnum 的某个常数具有等于 value 的值时，将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。 用于指示转换是否成功的返回值。
        /// </summary>
        /// <typeparam name="TEnum">要将 value 转换为的枚举类型。</typeparam>
        /// <param name="value">要转换的枚举名称或基础值的字符串表示形式。</param>
        /// <param name="result">此方法在返回时包含一个类型为 TEnum 的一个对象，其值由 value 表示。 该参数未经初始化即被传递。</param>
        /// <returns>如果 TEnum 的某个常数具有等于 value 的值，并且 value 参数成功转换，则为 true；否则为 false。</returns>
        /// <exception cref="System.ArgumentException">TEnum 不是枚举类型。</exception>
        public static bool TryParseWhenDefined<TEnum>(object value, out TEnum result) where TEnum : struct
        {
            result = default(TEnum);
            if (value == null)
            {
                return false;
            }

            Type enumType = typeof(TEnum);

            Type underlyingType = Enum.GetUnderlyingType(enumType);
            Type valueType = value.GetType();
            if (underlyingType == valueType)
            {
                if (Enum.IsDefined(enumType, value))
                {
                    return Enum.TryParse<TEnum>(value.ToString(), out result);
                }
            }
            else
            {
                if (Enum.IsDefined(enumType, value.ToString()))
                {
                    return Enum.TryParse<TEnum>(value.ToString(), out result);
                }
            }

            return false;
        }

    }
}
