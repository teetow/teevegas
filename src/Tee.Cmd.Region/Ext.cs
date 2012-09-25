using System.Collections.Generic;
using System.Linq;
using Sony.Vegas;

namespace Tee.Cmd.Region
{
	internal static class Ext
	{
		public static bool HasInside(this IEnumerable<Sony.Vegas.Region> Regions, Timecode Start, Timecode End)
		{
			return Regions.Any(reg => reg.HasInside(Start, End));
		}

		public static bool HasInside(this Sony.Vegas.Region Region, Timecode Start, Timecode End)
		{
			if (Start < Region.Position && End > Region.End)
				return true;
			return false;
		}
	}
}