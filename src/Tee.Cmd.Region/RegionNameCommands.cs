using System;
using System.Collections;
using System.Collections.Generic;
using Sony.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Project;

namespace Tee.Cmd.Region
{
	internal class RegionNameCommands
	{
		private Vegas myVegas;
		private readonly CustomCommand RegionNameAuto = new CustomCommand(CommandCategory.Edit, "&From &Events");
		private readonly CustomCommand RegionNameParent = new CustomCommand(CommandCategory.Edit, "Region &Name");
		private readonly CustomCommand RegionNameSeq = new CustomCommand(CommandCategory.Edit, "Sequentially");

		internal void RegionNameInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;
			RegionNameParent.AddChild(RegionNameAuto);
			RegionNameParent.AddChild(RegionNameSeq);

			RegionNameAuto.Invoked += RegionNameAuto_Invoked;
			RegionNameSeq.Invoked += RegionNameSeq_Invoked;

			CustomCommands.Add(RegionNameParent);
			CustomCommands.Add(RegionNameAuto);
			CustomCommands.Add(RegionNameSeq);
		}

		private void RegionNameAuto_Invoked(object sender, EventArgs e)
		{
			RegionNameChange(RegionNameMethod.FromEvents);
		}

		private void RegionNameSeq_Invoked(object sender, EventArgs e)
		{
			RegionNameChange(RegionNameMethod.Sequential);
		}

		/// Implementation
		///
		///
		private void RegionNameChange(RegionNameMethod Method)
		{
			var Groups = myVegas.GetRegionGroups();

			if (Method == RegionNameMethod.FromEvents)
				RegionNameFromEvents(Groups);
			else
				RegionNameSequential(Groups);
		}

		private void RegionNameFromEvents(IEnumerable<RegionGroup> Groups)
		{
			using (var undo = new UndoBlock("Name regions"))
			{
				foreach (RegionGroup Group in Groups)
				{
					var Names = new Dictionary<string, int>();
					var Best = new KeyValuePair<string, int>();

					foreach (TrackEvent Evt in Group.Events)
					{
						if (Names.ContainsKey(Evt.ActiveTake.Name))
							Names[Evt.ActiveTake.Name] += 1;
						else
							Names.Add(Evt.ActiveTake.Name, 1);
					}

					foreach (var Pair in Names)
					{
						if (Pair.Value > Best.Value)
							Best = Pair;
					}
					Group.Region.Label = Best.Key;
				}
			}
		}

		private void RegionNameSequential(IEnumerable<RegionGroup> Groups)
		{
			int CurrentIndex = 1;

			using (var undo = new UndoBlock("Name regions sequentially"))
			{
				foreach (RegionGroup Grp in Groups)
				{
					Grp.Region.Label = String.Format(Strings.SequentialNamingString, CurrentIndex++);
				}
			}
		}

		public class Strings
		{
			public const string SequentialNaming = "SequentialNamingString";
			public const string SequentialNamingString = "Region_{0:d3}";
		}
	}
}