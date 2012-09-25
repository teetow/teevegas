using System;

namespace NAudio.Gui.TrackView
{
	/// <summary>
	/// TimeLine Position Clicked event arguments
	/// </summary>
	public class TimeLinePositionClickedEventArgs : EventArgs
	{
		private readonly TimeSpan position;

		/// <summary>
		/// Creates a new TimeLinePositionClickedEventArgs
		/// </summary>
		public TimeLinePositionClickedEventArgs(TimeSpan position)
		{
			this.position = position;
		}

		/// <summary>
		/// The position clicked
		/// </summary>
		public TimeSpan Position
		{
			get { return position; }
		}
	}
}