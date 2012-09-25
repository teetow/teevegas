using System;
using System.Collections.Generic;

namespace NAudio.Gui.TrackView
{
	/// <summary>
	/// Holds details of a track for the trackvew
	/// </summary>
	public class Track
	{
		private readonly List<Clip> clips;

		/// <summary>
		/// Creates a new track
		/// </summary>
		public Track(string name)
		{
			Name = name;
			Height = 30;
			Volume = 1.0f;
			Pan = 0.0f;
			clips = new List<Clip>();
		}

		/// <summary>
		/// Track name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Track height
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// Volume
		/// </summary>
		public float Volume { get; set; }

		/// <summary>
		/// Pan
		/// </summary>
		public float Pan { get; set; }

		/// <summary>
		/// Clips contained in this track
		/// </summary>
		public List<Clip> Clips
		{
			get { return clips; }
		}

		/// <summary>
		/// Finds the clip at a specified time
		/// </summary>
		public Clip ClipAtTime(TimeSpan time)
		{
			foreach (Clip clip in clips)
			{
				if (clip.StartTime >= time && time < clip.EndTime)
				{
					return clip;
				}
			}
			return null;
		}
	}
}