using Infrastructure.Common.Security;
using System;

namespace Smart.API.Adapter.Common
{
    public static class EntityHash
    {
        /// <summary>
        /// 获取给定对象的数据MD5值。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HashValue(this object data)
        {
            return CipherHelper.MD5Encrypt(data.ToJson());
        }
        /// <summary>
        /// 判断指定对象的数据MD5值是否修改(与给定的oldHashValue不一致)。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="oldHashValue"></param>
        /// <returns></returns>
        public static bool IsHashChanged(this object data, string oldHashValue)
        {
            return HashValue(data) != oldHashValue;
        }
    }
}
