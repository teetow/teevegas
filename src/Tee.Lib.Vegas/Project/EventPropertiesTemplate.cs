using System;
using Sony.Vegas;

namespace Tee.Lib.Vegas.Project
{
	[Serializable]
	public class EventPropertiesTemplate
	{
		public SerializableFade FadeIn;

		public SerializableFade FadeOut;

		public bool Loop;

		public double PlaybackRate { get; private set; }

		public long LengthNanos { get; private set; }

		public long RegionOffsetNanos { get; private set; }

		public EventPropertiesTemplate(TrackEvent Event, Region ParentRegion = null)
		{
			FadeIn = new SerializableFade(Event.FadeIn);
			FadeOut = new SerializableFade(Event.FadeOut);
			Loop = Event.Loop;
			PlaybackRate = Event.PlaybackRate;
			LengthNanos = Event.Length.Nanos;
			RegionOffsetNanos = ParentRegion != null ? Event.Start.Nanos - ParentRegion.Position.Nanos : 0;
		}
	}
}