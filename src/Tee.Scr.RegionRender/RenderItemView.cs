using Tee.Lib.Vegas.Render;

namespace Tee.Scr.RegionRender
{
	internal class RenderItemView
	{
		internal RenderItemView(RenderItem RenderItem)
		{
			_renderItem = RenderItem;
		}

		private readonly RenderItem _renderItem;

		public bool Include { get { return !_renderItem.RenderParams.GetParam<bool>(RenderTags.NoRender); } }

		public string Start { get { return _renderItem.Start.ToString(); } }

		public string Length { get { return _renderItem.Length.ToString(); } }

		public string Name { get { return _renderItem.FilePath; } }

		public string Format { get { return _renderItem.RenderFormat.ToString(); } }

		public string Template { get { return _renderItem.RenderTemplate.ToString(); } }

		public override string ToString()
		{
			return string.Format("{0}: {1} ({2}) {3}:{4}", Name, Start, Length, Format, Template);
		}
	}
}