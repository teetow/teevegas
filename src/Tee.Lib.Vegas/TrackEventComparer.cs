using System.Collections.Generic;
using Sony.Vegas;

namespace Tee.Lib.Vegas
{
	public class TrackEventComparer : IComparer<TrackEvent>
	{
		#region IComparer<TrackEvent> Members

		int IComparer<TrackEvent>.Compare(TrackEvent x, TrackEvent y)
		{
			if (x.Start == y.Start)
				return 0;
			if (x.Start > y.Start)
				return 1;
			return -1;
		}

		#endregion
	}
}