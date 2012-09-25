using System;

namespace Configuration
{
	[Flags]
	public enum VegasDirectoryStatus
	{
		None = 0,
		DirectoryExists = 1,
		JunctionExists = 2
	}
}