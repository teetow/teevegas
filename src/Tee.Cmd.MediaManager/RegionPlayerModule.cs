using System;
using System.Collections;
using Sony.Vegas;

namespace Tee.Cmd.MediaManager
{
	public class RegionPlayerModule : ICustomCommandModule
	{
		private Vegas myVegas;
		private readonly CustomCommand RegionPlayerCommand = new CustomCommand(CommandCategory.View, Strings.RegionPlayerMenuTitle);

		public ICollection GetCustomCommands()
		{
			var customCommands = new ArrayList();
			RegionPlayerCommand.Invoked += RegionPlayerCommand_Invoked;
			RegionPlayerCommand.MenuPopup += RegionPlayerCommand_MenuPopup;
			customCommands.Add(RegionPlayerCommand);
			return customCommands;
		}

		private void RegionPlayerCommand_Invoked(object sender, EventArgs e)
		{
			if (!myVegas.ActivateDockView(Strings.RegionPlayerWindowTitle))
			{
				var player = new FormRegionPlayer { AutoLoadCommand = RegionPlayerCommand, PersistDockWindowState = true };
				myVegas.LoadDockView(player);
			}
			else
			{
				IDockView ctrl;
				myVegas.FindDockView(Strings.RegionPlayerWindowTitle, out ctrl);
				if (ctrl != null)
					ctrl.InvokeLoaded();
			}
		}

		private void RegionPlayerCommand_MenuPopup(object sender, EventArgs e)
		{
			var cmd = (CustomCommand)sender;
			cmd.Checked = myVegas.FindDockView(Strings.RegionPlayerWindowTitle);
		}

		public void InitializeModule(Vegas Vegas)
		{
			myVegas = Vegas;
		}
	}
}