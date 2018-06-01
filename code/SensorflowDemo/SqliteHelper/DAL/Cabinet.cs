using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
namespace SqliteHelper.DAL
{
	/// <summary>
	/// 数据访问类:Cabinet
	/// </summary>
	public partial class Cabinet
	{
        public Cabinet(string connectionStr)
        {
            SQLiteHelper.connectionString = string.Format("Data Source={0}", connectionStr);
        }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SqliteHelper.Model.Cabinet model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Cabinet(");
			strSql.Append("OrderNo,Name,Type,Location,SerialNumber,User,ColdTemp1,ColdTemp2,ColdTemp3,HotTemp1,HotTemp2,HotTemp3,Space,Power,Weight,PowerPort,NetPort)");
			strSql.Append(" values (");
			strSql.Append("@OrderNo,@Name,@Type,@Location,@SerialNumber,@User,@ColdTemp1,@ColdTemp2,@ColdTemp3,@HotTemp1,@HotTemp2,@HotTemp3,@Space,@Power,@Weight,@PowerPort,@NetPort)");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@OrderNo", DbType.Int32,4),
					new SQLiteParameter("@Name", DbType.String,50),
					new SQLiteParameter("@Type", DbType.String,50),
					new SQLiteParameter("@Location", DbType.String,200),
					new SQLiteParameter("@SerialNumber", DbType.String,50),
					new SQLiteParameter("@User", DbType.String,50),
					new SQLiteParameter("@ColdTemp1", DbType.String,50),
					new SQLiteParameter("@ColdTemp2", DbType.String,50),
					new SQLiteParameter("@ColdTemp3", DbType.String,50),
					new SQLiteParameter("@HotTemp1", DbType.String,50),
					new SQLiteParameter("@HotTemp2", DbType.String,50),
					new SQLiteParameter("@HotTemp3", DbType.String,50),
					new SQLiteParameter("@Space", DbType.Int32,4),
					new SQLiteParameter("@Power", DbType.Decimal,8),
					new SQLiteParameter("@Weight", DbType.Decimal,8),
					new SQLiteParameter("@PowerPort", DbType.Int32,4),
					new SQLiteParameter("@NetPort", DbType.Int32,4)};
			parameters[0].Value = model.OrderNo;
			parameters[1].Value = model.Name;
			parameters[2].Value = model.Type;
			parameters[3].Value = model.Location;
			parameters[4].Value = model.SerialNumber;
			parameters[5].Value = model.User;
			parameters[6].Value = model.ColdTemp1;
			parameters[7].Value = model.ColdTemp2;
			parameters[8].Value = model.ColdTemp3;
			parameters[9].Value = model.HotTemp1;
			parameters[10].Value = model.HotTemp2;
			parameters[11].Value = model.HotTemp3;
			parameters[12].Value = model.Space;
			parameters[13].Value = model.Power;
			parameters[14].Value = model.Weight;
			parameters[15].Value = model.PowerPort;
			parameters[16].Value = model.NetPort;

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
		public bool Update(SqliteHelper.Model.Cabinet model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Cabinet set ");
			strSql.Append("Name=@Name,");
			strSql.Append("Type=@Type,");
			strSql.Append("Location=@Location,");
			strSql.Append("SerialNumber=@SerialNumber,");
			strSql.Append("User=@User,");
			strSql.Append("ColdTemp1=@ColdTemp1,");
			strSql.Append("ColdTemp2=@ColdTemp2,");
			strSql.Append("ColdTemp3=@ColdTemp3,");
			strSql.Append("HotTemp1=@HotTemp1,");
			strSql.Append("HotTemp2=@HotTemp2,");
			strSql.Append("HotTemp3=@HotTemp3,");
			strSql.Append("Space=@Space,");
			strSql.Append("Power=@Power,");
			strSql.Append("Weight=@Weight,");
			strSql.Append("PowerPort=@PowerPort,");
			strSql.Append("NetPort=@NetPort");
			strSql.Append(" where OrderNo=@OrderNo ");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@Name", DbType.String,50),
					new SQLiteParameter("@Type", DbType.String,50),
					new SQLiteParameter("@Location", DbType.String,200),
					new SQLiteParameter("@SerialNumber", DbType.String,50),
					new SQLiteParameter("@User", DbType.String,50),
					new SQLiteParameter("@ColdTemp1", DbType.String,50),
					new SQLiteParameter("@ColdTemp2", DbType.String,50),
					new SQLiteParameter("@ColdTemp3", DbType.String,50),
					new SQLiteParameter("@HotTemp1", DbType.String,50),
					new SQLiteParameter("@HotTemp2", DbType.String,50),
					new SQLiteParameter("@HotTemp3", DbType.String,50),
					new SQLiteParameter("@Space", DbType.Int32,4),
					new SQLiteParameter("@Power", DbType.Decimal,8),
					new SQLiteParameter("@Weight", DbType.Decimal,8),
					new SQLiteParameter("@PowerPort", DbType.Int32,4),
					new SQLiteParameter("@NetPort", DbType.Int32,4),
					new SQLiteParameter("@OrderNo", DbType.Int32,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.Type;
			parameters[2].Value = model.Location;
			parameters[3].Value = model.SerialNumber;
			parameters[4].Value = model.User;
			parameters[5].Value = model.ColdTemp1;
			parameters[6].Value = model.ColdTemp2;
			parameters[7].Value = model.ColdTemp3;
			parameters[8].Value = model.HotTemp1;
			parameters[9].Value = model.HotTemp2;
			parameters[10].Value = model.HotTemp3;
			parameters[11].Value = model.Space;
			parameters[12].Value = model.Power;
			parameters[13].Value = model.Weight;
			parameters[14].Value = model.PowerPort;
			parameters[15].Value = model.NetPort;
			parameters[16].Value = model.OrderNo;

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
			strSql.Append("delete from Cabinet ");
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
			strSql.Append("delete from Cabinet ");
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
		public SqliteHelper.Model.Cabinet GetModel(int OrderNo)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select OrderNo,Name,Type,Location,SerialNumber,User,ColdTemp1,ColdTemp2,ColdTemp3,HotTemp1,HotTemp2,HotTemp3,Space,Power,Weight,PowerPort,NetPort from Cabinet ");
			strSql.Append(" where OrderNo=@OrderNo ");
			SQLiteParameter[] parameters = {
					new SQLiteParameter("@OrderNo", DbType.Int32,4)			};
			parameters[0].Value = OrderNo;

			SqliteHelper.Model.Cabinet model=new SqliteHelper.Model.Cabinet();
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
		public SqliteHelper.Model.Cabinet DataRowToModel(DataRow row)
		{
			SqliteHelper.Model.Cabinet model=new SqliteHelper.Model.Cabinet();
			if (row != null)
			{
					//model.OrderNo=row["OrderNo"].ToString();
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
                if (row["OrderNo"] != null)
                {
                    model.OrderNo = int.Parse(row["OrderNo"].ToString());
                }
                if (row["Type"]!=null)
				{
					model.Type=row["Type"].ToString();
				}
				if(row["Location"]!=null)
				{
					model.Location=row["Location"].ToString();
				}
				if(row["SerialNumber"]!=null)
				{
					model.SerialNumber=row["SerialNumber"].ToString();
				}
				if(row["User"]!=null)
				{
					model.User=row["User"].ToString();
				}
				if(row["ColdTemp1"]!=null)
				{
					model.ColdTemp1=row["ColdTemp1"].ToString();
				}
				if(row["ColdTemp2"]!=null)
				{
					model.ColdTemp2=row["ColdTemp2"].ToString();
				}
				if(row["ColdTemp3"]!=null)
				{
					model.ColdTemp3=row["ColdTemp3"].ToString();
				}
				if(row["HotTemp1"]!=null)
				{
					model.HotTemp1=row["HotTemp1"].ToString();
				}
				if(row["HotTemp2"]!=null)
				{
					model.HotTemp2=row["HotTemp2"].ToString();
				}
				if(row["HotTemp3"]!=null)
				{
					model.HotTemp3=row["HotTemp3"].ToString();
				}
				if(row["Space"]!=null && row["Space"].ToString()!="")
				{
					model.Space=int.Parse(row["Space"].ToString());
				}
				if(row["Power"]!=null && row["Power"].ToString()!="")
				{
					model.Power=decimal.Parse(row["Power"].ToString());
				}
				if(row["Weight"]!=null && row["Weight"].ToString()!="")
				{
					model.Weight=decimal.Parse(row["Weight"].ToString());
				}
				if(row["PowerPort"]!=null && row["PowerPort"].ToString()!="")
				{
					model.PowerPort=int.Parse(row["PowerPort"].ToString());
				}
				if(row["NetPort"]!=null && row["NetPort"].ToString()!="")
				{
					model.NetPort=int.Parse(row["NetPort"].ToString());
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
			strSql.Append("select OrderNo,Name,Type,Location,SerialNumber,User,ColdTemp1,ColdTemp2,ColdTemp3,HotTemp1,HotTemp2,HotTemp3,Space,Power,Weight,PowerPort,NetPort ");
			strSql.Append(" FROM Cabinet ");
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

