using Smart.API.Adapter.DataAccess;
using System;
using System.Data;
using System.Data.Common;
using Smart.API.Adapter.Models;

namespace Smart.API.Adapter.DataAccess.Sys
{

    /// <summary>
    /// 封装接口调用相关请求校验的逻辑。
    /// </summary>
    public class ApiAccessDAL : DataBase
    {

        public ApiAccessDAL() : base(DbName.SmartAPIAdapterCore) { }

        /// <summary>
        /// 根据接入号获取渠道接入信息
        /// </summary>
        /// <param name="accessId">接入渠道编码</param>
        /// <returns></returns>
        public Api_Channel GetApiChannelByAccessId(string accessId)
        {

            string sql = @"select * from [dbo].[Api_Channel] with(nolock) where [AccessId]=@accessId";

            using (DbCommand cmd = db.GetSqlStringCommand(sql))
            {

                db.AddInParameter(cmd, "@accessId", DbType.String, accessId);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                    return dt.ToObject<Api_Channel>();
                else
                    return null;
            }
        }

        /// <summary>
        /// 根据接入号获取渠道密钥信息
        /// </summary>
        /// <param name="accessId">接入渠道编码</param>
        /// <returns></returns>
        public Api_ChannelKey GetChannelKeyByAccessId(string accessId)
        {

            string sql = @"select top 1 * from [dbo].[Api_ChannelKey] with(nolock) where [AccessId]=@accessId";

            using (DbCommand cmd = db.GetSqlStringCommand(sql))
            {

                db.AddInParameter(cmd, "@accessId", DbType.String, accessId);

                DataTable dt = db.ExecuteDataSet(cmd).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                    return dt.ToObject<Api_ChannelKey>();
                else
                    return null;
            }
        }

        /// <summary>
        /// 判断某个接入渠道是否有指定接口权限。
        /// </summary>
        /// <param name="accessId">接入渠道编码</param>
        /// <param name="functionCode">访问接口名称</param>
        /// <returns></returns>
        public bool HasFunction(string accessId, string functionCode)
        {

            string sql = @"if exists (select 1 from [dbo].[Api_ChannelFunction] a with(nolock)
			inner join [dbo].[Api_Function] b with(nolock) on a.[FunctionId] = b.[FunctionId]
			where a.[AccessId] = @accessId and b.[FunctionCode] = @functionCode)
begin
	select 1;
end
else
begin
	select 0;
end";

            using (DbCommand cmd = db.GetSqlStringCommand(sql))
            {

                db.AddInParameter(cmd, "@accessId", DbType.String, accessId);
                db.AddInParameter(cmd, "@functionCode", DbType.String, functionCode);

                object val = db.ExecuteScalar(cmd);

                if (val == null || val == DBNull.Value) return false;

                return (val.ToString() == "1");
            }
        }

        /// <summary>
        /// 计算某个接入渠道接口访问频率是否超过限定配置值
        /// </summary>
        /// <param name="accessId">接入渠道编码</param>
        /// <param name="functionCode">访问接口名称</param>
        /// <returns>超过限定值返回true,否则返回false.</returns>
        public bool ComputingAccessFrequency(string accessId, string functionCode)
        {

            using (DbCommand cmd = db.GetStoredProcCommand("ComputingAccessFrequency"))
            {

                db.AddInParameter(cmd, "@accessId", DbType.String, accessId);
                db.AddInParameter(cmd, "@functionCode", DbType.String, functionCode);
                db.AddParameter(cmd, "@ReturnValue", DbType.Int32, ParameterDirection.ReturnValue,
                    null, DataRowVersion.Current, null);

                db.ExecuteNonQuery(cmd);

                object val = db.GetParameterValue(cmd, "@ReturnValue");

                if (val == null || val == DBNull.Value) return false;

                return (val.ToString() == "1");
            }
        }
    }
}
