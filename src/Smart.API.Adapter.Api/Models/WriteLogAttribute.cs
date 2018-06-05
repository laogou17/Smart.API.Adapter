using System;

namespace NEOCRM.Api {
	/// <summary>
	/// 标识启用该接口请求响应日志记录。
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class WriteLogAttribute : Attribute {
		/// <summary>
		/// 初始化WriteLogAttribute
		/// </summary>
		public WriteLogAttribute() { }
	}
}