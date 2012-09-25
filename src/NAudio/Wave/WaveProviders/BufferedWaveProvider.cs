using System;
using System.Collections.Generic;
using NAudio.Wave.WaveFormats;
using NAudio.Wave.WaveOutputs;

namespace NAudio.Wave.WaveProviders
{
	/// <summary>
	/// Provides a buffered store of samples
	/// Read method will return queued samples or fill buffer with zeroes
	/// based on code from trentdevers (http://naudio.codeplex.com/Thread/View.aspx?ThreadId=54133)
	/// </summary>
	public class BufferedWaveProvider : IWaveProvider
	{
		private readonly Queue<AudioBuffer> queue;
		private readonly WaveFormat waveFormat;

		/// <summary>
		/// Creates a new buffered WaveProvider
		/// </summary>
		/// <param name="waveFormat">WaveFormat</param>
		public BufferedWaveProvider(WaveFormat waveFormat)
		{
			this.waveFormat = waveFormat;
			queue = new Queue<AudioBuffer>();
			MaxQueuedBuffers = 100;
		}

		/// <summary>
		/// Maximum number of queued buffers
		/// </summary>
		public int MaxQueuedBuffers { get; set; }

		#region IWaveProvider Members

		/// <summary>
		/// Gets the WaveFormat
		/// </summary>
		public WaveFormat WaveFormat
		{
			get { return waveFormat; }
		}

		/// <summary>
		/// Reads from this WaveProvider
		/// Will always return count bytes, since we will zero-fill the buffer if not enough available
		/// </summary>
		public int Read(byte[] buffer, int offset, int count)
		{
			int read = 0;
			while (read < count)
			{
				int required = count - read;
				AudioBuffer audioBuffer = null;
				lock (queue)
				{
					if (queue.Count > 0)
					{
						audioBuffer = queue.Peek();
					}
				}

				if (audioBuffer == null)
				{
					// Return a zero filled buffer
					for (int n = 0; n < required; n++)
						buffer[offset + n] = 0;
					read += required;
				}
				else
				{
					int nread = audioBuffer.Buffer.Length - audioBuffer.Position;

					// If this buffer must be read in it's entirety
					if (nread <= required)
					{
						// Read entire buffer
						Buffer.BlockCopy(audioBuffer.Buffer, audioBuffer.Position, buffer, offset + read, nread);
						read += nread;

						lock (queue)
						{
							queue.Dequeue();
						}
					}
					else // the number of bytes that can be read is greater than that required
					{
						Buffer.BlockCopy(audioBuffer.Buffer, audioBuffer.Position, buffer, offset + read, required);
						audioBuffer.Position += required;
						read += required;
					}
				}
			}
			return read;
		}

		#endregion

		/// <summary>
		/// Adds samples. Takes a copy of buffer, so that buffer can be reused if necessary
		/// </summary>
		public void AddSamples(byte[] buffer, int offset, int count)
		{
			var nbuffer = new byte[count];
			Buffer.BlockCopy(buffer, offset, nbuffer, 0, count);
			lock (queue)
			{
				if (queue.Count >= MaxQueuedBuffers)
				{
					throw new InvalidOperationException("Too many queued buffers");
				}
				queue.Enqueue(new AudioBuffer(nbuffer));
			}
		}

		#region Nested type: AudioBuffer

		/// <summary>
		/// Internal helper class for a stored buffer
		/// </summary>
		private class AudioBuffer
		{
			/// <summary>
			/// Constructs a new AudioBuffer
			/// </summary>
			public AudioBuffer(byte[] buffer)
			{
				Buffer = buffer;
			}

			/// <summary>
			/// Gets the Buffer
			/// </summary>
			public byte[] Buffer { get; private set; }

			/// <summary>
			/// Gets or sets the position within the buffer we have read up to so far
			/// </summary>
			public int Position { get; set; }
		}

		#endregion
	}
}