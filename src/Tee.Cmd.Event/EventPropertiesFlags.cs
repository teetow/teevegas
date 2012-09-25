using System;

namespace Tee.Cmd.Event
{
	[Flags]
	public enum EventPropertiesFlags
	{
		None = 0x0000,
		FadeIn = 0x0001,
		FadeInCurve = 0x0002,
		FadeOut = 0x0004,
		FadeOutCurve = 0x0008,
		Gain = 0x0010,
		PitchMethod = 0x0020,
		PitchAmount = 0x0040,
		Loop = 0x0080,
		Length = 0x0100,
		RegionOffset = 0x0200,
		Fades = FadeIn | FadeInCurve | FadeOut | FadeOutCurve,
		All = Fades | Gain | PitchMethod | PitchAmount | Loop | Length | RegionOffset
	}
}