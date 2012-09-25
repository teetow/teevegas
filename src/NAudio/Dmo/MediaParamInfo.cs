using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	/// <summary>
	/// MP_PARAMINFO
	/// </summary>
	internal struct MediaParamInfo
	{
		public MediaParamCurveType mopCaps;
		public MediaParamType mpType;
		public float mpdMaxValue;
		public float mpdMinValue; // MP_DATA is a float
		public float mpdNeutralValue;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string szLabel;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string szUnitText;
	}
}