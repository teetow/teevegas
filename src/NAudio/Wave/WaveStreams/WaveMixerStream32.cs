using System;
using System.Collections.Generic;
using System.Diagnostics;
using NAudio.Wave.WaveFormats;

namespace NAudio.Wave.WaveStreams
{
	/// <summary>
	/// WaveStream that can mix together multiple 32 bit input streams
	/// (Normally used with stereo input channels)
	/// All channels must have the same number of inputs
	/// </summary>
	public class WaveMixerStream32 : WaveStream
	{
		private readonly int bytesPerSample;
		private bool autoStop;
		private List<WaveStream> inputStreams;
		private long length;
		private long position;
		private WaveFormat waveFormat;

		/// <summary>
		/// Creates a new 32 bit WaveMixerStream
		/// </summary>
		public WaveMixerStream32()
		{
			autoStop = true;
			waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
			bytesPerSample = 4;
			inputStreams = new List<WaveStream>();
		}


		/// <summary>
		/// Creates a new 32 bit WaveMixerStream
		/// </summary>
		/// <param name="inputStreams">An Array of WaveStreams - must all have the same format.
		/// Use WaveChannel is designed for this purpose.</param>
		/// <param name="autoStop">Automatically stop when all inputs have been read</param>
		/// <exception cref="ArgumentException">Thrown if the input streams are not 32 bit floating point,
		/// or if they have different formats to each other</exception>
		public WaveMixerStream32(IEnumerable<WaveStream> inputStreams, bool autoStop)
			: this()
		{
			this.autoStop = autoStop;

			foreach (WaveStream inputStream in inputStreams)
			{
				AddInputStream(inputStream);
			}
		}

		/// <summary>
		/// The number of inputs to this mixer
		/// </summary>
		public int InputCount
		{
			get { return inputStreams.Count; }
		}

		/// <summary>
		/// Automatically stop when all inputs have been read
		/// </summary>
		public bool AutoStop
		{
			get { return autoStop; }
			set { autoStop = value; }
		}

		/// <summary>
		/// <see cref="WaveStream.BlockAlign"/>
		/// </summary>
		public override int BlockAlign
		{
			get { return waveFormat.BlockAlign; // inputStreams[0].BlockAlign;
			}
		}

		/// <summary>
		/// Length of this Wave Stream (in bytes)
		/// <see cref="System.IO.Stream.Length"/>
		/// </summary>
		public override long Length
		{
			get { return length; }
		}

		/// <summary>
		/// Position within this Wave Stream (in bytes)
		/// <see cref="System.IO.Stream.Position"/>
		/// </summary>
		public override long Position
		{
			get
			{
				// all streams are at the same position
				return position;
			}
			set
			{
				lock (this)
				{
					value = Math.Min(value, Length);
					foreach (WaveStream inputStream in inputStreams)
					{
						inputStream.Position = Math.Min(value, inputStream.Length);
					}
					position = value;
				}
			}
		}

		/// <summary>
		/// <see cref="WaveStream.WaveFormat"/>
		/// </summary>
		public override WaveFormat WaveFormat
		{
			get { return waveFormat; }
		}

		/// <summary>
		/// Add a new input to the mixer
		/// </summary>
		/// <param name="waveStream">The wave input to add</param>
		public void AddInputStream(WaveStream waveStream)
		{
			if (waveStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
				throw new ArgumentException("Must be IEEE floating point", "waveStream.WaveFormat");
			if (waveStream.WaveFormat.BitsPerSample != 32)
				throw new ArgumentException("Only 32 bit audio currently supported", "waveStream.WaveFormat");

			if (inputStreams.Count == 0)
			{
				// first one - set the format
				int sampleRate = waveStream.WaveFormat.SampleRate;
				int channels = waveStream.WaveFormat.Channels;
				waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
			}
			else
			{
				if (!waveStream.WaveFormat.Equals(waveFormat))
					throw new ArgumentException("All incoming channels must have the same format", "inputStreams.WaveFormat");
			}

			lock (this)
			{
				inputStreams.Add(waveStream);
				length = Math.Max(length, waveStream.Length);
				// get to the right point in this input file
				Position = Position;
			}
		}

		/// <summary>
		/// Remove a WaveStream from the mixer
		/// </summary>
		/// <param name="waveStream">waveStream to remove</param>
		public void RemoveInputStream(WaveStream waveStream)
		{
			lock (this)
			{
				if (inputStreams.Remove(waveStream))
				{
					// recalculate the length
					length = 0;
					foreach (WaveStream inputStream in inputStreams)
					{
						length = Math.Max(length, waveStream.Length);
					}
				}
			}
		}

		/// <summary>
		/// Reads bytes from this wave stream
		/// </summary>
		/// <param name="buffer">buffer to read into</param>
		/// <param name="offset">offset into buffer</param>
		/// <param name="count">number of bytes required</param>
		/// <returns>Number of bytes read.</returns>
		/// <exception cref="ArgumentException">Thrown if an invalid number of bytes requested</exception>
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (autoStop)
			{
				if (position + count > length)
					count = (int) (length - position);

				// was a bug here, should be fixed now
				Debug.Assert(count >= 0, "length and position mismatch");
			}


			if (count%bytesPerSample != 0)
				throw new ArgumentException("Must read an whole number of samples", "count");

			// blank the buffer
			Array.Clear(buffer, offset, count);
			int bytesRead = 0;

			// sum the channels in
			var readBuffer = new byte[count];
			foreach (WaveStream inputStream in inputStreams)
			{
				if (inputStream.HasData(count))
				{
					int readFromThisStream = inputStream.Read(readBuffer, 0, count);
					// don't worry if input stream returns less than we requested - may indicate we have got to the end
					bytesRead = Math.Max(bytesRead, readFromThisStream);
					if (readFromThisStream > 0)
						Sum32BitAudio(buffer, offset, readBuffer, readFromThisStream);
				}
				else
				{
					bytesRead = Math.Max(bytesRead, count);
					inputStream.Position += count;
				}
			}
			position += count;
			return count;
		}

		/// <summary>
		/// Actually performs the mixing
		/// </summary>
		private static unsafe void Sum32BitAudio(byte[] destBuffer, int offset, byte[] sourceBuffer, int bytesRead)
		{
			fixed (byte* pDestBuffer = &destBuffer[offset],
				pSourceBuffer = &sourceBuffer[0])
			{
				var pfDestBuffer = (float*) pDestBuffer;
				var pfReadBuffer = (float*) pSourceBuffer;
				int samplesRead = bytesRead/4;
				for (int n = 0; n < samplesRead; n++)
				{
					pfDestBuffer[n] += pfReadBuffer[n];
				}
			}
		}

		/// <summary>
		/// Disposes this WaveStream
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (inputStreams != null)
				{
					foreach (WaveStream inputStream in inputStreams)
					{
						inputStream.Dispose();
					}
					inputStreams = null;
				}
			}
			else
			{
				Debug.Assert(false, "WaveMixerStream32 was not disposed");
			}
			base.Dispose(disposing);
		}
	}
}