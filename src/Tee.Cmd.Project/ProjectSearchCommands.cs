using System;
using System.Collections;
using Sony.Vegas;

namespace Tee.Cmd.Project
{
	internal class ProjectSearchCommands
	{
		private Vegas myVegas;
		private readonly CustomCommand ProjectSearchReplaceCommand = new CustomCommand(CommandCategory.View,
																					   ProjectSearchStrings.WindowTitle);

		internal void ProjectSearchInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;
			// Search and replace
			ProjectSearchReplaceCommand.MenuItemName = ProjectSearchStrings.MenuTitle;
			ProjectSearchReplaceCommand.DisplayName = ProjectSearchStrings.WindowTitle;
			ProjectSearchReplaceCommand.Invoked += ProjectSearchReplace_Invoked;
			ProjectSearchReplaceCommand.MenuPopup += ProjectSearchReplaceCommand_MenuPopup;

			CustomCommands.Add(ProjectSearchReplaceCommand);
		}

		private void ProjectSearchReplaceCommand_MenuPopup(object sender, EventArgs e)
		{
			var cmd = (CustomCommand)sender;
			cmd.Checked = myVegas.FindDockView(ProjectSearchStrings.WindowTitle);
		}

		private void ProjectSearchReplace_Invoked(object sender, EventArgs e)
		{
			if (!myVegas.ActivateDockView(ProjectSearchStrings.WindowTitle))
			{
				var searchForm = new FormSearchReplace { AutoLoadCommand = ProjectSearchReplaceCommand };
				myVegas.LoadDockView(searchForm);
			}
		}
	}
}