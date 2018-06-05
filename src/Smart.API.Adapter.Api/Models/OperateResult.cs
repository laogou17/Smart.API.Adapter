namespace NEOCRM.Api.Models {
	/// <summary>
	/// 表示执行某项操作的结果。
	/// </summary>
	public class OperateResult {
		/// <summary>
		/// 操作结果。
		/// </summary>
		/// <remarks>
		/// 当操作成功时返回true，否则返回false。
		/// 返回false时，通过Code、Message反映详细的操作失败原因。
		/// </remarks>
		public bool Successed { get; set; }
		/// <summary>
		/// 操作结果描述信息。
		/// </summary>
		public string Message { get; set; }
		/// <summary>
		/// 操作结果代码。
		/// </summary>
		public string Code { get; set; }
	}
	/// <summary>
	/// 表示执行某项操作的结果。
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class OperateResult<T> : OperateResult {
		/// <summary>
		/// 用户数据。
		/// </summary>
		public T Data { get; set; }
	}
}