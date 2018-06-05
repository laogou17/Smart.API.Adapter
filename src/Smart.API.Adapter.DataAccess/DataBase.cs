using Infrastructure.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace Smart.API.Adapter.DataAccess {
	public class DataBase {

		/// <summary>
		/// database names.
		/// </summary>
		public enum DbName {
			/// <summary>
			/// db
			/// </summary>
            SmartAPIAdapterCore,
		}

		public Database database = null;
		private string tableName = null;
		private string primaryKey = null;
		private bool isAutoincrement = true;

		private List<string> notContainField = new List<string>();

		DatabaseProviderFactory dbFactory {
			get {
				return new DatabaseProviderFactory();
			}
		}

		public DataBase() {
            this.database = dbFactory.Create(DbName.SmartAPIAdapterCore.ToString());
		}
		public DataBase(DbName dbName) {
			this.database = dbFactory.Create(dbName.ToString());
		}

		/// <summary>
		/// 调用Insert|Update|DeleteByKey|InsertAndGetID
		/// </summary>
		/// <param name="dbName"></param>
		/// <param name="tableName"></param>
		/// <param name="primaryKey"></param>
		public DataBase(DbName dbName, string tableName, string primaryKey) {
			this.database = dbFactory.Create(dbName.ToString());
			this.tableName = tableName;
			this.primaryKey = primaryKey;
		}

		public DataBase(DbName dbName, string tableName, string primaryKey, bool isAutoincrement) {
			this.database = dbFactory.Create(dbName.ToString());
			this.tableName = tableName;
			this.primaryKey = primaryKey;
			this.isAutoincrement = isAutoincrement;
		}


		public DataBase(DbName dbName, string tableName, string primaryKey, bool isAutoincrement, List<string> notContainField) {
			this.database = dbFactory.Create(dbName.ToString());
			this.tableName = tableName;
			this.primaryKey = primaryKey;
			this.isAutoincrement = isAutoincrement;
			this.notContainField = notContainField;
		}

		public DataBase(DbName dbName, string tableName, string primaryKey, List<string> notContainField) {
			this.database = dbFactory.Create(dbName.ToString());
			this.tableName = tableName;
			this.primaryKey = primaryKey;
			this.notContainField = notContainField;
		}

		protected Database db { get { return this.database; } }

		/// <summary>
		/// 表名
		/// </summary>
		protected string TableName {
			get {
				return tableName;
			}
		}

		/// <summary>
		/// 表主键
		/// </summary>
		protected string PrimaryKey {
			get {
				return primaryKey;
			}
		}


		public virtual bool Insert<TResult>(TResult obj) where TResult : new() {
			Hashtable hash = GetHashByEntity(obj, isAutoincrement);
			return Insert(hash, TableName, null);
		}

		public virtual bool Update<TResult>(TResult obj, string KeyId) where TResult : new() {
			Hashtable hash = GetHashByEntity(obj);
			return Update(KeyId, hash, TableName, null);
		}

		public virtual bool DeleteByKey(string Key) {
			string sqlcmd = string.Format("DELETE FROM {0} WHERE " + PrimaryKey + " = @PrimaryKey ", tableName);
			List<DbParameter> listParam = new List<DbParameter>() { 
				new SqlParameter() { DbType = DbType.String, ParameterName = "@PrimaryKey", Value = Key }};
			return ExecuteNoQueryBySql(sqlcmd, listParam) > 0;
		}

		public virtual int GetMaxID() {
			string sqlcmd = string.Format("select max({0}) with(nolock) from {1} ", PrimaryKey, tableName);
			object obj = null;
			using(DbCommand command = db.GetSqlStringCommand(sqlcmd)) {
				obj = db.ExecuteScalar(command);
			}
			if(obj == null) { return 1; } else { return int.Parse(obj.ToString()); }
		}

		public virtual ICollection<TResult> FindAll<TResult>() where TResult : new() {
			string sqlcmd = string.Format("SELECT * FROM {0} with(nolock) ", tableName);
			return GetEnityListBySqlString<TResult>(sqlcmd, null);
		}

		public virtual TResult FindByKey<TResult>(string Key) where TResult : new() {
			string sqlcmd = string.Format("SELECT * FROM {0} with(nolock) WHERE " + PrimaryKey + " = @PrimaryKey ", tableName);

			List<DbParameter> listParam = new List<DbParameter>() { 

				new SqlParameter() { DbType = DbType.String, ParameterName = "@PrimaryKey", Value = Key }};
			return GetEnityBySqlString<TResult>(sqlcmd, listParam);
		}

		public virtual int InsertAndGetID<TResult>(TResult obj) where TResult : new() {
			Hashtable hash = GetHashByEntity(obj, isAutoincrement);
			return InsertAndGetID(hash, TableName);
		}

		protected int InsertAndGetID(Hashtable recordField, string targetTable) {
			int result = 0;
			string fields = ""; // 字段名
			string vals = ""; // 字段值
			if(recordField == null || recordField.Count < 1) {
				return result;
			}
			if(isAutoincrement)
				recordField.Remove(primaryKey);
			SqlParameter[] param = new SqlParameter[recordField.Count];

			if(recordField.Count > 0)  //除主键外不为空则插入
            {
				IEnumerator eKeys = recordField.Keys.GetEnumerator();

				int i = 0;
				while(eKeys.MoveNext()) {
					string field = eKeys.Current.ToString();
					if(primaryKey != field || !isAutoincrement) {
						fields += ("[" + field + "],");
						vals += string.Format("@{0},", field);
						object val = recordField[eKeys.Current.ToString()];
						param[i] = new SqlParameter("@" + field, val);

						i++;
					}
					else {
						if(!isAutoincrement) {
							fields += ("[" + field + "],");
							vals += string.Format("@{0},", field);
							object val = recordField[eKeys.Current.ToString()];
							param[i] = new SqlParameter("@" + field, val);

							i++;
						}
					}
				}

				fields = fields.Trim(',');//除去前后的逗号
				vals = vals.Trim(',');//除去前后的逗号
				string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", targetTable, fields, vals);
				sql += ";select @@identity;";
				DbCommand command = db.GetSqlStringCommand(sql);
				command.Parameters.AddRange(param);

				using(IDataReader dr = db.ExecuteReader(command)) {
					if(dr.Read()) {
						result = Int32.Parse(dr[0].ToString());
					}
				}
			}

			return result;
		}

		protected bool Insert(Hashtable recordField, string targetTable, DbTransaction trans) {
			bool result = false;
			string fields = ""; // 字段名
			string vals = ""; // 字段值
			if(recordField == null || recordField.Count < 1) {
				return result;
			}
			if(isAutoincrement)
				recordField.Remove(primaryKey);
			SqlParameter[] param = new SqlParameter[recordField.Count];

			if(recordField.Count > 0)  //除主键外不为空则插入
            {
				IEnumerator eKeys = recordField.Keys.GetEnumerator();

				int i = 0;
				while(eKeys.MoveNext()) {
					string field = eKeys.Current.ToString();
					if(primaryKey != field) {
						fields += ("[" + field + "],");
						vals += string.Format("@{0},", field);
						object val = recordField[eKeys.Current.ToString()];
						param[i] = new SqlParameter("@" + field, val);

						i++;
					}
					else {
						if(!isAutoincrement) {
							fields += ("[" + field + "],");
							vals += string.Format("@{0},", field);
							object val = recordField[eKeys.Current.ToString()];
							param[i] = new SqlParameter("@" + field, val);

							i++;
						}
					}
				}

				fields = fields.Trim(',');//除去前后的逗号
				vals = vals.Trim(',');//除去前后的逗号
				string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", targetTable, fields, vals);

				DbCommand command = db.GetSqlStringCommand(sql);
				command.Parameters.AddRange(param);

				if(trans != null) {
					result = db.ExecuteNonQuery(command, trans) > 0;
				}
				else {
					result = db.ExecuteNonQuery(command) > 0;
				}

			}

			return result;
		}
		protected bool Update(string id, Hashtable recordField, string targetTable, DbTransaction trans) {
			string field = ""; // 字段名
			object val = null; // 值
			string setValue = ""; // 更新Set () 中的语句

			if(recordField == null || recordField.Count < 1) {
				return false;
			}
			recordField.Remove(primaryKey);
			SqlParameter[] param = new SqlParameter[recordField.Count + 1];
			int i = 0;

			bool result = false;
			if(recordField.Count > 0)  //除主键外有记录集时更新
            {
				IEnumerator eKeys = recordField.Keys.GetEnumerator();
				while(eKeys.MoveNext()) {
					field = eKeys.Current.ToString();
					if(primaryKey != field) {
						val = recordField[eKeys.Current.ToString()];
						setValue += string.Format("{0} = @{1},", ("[" + field + "]"), field);
						param[i] = new SqlParameter(string.Format("@{0}", field), val);

						i++;
					}
				}
				string sql = string.Format("UPDATE {0} SET {1} WHERE {2} = @primaryKey ", targetTable, setValue.Substring(0, setValue.Length - 1), primaryKey);
				param[recordField.Count] = new SqlParameter("@primaryKey", id);
				DbCommand command = db.GetSqlStringCommand(sql);
				command.Parameters.AddRange(param);


				if(trans != null) {
					result = db.ExecuteNonQuery(command, trans) > 0;
				}
				else {
					result = db.ExecuteNonQuery(command) > 0;
				}
			}
			return result;
		}

		protected virtual TResult GetEnityBySqlString<TResult>(string query, IEnumerable<DbParameter> parameterList, DbTransaction transaction = null) where TResult : new() {
			var dataTable = GetDataTableBySqlString(query, parameterList, transaction);
			return dataTable == null ? default(TResult) : dataTable.ToObject<TResult>();
		}

		protected virtual ICollection<TResult> GetEnityListBySqlString<TResult>(string query, IEnumerable<DbParameter> parameterList, DbTransaction transaction = null) where TResult : new() {
			var dataTable = GetDataTableBySqlString(query, parameterList, transaction);
			return dataTable == null ? default(ICollection<TResult>) : dataTable.ToObjectCollection<TResult>();
		}

		protected virtual TResult GetEnityByProcedure<TResult>(string storedProcedureName, IEnumerable<DbParameter> parameterList, DbTransaction transaction = null) where TResult : new() {
			var dataTable = GetDataTableByProcedure(storedProcedureName, parameterList, transaction);
			return dataTable == null ? default(TResult) : dataTable.ToObject<TResult>();
		}

		protected virtual ICollection<TResult> GetEnityListByProcedure<TResult>(string storedProcedureName, IEnumerable<DbParameter> parameterList, DbTransaction transaction = null) where TResult : new() {
			var dataTable = GetDataTableByProcedure(storedProcedureName, parameterList, transaction);
			return dataTable == null ? default(ICollection<TResult>) : dataTable.ToObjectCollection<TResult>();
		}

		protected virtual DataTable GetDataTableBySqlString(string query, IEnumerable<DbParameter> parameterList, DbTransaction transaction = null) {
			var ds = GetDataSetBySqlString(query, parameterList, transaction);
			if(ds != null &&
				ds.Tables.Count > 0 &&
				ds.Tables[0].Rows.Count > 0) {
				return ds.Tables[0];
			}
			return null;
		}

		protected virtual DataTable GetDataTableByProcedure(string storedProcedureName, IEnumerable<DbParameter> parameterList, DbTransaction transaction = null) {
			var ds = GetDataSetByProcedure(storedProcedureName, parameterList, transaction);
			if(ds != null &&
				ds.Tables.Count > 0 &&
				ds.Tables[0].Rows.Count > 0) {
				return ds.Tables[0];
			}
			return null;
		}

		protected virtual DataSet GetDataSetBySqlString(string query, IEnumerable<DbParameter> parameterList, DbTransaction transaction = null) {
			using(DbCommand command = db.GetSqlStringCommand(query)) {
				PrepareParameters(command, parameterList, transaction);
				return db.ExecuteDataSet(command);
			}
		}

		protected virtual DataSet GetDataSetByProcedure(string storedProcedureName, IEnumerable<DbParameter> parameterList, DbTransaction transaction = null) {
			using(DbCommand command = db.GetStoredProcCommand(storedProcedureName)) {
				PrepareParameters(command, parameterList, transaction);
				return db.ExecuteDataSet(command);
			}
		}


		protected virtual int ExecuteNoQueryByProcedure(string storedProcedureName, IEnumerable<DbParameter> parameterList = null, DbTransaction transaction = null) {
			using(DbCommand command = db.GetStoredProcCommand(storedProcedureName)) {
				PrepareParameters(command, parameterList, transaction);
				return db.ExecuteNonQuery(command);
			}
		}

		protected virtual int ExecuteNoQueryBySql(string query, IEnumerable<DbParameter> parameterList = null, DbTransaction transaction = null) {
			using(DbCommand command = db.GetSqlStringCommand(query)) {
				PrepareParameters(command, parameterList, transaction);
				return db.ExecuteNonQuery(command);
			}
		}
		protected virtual object ExecuteScalarBySql(string query, IEnumerable<DbParameter> parameterList = null, DbTransaction transaction = null) {
			using(DbCommand command = db.GetSqlStringCommand(query)) {
				PrepareParameters(command, parameterList, transaction);
				return db.ExecuteScalar(command);
			}
		}

		protected virtual object ExecuteScalarByProcedure(string storedProcedureName, IEnumerable<DbParameter> parameterList = null, DbTransaction transaction = null) {
			using(DbCommand command = db.GetStoredProcCommand(storedProcedureName)) {
				PrepareParameters(command, parameterList, transaction);
				return db.ExecuteScalar(command);
			}
		}

		protected void PrepareParameters(DbCommand command, IEnumerable<DbParameter> parameterList, DbTransaction transaction = null) {
			command.Parameters.Clear();

			if(transaction != null) {
				command.Transaction = transaction;
			}

			if(parameterList != null) {
				foreach(var p in parameterList) {
					//db.AddParameter(command, p.ParameterName, p.DbType, p.Direction, p.SourceColumn, p.SourceVersion, p.Value);
					command.Parameters.Add(p);
				}
			}
		}
		protected DataTable GetDataTableByProcedure(string storedProcedureName, IEnumerable<DbParameter> parameterList, out DbCommand command) {
			//  recordCount = 0;
			using(command = db.GetStoredProcCommand(storedProcedureName)) {
				PrepareParameters(command, parameterList, null);

				DataSet ds = db.ExecuteDataSet(command);

				if(ds.Tables.Count > 0) {
					// recordCount = (int)command.Parameters["@recordCount"].Value;
					return ds.Tables[0];
				}
			}

			return new DataTable();
		}
		public SqlCommand GetSqlCommand(SqlConnection connection, SqlTransaction sqlTran, string CommandText, List<SqlParameter> ParameterList) {
			SqlCommand command = new SqlCommand();
			command.Connection = connection;
			command.CommandText = CommandText;
			command.Transaction = sqlTran;
			command.Parameters.Clear();
			if(ParameterList != null && ParameterList.Count > 0) {
				foreach(SqlParameter sqlpar in ParameterList) {
					command.Parameters.Add(sqlpar);
				}

			}
			return command;

		}

		public string GetStr(List<int> Ids) {
			return string.Join(",", Ids);
		}

		protected virtual Hashtable GetHashByEntity<TResult>(TResult obj) {
			return GetHashByEntity<TResult>(obj, true);
		}
		protected virtual Hashtable GetHashByEntity<TResult>(TResult obj, bool myAutoincrement) {
			Hashtable ht = new Hashtable();
			PropertyInfo[] pis = obj.GetType().GetProperties();
			for(int i = 0; i < pis.Length; i++) {
				if(pis[i].Name != PrimaryKey) {
					object objValue = pis[i].GetValue(obj, null);

					objValue = (objValue == null) ? DBNull.Value : objValue;
					if(!notContainField.Contains(pis[i].Name)) {
						if(!ht.ContainsKey(pis[i].Name)) {
							ht.Add(pis[i].Name, objValue);
						}
					}
				}
				else {
					if(!myAutoincrement) {
						object objValue = pis[i].GetValue(obj, null);

						objValue = (objValue == null) ? DBNull.Value : objValue;

						if(!ht.ContainsKey(pis[i].Name)) {
							ht.Add(pis[i].Name, objValue);
						}
					}
				}
			}
			return ht;
		}


	}
}
