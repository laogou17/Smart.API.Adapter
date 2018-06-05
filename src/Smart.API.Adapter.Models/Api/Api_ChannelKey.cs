#if WebUI
namespace Smart.API.Adapter.Web.Models
#else
namespace Smart.API.Adapter.Models
#endif
{
    using System;

    /// <summary>
    /// define the entity mapping data model on the table : Api_ChannelKey
    /// </summary>
	[Serializable]
	public partial class Api_ChannelKey
	{		
    
        #region 构造函数
        
        public Api_ChannelKey() { }
        
        #endregion
    
		#region 私有变量
		private string _AccessId;
		private Guid _AccessKey;
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
		/// 接入渠道密钥
		/// </summary>
		public virtual Guid AccessKey
		{
			get
			{
				return this._AccessKey;
			}
			set
			{
				if ((this._AccessKey != value))
				{
					this._AccessKey = value;
				}
			}
		}
		#endregion
	}
}
