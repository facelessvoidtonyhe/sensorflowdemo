using System;
namespace SqliteHelper.Model
{
	/// <summary>
	/// ChangeLog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ChangeLog
	{
		public ChangeLog()
		{}
		#region Model
		private int _orderno;
		private int? _cabinetno;
		private int? _assetno;
		private DateTime? _createtime;
		private string _operationtype;
		private string _operationdetail;
		/// <summary>
		/// 
		/// </summary>
		public int OrderNo
		{
			set{ _orderno=value;}
			get{return _orderno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CabinetNo
		{
			set{ _cabinetno=value;}
			get{return _cabinetno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AssetNo
		{
			set{ _assetno=value;}
			get{return _assetno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperationType
		{
			set{ _operationtype=value;}
			get{return _operationtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperationDetail
		{
			set{ _operationdetail=value;}
			get{return _operationdetail;}
		}
		#endregion Model

	}
}

