using System;

namespace NAudio.Wave.WaveStreams
{
	/// <summary>
	/// An interface for WaveStreams which can report notification of individual samples
	/// </summary>
	public interface ISampleNotifier
	{
		/// <summary>
		/// About to start processing a block of samples
		/// </summary>
		event EventHandler Block;

		/// <summary>
		/// A sample has been detected
		/// </summary>
		event EventHandler<SampleEventArgs> Sample;
	}
}