using System.Collections;
using Sony.Vegas;

namespace Tee.Cmd.Project
{
	public class ProjectCommandModule : ICustomCommandModule
	{
		private Vegas myVegas;

		public void InitializeModule(Vegas vegas)
		{
			myVegas = vegas;
		}

		public ICollection GetCustomCommands()
		{
			var customCommands = new ArrayList();

			new ProjectSearchCommands().ProjectSearchInit(myVegas, ref customCommands);
			new ProjectFileCommands().ProjectFileInit(myVegas, ref customCommands);
			new SelectionCommands().SelectionInit(myVegas, ref customCommands);

			return customCommands;
		}
	}
}