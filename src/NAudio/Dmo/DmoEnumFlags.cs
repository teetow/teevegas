using System;

namespace NAudio.Dmo
{
	[Flags]
	internal enum DmoEnumFlags
	{
		None,
		DMO_ENUMF_INCLUDE_KEYED = 0x00000001
	}
}