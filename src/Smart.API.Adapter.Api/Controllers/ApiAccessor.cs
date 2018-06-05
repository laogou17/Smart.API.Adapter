using Infrastructure.Common.Security;
using Smart.API.Adapter.DataAccess.Sys;
using Smart.API.Adapter.Models;
using Smart.API.Adapter.Web.Api;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Smart.API.Adapter.Api.Controllers {
	/// <summary>
	/// 封装接口调用相关请求校验的逻辑。
	/// </summary>
	public class ApiAccessor : IApiAccessVerifier {
		ApiAccessDAL accessDal;
		public ApiAccessor() {
			this.accessDal = new ApiAccessDAL();
		}
		/// <summary>
		/// 校验接口请求参数签名。
		/// </summary>
		/// <param name="accessId">接入编码</param>
		/// <param name="signType">请求参数客户端签名方法。</param>
		/// <param name="sign">请求参数客户端签名。</param>
		/// <param name="plainText">待签名的文本字符串。</param>
		/// <returns>返回OperateResult对象。</returns>
		/// <remarks></remarks>
		public OperateResult VerifyAccessSign(string accessId, string random, string timestamp, string sign,
			string plainText) {

			OperateResult result = new OperateResult() { Code = "ILLEGAL_SIGN" };

			// 根据接入访问编码获取接入信息     
			Api_Channel channelaccess = accessDal.GetApiChannelByAccessId(accessId);

			if(channelaccess == null || !channelaccess.EnableStatus) {
				result.Code = "ILLEGAL_APP_ID";
				result.Message = "接入应用ID不存在或已被禁用。";
				return result;
			}

			Api_ChannelKey channelKey = accessDal.GetChannelKeyByAccessId(accessId);

			if(channelKey == null) {
				result.Code = "ILLEGAL_APP_KEY";
				result.Message = "接入密钥未初始化。";
				return result;
			}
			//验证时间戳
			long timestamplong = long.Parse(timestamp);
			long timestampbegin = Common.StringHelper.ConvertDateTimeInt(DateTime.Now.AddMinutes(-5));
			long timestampend = Common.StringHelper.ConvertDateTimeInt(DateTime.Now.AddMinutes(5));
			if(timestamplong <= timestampbegin || timestamplong >= timestampend) {//10分钟内有效 
				result.Code = "ILLEGAL_TIMESTAMP";
				result.Message = "请求已过期。";
				return result;
			}
			// 根据接入密钥重新组合待签名字符串
			string sn = "random" + random + "timestamp" + timestamp + "key" + channelKey.AccessKey.ToString().ToLower();
			MD5 md5 = MD5.Create();
			string serverSign = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(sn))).Replace("-", "");
			if(!String.Equals(serverSign, sign,
				StringComparison.CurrentCultureIgnoreCase)) {
				result.Code = "ILLEGAL_SIGN";
				result.Message = "签名不正确。";
				return result;
			}

			Infrastructure.Logging.TextLogger.WriteLog("Smart.API.Adapter.Api", "Smart.API.Adapter.Api plainText:" + plainText + "  serverSign:" + serverSign + " sign:" + sign);

			if(!String.Equals(serverSign, sign,
				StringComparison.CurrentCultureIgnoreCase)) {
				result.Code = "ILLEGAL_SIGN";
				result.Message = "签名不正确。";
				return result;
			}

			result.Successed = true;
			result.Code = "OK";

			return result;
		}
		/// <summary>
		/// 校验接口访问权限。
		/// </summary>
		/// <param name="accessId"></param>
		/// <param name="apiName"></param>
		/// <returns></returns>
		public OperateResult VerifyAccessRights(string accessId, string apiName) {
			// 默认没有调用权限
			OperateResult result = new OperateResult() {
				Code = "HAS_NO_PRIVILEGE",
				Message = "您未开通此接口的访问权限"
			};

			// 根据接入访问编码获取接入信息    
			Api_Channel channelaccess = accessDal.GetApiChannelByAccessId(accessId);

			if(channelaccess == null || !channelaccess.EnableStatus) {
				result.Code = "ILLEGAL_APP_ID";
				result.Message = "接入应用ID不存在或已被禁用。";
				return result;
			}

			if(accessDal.HasFunction(accessId, apiName)) {
				result.Successed = true;
				result.Code = "OK";
				result.Message = "OK";
			}

			return result;
		}

		/// <summary>
		/// 校验接口访问频率是否超过配置值。
		/// </summary>
		/// <param name="accessId"></param>
		/// <param name="functionCode"></param>
		/// <returns></returns>
		public OperateResult VerifyAccessFrequency(string accessId, string functionCode) {
			OperateResult result = new OperateResult();
			// 计算接入渠道接口访问频率是否超过限定配置值
			result.Successed = accessDal.ComputingAccessFrequency(accessId, functionCode);
			result.Code = result.Successed ? "ACCESS_FREQUENCY" : "OK";
			result.Message = result.Successed ? "请求操作过于频繁。" : "OK";

			return result;
		}
	}
}