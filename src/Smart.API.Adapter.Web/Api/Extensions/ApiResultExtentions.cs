using Smart.API.Adapter.Web.Api.Client;
using System;

namespace Smart.API.Adapter.Web.Api {
	public static class ApiResultExtentions {
		/// <summary>
		/// 确保 ApiResult.successed 返回True，否则抛出 <see cref="Smart.API.Adapter.Web.Api.ApiException"/> 。
		/// </summary>
		/// <param name="apiResult"></param>
		public static void EnsureSuccessful(this ApiResult apiResult) {
			if(apiResult == null) {
				throw new ArgumentNullException("apiResult");
			}
			if(!apiResult.successed) {
				throw new ApiException(apiResult.message, apiResult.code);
			}
		}
	}
}
