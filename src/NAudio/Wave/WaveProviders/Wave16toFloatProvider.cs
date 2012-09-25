using System;
using NAudio.Wave.WaveFormats;
using NAudio.Wave.WaveOutputs;

namespace NAudio.Wave.WaveProviders
{
	/// <summary>
	/// Converts 16 bit PCM to IEEE float, optionally adjusting volume along the way
	/// </summary>
	public class Wave16ToFloatProvider : IWaveProvider
	{
		private readonly IWaveProvider sourceProvider;
		private readonly WaveFormat waveFormat;
		private byte[] sourceBuffer;
		private volatile float volume;

		/// <summary>
		/// Creates a new Wave16toFloatProvider
		/// </summary>
		/// <param name="sourceProvider">the source provider</param>
		public Wave16ToFloatProvider(IWaveProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
				throw new ApplicationException("Only PCM supported");
			if (sourceProvider.WaveFormat.BitsPerSample != 16)
				throw new ApplicationException("Only 16 bit audio supported");

			waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sourceProvider.WaveFormat.SampleRate,
			                                                  sourceProvider.WaveFormat.Channels);

			this.sourceProvider = sourceProvider;
			volume = 1.0f;
		}

		/// <summary>
		/// Volume of this channel. 1.0 = full scale
		/// </summary>
		public float Volume
		{
			get { return volume; }
			set { volume = value; }
		}

		#region IWaveProvider Members

		/// <summary>
		/// Reads bytes from this wave stream
		/// </summary>
		/// <param name="destBuffer">The destination buffer</param>
		/// <param name="offset">Offset into the destination buffer</param>
		/// <param name="numBytes">Number of bytes read</param>
		/// <returns>Number of bytes read.</returns>
		public int Read(byte[] destBuffer, int offset, int numBytes)
		{
			int sourceBytesRequired = numBytes/2;
			byte[] sourceBuffer = GetSourceBuffer(sourceBytesRequired);
			int sourceBytesRead = sourceProvider.Read(sourceBuffer, offset, sourceBytesRequired);
			var sourceWaveBuffer = new WaveBuffer(sourceBuffer);
			var destWaveBuffer = new WaveBuffer(destBuffer);

			int sourceSamples = sourceBytesRead/2;
			int destOffset = offset/4;
			for (int sample = 0; sample < sourceSamples; sample++)
			{
				destWaveBuffer.FloatBuffer[destOffset++] = (sourceWaveBuffer.ShortBuffer[sample]/32768f)*volume;
			}

			return sourceSamples*4;
		}

		/// <summary>
		/// <see cref="IWaveProvider.WaveFormat"/>
		/// </summary>
		public WaveFormat WaveFormat
		{
			get { return waveFormat; }
		}

		#endregion

		/// <summary>
		/// Helper function to avoid creating a new buffer every read
		/// </summary>
		private byte[] GetSourceBuffer(int bytesRequired)
		{
			if (sourceBuffer == null || sourceBuffer.Length < bytesRequired)
			{
				sourceBuffer = new byte[bytesRequired];
			}
			return sourceBuffer;
		}
	}
}