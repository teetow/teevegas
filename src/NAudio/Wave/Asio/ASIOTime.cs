using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
	internal struct ASIOTime
	{
		// both input/output
		public int reserved1;
		public int reserved2;
		public int reserved3;
		public int reserved4;
		public AsioTimeInfo timeInfo; // required
		public ASIOTimeCode timeCode; // optional, evaluated if (timeCode.flags & kTcValid)
	}
}