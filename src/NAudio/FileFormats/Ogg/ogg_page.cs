using System;
using System.Runtime.InteropServices;

namespace NAudio.FileFormats.Ogg
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	internal struct ogg_page
	{
		public IntPtr header;
		public int header_len;
		public IntPtr body;
		public int body_len;
	}
}