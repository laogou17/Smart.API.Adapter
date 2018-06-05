using System;
using System.Runtime.Serialization;

namespace Smart.API.Adapter.Web.Api {

	/// <summary>
	/// 定义错误消息响应内容。
	/// </summary>
	[DataContract(Name = "err")]
	public class ApiError {
		/// <summary>
		/// 初始化 <see cref="Smart.API.Adapter.Api.ApiError"/> 类的新实例。
		/// </summary>
		public ApiError() {
			this.Code = "SERVER_ERROR";
			this.Message = "";
		}

		/// <summary>
		/// 使用指定的错误代码与错误消息初始化 <see cref="Smart.API.Adapter.Api.ApiError"/> 类的新实例。
		/// </summary>
		/// <param name="code"></param>
		/// <param name="message"></param>
		public ApiError(string code, string message) {
			this.Code = code;
			this.Message = message;
		}

		/// <summary>
		/// 解释API请求失败的错误代码。
		/// </summary>
		[DataMember(Name = "code")]
		public string Code { get; set; }

		/// <summary>
		/// 解释API请求失败的错误信息。
		/// </summary>
		[DataMember(Name = "message")]
		public string Message { get; set; }
		/// <summary>
		/// 错误堆栈。
		/// </summary>
		[DataMember(Name = "stackTrace")]
		public string stackTrace { get; set; }
	}

	/// <summary>
	/// API应用程序异常。
	/// </summary>
	/// <remarks>400</remarks>
	[Serializable]
	public class ApiException : ApplicationException {
		/// <summary>
		/// 解释异常原因的错误代码。
		/// </summary>
		public string ErrorCode { get; set; }
		/// <summary>
		/// 初始化 Smart.API.Adapter.Api.ApiException 类的新实例。
		/// </summary>
		public ApiException()
			: base() {
			this.ErrorCode = "SYSTEM_ERROR";
		}
		/// <summary>
		/// 使用指定错误消息初始化 Smart.API.Adapter.Api.ApiException 类的新实例。
		/// </summary>
		/// <param name="message">解释异常原因的错误信息。</param>
		public ApiException(string message) :
			this(message, "SYSTEM_ERROR", null) {

		}
		/// <summary>
		/// 使用指定错误消息和错误代码来初始化 Smart.API.Adapter.Api.ApiException 类的新实例。
		/// </summary>
		/// <param name="message">解释异常原因的错误信息。</param>
		/// <param name="errorCode">解释异常原因的错误代码。</param>
		public ApiException(string message, string errorCode)
			: this(message, errorCode, null) {

		}
		/// <summary>
		/// 使用指定错误消息、错误代码和对作为此异常原因的内部异常的引用来初始化 Smart.API.Adapter.Api.ApiException 类的新实例。
		/// </summary>
		/// <param name="message">解释异常原因的错误信息。</param>
		/// <param name="errorCode">解释异常原因的错误代码。</param>
		/// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。</param>
		public ApiException(string message, string errorCode, Exception innerException)
			: base(message, innerException) {
			this.ErrorCode = errorCode;
		}
	}

	/// <summary>
	/// API请求参数无效或不正确时引发该异常。
	/// </summary>
	/// <remarks>400</remarks>
	public class ArgumentException : ApiException {
		/// <summary>
		/// 默认API请求参数无效时的错误代码。
		/// </summary>
		public static readonly string DefaultCode = "ILLEGAL_ARGUMENT";

		/// <summary>
		/// 初始化 Smart.API.Adapter.Api.ArgumentException 类的新实例。
		/// </summary>
		public ArgumentException()
			: base() {
			this.ErrorCode = DefaultCode;
		}
		/// <summary>
		/// 使用指定错误消息来初始化 Smart.API.Adapter.Api.ArgumentException 类的新实例。
		/// </summary>
		/// <param name="message">解释异常原因的错误信息。</param>
		public ArgumentException(string message)
			: this(message, DefaultCode) {

		}
		/// <summary>
		/// 使用指定错误消息和错误代码来初始化 Smart.API.Adapter.Api.ArgumentException 类的新实例。
		/// </summary>
		/// <param name="message">解释异常原因的错误信息。</param>
		/// <param name="errorCode">解释异常原因的错误代码。</param>
		public ArgumentException(string message, string errorCode) : base(message, errorCode) { }
	}

	/// <summary>
	/// API请求参数签名错误时引发该异常。
	/// </summary>
	/// <remarks>401</remarks>
	public class InvalidSignException : ApiException {
		/// <summary>
		/// 初始化 Smart.API.Adapter.Api.InvalidSignException 类的新实例。
		/// </summary>
		public InvalidSignException()
			: base("签名不正确", "ILLEGAL_SIGN ") {
		}
		/// <summary>
		/// 使用指定错误消息和错误代码来初始化 Smart.API.Adapter.Api.InvalidSignException 类的新实例。
		/// </summary>
		/// <param name="message">解释异常原因的错误信息。</param>
		/// <param name="errorCode">解释异常原因的错误代码。</param>
		public InvalidSignException(string message, string errorCode) : base(message, errorCode) { }
	}

	/// <summary>
	/// 无API接口访问权限时引发该异常。
	/// </summary>
	public class NoAccessRightException : ApiException {
		/// <summary>
		/// 初始化 Smart.API.Adapter.Api.NoAccessRightException 类的新实例。
		/// </summary>
		public NoAccessRightException()
			: this("您未开通此接口的访问权限") {
		}
		/// <summary>
		/// 使用指定错误消息来初始化 Smart.API.Adapter.Api.NoAccessRightException 类的新实例。
		/// </summary>
		/// <param name="message">解释异常原因的错误信息。</param>
		public NoAccessRightException(string message)
			: this(message, "HAS_NO_PRIVILEGE") {

		}
		/// <summary>
		/// 使用指定错误消息和错误代码来初始化 Smart.API.Adapter.Api.NoAccessRightException 类的新实例。
		/// </summary>
		/// <param name="message">解释异常原因的错误信息。</param>
		/// <param name="errorCode">解释异常原因的错误代码。</param>
		public NoAccessRightException(string message, string errorCode) : base(message, errorCode) { }
	}

	/// <summary>
	/// API请求过于频繁时引发该异常。
	/// </summary>
	/// <remarks>403</remarks>
	public class AccessFrequencyException : ApiException {
		/// <summary>
		/// 初始化 Smart.API.Adapter.Api.AccessFrequencyException 类的新实例。
		/// </summary>
		public AccessFrequencyException()
			: this("访问过于频繁") {
		}
		/// <summary>
		/// 使用指定错误消息来初始化 Smart.API.Adapter.Api.AccessFrequencyException 类的新实例。
		/// </summary>
		/// <param name="message">解释异常原因的错误信息。</param>
		public AccessFrequencyException(string message)
			: base(message, "ACCESS_FREQUENCY") {

		}
	}
}
