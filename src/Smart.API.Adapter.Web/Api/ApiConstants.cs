namespace Smart.API.Adapter.Web.Api {
	/// <summary>
	/// Constants define
	/// </summary>
	public static class ApiConstants {
		/// <summary>
		/// Get the api name prefix. default is 'Smart.API.Adapter.Api.'
		/// </summary>
		public const string NamePrefix = "Smart.API.Adapter.Api.";

		/// <summary>
		/// Get the appid parameter key. default is 'accessId'
		/// </summary>
		public const string ParamAppId = "appId";
		/// <summary>
		/// Get the signature parameter key. default is 'sign'
		/// </summary>
		public const string ParamSignature = "sign";
		/// <summary>
		/// Get the sign type parameter key. default is 'random'
		/// </summary>
		public const string ParamRandom = "random";
		/// <summary>
		/// Get the api request data format parameter key. default is 'timestamp'
		/// </summary>
		public const string ParamTimestamp = "timestamp";
		/// <summary>
		/// Get the api version parameter key. default is 'v'
		/// </summary>
		public const string ParamVersion = "v";
		/// <summary>
		/// Get the api request data key. default is 'data'
		/// </summary>
		public const string ParamRequestData = "data";


		/// <summary>
		/// WebApi_
		/// </summary>
		public const string KeyPrefix = "WebApi_";
		/// <summary>
		/// Get the web api context key for http request properties.
		/// </summary>
		public const string ContextKey = "WebApi_Context";
		/// <summary>
		/// WebApi_StartTime
		/// </summary>
		public const string StartInterfaceTime = "WebApi_StartTime";
	}
}
