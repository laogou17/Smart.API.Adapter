using Smart.API.Adapter.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Smart.API.Adapter.Common
{
    public class InterfaceHttpProxyApi
    {
        static readonly TimeSpan DefaultTimeOut = TimeSpan.FromSeconds(5);//TODO:可配置
        private string _BaseAddress;
        public InterfaceHttpProxyApi(string BaseAddress)
        {
            _BaseAddress = BaseAddress;
        }

        public ApiResult PostRaw(string relativeUri, object parameters, TimeSpan? timeout = null)
        {
            using (var client = GetHttpClient(timeout))
            {
                var response = client.PostAsync(relativeUri, new StringContent(parameters.ToJson())).Result;
                return HandleApiResult(response);
            }
        }

        public ApiResult<T> PostRaw<T>(string relativeUri, object parameters, TimeSpan? timeout = null)
        {
            using (var client = GetHttpClient(timeout))
            {
                var response = client.PostAsync(relativeUri, new StringContent(parameters.ToJson())).Result;
                return HandleApiResult<T>(response);
            }
        }

        public ApiResult<T> Get<T>(string relativeUri, TimeSpan? timeout = null)
        {
            using (var client = GetHttpClient(timeout))
            {
                var response = client.GetAsync(relativeUri).Result;
                return HandleApiResult<T>(response);
            }
        }

        HttpClient GetHttpClient(TimeSpan? timeout)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_BaseAddress);
            client.Timeout = timeout.HasValue ? timeout.Value : DefaultTimeOut;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public ApiResult HandleApiResult(HttpResponseMessage response)
        {
            var apiResult = new ApiResult();
            if (response.IsSuccessStatusCode)
            {
                apiResult.code = "OK";
                apiResult.successed = true;
            }
            else
            {
                ParseErrorResponse(response, apiResult);
            }
            return apiResult;
        }

        public ApiResult<T> HandleApiResult<T>(HttpResponseMessage response)
        {
            var apiResult = new ApiResult<T>();
            if (response.IsSuccessStatusCode)
            {
                EnsureResponseContentTypeWithApplicationJson(response.Content);
                apiResult.data = response.Content.ReadAsStringAsync().Result.FromJson<T>();
                apiResult.code = "OK";
                apiResult.successed = true;
            }
            else
            {
                ParseErrorResponse(response, apiResult);
            }
            return apiResult;
        }

        private class RestfulFormRawJsonContent : ByteArrayContent
        {
            public RestfulFormRawJsonContent(object data)
                : base(GetContentByteArray(data))
            {
                Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }

            private static byte[] GetContentByteArray(object data)
            {
                if (data == null)
                {
                    throw new ArgumentNullException("data");
                }
                System.Diagnostics.Contracts.Contract.EndContractBlock();

                return Encoding.UTF8.GetBytes(data.ToJson());
            }
        }

        private void ParseErrorResponse(HttpResponseMessage response, ApiResult apiResult)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                EnsureResponseContentTypeWithApplicationJson(response.Content);
                var innerResult = response.Content.ReadAsStringAsync().Result.FromJson<ApiError>();
                apiResult.code = "BadRequest";
                apiResult.message = innerResult.Message;
#if DEBUG
                apiResult.stackTrace = innerResult.stackTrace;
#endif
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                apiResult.code = "InternalServerError";
                apiResult.message = string.Format("HTTP 500。访问接口时，服务器返回异常。{0}", response.ReasonPhrase);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.MethodNotAllowed)
            {
                apiResult.code = "MethodNotAllowed";
                apiResult.message = string.Format("HTTP 405。请求的资源\"{0}\"上不允许请求方法({1})。", response.RequestMessage.RequestUri, response.RequestMessage.Method.ToString());
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                apiResult.code = "NotFound";
                apiResult.message = string.Format("HTTP 404。接口地址\"{0}\"不存在", response.RequestMessage.RequestUri);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
            {
                apiResult.code = "ServiceUnavailable";
                apiResult.message = "HTTP 503。接口服务器不可用。";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadGateway)
            {
                apiResult.code = "BadGateway";
                apiResult.message = "HTTP 502。中间代理服务器从另一代理或原服务器接收到错误响应。";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
            {
                apiResult.code = "GateWayTimeout";
                apiResult.message = "HTTP 504。访问接口时，服务器响应超时。";
            }
            else
            {
                try 
	            {	        
		               EnsureResponseContentTypeWithApplicationJson(response.Content);
                    var innerResult = response.Content.ReadAsStringAsync().Result.FromJson<ApiError>();
                apiResult.code = "InterfaceHttpApiFail";
                apiResult.message = innerResult.Message;
#if DEBUG
                    apiResult.stackTrace = innerResult.stackTrace;
#endif
	            }
	            catch (Exception exp)
	            {
		  apiResult.code = "InterfaceHttpApiFail";
                apiResult.message = exp.Message;
#if DEBUG
                    apiResult.stackTrace = exp.StackTrace;
#endif
	            }
            }
        }

        private void EnsureResponseContentTypeWithApplicationJson(HttpContent content)
        {
            string mediaType = content.Headers.ContentType.MediaType;
            if (mediaType != "application/json" && mediaType != "text/json")
            {
                throw new InterfaceSyncProxyException("调用接口未能按照预期返回响应媒体类型\"application/json\"");
            }
        }
    }
}
