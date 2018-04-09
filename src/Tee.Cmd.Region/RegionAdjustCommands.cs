using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ScriptPortal.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Dialogs;
using Tee.Lib.Vegas.Project;
using Tee.Lib.Vegas.ScriptOption;

namespace Tee.Cmd.Region
{
	internal class RegionAdjustCommands
	{
		private Vegas myVegas;

		private readonly CustomCommand RegionAdjustAutoSize = new CustomCommand(CommandCategory.Edit, "Auto size");
		private readonly CustomCommand RegionAdjustNudgeLeft = new CustomCommand(CommandCategory.Edit, "Nudge &Left");
		private readonly CustomCommand RegionAdjustNudgeRight = new CustomCommand(CommandCategory.Edit, "Nudge &Right");
		private readonly CustomCommand RegionAdjustParent = new CustomCommand(CommandCategory.Edit, "Region &Adjust");
		private readonly CustomCommand RegionAdjustResizeIn = new CustomCommand(CommandCategory.Edit, "Size &Down");
		private readonly CustomCommand RegionAdjustResizeOut = new CustomCommand(CommandCategory.Edit, "Size &Up");
		private readonly CustomCommand RegionAdjustSpacingIn = new CustomCommand(CommandCategory.Edit, "Spacing &In");
		private readonly CustomCommand RegionAdjustSpacingOut = new CustomCommand(CommandCategory.Edit, "Spacing &Out");

		internal void RegionAdjustInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;
			RegionAdjustParent.AddChild(RegionAdjustAutoSize);
			RegionAdjustParent.AddChild(RegionAdjustNudgeLeft);
			RegionAdjustParent.AddChild(RegionAdjustNudgeRight);
			RegionAdjustParent.AddChild(RegionAdjustResizeIn);
			RegionAdjustParent.AddChild(RegionAdjustResizeOut);
			RegionAdjustParent.AddChild(RegionAdjustSpacingIn);
			RegionAdjustParent.AddChild(RegionAdjustSpacingOut);

			RegionAdjustAutoSize.Invoked += RegionAdjustAutoSize_Invoked;
			RegionAdjustNudgeLeft.Invoked += RegionAdjustNudgeLeft_Invoked;
			RegionAdjustNudgeRight.Invoked += RegionAdjustNudgeRight_Invoked;
			RegionAdjustResizeIn.Invoked += RegionAdjustResizeIn_Invoked;
			RegionAdjustResizeOut.Invoked += RegionAdjustResizeOut_Invoked;
			RegionAdjustSpacingIn.Invoked += RegionAdjustSpacingIn_Invoked;
			RegionAdjustSpacingOut.Invoked += RegionAdjustSpacingOut_Invoked;

			CustomCommands.Add(RegionAdjustParent);
			CustomCommands.Add(RegionAdjustAutoSize);
			CustomCommands.Add(RegionAdjustNudgeLeft);
			CustomCommands.Add(RegionAdjustNudgeRight);
			CustomCommands.Add(RegionAdjustResizeIn);
			CustomCommands.Add(RegionAdjustResizeOut);
			CustomCommands.Add(RegionAdjustSpacingIn);
			CustomCommands.Add(RegionAdjustSpacingOut);
		}

		private void RegionAdjustAutoSize_Invoked(object sender, EventArgs e)
		{
			var time = FormTimeEntry.GetUserTime("Region adjust", "Enter headroom", "Headroom");
			if (time == null) return;
			RegionsAutoSize(time);
		}

		private void RegionAdjustSpacingOut_Invoked(object sender, EventArgs e)
		{
			var time = FormTimeEntry.GetUserTime("Region adjust", "Move wider by");
			if (time == null) return;
			RegionsNudge(time, true);
		}

		private void RegionAdjustSpacingIn_Invoked(object sender, EventArgs e)
		{
			var time = FormTimeEntry.GetUserTime("Region adjust", "Move closer by");
			if (time == null) return;
			RegionsNudge(time, true);
		}

		private void RegionAdjustResizeOut_Invoked(object sender, EventArgs e)
		{
			var time = FormTimeEntry.GetUserTime("Region adjust", "Lengthen by");
			if (time == null) return;
			RegionsResize(time);
		}

		private void RegionAdjustResizeIn_Invoked(object sender, EventArgs e)
		{
			var time = FormTimeEntry.GetUserTime("Region adjust", "Shorten by");
			if (time == null) return;
			time = Timecode.FromSeconds(0) - time;
			RegionsResize(time);
		}

		private void RegionAdjustNudgeRight_Invoked(object sender, EventArgs e)
		{
			var time = FormTimeEntry.GetUserTime("Region adjust", "Nudge right by");
			if (time == null) return;
			RegionsNudge(time, false);
		}

		private void RegionAdjustNudgeLeft_Invoked(object sender, EventArgs e)
		{
			var time = FormTimeEntry.GetUserTime("Region adjust", "Nudge left by");
			if (time == null) return;
			time = Timecode.FromSeconds(0) - time;
			RegionsNudge(time, false);
		}

		private void RegionsAutoSize(Timecode Headroom)
		{
			var Groups = myVegas.GetRegionGroups();

			// check overlapping groups
			var Overlaps = new List<RegionGroup>();
			Timecode LastEnd = null;

			foreach (RegionGroup Grp in Groups)
			{
				if (LastEnd != null && Grp.Region.Position < LastEnd)
				{
					Overlaps.Add(Grp);
				}
				LastEnd = Grp.Region.End;
			}

			if (Overlaps.Count != 0)
			{
				if (MessageBox.Show(
						"This project contains overlapping / duplicate regions. Autosize would likely screw everything up. Continue anyway?",
						"Overlaps detected", MessageBoxButtons.YesNo) != DialogResult.Yes)
				{
					return;
				}
			}

			using (var undo = new UndoBlock("Fit regions to events"))
			{
				foreach (RegionGroup Grp in Groups)
				{
					if (Grp.Events.Count == 0) continue;
					Grp.Region.Position = Grp.Start - Headroom;
					Grp.Region.End = Grp.End + Headroom;
				}
			}
		}

		/// Implementation
		///
		///
		private void RegionsNudge(Timecode Time, bool Cumulative)
		{
			Timecode ZeroTime = myVegas.Project.Ruler.StartTime;
			Timecode Adjustment = Time;

			if (Cumulative)
				Adjustment = Timecode.FromSeconds(0);

			var Groups = myVegas.GetRegionGroups();

			using (var undo = new UndoBlock("Nudge regions by " + Time))
			{
				foreach (var group in Groups)
				{
					Timecode newTime = group.Region.Position + Adjustment;
					Timecode localAdjustment = Timecode.FromSeconds(0);

					// zero time detection
					if (newTime < ZeroTime)
					{
						if (ScriptOptions.GetValue(Strings.ScriptName, "ShowZeroTimeWarning", true))
						{
							myVegas.ShowError("Cannot move beyond start of timeline",
											  "Your operation was aborted to avoid moving events outside the timeline");
						}
						return;
					}

					// collision detection
					foreach (var grp in Groups)
					{
						if (!grp.Equals(@group))
						{
							if (newTime <= grp.Region.End && newTime >= grp.Region.Position)
							{
								localAdjustment = newTime - grp.Region.End;
								break;
							}
						}
					}
					group.MoveTo(newTime - localAdjustment);
					if (Cumulative) Adjustment += Time;
				}
			}
		}

		private void RegionsResize(Timecode Time)
		{
			var groups = myVegas.GetRegionGroups();

			using (var undo = new UndoBlock("Resize regions by " + Time))
			{
				foreach (RegionGroup Group in groups)
				{
					Group.AdjustRegionLength(Group.Region.Length + Time);
				}
			}
		}
	}
}