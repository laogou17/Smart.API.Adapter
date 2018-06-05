using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NEOCRM.Models.DTO {


	/// <summary>
	/// 客户身份鉴权
	/// </summary>
	[DataContract(Name = "customercheck")]
	public class CustomerCheckRequestModel : ApiRequestBase {
		/// <summary>
		/// 所属bu 1-在线 2-普惠 3-分期 4-新财富
		/// </summary>
		public int bu { set; get; }

		/// <summary>
		/// 手机号码
		/// </summary>
		public string phonenum { set; get; }

		/// <summary>
		/// 身份证号码后4位
		/// </summary>
		public string idpart { set; get; }

	}

	/// <summary>
	/// 客户查询
	/// </summary>
	[DataContract(Name = "customerquery")]
	public class CustomerRequestModel : ApiRequestBase {
		/// <summary>
		/// 所属bu 1-在线 2-普惠 3-分期 4-新财富
		/// </summary>
		public int bu { set; get; }

		/// <summary>
		/// 号码类型
		/// </summary>
		public int numtype { set; get; }

		/// <summary>
		/// 客户号码
		/// </summary>
		public string num { set; get; }

	}


	/// <summary>
	/// 潜客信息查询
	/// </summary>
	[DataContract(Name = "potentialcustomerquery")]
	public class PotentialCustomerQueryRequestModel : ApiRequestBase {
		/// <summary>
		/// 所属bu 1-在线 2-普惠 3-分期 4-新财富
		/// </summary>
		public int bu { set; get; }

		/// <summary>
		/// 登记起始时间
		/// </summary>
		public DateTime registbegintime { set; get; }

		/// <summary>
		/// 登记结束时间
		/// </summary>
		public DateTime registendtime { set; get; }

	}
}
