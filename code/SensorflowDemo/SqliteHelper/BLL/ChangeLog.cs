using System;
using System.Data;
using System.Collections.Generic;
using SqliteHelper.Model;
namespace SqliteHelper.BLL
{
	/// <summary>
	/// ChangeLog
	/// </summary>
	public partial class ChangeLog
	{
        private readonly SqliteHelper.DAL.ChangeLog dal;
        public ChangeLog(string dbPath)
        {
            dal = new DAL.ChangeLog(dbPath);
        }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SqliteHelper.Model.ChangeLog model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SqliteHelper.Model.ChangeLog model)
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
		public bool DeleteList(string OrderNolist )
		{
			return dal.DeleteList(OrderNolist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SqliteHelper.Model.ChangeLog GetModel(int OrderNo)
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
		public List<SqliteHelper.Model.ChangeLog> GetModelList(string strWhere)
		{
            DataTable dt = dal.GetList(strWhere);
			return DataTableToList(dt);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<SqliteHelper.Model.ChangeLog> DataTableToList(DataTable dt)
		{
			List<SqliteHelper.Model.ChangeLog> modelList = new List<SqliteHelper.Model.ChangeLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				SqliteHelper.Model.ChangeLog model;
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

		#endregion  ExtensionMethod
	}
}

