using System;
using System.Collections.Generic;
using System.Linq;
using ScriptPortal.Vegas;
using Tee.Lib.Vegas;

namespace Tee.Cmd.Event
{
	internal static class Ext
	{
		internal static Timecode DistanceToRegion(this Vegas Vegas, TrackEvent Event)
		{
			var surroundingRegions =
				Vegas.Project.Regions.Where(r => r.Position <= Event.Start && r.End >= Event.End).ToList();

			if (!surroundingRegions.Any())
				return null;

			Region region = surroundingRegions[0];

			return (Event.Start - region.Position);
		}

		internal static Region FindSurroundingRegion(this Vegas Vegas, TrackEvent Event)
		{
			return Vegas.Project.Regions.FirstOrDefault(r => r.ContainsTime(Event.Start, Event.Length));
		}

		internal static void SetGain(this TrackEvent Event, float Gain)
		{
			Event.FadeIn.Gain = Gain;
		}

		internal static void SetNormalizationGain(this AudioEvent Event, float Gain)
		{
			Event.NormalizeGain = Gain;
		}

		internal static MetaMarker FindCurrentMetaMarker(this TrackEvent Event)
		{
			List<MetaMarker> metamarkers = Event.ActiveTake.Media.Markers.Select(mk => new MetaMarker(mk, Event)).ToList();
			if (metamarkers.Count == 0)
				return null;

			if (metamarkers.Count == 1)
				return metamarkers[0];

			List<MetaMarker> relevantMarkers = metamarkers.Where(item => item.IsWithinEventBounds).ToList();
			// select only visible markers

			if (!relevantMarkers.Any())
			{
				relevantMarkers = metamarkers;
			}

			relevantMarkers.Sort(delegate(MetaMarker A, MetaMarker B)
			{
				if (Math.Abs(A.LocalMarkerOffset.Nanos) < Math.Abs(B.LocalMarkerOffset.Nanos))
					return -1;
				return 1;
			});

			MetaMarker mmk = relevantMarkers[0];

			return mmk;
		}

		internal static Marker FindCurrentMarker(this TrackEvent Event)
		{
			MetaMarker mmk = Event.FindCurrentMetaMarker();
			if (mmk != null)
				return mmk.Marker;
			return null;
		}
	}
}