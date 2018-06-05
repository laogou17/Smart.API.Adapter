using System;
namespace Smart.API.Adapter.Common.Cache {
	/// <summary>
	/// 定义Redis缓存键。
	/// </summary>
	public class CacheKeys {
		/// <summary>
		/// 注册验证码缓存键
		/// </summary>
		public const string RegisterCodeCacheKey = "RegisterCode:{0}";
		public static readonly TimeSpan RegisterCodeCacheExpire = new TimeSpan(0,0, 60);
		
	}
}
