using System.IO;
using NAudio.Wave.WaveFormats;

namespace NAudio.Wave.WaveStreams
{
	/// <summary>
	/// WaveStream that simply passes on data from its source stream
	/// (e.g. a MemoryStream)
	/// </summary>
	public class RawSourceWaveStream : WaveStream
	{
		private readonly Stream sourceStream;
		private readonly WaveFormat waveFormat;

		/// <summary>
		/// Initialises a new instance of RawSourceWaveStream
		/// </summary>
		/// <param name="sourceStream">The source stream containing raw audio</param>
		/// <param name="waveFormat">The waveformat of the audio in the source stream</param>
		public RawSourceWaveStream(Stream sourceStream, WaveFormat waveFormat)
		{
			this.sourceStream = sourceStream;
			this.waveFormat = waveFormat;
		}

		/// <summary>
		/// The WaveFormat of this stream
		/// </summary>
		public override WaveFormat WaveFormat
		{
			get { return waveFormat; }
		}

		/// <summary>
		/// The length in bytes of this stream (if supported)
		/// </summary>
		public override long Length
		{
			get { return sourceStream.Length; }
		}

		/// <summary>
		/// The current position in this stream
		/// </summary>
		public override long Position
		{
			get { return sourceStream.Position; }
			set { sourceStream.Position = value; }
		}

		/// <summary>
		/// Reads data from the stream
		/// </summary>
		public override int Read(byte[] buffer, int offset, int count)
		{
			return sourceStream.Read(buffer, offset, count);
		}
	}
}