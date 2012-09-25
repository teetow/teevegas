using System;
using NAudio.Wave.WaveFormats;

namespace NAudio.FileFormats.Mp3
{
	/// <summary>
	/// Interface for MP3 frame by frame decoder
	/// </summary>
	public interface IMp3FrameDecompressor : IDisposable
	{
		/// <summary>
		/// PCM format that we are converting into
		/// </summary>
		WaveFormat OutputFormat { get; }

		/// <summary>
		/// Decompress a single MP3 frame
		/// </summary>
		/// <param name="frame">Frame to decompress</param>
		/// <param name="dest">Output buffer</param>
		/// <param name="destOffset">Offset within output buffer</param>
		/// <returns>Bytes written to output buffer</returns>
		int DecompressFrame(Mp3Frame frame, byte[] dest, int destOffset);
	}
}