using Sony.Vegas;

namespace Tee.Cmd.MediaManager
{
	public class MediaListItem
	{
		public MediaListItem(Media Media)
		{
			this.Media = Media;
		}

		public Media Media { get; set; }

		public string FilePath
		{
			get { return Media.FilePath; }
		}

		public int UseCount
		{
			get { return Media.UseCount; }
		}
	}
}