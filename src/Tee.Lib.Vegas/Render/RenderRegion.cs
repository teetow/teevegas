using ScriptPortal.Vegas;

namespace Tee.Lib.Vegas.Render
{
	public class RenderRegion
	{
		public string Label { get; set; }

		public Timecode Position { get; set; }

		public Timecode Length { get; set; }

		public Timecode End
		{
			get { return Position + Length; }
		}

		public RenderRegion(Region Region)
		{
			Position = Region.Position;
			Length = Region.Length;
			Label = Region.Label;
		}

		public bool EqualsRegion(Region Region)
		{
			if (Region.Label != Label)
				return false;
			if (Region.Position != Position)
				return false;
			if (Region.Length != Length)
				return false;
			return true;
		}
	}
}