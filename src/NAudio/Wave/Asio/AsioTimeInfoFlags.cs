using System;

namespace NAudio.Wave.Asio
{
	[Flags]
	internal enum AsioTimeInfoFlags
	{
		kSystemTimeValid = 1, // must always be valid
		kSamplePositionValid = 1 << 1, // must always be valid
		kSampleRateValid = 1 << 2,
		kSpeedValid = 1 << 3,
		kSampleRateChanged = 1 << 4,
		kClockSourceChanged = 1 << 5
	}
}