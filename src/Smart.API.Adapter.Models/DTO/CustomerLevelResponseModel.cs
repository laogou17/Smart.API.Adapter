using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NEOCRM.Models.DTO {
	public class CustomerLevelResponseModel {

		/// <summary>
		/// 客户等级 1-铜牌 2-银牌 3-金牌 4-钻石
		/// </summary>
		public string level { set; get; }

		/// <summary>
		/// VIP客户经理
		/// </summary>
		public string vipmanager { set; get; }
	}
}
