using System;
using System.Drawing;

namespace NAudio.Gui.TrackView
{
	/// <summary>
	/// A trackview clip
	/// </summary>
	public class Clip
	{
		private Color backColor = Color.PowderBlue;
		private TimeSpan duration;
		private Color foreColor = Color.Black;
		private TimeSpan startTime;

		/// <summary>
		/// Creates a new trackview clip
		/// </summary>
		public Clip(string name, TimeSpan startTime, TimeSpan duration)
		{
			Name = name;
			this.startTime = startTime;
			this.duration = duration;
		}

		/// <summary>
		/// Clip Name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Foreground Colour
		/// </summary>
		public Color ForeColor
		{
			get { return foreColor; }
			set { foreColor = value; }
		}

		/// <summary>
		/// Background Colour
		/// </summary>
		public Color BackColor
		{
			get { return backColor; }
			set { backColor = value; }
		}

		/// <summary>
		/// Start Time
		/// </summary>
		public TimeSpan StartTime
		{
			get { return startTime; }
			set { startTime = value; }
		}

		/// <summary>
		/// Duration
		/// </summary>
		public TimeSpan Duration
		{
			get { return duration; }
			set { duration = value; }
		}

		/// <summary>
		/// End Time
		/// </summary>
		public TimeSpan EndTime
		{
			get { return startTime + duration; }
		}
	}
}