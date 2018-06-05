#if WebUI
namespace Smart.API.Adapter.Web.Models
#else
namespace Smart.API.Adapter.Models
#endif
{
    using System;

    /// <summary>
    /// define the entity mapping data model on the table : InterfaceLog
    /// </summary>
	[Serializable]
	public partial class InterfaceLog
	{		
    
        #region 构造函数
        
        public InterfaceLog() { }
        
        #endregion
    
		#region 私有变量
		private int _LogId;
		private DateTime _CreatedTime;
		private string _ApplicationName;
		private string _InterfaceName;
		private string _MethodName;
		private DateTime _RequestTime;
		private string _RequestContent;
		private DateTime _ResponseTime;
		private string _ResponseContent;
		private string _ServerName;
		private string _IPAddress;
		private Nullable<DateTime> _QueuedTime;
		private Nullable<DateTime> _DequeuedTime;
		private Guid _rowguid;
		#endregion

		#region 属性
		/// <summary>
		/// 
		/// </summary>
		public virtual int LogId
		{
			get
			{
				return this._LogId;
			}
			set
			{
				if ((this._LogId != value))
				{
					this._LogId = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual DateTime CreatedTime
		{
			get
			{
				return this._CreatedTime;
			}
			set
			{
				if ((this._CreatedTime != value))
				{
					this._CreatedTime = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual string ApplicationName
		{
			get
			{
				return this._ApplicationName ?? "";
			}
			set
			{
				if ((this._ApplicationName != value))
				{
					this._ApplicationName = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual string InterfaceName
		{
			get
			{
				return this._InterfaceName ?? "";
			}
			set
			{
				if ((this._InterfaceName != value))
				{
					this._InterfaceName = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual string MethodName
		{
			get
			{
				return this._MethodName ?? "";
			}
			set
			{
				if ((this._MethodName != value))
				{
					this._MethodName = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual DateTime RequestTime
		{
			get
			{
				return this._RequestTime;
			}
			set
			{
				if ((this._RequestTime != value))
				{
					this._RequestTime = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual string RequestContent
		{
			get
			{
				return this._RequestContent ?? "";
			}
			set
			{
				if ((this._RequestContent != value))
				{
					this._RequestContent = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual DateTime ResponseTime
		{
			get
			{
				return this._ResponseTime;
			}
			set
			{
				if ((this._ResponseTime != value))
				{
					this._ResponseTime = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual string ResponseContent
		{
			get
			{
				return this._ResponseContent ?? "";
			}
			set
			{
				if ((this._ResponseContent != value))
				{
					this._ResponseContent = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual string ServerName
		{
			get
			{
				return this._ServerName ?? "";
			}
			set
			{
				if ((this._ServerName != value))
				{
					this._ServerName = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual string IPAddress
		{
			get
			{
				return this._IPAddress ?? "";
			}
			set
			{
				if ((this._IPAddress != value))
				{
					this._IPAddress = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual Nullable<DateTime> QueuedTime
		{
			get
			{
				return this._QueuedTime;
			}
			set
			{
				if ((this._QueuedTime != value))
				{
					this._QueuedTime = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual Nullable<DateTime> DequeuedTime
		{
			get
			{
				return this._DequeuedTime;
			}
			set
			{
				if ((this._DequeuedTime != value))
				{
					this._DequeuedTime = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual Guid rowguid
		{
			get
			{
				return this._rowguid;
			}
			set
			{
				if ((this._rowguid != value))
				{
					this._rowguid = value;
				}
			}
		}
		#endregion
	}
}
