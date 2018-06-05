#if WebUI
namespace Smart.API.Adapter.Web.Models
#else
namespace Smart.API.Adapter.Models
#endif
{
    using System;

    /// <summary>
    /// define the entity mapping data model on the table : Api_Channel
    /// </summary>
	[Serializable]
	public partial class Api_Channel
	{		
    
        #region 构造函数
        
        public Api_Channel() { }
        
        #endregion
    
		#region 私有变量
		private string _AccessId;
		private string _ChannelName;
		private int _ChannelType;
		private string _ContactUser;
		private string _ContactMobile;
		private string _Remark;
		private bool _EnableStatus;
		private string _Creator;
		private DateTime _CreateTime;
		private string _ModifyUser;
		private DateTime _ModifyTime;
		#endregion

		#region 属性
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
		/// 接入渠道名称
		/// </summary>
		public virtual string ChannelName
		{
			get
			{
				return this._ChannelName ?? "";
			}
			set
			{
				if ((this._ChannelName != value))
				{
					this._ChannelName = value;
				}
			}
		}
		/// <summary>
		/// 接入渠道类型 0-自有渠道 1-第三方渠道
		/// </summary>
		public virtual int ChannelType
		{
			get
			{
				return this._ChannelType;
			}
			set
			{
				if ((this._ChannelType != value))
				{
					this._ChannelType = value;
				}
			}
		}
		/// <summary>
		/// 联系人
		/// </summary>
		public virtual string ContactUser
		{
			get
			{
				return this._ContactUser ?? "";
			}
			set
			{
				if ((this._ContactUser != value))
				{
					this._ContactUser = value;
				}
			}
		}
		/// <summary>
		/// 联系电话
		/// </summary>
		public virtual string ContactMobile
		{
			get
			{
				return this._ContactMobile ?? "";
			}
			set
			{
				if ((this._ContactMobile != value))
				{
					this._ContactMobile = value;
				}
			}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public virtual string Remark
		{
			get
			{
				return this._Remark ?? "";
			}
			set
			{
				if ((this._Remark != value))
				{
					this._Remark = value;
				}
			}
		}
		/// <summary>
		/// 启用状态
		/// </summary>
		public virtual bool EnableStatus
		{
			get
			{
				return this._EnableStatus;
			}
			set
			{
				if ((this._EnableStatus != value))
				{
					this._EnableStatus = value;
				}
			}
		}
		/// <summary>
		/// 创建人
		/// </summary>
		public virtual string Creator
		{
			get
			{
				return this._Creator ?? "";
			}
			set
			{
				if ((this._Creator != value))
				{
					this._Creator = value;
				}
			}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public virtual DateTime CreateTime
		{
			get
			{
				return this._CreateTime;
			}
			set
			{
				if ((this._CreateTime != value))
				{
					this._CreateTime = value;
				}
			}
		}
		/// <summary>
		/// 修改人
		/// </summary>
		public virtual string ModifyUser
		{
			get
			{
				return this._ModifyUser ?? "";
			}
			set
			{
				if ((this._ModifyUser != value))
				{
					this._ModifyUser = value;
				}
			}
		}
		/// <summary>
		/// 修改时间
		/// </summary>
		public virtual DateTime ModifyTime
		{
			get
			{
				return this._ModifyTime;
			}
			set
			{
				if ((this._ModifyTime != value))
				{
					this._ModifyTime = value;
				}
			}
		}
		#endregion
	}
}
