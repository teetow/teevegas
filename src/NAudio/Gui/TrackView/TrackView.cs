using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NAudio.Gui.TrackView
{
	/// <summary>
	/// TrackView is a control similar to those found in the main view of
	/// DAWs, showing time in the X axis and tracks / channels in the Y
	/// It is currently in the preliminary stages of development
	/// </summary>
	public partial class TrackView : UserControl
	{
		private readonly List<Track> tracks = new List<Track>();
		private TimeSpan nowTime;
		private double pixelsPerSecond = 80;

		/// <summary>
		/// Create a new trackview control
		/// </summary>
		public TrackView()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			DoubleBuffered = true;
			InitializeComponent();
		}

		/// <summary>
		/// The tracks displayed by this control
		/// </summary>
		public List<Track> Tracks
		{
			get { return tracks; }
		}

		/// <summary>
		/// The location of the current time cursor
		/// </summary>
		public TimeSpan NowTime
		{
			get { return nowTime; }
			set
			{
				nowTime = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Pixels per second.
		/// Effectively a zoom level
		/// </summary>
		public double PixelsPerSecond
		{
			get { return pixelsPerSecond; }
			set
			{
				pixelsPerSecond = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Right-click detected
		/// </summary>
		public event EventHandler<TrackViewClickEventArgs> RightClick;

		/// <summary>
		/// <see cref="Control.OnPaint" />
		/// </summary>
		protected override void OnPaint(PaintEventArgs e)
		{
			//int y = 0;
			foreach (Track track in tracks)
			{
				PaintTrack(track, e.Graphics);
				e.Graphics.TranslateTransform(0, track.Height);
			}
			e.Graphics.ResetTransform();
			int x = TimeToX(nowTime);
			e.Graphics.DrawLine(Pens.Red, x, 0, x, Height);

			base.OnPaint(e);
		}

		private int TimeToX(TimeSpan t)
		{
			return (int) (pixelsPerSecond*t.TotalSeconds);
		}

		private TimeSpan XToTime(int x)
		{
			return TimeSpan.FromSeconds(x/pixelsPerSecond);
		}

		private void PaintTrack(Track track, Graphics g)
		{
			foreach (Clip clip in track.Clips)
			{
				int clipX = TimeToX(clip.StartTime);
				g.TranslateTransform(clipX, 0);
				PaintClip(clip, track.Height, g);
				g.TranslateTransform(-clipX, 0);
			}
		}

		private void PaintClip(Clip clip, int trackHeight, Graphics g)
		{
			Brush clipForegroundBrush = new SolidBrush(clip.ForeColor);
			var clipForegroundPen = new Pen(clip.ForeColor);
			var clipRectangle = new Rectangle(0, 0, TimeToX(clip.Duration), trackHeight);
			g.FillRectangle(new SolidBrush(clip.BackColor), clipRectangle);
			g.DrawRectangle(clipForegroundPen, clipRectangle);
			g.DrawString(clip.Name, Font, clipForegroundBrush, new PointF(1, 1));
		}

		/// <summary>
		/// Mouse click event handler
		/// </summary>
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			if (e.Button == MouseButtons.Right)
			{
				TimeSpan mouseTime = XToTime(e.X);
				Track track = TrackAtY(e.Y);
				Clip clip = null;
				if (track != null)
				{
					clip = track.ClipAtTime(mouseTime);
				}
				if (RightClick != null)
				{
					RightClick(this, new TrackViewClickEventArgs(mouseTime, track, clip, e.Location));
				}
			}
		}

		private Track TrackAtY(int y)
		{
			int trackPos = 0;
			foreach (Track track in tracks)
			{
				trackPos += track.Height;
				if (y < trackPos)
					return track;
			}

			return null;
		}
	}
}