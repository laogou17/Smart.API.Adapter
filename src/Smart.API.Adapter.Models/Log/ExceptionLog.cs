#if WebUI
namespace Smart.API.Adapter.Web.Models
#else
namespace Smart.API.Adapter.Models
#endif
{
    using System;

    /// <summary>
    /// define the entity mapping data model on the table : ExceptionLog
    /// </summary>
	[Serializable]
	public partial class ExceptionLog
	{		
    
        #region 构造函数
        
        public ExceptionLog() { }
        
        #endregion
    
		#region 私有变量
		private int _LogId;
		private DateTime _CreatedTime;
		private string _ApplicationName;
		private string _ModuleName;
		private DateTime _ExceptionTime;
		private string _Message;
		private string _StackTrace;
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
		public virtual string ModuleName
		{
			get
			{
				return this._ModuleName ?? "";
			}
			set
			{
				if ((this._ModuleName != value))
				{
					this._ModuleName = value;
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public virtual DateTime ExceptionTime
		{
			get
			{
				return this._ExceptionTime;
			}
			set
			{
				if ((this._ExceptionTime != value))
				{
					this._ExceptionTime = value;
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
		public virtual string StackTrace
		{
			get
			{
				return this._StackTrace ?? "";
			}
			set
			{
				if ((this._StackTrace != value))
				{
					this._StackTrace = value;
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
