using System;
using System.IO;

namespace Tee.Scr.RegionRender
{
	internal static class RegionRenderStrings
	{
		public const string RegionRenderCommandName = "2RegionRender";
		public const string RegionRenderMenuName = "&RegionRender";
		public const string RegionRenderWindowTitle = "RegionRender";

		public const string RegionParamsCommandName = "1RegionParams";
		public const string RegionParamsMenuName = "&Set up project for RegionRender";
		public const string RegionParamsWindowTitle = "Project setup";

		public const string ScriptName = "RegionRender";
		public const string ScriptSetupArg = "setup";

		public const string CustomTagConfigFile = "RootDirConfigFile";
		public static string CustomTagConfigFileDefault = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"TeeVegas\CustomTags.conf");

		public const string SelectionOnlyOptionName = "SelectionOnly";
		public const string LastTargetDir = "LastTargetDir";

		public const string LastTargetDirDefault = @"C:\";
	}
}