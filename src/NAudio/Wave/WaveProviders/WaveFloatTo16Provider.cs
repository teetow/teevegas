using System;
using NAudio.Wave.WaveFormats;
using NAudio.Wave.WaveOutputs;

namespace NAudio.Wave.WaveProviders
{
	/// <summary>
	/// Converts IEEE float to 16 bit PCM, optionally clipping and adjusting volume along the way
	/// </summary>
	public class WaveFloatTo16Provider : IWaveProvider
	{
		private readonly IWaveProvider sourceProvider;
		private readonly WaveFormat waveFormat;
		private byte[] sourceBuffer;
		private volatile float volume;

		/// <summary>
		/// Creates a new WaveFloatTo16Provider
		/// </summary>
		/// <param name="sourceProvider">the source provider</param>
		public WaveFloatTo16Provider(IWaveProvider sourceProvider)
		{
			if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
				throw new ApplicationException("Only PCM supported");
			if (sourceProvider.WaveFormat.BitsPerSample != 32)
				throw new ApplicationException("Only 32 bit audio supported");

			waveFormat = new WaveFormat(sourceProvider.WaveFormat.SampleRate, 16, sourceProvider.WaveFormat.Channels);

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
			int sourceBytesRequired = numBytes*2;
			byte[] sourceBuffer = GetSourceBuffer(sourceBytesRequired);
			int sourceBytesRead = sourceProvider.Read(sourceBuffer, offset, sourceBytesRequired);
			var sourceWaveBuffer = new WaveBuffer(sourceBuffer);
			var destWaveBuffer = new WaveBuffer(destBuffer);

			int sourceSamples = sourceBytesRead/4;
			int destOffset = offset/2;
			for (int sample = 0; sample < sourceSamples; sample++)
			{
				// adjust volume
				float sample32 = sourceWaveBuffer.FloatBuffer[sample]*volume;
				// clip
				if (sample32 > 1.0f)
					sample32 = 1.0f;
				if (sample32 < -1.0f)
					sample32 = -1.0f;
				destWaveBuffer.ShortBuffer[destOffset++] = (short) (sample32*32767);
			}

			return sourceSamples*2;
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