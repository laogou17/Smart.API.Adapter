using StackExchange.Redis;
using System;
using System.Configuration;

namespace Smart.API.Adapter.Common.Cache
{

    /// <summary>
    /// The redis cache helper.
    /// </summary>
    public static class CacheHelper
    {

        private static readonly string configuration = ConfigurationManager.AppSettings["cache-connectionString"];
        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            if (string.IsNullOrEmpty(configuration))
            {
                throw new ConfigurationErrorsException("缓存服务器连接字符串[cache-connectionString]未配置.");
            }
            return ConnectionMultiplexer.Connect(configuration);
        });
        private static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
        /// <summary>
        /// Returns if key exists.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>1 if the key exists. 0 if the key does not exist.</returns>
        public static bool Exists(string key)
        {
            bool IsExists = false;
            try
            {
                IDatabase cache = Connection.GetDatabase();

                IsExists = cache.KeyExists(key);
            }
            catch
            {
                IsExists = false;
            }
            return IsExists;
        }
        /// <summary>
        /// Set key to hold the string value. If key already holds a value, it is overwritten,regardless of its type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>True if the keys were set, else False</returns>
        public static bool Set(string key, string value)
        {
            try
            {
                return Set(key, value, null);
            }
            catch { return false; }
        }
        /// <summary>
        /// Set key to hold the string value. If key already holds a value, it is overwritten,regardless of its type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns>True if the keys were set, else False</returns>
        public static bool Set(string key, string value, TimeSpan? expiry)
        {
            try
            {
                IDatabase cache = Connection.GetDatabase();
                return cache.StringSet(key, value, expiry);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Set key to hold the object value. If key already holds a value, it is overwritten,regardless of its type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>True if the keys were set, else False</returns>
        public static bool Set(string key, object value)
        {
            try
            {
                return Set(key, value, null);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Set key to hold the object value. If key already holds a value, it is overwritten,regardless of its type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns>True if the keys were set, else False</returns>
        public static bool Set(string key, object value, TimeSpan? expiry)
        {
            try
            {
                IDatabase cache = Connection.GetDatabase();
                return cache.Set(key, value, expiry);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Get the value of key. If the key does not exist the special value nil is
        /// returned. An error is returned if the value stored at key is not a string,
        /// because GET only handles string values.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>the value of key, or nil when key does not exist.</returns>
        public static string Get(string key)
        {
            try
            {
                IDatabase cache = Connection.GetDatabase();
                return cache.StringGet(key);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Get the value of key. If the key does not exist the special value nil is
        /// returned. An error is returned if the value stored at key is not a string,
        /// because GET only handles string values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>the value of key, or nil when key does not exist.</returns>
        public static T Get<T>(string key)
        {
            try
            {
                IDatabase cache = Connection.GetDatabase();
                return cache.Get<T>(key);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public static bool Delete(string key)
        {
            try
            {
                IDatabase cache = Connection.GetDatabase();
                return cache.KeyDelete(key);
            }
            catch
            {
                return false;
            }
        }

    }
}
