#if WebUI
namespace Smart.API.Adapter.Web.Models
#else
namespace Smart.API.Adapter.Models
#endif
{
    using System;

    /// <summary>
    /// define the entity mapping data model on the table : Api_Function
    /// </summary>
	[Serializable]
	public partial class Api_Function
	{		
    
        #region 构造函数
        
        public Api_Function() { }
        
        #endregion
    
		#region 私有变量
		private int _FunctionId;
		private string _FunctionCode;
		private string _FunctionName;
		private string _FunctionCategory;
		private int _ProviderType;
		private bool _EnableStatus;
		private string _Remark;
		#endregion

		#region 属性
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
		/// 接口编码
		/// </summary>
		public virtual string FunctionCode
		{
			get
			{
				return this._FunctionCode ?? "";
			}
			set
			{
				if ((this._FunctionCode != value))
				{
					this._FunctionCode = value;
				}
			}
		}
		/// <summary>
		/// 接口名称
		/// </summary>
		public virtual string FunctionName
		{
			get
			{
				return this._FunctionName ?? "";
			}
			set
			{
				if ((this._FunctionName != value))
				{
					this._FunctionName = value;
				}
			}
		}
		/// <summary>
		/// 接口类别
		/// </summary>
		public virtual string FunctionCategory
		{
			get
			{
				return this._FunctionCategory ?? "";
			}
			set
			{
				if ((this._FunctionCategory != value))
				{
					this._FunctionCategory = value;
				}
			}
		}
		/// <summary>
		/// 接口类型(用于区分不同版本)
		/// </summary>
		public virtual int ProviderType
		{
			get
			{
				return this._ProviderType;
			}
			set
			{
				if ((this._ProviderType != value))
				{
					this._ProviderType = value;
				}
			}
		}
		/// <summary>
		/// 启用状态 0-未启用 1-启用（默认）
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
		#endregion
	}
}
