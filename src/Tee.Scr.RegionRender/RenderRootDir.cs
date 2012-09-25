using System;
using System.Linq;
using System.Text;

namespace Tee.Scr.RegionRender
{
	[Serializable]
	internal class RenderRootDir
	{
		public string Name { get; set; }

		public string Directory { get; set; }

		public RenderRootDir()
		{
		}
	}
}