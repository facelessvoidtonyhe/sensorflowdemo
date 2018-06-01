using System;
namespace SqliteHelper.Model
{
	/// <summary>
	/// Asset:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Asset
	{
		public Asset()
		{}
		#region Model
		private int    _orderno;
		private string _name;
		private string _bm;
		private string _type;
		private string _assettm;
		private string _assetmodel;
		private string _labelid;
		private string _rfidid;
		private string _state;
		private string _useunit;
		private string _purpose;
		private string _maintenancestate;
		private string _maintenanceunit;
		private DateTime? _maintenancedeadline;
		private int? _cabinetno;
		private int? _startlayer;
		private int? _usedlayer;
		private decimal? _usedpower;
		private decimal? _usedweight;
		private int? _usedpowerport;
		private int? _usednetport;
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BM
		{
			set{ _bm=value;}
			get{return _bm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AssetTM
		{
			set{ _assettm=value;}
			get{return _assettm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AssetModel
		{
			set{ _assetmodel=value;}
			get{return _assetmodel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LabelId
		{
			set{ _labelid=value;}
			get{return _labelid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RfidId
		{
			set{ _rfidid=value;}
			get{return _rfidid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseUnit
		{
			set{ _useunit=value;}
			get{return _useunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Purpose
		{
			set{ _purpose=value;}
			get{return _purpose;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MaintenanceState
		{
			set{ _maintenancestate=value;}
			get{return _maintenancestate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MaintenanceUnit
		{
			set{ _maintenanceunit=value;}
			get{return _maintenanceunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? MaintenanceDeadline
		{
			set{ _maintenancedeadline=value;}
			get{return _maintenancedeadline;}
		}

        public string MaintenanceDeadlineStr
        {
            get { return _maintenancedeadline.Value.ToString("yyyy-MM-dd HH:mm"); }
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
		public int? StartLayer
		{
			set{ _startlayer=value;}
			get{return _startlayer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UsedLayer
		{
			set{ _usedlayer=value;}
			get{return _usedlayer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? UsedPower
		{
			set{ _usedpower=value;}
			get{return _usedpower;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? UsedWeight
		{
			set{ _usedweight=value;}
			get{return _usedweight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UsedPowerPort
		{
			set{ _usedpowerport=value;}
			get{return _usedpowerport;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UsedNetPort
		{
			set{ _usednetport=value;}
			get{return _usednetport;}
		}

        /// <summary>
        /// 预占、正常、非法在架、丢失
        /// </summary>
        public string CabinetState { set; get; }
		#endregion Model

	}
}

