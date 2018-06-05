using StackExchange.Redis;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Smart.API.Adapter.Common.Cache {
	public static class StackExchangeRedisExtensions {
		public static T Get<T>(this IDatabase cache, string key) {
			return Deserialize<T>(cache.StringGet(key));
		}

		public static object Get(this IDatabase cache, string key) {
			return Deserialize<object>(cache.StringGet(key));
		}

		public static bool Set(this IDatabase cache, string key, object value) {
			return cache.StringSet(key, Serialize(value));
		}

		public static bool Set(this IDatabase cache, string key, object value, TimeSpan? expiry) {
			return cache.StringSet(key, Serialize(value), expiry);
		}

		static byte[] Serialize(object o) {
			if(o == null) {
				return null;
			}

			BinaryFormatter binaryFormatter = new BinaryFormatter();
			using(MemoryStream memoryStream = new MemoryStream()) {
				binaryFormatter.Serialize(memoryStream, o);
				byte[] objectDataAsStream = memoryStream.ToArray();
				return objectDataAsStream;
			}
		}

		static T Deserialize<T>(byte[] stream) {
			if(stream == null) {
				return default(T);
			}

			BinaryFormatter binaryFormatter = new BinaryFormatter();
			using(MemoryStream memoryStream = new MemoryStream(stream)) {
				T result = (T)binaryFormatter.Deserialize(memoryStream);
				return result;
			}
		}
	}
}
