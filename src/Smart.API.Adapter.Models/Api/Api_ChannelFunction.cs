#if WebUI
namespace Smart.API.Adapter.Web.Models
#else
namespace Smart.API.Adapter.Models
#endif
{
    using System;

    /// <summary>
    /// define the entity mapping data model on the table : Api_ChannelFunction
    /// </summary>
	[Serializable]
	public partial class Api_ChannelFunction
	{		
    
        #region 构造函数
        
        public Api_ChannelFunction() { }
        
        #endregion
    
		#region 私有变量
		private string _AccessId;
		private int _FunctionId;
		#endregion

		#region 属性
		/// <summary>
		/// 
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
		/// 
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
		#endregion
	}
}
