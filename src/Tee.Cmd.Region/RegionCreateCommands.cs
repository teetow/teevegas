using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sony.Vegas;
using Tee.Lib.Vegas;

namespace Tee.Cmd.Region
{
	public class RegionCreateCommands
	{
		private Vegas myVegas;
		private readonly CustomCommand RegionCreateFromEvents = new CustomCommand(CommandCategory.Edit, "From &Events");
		private readonly CustomCommand RegionCreateParent = new CustomCommand(CommandCategory.Edit, "Region &Create");

		internal void RegionCreateInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;
			RegionCreateParent.AddChild(RegionCreateFromEvents);

			RegionCreateFromEvents.Invoked += RegionCreateFromEvents_Invoked;

			CustomCommands.Add(RegionCreateParent);
			CustomCommands.Add(RegionCreateFromEvents);
		}

		///
		/// Implementation
		///
		internal void RegionCreateFromEvents_Invoked(object sender, EventArgs e)
		{
			var eventgroups = myVegas.Project.GetEventGroups();
			var regions = (myVegas.Project.Regions.Count != 0)
												? new List<Sony.Vegas.Region>(myVegas.Project.Regions)
												: new List<Sony.Vegas.Region>();

			bool selectionSet = false;
			if (myVegas.SelectionLength != Timecode.FromMilliseconds(0))
				if (myVegas.Cursor == myVegas.SelectionStart ||
					myVegas.Cursor == myVegas.SelectionStart + myVegas.SelectionLength)
					selectionSet = true;

			using (var undo = new UndoBlock("Create regions"))
			{
				foreach (var egrp in eventgroups)
				{
					Timecode groupStart = null;
					Timecode groupEnd = null;
					foreach (var ev in egrp)
					{
						if (groupStart == null || ev.Start < groupStart)
							groupStart = ev.Start;
						if (groupEnd == null || ev.End > groupEnd)
							groupEnd = ev.End;
					}

					// skip outside seletion
					if (selectionSet)
						if (groupEnd >= (myVegas.SelectionStart + myVegas.SelectionLength) ||
							groupStart <= myVegas.SelectionStart)
							continue;

					// don't write inside existing regions
					if (regions.Any(reg => reg.ContainsTime(groupStart, (groupEnd - groupStart))))
						continue;

					// don't surround existing regions
					if (regions.HasInside(groupStart, groupEnd))
						continue;

					var NewRegion = new Sony.Vegas.Region(groupStart, (groupEnd - groupStart));
					myVegas.Project.Regions.Add(NewRegion);
				}
			}
		}

		public static class Strings
		{
			public static string ShowClearRegionDialog = "ShowClearRegionDialog";
		}
	}
}