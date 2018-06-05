using System;
using System.Data;
using System.Data.Common;
namespace Smart.API.Adapter.DataAccess.Sys {
	public class SequenceDAL : DataBase {

		public SequenceDAL() : base(DbName.SmartAPIAdapterCore) { }

		/// <summary>
		/// 获取参数 name 所指定序列的下一个序列值。
		/// </summary>
		/// <param name="name">序列名称。</param>
		/// <returns>返回序列的下一个序列值</returns>
		public long GetSequenceValue(string name) {
			using(DbCommand cmd = db.GetStoredProcCommand("ComputingNextSequenceValue")) {
				db.AddInParameter(cmd, "@name", DbType.String, name);
				var returnValue = db.ExecuteScalar(cmd);
				if(returnValue == DBNull.Value) {
					throw new InvalidOperationException("指定的序列 “" + name + "” 不存在。");
				}
				return Convert.ToInt64(returnValue.ToString());
			}
		}
	}
}
