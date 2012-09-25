using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
	internal struct AsioTimeInfo
	{
		public double speed; // absolute speed (1. = nominal)
		public ASIO64Bit systemTime; // system time related to samplePosition, in nanoseconds
		public ASIO64Bit samplePosition;
		public double sampleRate; // current rate
		public AsioTimeInfoFlags flags; // (see below)
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)] public string reserved;
	}
}