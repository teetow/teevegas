using System;
using System.Runtime.InteropServices;

namespace NAudio.FileFormats.Ogg
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	internal class vorbis_dsp_state
	{
		public int analysisp;
		public IntPtr vi = IntPtr.Zero; // vorbis_info *vi;

		public IntPtr pcm = IntPtr.Zero; // float **pcm;
		public IntPtr pcmret = IntPtr.Zero; //float **pcmret;
		public int pcm_storage;
		public int pcm_current;
		public int pcm_returned;

		public int preextrapolate;
		public int eofflag;

		public int lW;
		public int W;
		public int nW;
		public int centerW;

		public long granulepos;
		public long sequence;

		public long glue_bits;
		public long time_bits;
		public long floor_bits;
		public long res_bits;

		public IntPtr backend_state = IntPtr.Zero;
	}
}