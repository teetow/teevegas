using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
	internal struct ASIOChannelInfo
	{
		public int channel; // on input, channel index
		public bool isInput; // on input
		public bool isActive; // on exit
		public int channelGroup; // dto
		[MarshalAs(UnmanagedType.U4)] public ASIOSampleType type; // dto
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public String name; // dto
	};
}