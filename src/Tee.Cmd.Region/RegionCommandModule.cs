using System.Collections;
using ScriptPortal.Vegas;

namespace Tee.Cmd.Region
{
	public class RegionCommandModule : ICustomCommandModule
	{
		private Vegas myVegas;

		public void InitializeModule(Vegas vegas)
		{
			myVegas = vegas;
		}

		public ICollection GetCustomCommands()
		{
			var CustomCommands = new ArrayList();

			new RegionCreateCommands().RegionCreateInit(myVegas, ref CustomCommands);
			new RegionAdjustCommands().RegionAdjustInit(myVegas, ref CustomCommands);
			new RegionNameCommands().RegionNameInit(myVegas, ref CustomCommands);
			new RegionMarkerCommands().MarkerInit(myVegas, ref CustomCommands);

			return CustomCommands;
		}
	}
}