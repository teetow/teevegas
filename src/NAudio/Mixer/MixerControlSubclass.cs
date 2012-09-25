using System;

namespace NAudio.Mixer
{
	[Flags]
	internal enum MixerControlSubclass
	{
		SwitchBoolean = 0x00000000,
		SwitchButton = 0x01000000,
		MeterPolled = 0x00000000,
		TimeMicrosecs = 0x00000000,
		TimeMillisecs = 0x01000000,
		ListSingle = 0x00000000,
		ListMultiple = 0x01000000,
		Mask = 0x0F000000
	}
}