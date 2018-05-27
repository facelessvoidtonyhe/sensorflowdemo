using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
namespace SqliteHelper.DAL
{
	/// <summary>
	/// 数据访问类:ChangeLog
	/// </summary>
	public partial class ChangeLog
	{
        public ChangeLog(string connectionStr)
        {
            SQLiteHelper.connectionString = string.Format("Data Source={0}", connectionStr);
        }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SqliteHelper.Model.ChangeLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ChangeLog(");
			strSql.Append("OrderNo,CabinetNo,AssetNo,CreateTime,OperationType,OperationDetail)");
			strSql.Append(" values (");
			strSql.Append("@OrderNo,@CabinetNo,@AssetNo,@CreateTime,@OperationType,@OperationDetail)");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@OrderNo", DbType.Int32,4),
					new SQLiteParameter("@CabinetNo", DbType.Int32,4),
					new SQLiteParameter("@AssetNo", DbType.Int32,4),
					new SQLiteParameter("@CreateTime", DbType.DateTime),
					new SQLiteParameter("@OperationType", DbType.String,50),
					new SQLiteParameter("@OperationDetail", DbType.String,50)};
			parameters[0].Value = model.OrderNo;
			parameters[1].Value = model.CabinetNo;
			parameters[2].Value = model.AssetNo;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.OperationType;
			parameters[5].Value = model.OperationDetail;

			int rows=SQLiteHelper.ExecuteNonQuery(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SqliteHelper.Model.ChangeLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ChangeLog set ");
			strSql.Append("CabinetNo=@CabinetNo,");
			strSql.Append("AssetNo=@AssetNo,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("OperationType=@OperationType,");
			strSql.Append("OperationDetail=@OperationDetail");
			strSql.Append(" where OrderNo=@OrderNo ");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@CabinetNo", DbType.Int32,4),
					new SQLiteParameter("@AssetNo", DbType.Int32,4),
					new SQLiteParameter("@CreateTime", DbType.DateTime),
					new SQLiteParameter("@OperationType", DbType.String,50),
					new SQLiteParameter("@OperationDetail", DbType.String,50),
					new SQLiteParameter("@OrderNo", DbType.Int32,4)};
			parameters[0].Value = model.CabinetNo;
			parameters[1].Value = model.AssetNo;
			parameters[2].Value = model.CreateTime;
			parameters[3].Value = model.OperationType;
			parameters[4].Value = model.OperationDetail;
			parameters[5].Value = model.OrderNo;

			int rows=SQLiteHelper.ExecuteNonQuery(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int OrderNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ChangeLog ");
			strSql.Append(" where OrderNo=@OrderNo ");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@OrderNo", DbType.Int32,4)			};
			parameters[0].Value = OrderNo;

			int rows=SQLiteHelper.ExecuteNonQuery(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string OrderNolist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ChangeLog ");
			strSql.Append(" where OrderNo in ("+OrderNolist + ")  ");
			int rows=SQLiteHelper.ExecuteNonQuery(strSql.ToString(),null);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SqliteHelper.Model.ChangeLog GetModel(int OrderNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select OrderNo,CabinetNo,AssetNo,CreateTime,OperationType,OperationDetail from ChangeLog ");
			strSql.Append(" where OrderNo=@OrderNo ");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@OrderNo", DbType.Int32,4)			};
			parameters[0].Value = OrderNo;

			SqliteHelper.Model.ChangeLog model=new SqliteHelper.Model.ChangeLog();
			DataTable dt=SQLiteHelper.ExecuteDataTable(strSql.ToString(),parameters);
			if(dt.Rows.Count>0)
			{
				return DataRowToModel(dt.Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SqliteHelper.Model.ChangeLog DataRowToModel(DataRow row)
		{
			SqliteHelper.Model.ChangeLog model=new SqliteHelper.Model.ChangeLog();
			if (row != null)
			{
					//model.OrderNo=row["OrderNo"].ToString();
				if(row["CabinetNo"]!=null && row["CabinetNo"].ToString()!="")
				{
					model.CabinetNo=int.Parse(row["CabinetNo"].ToString());
				}
				if(row["AssetNo"]!=null && row["AssetNo"].ToString()!="")
				{
					model.AssetNo=int.Parse(row["AssetNo"].ToString());
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["OperationType"]!=null)
				{
					model.OperationType=row["OperationType"].ToString();
				}
				if(row["OperationDetail"]!=null)
				{
					model.OperationDetail=row["OperationDetail"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataTable GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select OrderNo,CabinetNo,AssetNo,CreateTime,OperationType,OperationDetail ");
			strSql.Append(" FROM ChangeLog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return SQLiteHelper.ExecuteDataTable(strSql.ToString());
		}
		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

