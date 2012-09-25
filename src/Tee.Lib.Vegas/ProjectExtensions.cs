using System.Collections.Generic;
using System.Linq;
using Sony.Vegas;
using Tee.Lib.Vegas.Project;

namespace Tee.Lib.Vegas
{
	public static class ProjectExtensions
	{
		public static List<List<TrackEvent>> GetEventGroups(this Sony.Vegas.Project Project)
		{
			List<TrackEvent> events = Project.GetSelectedEvents(true);
			if (events.Count == 0)
				events = Project.GetAllEvents();

			var groups = new List<List<TrackEvent>>();

			for (int i = 0; i < events.Count(); i++)
			{
				var ev = events[i];

				bool foundgrp = false;
				foreach (var grp in groups)
				{
					if (grp.Any(grpEv => grpEv.Start < ev.End && grpEv.End > ev.Start))
					{
						grp.Add(ev);
						foundgrp = true;
					}
				}
				if (!foundgrp)
				{
					groups.Add(new List<TrackEvent> { ev });
				}
			}

			return groups;
		}

		public static List<TrackEvent> GetAllEvents(this Sony.Vegas.Project Project)
		{
			return Project.Tracks.SelectMany(CTrack => CTrack.Events).ToList();
		}

		public static List<RegionGroup> GetRegionGroups(this Sony.Vegas.Vegas Vegas)
		{
			Timecode selStart = Vegas.Transport.SelectionStart;
			Timecode selEnd = Vegas.Transport.SelectionStart + Vegas.Transport.SelectionLength;

			if (selStart > selEnd)
				Ext.SwapTimecode(ref selStart, ref selEnd);

			bool selection = Vegas.SelectionLength != Timecode.FromNanos(0) && (Vegas.Transport.CursorPosition == selStart || Vegas.Transport.CursorPosition == selEnd);

			var rGrps = new List<RegionGroup>();

			foreach (var r in Vegas.Project.Regions)
			{
				if (selection && (r.Position < selStart || r.End > selEnd))
					continue;

				var rgr = new RegionGroup(r);

				foreach (Track t in Vegas.Project.Tracks)
				{
					foreach (TrackEvent ev in t.Events)
					{
						if (ev.Start < r.End && ev.End > r.Position)
						{
							rgr.AddEvent(ev);
						}
					}
				}
				rGrps.Add(rgr);
			}
			return rGrps;
		}

		public static List<TrackEvent> GetSelectedEvents(this Sony.Vegas.Project Project, bool SortByTime = false)
		{
			var selectedEvents = new List<TrackEvent>();
			foreach (Track trk in Project.Tracks)
			{
				selectedEvents.AddRange(trk.Events.Where(ev => ev.Selected));
			}

			if (SortByTime)
			{
				selectedEvents = selectedEvents.SortByTime();
			}
			return selectedEvents;
		}

		public static List<TrackEvent> SortByTime(this List<TrackEvent> events)
		{
			var sortedEvents = new List<TrackEvent>(events);
			sortedEvents.Sort(delegate(TrackEvent a, TrackEvent b)
			{
				int startCompare = Comparer<Timecode>.Default.Compare(a.Start, b.Start);
				if (startCompare != 0)
					return startCompare;

				int endCompare = Comparer<Timecode>.Default.Compare(a.End, b.End);
				if (endCompare != 0)
					return endCompare;
				return Comparer<int>.Default.Compare(a.Track.Index, b.Track.Index);
			});
			return sortedEvents;
		}

		public static Region FindRegion(this Sony.Vegas.Project Project, string RegionName)
		{
			return FindRegion(Project, RegionName, Timecode.FromSeconds(0));
		}

		public static Region FindRegion(this Sony.Vegas.Project Project, string RegionName, Timecode After)
		{
			var matchingRegions = new List<Region>();

			foreach (Region r in Project.Regions)
			{
				if (r.Label.ToLower().Contains(RegionName.ToLower()))
				{
					matchingRegions.Add(r);

					if (r.Position > After)
						return r; // assumes sorted list of regions.
				}
			}
			if (matchingRegions.Any())
				return matchingRegions[0];

			return null;
		}

		public static Region GetSelectedRegion(this Sony.Vegas.Vegas Vegas)
		{
			if (Vegas.Transport.SelectionLength == Timecode.FromSeconds(0))
				return null;

			return Vegas.Project.Regions.FirstOrDefault(r => r.Position == Vegas.Transport.SelectionStart && r.Length == Vegas.Transport.SelectionLength);
		}

		public static List<Region> GetSelectedRegions(this Sony.Vegas.Vegas Vegas)
		{
			if (Vegas.Transport.SelectionLength == Timecode.FromSeconds(0))
				return null;

			Timecode start, end;
			Vegas.Transport.GetNormalizedTimeSelection(out start, out end);

			return Vegas.Project.Regions.Where(r => r.Position >= start && r.End <= end).ToList();
		}

		public static void GetNormalizedTimeSelection(this TransportControl Transport, out Timecode Start, out Timecode End)
		{
			if (Transport.SelectionLength < Timecode.FromNanos(0))
			{
				End = Transport.SelectionStart;
				Start = End + Transport.SelectionLength;
			}
			else
			{
				Start = Transport.SelectionStart;
				End = Start = Transport.SelectionLength;
			}
		}

		public static List<Marker> GetSelectedMarkers(this Sony.Vegas.Vegas Vegas)
		{
			if (Vegas.SelectionLength.Nanos == 0)
				return new List<Marker>();

			Timecode start = Vegas.Transport.SelectionStart;
			Timecode end = Vegas.Transport.SelectionStart + Vegas.Transport.SelectionLength;
			if (start > end)
			{
				Timecode temp = start;
				start = end;
				end = temp;
			}

			return Vegas.Project.Markers.Where(mk => mk.Position >= start && mk.Position <= end).ToList();
		}
	}
}