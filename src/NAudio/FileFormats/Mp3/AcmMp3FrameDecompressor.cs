using System;
using NAudio.Wave.Compression;
using NAudio.Wave.WaveFormats;

namespace NAudio.FileFormats.Mp3
{
	internal class AcmMp3FrameDecompressor : IDisposable, IMp3FrameDecompressor
	{
		private readonly WaveFormat pcmFormat;
		private AcmStream conversionStream;

		public AcmMp3FrameDecompressor(WaveFormat sourceFormat)
		{
			pcmFormat = AcmStream.SuggestPcmFormat(sourceFormat);
			conversionStream = new AcmStream(sourceFormat, pcmFormat);
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (conversionStream != null)
			{
				conversionStream.Dispose();
				conversionStream = null;
			}
		}

		#endregion

		#region IMp3FrameDecompressor Members

		public WaveFormat OutputFormat
		{
			get { return pcmFormat; }
		}

		public int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset)
		{
			Array.Copy(frame.RawData, conversionStream.SourceBuffer, frame.FrameLength);
			int sourceBytesConverted = 0;
			int converted = conversionStream.Convert(frame.FrameLength, out sourceBytesConverted);
			if (sourceBytesConverted != frame.FrameLength)
			{
				throw new InvalidOperationException("Couldn't convert the whole MP3 frame");
			}
			Array.Copy(conversionStream.DestBuffer, 0, dest, destOffset, converted);
			return converted;
		}

		#endregion
	}
}