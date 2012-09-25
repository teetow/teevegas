using System.Linq;
using Sony.Vegas;

namespace Tee.Scr.RegionRender
{
	internal static class Ext
	{
		internal static Region NextRegion(this Project Project, Timecode timecode)
		{
			return Project.Regions.FirstOrDefault(r => r.Position > timecode);
		}

		internal static bool RegionsBetween(this Project Project, Timecode A, Timecode B)
		{
			Timecode start = A;
			Timecode end = B;
			if (A > B)
			{
				start = B;
				end = A;
			}

			return Project.Regions.Any(region => region.End <= end && region.End >= start);
		}
	}
}