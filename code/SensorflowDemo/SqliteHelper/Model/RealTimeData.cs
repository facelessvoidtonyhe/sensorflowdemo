using System;
namespace SqliteHelper.Model
{
	/// <summary>
	/// RealTimeData:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RealTimeData
	{
		public RealTimeData()
		{}
		#region Model
		private int _orderno;
		private int? _cabinetno;
		private int? _layerindex;
		private string _rfid;
		private DateTime? _updatetime;
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
		public int? LayerIndex
		{
			set{ _layerindex=value;}
			get{return _layerindex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RFID
		{
			set{ _rfid=value;}
			get{return _rfid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion Model

	}
}

