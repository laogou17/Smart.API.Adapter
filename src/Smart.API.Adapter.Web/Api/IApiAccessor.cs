namespace Smart.API.Adapter.Web.Api {
	/// <summary>
	/// 定义接口访问控制验证接口。
	/// </summary>
	public interface IApiAccessVerifier {
		/// <summary>
		/// 校验接口请求参数签名。
		/// </summary>
		/// <param name="accessId">接入编码</param>
		/// <param name="signType">请求参数客户端签名方法。</param>
		/// <param name="sign">请求参数客户端签名。</param>
		/// <param name="plainText">待签名的文本字符串。</param>
		/// <returns>返回OperateResult对象。</returns>
		/// <remarks></remarks>
		OperateResult VerifyAccessSign(string accessId, string random, string timestamp, string sign, string plainText);
		/// <summary>
		/// 校验接口访问权限。
		/// </summary>
		/// <param name="accessId">接入渠道编码</param>
		/// <param name="functionCode">访问接口名称</param>
		/// <returns></returns>
		OperateResult VerifyAccessRights(string accessId, string functionCode);
		/// <summary>
		/// 校验接口访问频率是否超过配置值。
		/// </summary>
		/// <param name="accessId">接入渠道编码</param>
		/// <param name="functionCode">访问接口名称</param>
		/// <returns></returns>
		OperateResult VerifyAccessFrequency(string accessId, string functionCode);
	}
}
