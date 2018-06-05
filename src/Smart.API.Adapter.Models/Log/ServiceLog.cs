#if WebUI
namespace Smart.API.Adapter.Web.Models
#else
namespace Smart.API.Adapter.Models
#endif
{
    using System;

    /// <summary>
    /// define the entity mapping data model on the table : ServiceLog
    /// </summary>
	[Serializable]
	public partial class ServiceLog
	{		
    
        #region 构造函数
        
        public ServiceLog() { }
        
        #endregion
    
		#region 私有变量
		private int _LogId;
		private DateTime _CreatedTime;
		private string _ApplicationName;
		private string _ServiceName;
		private DateTime _ServiceRunTime;
		private string _Message;
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
		public virtual string ServiceName
		{
			get
			{
				return this._ServiceName ?? "";
			}
			set
			{
				if ((this._ServiceName != value))
				{
					this._ServiceName = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual DateTime ServiceRunTime
		{
			get
			{
				return this._ServiceRunTime;
			}
			set
			{
				if ((this._ServiceRunTime != value))
				{
					this._ServiceRunTime = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual string Message
		{
			get
			{
				return this._Message ?? "";
			}
			set
			{
				if ((this._Message != value))
				{
					this._Message = value;
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
