using System;
using System.Collections;
using Sony.Vegas;

namespace Tee.Cmd.MediaManager
{
	public class MediaManagerCommandModule : ICustomCommandModule
	{
		private readonly CustomCommand MediaManagerCommand = new CustomCommand(CommandCategory.View, Strings.MenuTitle);
		private FormMediaManager mediamanagerform;
		private Vegas myVegas;

		#region ICustomCommandModule Members

		public void InitializeModule(Vegas vegas)
		{
			myVegas = vegas;
		}

		public ICollection GetCustomCommands()
		{
			var CustomCommands = new ArrayList();
			MediaManagerInit(ref CustomCommands);
			return CustomCommands;
		}

		#endregion ICustomCommandModule Members

		private void MediaManagerInit(ref ArrayList CustomCommands)
		{
			MediaManagerCommand.Invoked += MediaManagerCommand_Invoked;
			MediaManagerCommand.MenuPopup += MediaManagerCommand_MenuPopup;
			CustomCommands.Add(MediaManagerCommand);
		}

		private void MediaManagerCommand_MenuPopup(object sender, EventArgs e)
		{
			var cmd = (CustomCommand)sender;
			cmd.Checked = myVegas.FindDockView(Strings.WindowTitle);
		}

		private void MediaManagerCommand_Invoked(object sender, EventArgs e)
		{
			if (!myVegas.ActivateDockView(Strings.WindowTitle))
			{
				if (mediamanagerform == null)
				{
					mediamanagerform = new FormMediaManager { AutoLoadCommand = MediaManagerCommand, PersistDockWindowState = true, MyVegas = myVegas };
				}
				myVegas.LoadDockView(mediamanagerform);
				mediamanagerform.PopulateMediaListItems();
			}
			else
			{
				IDockView ctrl;
				myVegas.FindDockView(Strings.WindowTitle, out ctrl);
				if (ctrl != null)
					ctrl.InvokeLoaded();
			}
		}
	}
}