using System.Runtime.InteropServices;

namespace NAudio.Wave.WaveFormats
{
	/// <summary>
	/// The WMA wave format. 
	/// May not be much use because WMA codec is a DirectShow DMO not an ACM
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 2)]
	internal class WmaWaveFormat : WaveFormat
	{
		private short wValidBitsPerSample; // bits of precision 
		private int dwChannelMask; // which channels are present in stream 
		private int dwReserved1;
		private int dwReserved2;
		private short wEncodeOptions;
		private short wReserved3;

		public WmaWaveFormat(int sampleRate, int bitsPerSample, int channels)
			: base(sampleRate, bitsPerSample, channels)
		{
			wValidBitsPerSample = (short) bitsPerSample;
			if (channels == 1)
				dwChannelMask = 1;
			else if (channels == 2)
				dwChannelMask = 3;

			// WMAUDIO3 is Pro
			waveFormatTag = WaveFormatEncoding.WAVE_FORMAT_WMAUDIO2;
		}
	}
}