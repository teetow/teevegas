using System.ComponentModel;
using ScriptPortal.Vegas;

namespace Tee.Cmd.MediaManager
{
	public class RegionView : IEditableObject
	{
		public Region Region;

		public RegionView(Region Region)
		{
			this.Region = Region;
		}

		public string Label
		{
			get { return Region.Label; }
			set
			{
				using (var undo = new UndoBlock("Rename region"))
				{
					Region.Label = value;
				}
			}
		}

		public Timecode Position
		{
			get { return Region.Position; }
			set
			{
				using (var undo = new UndoBlock("Move region"))
				{
					Timecode tc = Timecode.FromPositionString(value.ToString(), RulerFormat.Unknown);
					Region.Position = tc;
				}
			}
		}

		public Timecode Length
		{
			get { return Region.Length; }
			set
			{
				using (var undo = new UndoBlock("Resize region"))
				{
					Timecode tc = Timecode.FromString(value.ToString(), RulerFormat.Unknown);
					Region.Length = tc;
				}
			}
		}

		#region IEditableObject Members

		public void BeginEdit()
		{
		}

		public void CancelEdit()
		{
		}

		public void EndEdit()
		{
		}

		#endregion
	}
}