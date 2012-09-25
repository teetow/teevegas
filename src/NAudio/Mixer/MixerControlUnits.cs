using System;

namespace NAudio.Mixer
{
	[Flags]
	internal enum MixerControlUnits
	{
		Custom = 0x00000000,
		Boolean = 0x00010000,
		Signed = 0x00020000,
		Unsigned = 0x00030000,
		Decibels = 0x00040000, // in 10ths
		Percent = 0x00050000, // in 10ths
		Mask = 0x00FF0000
	}
}