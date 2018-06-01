using System;
using System.Data;
using System.Collections.Generic;
using SqliteHelper.Model;
namespace SqliteHelper.BLL
{
    /// <summary>
    /// Asset
    /// </summary>
    public partial class Asset
    {
        private readonly SqliteHelper.DAL.Asset dal;
        public Asset(string dbPath)
        {
            dal = new DAL.Asset(dbPath);
        }
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SqliteHelper.Model.Asset model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SqliteHelper.Model.Asset model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int OrderNo)
        {
            return dal.Delete(OrderNo);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string OrderNolist)
        {
            return dal.DeleteList(OrderNolist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SqliteHelper.Model.Asset GetModel(int OrderNo)
        {
            return dal.GetModel(OrderNo);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SqliteHelper.Model.Asset> GetModelList(string strWhere)
        {
            DataTable dt = dal.GetList(strWhere);
            return DataTableToList(dt);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SqliteHelper.Model.Asset> DataTableToList(DataTable dt)
        {
            List<SqliteHelper.Model.Asset> modelList = new List<SqliteHelper.Model.Asset>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SqliteHelper.Model.Asset model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetAllList()
        {
            return GetList("");
        }
        #endregion  BasicMethod
        #region  ExtensionMethod
        public void Update(string sql)
        {
            dal.Update(sql);
        }
        #endregion  ExtensionMethod
    }
}

