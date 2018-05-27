using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
namespace SqliteHelper.DAL
{
	/// <summary>
	/// 数据访问类:RealTimeData
	/// </summary>
	public partial class RealTimeData
	{
        public RealTimeData(string connectionStr)
        {
            SQLiteHelper.connectionString = string.Format("Data Source={0}", connectionStr);
        }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SqliteHelper.Model.RealTimeData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into RealTimeData(");
			strSql.Append("OrderNo,CabinetNo,LayerIndex,RFID,UpdateTime)");
			strSql.Append(" values (");
			strSql.Append("@OrderNo,@CabinetNo,@LayerIndex,@RFID,@UpdateTime)");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@OrderNo", DbType.Int32,4),
					new SQLiteParameter("@CabinetNo", DbType.Int32,4),
					new SQLiteParameter("@LayerIndex", DbType.Int32,4),
					new SQLiteParameter("@RFID", DbType.String,50),
					new SQLiteParameter("@UpdateTime", DbType.DateTime)};
			parameters[0].Value = model.OrderNo;
			parameters[1].Value = model.CabinetNo;
			parameters[2].Value = model.LayerIndex;
			parameters[3].Value = model.RFID;
			parameters[4].Value = model.UpdateTime;

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
		public bool Update(SqliteHelper.Model.RealTimeData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update RealTimeData set ");
			strSql.Append("CabinetNo=@CabinetNo,");
			strSql.Append("LayerIndex=@LayerIndex,");
			strSql.Append("RFID=@RFID,");
			strSql.Append("UpdateTime=@UpdateTime");
			strSql.Append(" where OrderNo=@OrderNo ");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@CabinetNo", DbType.Int32,4),
					new SQLiteParameter("@LayerIndex", DbType.Int32,4),
					new SQLiteParameter("@RFID", DbType.String,50),
					new SQLiteParameter("@UpdateTime", DbType.DateTime),
					new SQLiteParameter("@OrderNo", DbType.Int32,4)};
			parameters[0].Value = model.CabinetNo;
			parameters[1].Value = model.LayerIndex;
			parameters[2].Value = model.RFID;
			parameters[3].Value = model.UpdateTime;
			parameters[4].Value = model.OrderNo;

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
			strSql.Append("delete from RealTimeData ");
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
			strSql.Append("delete from RealTimeData ");
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
		public SqliteHelper.Model.RealTimeData GetModel(int OrderNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select OrderNo,CabinetNo,LayerIndex,RFID,UpdateTime from RealTimeData ");
			strSql.Append(" where OrderNo=@OrderNo ");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@OrderNo", DbType.Int32,4)			};
			parameters[0].Value = OrderNo;

			SqliteHelper.Model.RealTimeData model=new SqliteHelper.Model.RealTimeData();
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
		public SqliteHelper.Model.RealTimeData DataRowToModel(DataRow row)
		{
			SqliteHelper.Model.RealTimeData model=new SqliteHelper.Model.RealTimeData();
			if (row != null)
			{
					//model.OrderNo=row["OrderNo"].ToString();
				if(row["CabinetNo"]!=null && row["CabinetNo"].ToString()!="")
				{
					model.CabinetNo=int.Parse(row["CabinetNo"].ToString());
				}
				if(row["LayerIndex"]!=null && row["LayerIndex"].ToString()!="")
				{
					model.LayerIndex=int.Parse(row["LayerIndex"].ToString());
				}
				if(row["RFID"]!=null)
				{
					model.RFID=row["RFID"].ToString();
				}
				if(row["UpdateTime"]!=null && row["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(row["UpdateTime"].ToString());
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
			strSql.Append("select OrderNo,CabinetNo,LayerIndex,RFID,UpdateTime ");
			strSql.Append(" FROM RealTimeData ");
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

