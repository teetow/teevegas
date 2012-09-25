using System;
using System.Runtime.InteropServices;

namespace NAudio.FileFormats.Ogg
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	internal class vorbis_info
	{
		public int version;
		public int channels;
		public int rate;
		public int bitrate_upper;
		public int bitrate_nominal;
		public int bitrate_lower;
		public int bitrate_window;
		public IntPtr codec_setup = IntPtr.Zero; // void *
	}
}