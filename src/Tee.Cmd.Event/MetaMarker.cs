using System;
using ScriptPortal.Vegas;

namespace Tee.Cmd.Event
{
	/// <summary>
	/// A MetaMarker represents a marker in the context of a media file.
	/// </summary>
	public class MetaMarker
	{
		private readonly TrackEvent _parentEvent;
		private readonly Marker _marker;

		public MetaMarker(Marker Marker, TrackEvent ParentEvent)
		{
			_marker = Marker;
			_parentEvent = ParentEvent;
		}

		public Marker Marker
		{
			get { return _marker; }
		}

		/// <summary>
		/// Returns how far into the event the marker is, expressed in local time
		/// </summary>
		public Timecode LocalMarkerOffset
		{
			get
			{
				return
					Timecode.FromNanos(_marker.Position.Nanos - (long)(_parentEvent.ActiveTake.Offset.Nanos * _parentEvent.PlaybackRate));
			}
		}

		/// <summary>
		/// Returns how far into the event the marker is, expressed in global time (pitch-adjusted)
		/// </summary>
		public Timecode GlobalMarkerOffset
		{
			get
			{
				return
					Timecode.FromNanos((long)(_marker.Position.Nanos / _parentEvent.PlaybackRate) - _parentEvent.ActiveTake.Offset.Nanos);
			}
		}

		/// <summary>
		/// Represents the marker position relative to media start, expressed in global time (pitch-adjusted)
		/// </summary>
		public Timecode GlobalMarkerPosition
		{
			get { return Timecode.FromNanos((long)(_marker.Position.Nanos / _parentEvent.PlaybackRate)); }
		}

		public Timecode LocalMarkerPosition
		{
			get { return Timecode.FromNanos((long)(_marker.Position.Nanos * _parentEvent.PlaybackRate)); }
		}

		public bool IsWithinEventBounds
		{
			get
			{
				// this is tricky. We need a tolerance of one timeline unit.
				if (GlobalMarkerOffset < Timecode.FromNanos(0))
					return false;
				if (GlobalMarkerOffset > _parentEvent.Length)
					return false;
				return true;
			}
		}

		public override string ToString()
		{
			return String.Format("[{0}] {1} (\"{2}\") Local: {3} Global: {4}", Marker.Index, Marker.Position, Marker.Label,
								 LocalMarkerOffset, GlobalMarkerOffset);
		}
	}
}