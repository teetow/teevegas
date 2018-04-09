using ScriptPortal.Vegas;

namespace Tee.Lib.Vegas.Project
{
	public class RegionGroup
	{
		private readonly EventGroup _Events;
		private readonly Region _Region;

		public RegionGroup(Region Region)
		{
			_Events = new EventGroup();
			_Region = Region;
		}

		public EventGroup Events
		{
			get { return _Events; }
		}

		public Region Region
		{
			get { return _Region; }
		}

		public Timecode Start
		{
			get
			{
				Timecode BestTime = null;
				foreach (TrackEvent Evt in _Events)
				{
					if (BestTime == null || Evt.Start < BestTime)
						BestTime = Evt.Start;
				}
				return BestTime;
			}
		}

		public Timecode End
		{
			get
			{
				Timecode BestTime = null;
				foreach (TrackEvent Evt in _Events)
				{
					if (BestTime == null || Evt.End > BestTime)
						BestTime = Evt.End;
				}
				return BestTime;
			}
		}

		public Timecode Length
		{
			get { return (_Region.Length); }
		}

		public string Name
		{
			get { return _Region.Label; }
		}

		public Timecode Timestamp { get; set; }

		public void AddEvent(TrackEvent Event)
		{
			_Events.Add(Event);
			_Events.Sort();
		}

		public void RemoveEvent(TrackEvent Event)
		{
			if (_Events.Contains(Event))
				_Events.Remove(Event);
		}

		public void MoveBy(Timecode Time)
		{
			foreach (TrackEvent Event in _Events)
			{
				Event.Start += Time;
			}
			_Region.Position += Time;
		}

		public void MoveTo(Timecode Time)
		{
			foreach (TrackEvent Event in _Events)
			{
				Event.Start = Time + (Event.Start - _Region.Position);
			}
			_Region.Position = Time;
		}

		public void AdjustRegionBounds(Timecode StartTime, Timecode EndTime)
		{
			_Region.Position = StartTime;
			_Region.End = EndTime;
		}

		public void AdjustRegionLength(Timecode LengthAdjustment)
		{
			if (LengthAdjustment < Timecode.FromNanos(0))
				LengthAdjustment = Timecode.FromNanos(0);
			_Region.End = _Region.Position + LengthAdjustment;
		}

		public bool ContainsEvent(TrackEvent Event)
		{
			if (_Events.Contains(Event))
				return true;
			return false;
		}

		public void AdjustRegionToEvents(Timecode Tolerance)
		{
			// auto lol
			_Region.Position = Start - Tolerance;
			_Region.End = End + Tolerance;
		}
	}
}