using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
	internal struct ASIOTimeCode
	{
		public double speed; // speed relation (fraction of nominal speed)
		// ASIOSamples     timeCodeSamples;        // time in samples
		public ASIO64Bit timeCodeSamples; // time in samples
		public ASIOTimeCodeFlags flags; // some information flags (see below)
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)] public string future;
	}
}