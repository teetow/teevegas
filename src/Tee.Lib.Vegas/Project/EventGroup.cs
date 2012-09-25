using System.Collections.Generic;
using Sony.Vegas;

namespace Tee.Lib.Vegas.Project
{
	public class EventGroup : List<TrackEvent>
	{
		public Timecode Start
		{
			get
			{
				Timecode start = null;
				foreach (TrackEvent CEvent in this)
				{
					if (start == null || CEvent.Start < start)
						start = CEvent.Start;
				}
				return start;
			}
		}

		public Timecode End
		{
			get
			{
				Timecode end = null;
				foreach (TrackEvent CEvent in this)
				{
					if (end == null || CEvent.End > end)
						end = CEvent.End;
				}
				return end;
			}
		}

		public Timecode Length
		{
			get { return End - Start; }
		}

		public new void Add(TrackEvent Event)
		{
			base.Add(Event);
		}

		public new void Remove(TrackEvent Event)
		{
			if (Contains(Event))
			{
				base.Remove(Event);
			}
		}

		public new void Sort()
		{
			Sort(new TrackEventComparer());
		}
	}
}