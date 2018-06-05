namespace System.Net.Http {
	/// <summary>
	/// 一个帮助器类，它用于检索并比较标准 HTTP 方法并且用于创建新的 HTTP 方法。
	/// </summary>
	public class HttpMethods : HttpMethod {

		private static readonly HttpMethod patchMethod = new HttpMethod("PATCH");

		public static HttpMethod Patch {
			get { return patchMethod; }
		}

		public HttpMethods(string method) : base(method) { }
	}
}
