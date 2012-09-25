using System.Collections.Generic;

namespace Tee.Lib.Vegas.Render
{
	public static class RenderTags
	{
		public static List<string> NamingTags = new List<string>
		{
			Region,
			Track,
			Bus
		};

		public const string RootDir = "rootdir";
		public const string TargetDir = "targetdir";
		public const string NamingMask = "namingmask";

		public const string AudioFmt = "audiofmt";
		public const string AudioTpl = "audiotpl";
		public const string VideoFmt = "videofmt";
		public const string VideoTpl = "videotpl";

		public const string DoStems = "dostems";
		public const string DoVideo = "dovideo";
		public const string DoPadding = "dopadding";
		public const string PaddingAmt = "paddingamt";

		public const string NoEmpty = "noempty";
		public const string NoCounters = "nocounters";
		public const string DoReadonly = "doreadonly";

		public const string NoRender = "norender";

		public const string Region = "region";
		public const string Marker = "marker";
		public const string Track = "track";
		public const string Bus = "bus";
		public const string Counter = "c";
		public const string CounterDigits = "counterdigits";

		public static string Format(string Tag)
		{
			if (Tag == null || Tag.Trim() == string.Empty)
				return string.Empty;
			return ("{" + Tag.ToLowerInvariant().Trim() + "}");
		}

		public static List<string> Tags = new List<string>()
		{
			RootDir, TargetDir, NamingMask,
			AudioFmt, AudioTpl,
			VideoFmt, VideoTpl,
			DoStems, DoVideo,
			DoPadding, PaddingAmt,
			DoReadonly,
			NoRender, NoEmpty, NoCounters
			};
	}
}