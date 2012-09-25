using System;

namespace NAudio.Dmo
{
	[Flags]
	internal enum DmoInputStatusFlags
	{
		None,
		DMO_INPUT_STATUSF_ACCEPT_DATA = 0x1
	}
}