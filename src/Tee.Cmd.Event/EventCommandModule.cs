using System.Collections;
using ScriptPortal.Vegas;

namespace Tee.Cmd.Event
{
	public class EventCommandModule : ICustomCommandModule
	{
		private Vegas myVegas;

		public void InitializeModule(Vegas Vegas)
		{
			myVegas = Vegas;
		}

		public ICollection GetCustomCommands()
		{
			var customCommands = new ArrayList();

			new EventPitchCommands().EventPitchInit(myVegas, ref customCommands);
			new EventPositionCommands().EventPosInit(myVegas, ref customCommands);
			new EventEdgeCommands().EventEdgeInit(myVegas, ref customCommands);
			new EventMetaTakesCommands().MetaTakesInit(myVegas, ref customCommands);
			new EventPropertiesCommands().EventPropertiesInit(myVegas, ref customCommands);

			return customCommands;
		}
	}
}