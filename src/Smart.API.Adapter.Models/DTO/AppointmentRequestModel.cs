using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NEOCRM.Models.DTO {

	/// <summary>
	/// 客户接触记录
	/// </summary>
	[DataContract(Name = "appointment")]
	public class AppointmentRequestModel : ApiRequestBase {

		/// <summary>
		/// 所属bu 1-在线 2-普惠 3-分期 4-新财富
		/// </summary>
		public int bu { set; get; }

		/// <summary>
		/// 接触记录类型 1-客户/用户 2-潜客 3-游离
		/// </summary>
		public int type { set; get; }

		/// <summary>
		/// 接触客户号码类型 1-手机号码 2-微信号 3-QQ号
		/// </summary>
		public int numtype { set; get; }


		/// <summary>
		/// 接触客户号码 根据numType分别表示手机号码、qq号码、微信号码
		/// </summary>
		public string num { set; get; }

		/// <summary>
		/// 接触渠道 语音/微信/QQ
		/// </summary>
		public string channel { set; get; }

		/// <summary>
		/// 接触人员工号
		/// </summary>
		public string agentno { set; get; }

		/// <summary>
		/// 主动/被动
		/// </summary>
		public string method { set; get; }

		/// <summary>
		/// 咨询问题分类 
		/// </summary>
		public string inquiretype { set; get; }

		/// <summary>
		/// 是否转接其他人员
		/// </summary>
		public bool istransfer { set; get; }

		/// <summary>
		/// 是否需要跟进
		/// </summary>
		public bool needfollow { set; get; }

		/// <summary>
		/// 客户留言/交办
		/// </summary>
		public string message { set; get; }

		/// <summary>
		/// 客户满意度
		/// </summary>
		public string satisfaction { set; get; }

		/// <summary>
		/// 是否推荐
		/// </summary>
		public bool isrecommend { set; get; }

		/// <summary>
		/// 调听URL
		/// </summary>
		public string replayurl { set; get; }
	}


	/// <summary>
	/// 客户接触记录
	/// </summary>
	[DataContract(Name = "customerrefresh")]
	public class CustomerRefreshRequestModel : ApiRequestBase {

		/// <summary>
		/// 所属bu 1-在线 2-普惠 3-分期 4-新财富
		/// </summary>
		public int bu { set; get; }

		/// <summary>
		/// 接触客户号码类型 1-手机号码 2-微信号 3-QQ号
		/// </summary>
		public int numtype { set; get; }


		/// <summary>
		/// 接触客户号码 根据numType分别表示手机号码、qq号码、微信号码
		/// </summary>
		public string num { set; get; }
	}
}
