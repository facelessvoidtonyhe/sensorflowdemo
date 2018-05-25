using System;
namespace SqliteHelper.Model
{
	/// <summary>
	/// Cabinet:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Cabinet
	{
		public Cabinet()
		{}
		#region Model
		private int _orderno;
		private string _name;
		private string _type;
		private string _location;
		private string _serialnumber;
		private string _user;
		private string _coldtemp1;
		private string _coldtemp2;
		private string _coldtemp3;
		private string _hottemp1;
		private string _hottemp2;
		private string _hottemp3;
		private int? _space;
		private decimal? _power;
		private decimal? _weight;
		private int? _powerport;
		private int? _netport;
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
		public string Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Location
		{
			set{ _location=value;}
			get{return _location;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SerialNumber
		{
			set{ _serialnumber=value;}
			get{return _serialnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string User
		{
			set{ _user=value;}
			get{return _user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ColdTemp1
		{
			set{ _coldtemp1=value;}
			get{return _coldtemp1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ColdTemp2
		{
			set{ _coldtemp2=value;}
			get{return _coldtemp2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ColdTemp3
		{
			set{ _coldtemp3=value;}
			get{return _coldtemp3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HotTemp1
		{
			set{ _hottemp1=value;}
			get{return _hottemp1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HotTemp2
		{
			set{ _hottemp2=value;}
			get{return _hottemp2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HotTemp3
		{
			set{ _hottemp3=value;}
			get{return _hottemp3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Space
		{
			set{ _space=value;}
			get{return _space;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Power
		{
			set{ _power=value;}
			get{return _power;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Weight
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PowerPort
		{
			set{ _powerport=value;}
			get{return _powerport;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? NetPort
		{
			set{ _netport=value;}
			get{return _netport;}
		}
		#endregion Model

	}
}

