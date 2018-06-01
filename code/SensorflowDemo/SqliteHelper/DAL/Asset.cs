
using System;
using System.Data;
using System.Text;
using System.Data.SQLite;
namespace SqliteHelper.DAL
{
    /// <summary>
    /// 数据访问类:Asset
    /// </summary>
    public partial class Asset
    {
        public Asset(string connectionStr)
        {
            SQLiteHelper.connectionString = string.Format("Data Source={0}", connectionStr);
        }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SqliteHelper.Model.Asset model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Asset(");
            strSql.Append("OrderNo,Name,BM,Type,AssetTM,AssetModel,LabelId,RfidId,State,UseUnit,Purpose,MaintenanceState,MaintenanceUnit,MaintenanceDeadline,CabinetNo,StartLayer,UsedLayer,UsedPower,UsedWeight,UsedPowerPort,UsedNetPort)");
            strSql.Append(" values (");
            strSql.Append("@OrderNo,@Name,@BM,@Type,@AssetTM,@AssetModel,@LabelId,@RfidId,@State,@UseUnit,@Purpose,@MaintenanceState,@MaintenanceUnit,@MaintenanceDeadline,@CabinetNo,@StartLayer,@UsedLayer,@UsedPower,@UsedWeight,@UsedPowerPort,@UsedNetPort)");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@OrderNo", DbType.Int32,4),
                    new SQLiteParameter("@Name", DbType.String),
                    new SQLiteParameter("@BM", DbType.String),
                    new SQLiteParameter("@Type", DbType.String),
                    new SQLiteParameter("@AssetTM", DbType.String),
                    new SQLiteParameter("@AssetModel", DbType.String),
                    new SQLiteParameter("@LabelId", DbType.String),
                    new SQLiteParameter("@RfidId", DbType.String),
                    new SQLiteParameter("@State", DbType.String),
                    new SQLiteParameter("@UseUnit", DbType.String),
                    new SQLiteParameter("@Purpose", DbType.String),
                    new SQLiteParameter("@MaintenanceState", DbType.String),
                    new SQLiteParameter("@MaintenanceUnit", DbType.String),
                    new SQLiteParameter("@MaintenanceDeadline", DbType.DateTime),
                    new SQLiteParameter("@CabinetNo", DbType.Int32,4),
                    new SQLiteParameter("@StartLayer", DbType.Int32,4),
                    new SQLiteParameter("@UsedLayer", DbType.Int32,4),
                    new SQLiteParameter("@UsedPower", DbType.Decimal,8),
                    new SQLiteParameter("@UsedWeight", DbType.Decimal,8),
                    new SQLiteParameter("@UsedPowerPort", DbType.Int32,4),
                    new SQLiteParameter("@UsedNetPort", DbType.Int32,4)};
            parameters[0].Value = model.OrderNo;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.BM;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.AssetTM;
            parameters[5].Value = model.AssetModel;
            parameters[6].Value = model.LabelId;
            parameters[7].Value = model.RfidId;
            parameters[8].Value = model.State;
            parameters[9].Value = model.UseUnit;
            parameters[10].Value = model.Purpose;
            parameters[11].Value = model.MaintenanceState;
            parameters[12].Value = model.MaintenanceUnit;
            parameters[13].Value = model.MaintenanceDeadline;
            parameters[14].Value = model.CabinetNo;
            parameters[15].Value = model.StartLayer;
            parameters[16].Value = model.UsedLayer;
            parameters[17].Value = model.UsedPower;
            parameters[18].Value = model.UsedWeight;
            parameters[19].Value = model.UsedPowerPort;
            parameters[20].Value = model.UsedNetPort;

            int rows = SQLiteHelper.ExecuteNonQuery(strSql.ToString(), parameters);
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
        public bool Update(SqliteHelper.Model.Asset model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Asset set ");
            strSql.Append("Name=@Name,");
            strSql.Append("BM=@BM,");
            strSql.Append("Type=@Type,");
            strSql.Append("AssetTM=@AssetTM,");
            strSql.Append("AssetModel=@AssetModel,");
            strSql.Append("LabelId=@LabelId,");
            strSql.Append("RfidId=@RfidId,");
            strSql.Append("State=@State,");
            strSql.Append("UseUnit=@UseUnit,");
            strSql.Append("Purpose=@Purpose,");
            strSql.Append("MaintenanceState=@MaintenanceState,");
            strSql.Append("MaintenanceUnit=@MaintenanceUnit,");
            strSql.Append("MaintenanceDeadline=@MaintenanceDeadline,");
            strSql.Append("CabinetNo=@CabinetNo,");
            strSql.Append("StartLayer=@StartLayer,");
            strSql.Append("UsedLayer=@UsedLayer,");
            strSql.Append("UsedPower=@UsedPower,");
            strSql.Append("UsedWeight=@UsedWeight,");
            strSql.Append("UsedPowerPort=@UsedPowerPort,");
            strSql.Append("UsedNetPort=@UsedNetPort");
            strSql.Append(" where OrderNo=@OrderNo ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@Name", DbType.String),
                    new SQLiteParameter("@BM", DbType.String),
                    new SQLiteParameter("@Type", DbType.String),
                    new SQLiteParameter("@AssetTM", DbType.String),
                    new SQLiteParameter("@AssetModel", DbType.String),
                    new SQLiteParameter("@LabelId", DbType.String),
                    new SQLiteParameter("@RfidId", DbType.String),
                    new SQLiteParameter("@State", DbType.String),
                    new SQLiteParameter("@UseUnit", DbType.String),
                    new SQLiteParameter("@Purpose", DbType.String),
                    new SQLiteParameter("@MaintenanceState", DbType.String),
                    new SQLiteParameter("@MaintenanceUnit", DbType.String),
                    new SQLiteParameter("@MaintenanceDeadline", DbType.DateTime),
                    new SQLiteParameter("@CabinetNo", DbType.Int32,4),
                    new SQLiteParameter("@StartLayer", DbType.Int32,4),
                    new SQLiteParameter("@UsedLayer", DbType.Int32,4),
                    new SQLiteParameter("@UsedPower", DbType.Decimal,8),
                    new SQLiteParameter("@UsedWeight", DbType.Decimal,8),
                    new SQLiteParameter("@UsedPowerPort", DbType.Int32,4),
                    new SQLiteParameter("@UsedNetPort", DbType.Int32,4),
                    new SQLiteParameter("@OrderNo", DbType.Int32,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.BM;
            parameters[2].Value = model.Type;
            parameters[3].Value = model.AssetTM;
            parameters[4].Value = model.AssetModel;
            parameters[5].Value = model.LabelId;
            parameters[6].Value = model.RfidId;
            parameters[7].Value = model.State;
            parameters[8].Value = model.UseUnit;
            parameters[9].Value = model.Purpose;
            parameters[10].Value = model.MaintenanceState;
            parameters[11].Value = model.MaintenanceUnit;
            parameters[12].Value = model.MaintenanceDeadline;
            parameters[13].Value = model.CabinetNo;
            parameters[14].Value = model.StartLayer;
            parameters[15].Value = model.UsedLayer;
            parameters[16].Value = model.UsedPower;
            parameters[17].Value = model.UsedWeight;
            parameters[18].Value = model.UsedPowerPort;
            parameters[19].Value = model.UsedNetPort;
            parameters[20].Value = model.OrderNo;

            int rows = SQLiteHelper.ExecuteNonQuery(strSql.ToString(), parameters);
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

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Asset ");
            strSql.Append(" where OrderNo=@OrderNo ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@OrderNo", DbType.Int32,4)         };
            parameters[0].Value = OrderNo;

            int rows = SQLiteHelper.ExecuteNonQuery(strSql.ToString(), parameters);
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
        public bool DeleteList(string OrderNolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Asset ");
            strSql.Append(" where OrderNo in (" + OrderNolist + ")  ");
            int rows = SQLiteHelper.ExecuteNonQuery(strSql.ToString(), null);
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
        public SqliteHelper.Model.Asset GetModel(int OrderNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderNo,Name,BM,Type,AssetTM,AssetModel,LabelId,RfidId,State,UseUnit,Purpose,MaintenanceState,MaintenanceUnit,MaintenanceDeadline,CabinetNo,StartLayer,UsedLayer,UsedPower,UsedWeight,UsedPowerPort,UsedNetPort from Asset ");
            strSql.Append(" where OrderNo=@OrderNo ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@OrderNo", DbType.Int32,4)         };
            parameters[0].Value = OrderNo;

            SqliteHelper.Model.Asset model = new SqliteHelper.Model.Asset();
            DataTable dt = SQLiteHelper.ExecuteDataTable(strSql.ToString(), parameters);
            if (dt.Rows.Count > 0)
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
        public SqliteHelper.Model.Asset DataRowToModel(DataRow row)
        {
            SqliteHelper.Model.Asset model = new SqliteHelper.Model.Asset();
            if (row != null)
            {

                if (row["OrderNo"] != null)
                {
                    model.OrderNo = int.Parse(row["OrderNo"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["BM"] != null)
                {
                    model.BM = row["BM"].ToString();
                }
                if (row["Type"] != null)
                {
                    model.Type = row["Type"].ToString();
                }
                if (row["AssetTM"] != null)
                {
                    model.AssetTM = row["AssetTM"].ToString();
                }
                if (row["AssetModel"] != null)
                {
                    model.AssetModel = row["AssetModel"].ToString();
                }
                if (row["LabelId"] != null)
                {
                    model.LabelId = row["LabelId"].ToString();
                }
                if (row["RfidId"] != null)
                {
                    model.RfidId = row["RfidId"].ToString();
                }
                if (row["State"] != null)
                {
                    model.State = row["State"].ToString();
                }
                if (row["UseUnit"] != null)
                {
                    model.UseUnit = row["UseUnit"].ToString();
                }
                if (row["Purpose"] != null)
                {
                    model.Purpose = row["Purpose"].ToString();
                }
                if (row["MaintenanceState"] != null)
                {
                    model.MaintenanceState = row["MaintenanceState"].ToString();
                }
                if (row["MaintenanceUnit"] != null)
                {
                    model.MaintenanceUnit = row["MaintenanceUnit"].ToString();
                }
                if (row["MaintenanceDeadline"] != null && row["MaintenanceDeadline"].ToString() != "")
                {
                    model.MaintenanceDeadline = DateTime.Parse(row["MaintenanceDeadline"].ToString());
                }
                if (row["CabinetNo"] != null && row["CabinetNo"].ToString() != "")
                {
                    model.CabinetNo = int.Parse(row["CabinetNo"].ToString());
                }
                if (row["StartLayer"] != null && row["StartLayer"].ToString() != "")
                {
                    model.StartLayer = int.Parse(row["StartLayer"].ToString());
                }
                if (row["UsedLayer"] != null && row["UsedLayer"].ToString() != "")
                {
                    model.UsedLayer = int.Parse(row["UsedLayer"].ToString());
                }
                if (row["UsedPower"] != null && row["UsedPower"].ToString() != "")
                {
                    model.UsedPower = decimal.Parse(row["UsedPower"].ToString());
                }
                if (row["UsedWeight"] != null && row["UsedWeight"].ToString() != "")
                {
                    model.UsedWeight = decimal.Parse(row["UsedWeight"].ToString());
                }
                if (row["UsedPowerPort"] != null && row["UsedPowerPort"].ToString() != "")
                {
                    model.UsedPowerPort = int.Parse(row["UsedPowerPort"].ToString());
                }
                if (row["UsedNetPort"] != null && row["UsedNetPort"].ToString() != "")
                {
                    model.UsedNetPort = int.Parse(row["UsedNetPort"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderNo,Name,BM,Type,AssetTM,AssetModel,LabelId,RfidId,State,UseUnit,Purpose,MaintenanceState,MaintenanceUnit,MaintenanceDeadline,CabinetNo,StartLayer,UsedLayer,UsedPower,UsedWeight,UsedPowerPort,UsedNetPort ");
            strSql.Append(" FROM Asset ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SQLiteHelper.ExecuteDataTable(strSql.ToString(), null);
        }
        #endregion  BasicMethod
        #region  ExtensionMethod
        public void Update(string sql)
        {
            SQLiteHelper.ExecuteNonQuery(sql, null);
        }
        #endregion  ExtensionMethod
    }
}

