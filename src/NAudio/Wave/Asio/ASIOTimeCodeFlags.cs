using System;

namespace NAudio.Wave.Asio
{
	[Flags]
	internal enum ASIOTimeCodeFlags
	{
		kTcValid = 1,
		kTcRunning = 1 << 1,
		kTcReverse = 1 << 2,
		kTcOnspeed = 1 << 3,
		kTcStill = 1 << 4,
		kTcSpeedValid = 1 << 8
	};
}