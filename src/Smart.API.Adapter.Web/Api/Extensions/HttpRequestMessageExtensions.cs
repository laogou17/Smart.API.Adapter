using Smart.API.Adapter.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Http;

namespace Smart.API.Adapter.Web.Api {

	/// <summary>
	/// HttpRequestMessage class extend methods
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpRequestMessageExtensions {

		/// <summary>
		/// 设置HttpRequestMessage属性。
		/// </summary>
		/// <param name="request"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool TrySetPropertyValue(this HttpRequestMessage request, string key, object value) {
			var properties = request.Properties;

			if(properties != null) {
				if(properties.ContainsKey(key)) {
					properties[key] = value;
					return true;
				}
				else {
					properties.Add(key, value);
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 获取HttpRequestMessage属性
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="request"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool TryGetPropertyValue<T>(this HttpRequestMessage request, string key, out T value) {
			value = default(T);
			var properties = request.Properties;

			if(properties != null && properties.ContainsKey(key)) {
				try {
					value = (T)properties[key];
					return true;
				}
				catch {
					return false;
				}
			}

			return false;
		}

		/// <summary>
		/// 检测当前请求内容是否来自Body部分。
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static bool IsMessageFromBody(this HttpRequestMessage request) {
			return (request.Method == HttpMethod.Post || request.Method == HttpMethod.Put);
		}

		/// <summary>
		/// 创建分页查询结果响应。
		/// </summary>
		/// <typeparam name="T">数据项对象类型。</typeparam>
		/// <param name="request"></param>
		/// <param name="recordCount">查询结果记录总数。</param>
		/// <param name="value">查询结果记录集合。</param>
		/// <returns></returns>
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request,
			int recordCount, ICollection<T> value) {
				return request.CreateResponse<PagedResponse<T>>(HttpStatusCode.OK,
					new PagedResponse<T>(recordCount, value));
		}

		/// <summary>
		/// 创建API请求响应。
		/// </summary>
		/// <typeparam name="T">HTTP 响应消息的类型。</typeparam>
		/// <param name="request">导致此响应消息的 HTTP 请求消息。</param>
		/// <returns>与关联的 <see cref="System.Net.Http.HttpRequestMessage"/> 
		/// 连接的已初始化 <see cref="System.Net.Http.HttpResponseMessage"/>。
		/// </returns>
		public static HttpResponseMessage CreateOKResponse(this HttpRequestMessage request) {
			return request.CreateResponse<OkResponse>(HttpStatusCode.OK, new OkResponse("OK"));
		}

		/// <summary>
		/// 创建API请求响应。
		/// </summary>
		/// <typeparam name="T">HTTP 响应消息的类型。</typeparam>
		/// <param name="request">导致此响应消息的 HTTP 请求消息。</param>
		/// <param name="value"> HTTP 响应消息的内容。</param>
		/// <returns>与关联的 <see cref="System.Net.Http.HttpRequestMessage"/> 
		/// 连接的已初始化 <see cref="System.Net.Http.HttpResponseMessage"/>。
		/// </returns>
		public static HttpResponseMessage CreateOKResponse<T>(this HttpRequestMessage request, T value) {
			return request.CreateResponse<OkResponse>(HttpStatusCode.OK, new OkResponse(value));
		}

		/// <summary>
		/// 创建分页查询结果响应。
		/// </summary>
		/// <typeparam name="T">数据项对象类型。</typeparam>
		/// <param name="request"></param>
		/// <param name="recordCount">查询结果记录总数。</param>
		/// <param name="value">查询结果记录集合。</param>
		/// <returns></returns>
		public static HttpResponseMessage CreateOKResponse<T>(this HttpRequestMessage request,
			int recordCount, ICollection<T> value) {
			return request.CreateResponse<PagedResponse<T>>(HttpStatusCode.OK,
					new PagedResponse<T>(recordCount, value));
		}


		/// <summary>
		/// 创建分页查询结果响应。
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="request"></param>
		/// <param name="recordCount"></param>
		/// <param name="nextlink"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static HttpResponseMessage CreateOKResponse<T>(this HttpRequestMessage request,
			int recordCount, int pagenum, string pagingcookie, ICollection<T> value) {
			return request.CreateResponse<PagedResponse<T>>(HttpStatusCode.OK,
					new PagedResponse<T>(recordCount, pagenum, pagingcookie, value));
		}

		/// <summary>
		/// 创建用于表示错误消息的 System.Net.Http.HttpResponseMessage。
		/// </summary>
		/// <param name="request">HTTP 请求。</param>
		/// <param name="exception">异常。</param>
		/// <returns><see cref="System.Net.Http.HttpResponseMessage"/>,
		/// 其内容是包含详细错误代码与错误消息的<see cref="Smart.API.Adapter.Web.Api.ApiError"/>对象实例的序列化表示形式。</returns>
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, Exception exception) {
			string errorCode = "SERVER_ERROR";
			string errorMessage = exception.Message;
			HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

			if(exception is System.ArgumentException ||
				exception is System.ArgumentNullException) {
				errorCode = "ILLEGAL_ARGUMENT";
				statusCode = HttpStatusCode.BadRequest;
			}
			else if(exception is ApiException) {
				var apiException = (ApiException)exception;
				errorCode = apiException.ErrorCode;

				if(exception is InvalidSignException) {
					statusCode = HttpStatusCode.Unauthorized;
				}
				else if(exception is NoAccessRightException) {
					statusCode = HttpStatusCode.Forbidden;
				}
				else if(exception is AccessFrequencyException) {
					statusCode = HttpStatusCode.Forbidden;
				}
				else {
					statusCode = HttpStatusCode.BadRequest;
				}
			}
			ApiError error = new ApiError(errorCode, errorMessage);
#if DEBUG
			error.stackTrace = exception.StackTrace;
#endif
			//Infrastructure.Logging.TextLogger.WriteLog("Smart.API.Adapter.Unicustview.ApiError",exception.ToString());

			return request.CreateResponse<ApiError>(statusCode, error);
		}
	}
}
