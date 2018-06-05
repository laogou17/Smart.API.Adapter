using System.Collections.Generic;
using System.Web.Security;

namespace Smart.API.Adapter.Web.Api.Models {
	/// <summary>
	/// 定义API响应对象封装。
	/// </summary>
	public class ApiResponseBase {
		/// <summary>
		/// 标识请求操作是否成功。
		/// </summary>
		/// <remarks>
		/// 当返回为false时，可参照 code（错误代码）、message（错误消息）。
		/// </remarks>
		public bool successed { get; set; }

		/// <summary>
		/// ApiResponseBase 构造函数。
		/// </summary>
		public ApiResponseBase() { }
	}
	/// <summary>
	/// 封装成功响应数据对象。
	/// </summary>
	public class OkResponse : ApiResponseBase {
		/// <summary>
		/// 响应业务数据对象。
		/// </summary>
		public object data { get; set; }
		public OkResponse()
			: this(null) {
		}
		public OkResponse(object data)
			: base() {
			this.successed = true;
			this.data = data;
		}
	}

	/// <summary>
	/// 封装分页响应数据对象。
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PagedResponse<T> : OkResponse {
		/// <summary>
		/// 记录总数。
		/// </summary>
		public int recordCount { get; set; }

		/// <summary>
		/// 当前页
		/// </summary>
		public int pagenum { set; get; }

		/// <summary>
		/// 下一页连接
		/// </summary>
		public string pagingnext { get; set; }

		/// <summary>
		/// 响应业务数据对象。
		/// </summary>
		public new ICollection<T> data { get; set; }
		public PagedResponse() : base() { }
		public PagedResponse(int recordCount, ICollection<T> data)
			: base() {
			this.recordCount = recordCount;
			this.data = data;
			this.pagenum = 1;
		}

		public PagedResponse(int recordCount, int pagenum, string pagingnext, ICollection<T> data)
			: base() {
			this.recordCount = recordCount;
			this.data = data;
			this.pagingnext = pagingnext;
			this.pagenum = pagenum;
		}
	}
	/// <summary>
	/// 封装错误响应数据对象。
	/// </summary>
	public class ErrorResponse : ApiResponseBase {
		/// <summary>
		/// 错误代码。
		/// </summary>
		public string code { get; set; }
		/// <summary>
		/// 错误消息。
		/// </summary>
		public string message { get; set; }
		/// <summary>
		/// 错误堆栈。
		/// </summary>
		public string stackTrace { get; set; }
		public ErrorResponse() : base() { }
		public ErrorResponse(string code, string message, string stackTrace = "")
			: base() {
			this.code = code;
			this.message = message;
			this.stackTrace = stackTrace;
		}
	}
	/// <summary>
	/// 封装未登录响应数据对象。
	/// </summary>
	public class UnauthorizedResponse : ErrorResponse {

		/// <summary>
		/// 登录地址。
		/// </summary>
		public string loginUrl { get; set; }

		public UnauthorizedResponse()
			: this(FormsAuthentication.LoginUrl) {

		}
		public UnauthorizedResponse(string loginUrl)
			: this(loginUrl, "401", "Unauthorized") {

		}
		public UnauthorizedResponse(string loginUrl, string code, string message)
			: base(code, message) {
			this.loginUrl = loginUrl;
		}
	}
}
