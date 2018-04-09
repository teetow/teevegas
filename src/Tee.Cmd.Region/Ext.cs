using System.Collections.Generic;
using System.Linq;
using ScriptPortal.Vegas;

namespace Tee.Cmd.Region
{
	internal static class Ext
	{
		public static bool HasInside(this IEnumerable<ScriptPortal.Vegas.Region> Regions, Timecode Start, Timecode End)
		{
			return Regions.Any(reg => reg.HasInside(Start, End));
		}

		public static bool HasInside(this ScriptPortal.Vegas.Region Region, Timecode Start, Timecode End)
		{
			if (Start < Region.Position && End > Region.End)
				return true;
			return false;
		}
	}
}