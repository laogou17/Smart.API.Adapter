using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NEOCRM.Models.DTO {

	[DataContract(Name = "potentialcustomer")]
	public class PotentialCustomerRegistrationRequestModel : ApiRequestBase {

		/// <summary>
		/// 所属bu
		/// </summary>
		public int bu { set; get; }
		/// <summary>
		/// 潜客来源渠道
		/// </summary>
		public int source { set; get; }

		/// <summary>
		/// 潜客首次接触目的
		/// </summary>
		public int firstpurpose { set; get; }

		/// <summary>
		/// 性别
		/// </summary>
		public int gender { set; get; }

		/// <summary>
		/// 手机号码
		/// </summary>
		public string phonenum { set; get; }

		/// <summary>
		/// QQ号
		/// </summary>
		public string qq { set; get; }

		/// <summary>
		/// 微信
		/// </summary>
		public string wechat { set; get; }

		/// <summary>
		/// 所在地
		/// </summary>
		public string city { set; get; }

		/// <summary>
		/// 备注
		/// </summary>
		public string comment { set; get; }

		/// <summary>
		/// 登记人
		/// </summary>
		public string register { set; get; }


		/// <summary>
		/// 登记时间
		/// </summary>
		public DateTime registtime { set; get; }
	}
}
