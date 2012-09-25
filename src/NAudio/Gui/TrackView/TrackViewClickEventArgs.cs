using System;
using System.Drawing;

namespace NAudio.Gui.TrackView
{
	/// <summary>
	/// Event for clicking the track view
	/// </summary>
	public class TrackViewClickEventArgs : EventArgs
	{
		private readonly Clip clip;
		private readonly Point location;
		private readonly TimeSpan time;
		private readonly Track track;

		/// <summary>
		/// Creates a new trackview clicked event
		/// </summary>
		/// <param name="time">Time of the cusor</param>
		/// <param name="track">Track clicked on</param>
		/// <param name="clip">Any clip under the cursor</param>
		/// <param name="location">Mouse location</param>
		public TrackViewClickEventArgs(TimeSpan time, Track track, Clip clip, Point location)
		{
			this.time = time;
			this.track = track;
			this.clip = clip;
			this.location = location;
		}

		/// <summary>
		/// Time clicked on
		/// </summary>
		public TimeSpan Time
		{
			get { return time; }
		}

		/// <summary>
		/// Track clicked on
		/// </summary>
		public Track Track
		{
			get { return track; }
		}

		/// <summary>
		/// Clip clicked on
		/// </summary>
		public Clip Clip
		{
			get { return clip; }
		}

		/// <summary>
		/// Mouse location
		/// </summary>
		public Point Location
		{
			get { return location; }
		}
	}
}