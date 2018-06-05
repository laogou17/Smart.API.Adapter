#if WebUI
namespace Smart.API.Adapter.Web.Models
#else
namespace Smart.API.Adapter.Models
#endif
{
    using System;

    /// <summary>
    /// define the entity mapping data model on the table : Api_ChannelAccessFrequency
    /// </summary>
	[Serializable]
	public partial class Api_ChannelAccessFrequency
	{		
    
        #region 构造函数
        
        public Api_ChannelAccessFrequency() { }
        
        #endregion
    
		#region 私有变量
		private int _Id;
		private int _FunctionId;
		private string _AccessId;
		private string _AccessFrequency;
		private int _AccessLimit;
		private Nullable<int> _AccessCount;
		private Nullable<DateTime> _AccessTime;
		#endregion

		#region 属性
		/// <summary>
		/// 主键，自增长
		/// </summary>
		public virtual int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this._Id = value;
				}
			}
		}
		/// <summary>
		/// 接口ID
		/// </summary>
		public virtual int FunctionId
		{
			get
			{
				return this._FunctionId;
			}
			set
			{
				if ((this._FunctionId != value))
				{
					this._FunctionId = value;
				}
			}
		}
		/// <summary>
		/// 接入渠道编码
		/// </summary>
		public virtual string AccessId
		{
			get
			{
				return this._AccessId ?? "";
			}
			set
			{
				if ((this._AccessId != value))
				{
					this._AccessId = value;
				}
			}
		}
		/// <summary>
		/// 限定访问频率 Minute-分钟 Hour-小时 Day-天 Week-周
		/// </summary>
		public virtual string AccessFrequency
		{
			get
			{
				return this._AccessFrequency ?? "";
			}
			set
			{
				if ((this._AccessFrequency != value))
				{
					this._AccessFrequency = value;
				}
			}
		}
		/// <summary>
		/// 限定访问次数
		/// </summary>
		public virtual int AccessLimit
		{
			get
			{
				return this._AccessLimit;
			}
			set
			{
				if ((this._AccessLimit != value))
				{
					this._AccessLimit = value;
				}
			}
		}
		/// <summary>
		/// 单位时间内已累计访问的次数
		/// </summary>
		public virtual Nullable<int> AccessCount
		{
			get
			{
				return this._AccessCount;
			}
			set
			{
				if ((this._AccessCount != value))
				{
					this._AccessCount = value;
				}
			}
		}
		/// <summary>
		/// 最后访问时间
		/// </summary>
		public virtual Nullable<DateTime> AccessTime
		{
			get
			{
				return this._AccessTime;
			}
			set
			{
				if ((this._AccessTime != value))
				{
					this._AccessTime = value;
				}
			}
		}
		#endregion
	}
}
