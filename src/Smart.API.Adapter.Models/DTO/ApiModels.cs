using System;
using System.Collections.Generic;
using System.Linq;
#if WebUI
namespace NEOCRM.Web.Models.DTO
#else
namespace NEOCRM.Models.DTO
#endif
 {
	/// <summary>
	/// 定义API接口通用请求参数。
	/// </summary>
	public abstract class ApiRequestBase {
		/// <summary>
		/// 接口版本号。如：1.0
		/// </summary>
		public string v { get; set; }
		/// <summary>
		/// 接入渠道编码。
		/// </summary>
		public string accessId { get; set; }
		/// <summary>
		/// 请求参数签名。
		/// </summary>
		public string sign { get; set; }
		/// <summary>
		/// 请求参数签名算法。如：md5
		/// </summary>
		public string signType { get; set; }
	}
	/// <summary>
	/// 排序方式。
	/// </summary>
	public enum SortMode {
		/// <summary>
		/// 升序
		/// </summary>
		asc,
		/// <summary>
		/// 降序
		/// </summary>
		desc
	}
	/// <summary>
	/// 定义API接口请求参数基类。
	/// </summary>
	public class ApiRequestModel : ApiRequestBase {
		/// <summary>
		/// 为了安全，继承子类必须显示指定支持的排序列。
		/// </summary>
		[NonSerialized]
		private SortDictionary _SupportedSortFields;
		[System.Runtime.Serialization.IgnoreDataMember]
		public SortDictionary SupportedSortFields {
			get {
				return this._SupportedSortFields;
			}
			protected set { this._SupportedSortFields = value; }
		}
		/// <summary>
		/// 获取最终安全的排序表达式。
		/// </summary>
		public string SortExpression {
			get {
				if(string.IsNullOrEmpty(this.Sort))
					return string.Empty;
				string expression = "";
				string[] fields = this.Sort.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

				foreach(string field in fields) {
					string[] sortField = field.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
					if(sortField.Length < 2)
						continue;
					string fieldName = sortField[0];
					if(!SupportedSortFields.ContainsKey(fieldName))
						continue;
					SortMode sortMode = SortMode.asc;
					if(!Enum.TryParse<SortMode>(sortField[1], true, out sortMode))
						continue;
					if(expression == "") {
						expression += string.Format("{0} {1}", SupportedSortFields[fieldName], sortMode.ToString());
					}
					else {
						expression += string.Format(",{0} {1}", SupportedSortFields[fieldName], sortMode.ToString());
					}
				}

				return expression;
			}
		}
		/// <summary>
		/// 多个字段使用英文半角逗号（,）分隔。例如：id|asc,createdTime|desc
		/// </summary>
		public string Sort { get; set; }
		private int _pageIndex = 1;
		/// <summary>
		/// 分页查询时，请求查询的页码。默认1表示第一页。
		/// </summary>
		public int PageIndex {
			get { return _pageIndex; }
			set {
				if(value < 1) {
					_pageIndex = 1;
				}
				else {
					_pageIndex = value;
				}
			}
		}
		private int _pageSize = 10;
		/// <summary>
		/// 分页查询时，请求分页的大小。默认每页10条数据。最大限制为每页50条数据。
		/// </summary>
		public int PageSize {
			get { return _pageSize; }
			set {
				if(value > 50) {
					_pageSize = 50;
				}
				else if(value < 1) {
					_pageSize = 10;
				}
				else {
					_pageSize = value;
				}
			}
		}

		public ApiRequestModel() {
			this.SupportedSortFields = new SortDictionary();
		}
	}

	public class SortDictionary : Dictionary<string, string> {
		public SortDictionary() : base() { }
		public override string ToString() {
			return string.Join("&", this.Select(pair => pair.Key + "=" + pair.Value));
		}
	}
	/// <summary>
	/// 用于中间接口层返回分页数据响应时的数据结构。
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PagedResult<T> {
		/// <summary>
		/// 记录总数。
		/// </summary>
		public int recordCount { get; set; }
		/// <summary>
		/// 响应业务数据对象。
		/// </summary>
		public ICollection<T> data { get; set; }
		public PagedResult(int recordCount, ICollection<T> data) {
			this.recordCount = recordCount;
			this.data = data;
		}
	}
}
