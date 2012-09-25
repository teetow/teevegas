using System;

namespace NAudio.Wave.WaveStreams
{
	/// <summary>
	/// Sample event arguments
	/// </summary>
	public class SampleEventArgs : EventArgs
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public SampleEventArgs(float left, float right)
		{
			Left = left;
			Right = right;
		}

		/// <summary>
		/// Left sample
		/// </summary>
		public float Left { get; set; }

		/// <summary>
		/// Right sample
		/// </summary>
		public float Right { get; set; }
	}
}