using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sony.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Dialogs;
using Tee.Lib.Vegas.ScriptOption;

namespace Tee.Cmd.Project
{
	public class SelectionCommands
	{
		private Vegas myVegas;

		private readonly CustomCommand SelectionParent = new CustomCommand(CommandCategory.Edit, "&Selection");
		private readonly CustomCommand SelectionFindAgainCommand = new CustomCommand(CommandCategory.Edit, "Find &next");
		private readonly CustomCommand SelectionFindRegionCommand = new CustomCommand(CommandCategory.Edit, "&Find region...");
		private readonly CustomCommand SelectionFitToEventsCommand = new CustomCommand(CommandCategory.Edit, "Fit to &events");
		private readonly CustomCommand SelectionSetToCurrentRegion = new CustomCommand(CommandCategory.Edit, "Fit to &region");

		internal void SelectionInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;

			SelectionFitToEventsCommand.Invoked += SelectionFitToEnclosedEvents_Invoke;
			SelectionParent.AddChild(SelectionFitToEventsCommand);

			SelectionFindRegionCommand.Invoked += SelectionFindRegion_Invoke;
			SelectionParent.AddChild(SelectionFindRegionCommand);

			SelectionFindAgainCommand.Invoked += SelectionFindAgain_Invoke;
			SelectionParent.AddChild(SelectionFindAgainCommand);

			SelectionSetToCurrentRegion.Invoked += SelectionSetToCurrentRegion_Invoked;
			SelectionParent.AddChild(SelectionSetToCurrentRegion);

			CustomCommands.Add(SelectionParent);
			CustomCommands.Add(SelectionFindRegionCommand);
			CustomCommands.Add(SelectionFindAgainCommand);
			CustomCommands.Add(SelectionFitToEventsCommand);
			CustomCommands.Add(SelectionSetToCurrentRegion);
		}

		private void SelectionFitToEnclosedEvents_Invoke(object sender, EventArgs e)
		{
			SelectionFitToEvents();
		}

		private void SelectionFindRegion_Invoke(object sender, EventArgs e)
		{
			SelectionFindRegion();
		}

		private void SelectionFindAgain_Invoke(object sender, EventArgs e)
		{
			SelectionFindAgain();
		}

		private void SelectionSetToCurrentRegion_Invoked(object sender, EventArgs e)
		{
			Timecode cursor = myVegas.Transport.CursorPosition;
			Region candidate = null;
			foreach (Region reg in myVegas.Project.Regions)
			{
				if (reg.Position <= cursor && reg.End >= cursor)
				{
					candidate = reg;
					if (myVegas.Transport.SelectionStart == reg.Position &&
						myVegas.Transport.SelectionStart + myVegas.Transport.SelectionLength == reg.End)
					{
						continue;
					}
					break;
				}
			}
			if (candidate == null)
				return;

			myVegas.Transport.SelectionStart = candidate.Position;
			myVegas.Transport.SelectionLength = candidate.Length;
		}

		private void SelectionFitToEvents()
		{
			List<TrackEvent> sel = myVegas.Project.GetSelectedEvents(true);
			if (sel.Count == 0)
				return;
			myVegas.Transport.LoopRegionStart = sel.First().Start;
			myVegas.Transport.LoopRegionLength = sel.Last().End - myVegas.Transport.LoopRegionStart;
		}

		private void SelectionFindRegion()
		{
			try
			{
				//Options.ScriptOptionCollection options = Options.GetOptions("Find Region");
				var myPrompt = new FormSimplePrompt
								{
									tbUserData = { Text = ScriptOptions.GetValue(SelectionStrings.FindRegionKeyName, SelectionStrings.FindRegionLastString, "") },
									Text = "Find Region",
									lblPrompt = { Text = "Search for:" },
									lblDescription = { Text = "Partial name of region to find" }
								};

				if (myPrompt.ShowDialog() == DialogResult.OK)
				{
					Region selRegion = myVegas.GetSelectedRegion();
					Timecode startTime = (selRegion != null) ? selRegion.Position : Timecode.FromSeconds(0);
					Region bestRegion = myVegas.Project.FindRegion(myPrompt.tbUserData.Text, startTime);

					if (bestRegion == null)
						return;

					myVegas.Transport.SelectionStart = bestRegion.Position;
					myVegas.Transport.SelectionLength = bestRegion.Length;

					ScriptOptions.SetValue(SelectionStrings.FindRegionKeyName, SelectionStrings.FindRegionLastString, myPrompt.tbUserData.Text);
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}

		private void SelectionFindAgain()
		{
			string searchString = ScriptOptions.GetValue(SelectionStrings.FindRegionKeyName, SelectionStrings.FindRegionLastString, "");
			if (!string.IsNullOrEmpty(searchString))
			{
				Region selectedRegion = myVegas.GetSelectedRegion();
				var bestRegion = myVegas.Project.FindRegion(searchString, (selectedRegion != null) ? selectedRegion.Position : Timecode.FromSeconds(0));

				if (bestRegion == null)
					return;

				myVegas.Transport.SelectionStart = bestRegion.Position;
				myVegas.Transport.SelectionLength = bestRegion.Length;
			}
		}
	}
}