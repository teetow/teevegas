using System;

namespace NAudio.Gui.TrackView
{
	/// <summary>
	/// Audio Clip
	/// </summary>
	public class AudioClip : Clip
	{
		/// <summary>
		/// Creates a new Audio Clip
		/// </summary>
		public AudioClip(string name, TimeSpan startTime, TimeSpan duration)
			: base(name, startTime, duration)
		{
		}

		/// <summary>
		/// Source File Name
		/// </summary>
		public string SourceFileName { get; set; }
	}
}