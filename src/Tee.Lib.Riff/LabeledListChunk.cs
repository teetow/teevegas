using System;
using System.IO;
using System.Text;

namespace Tee.Lib.Riff
{
	public abstract class LabeledListChunk : ListChunk
	{
		/*
			Offset 	Size 	Description 		Value
			0x00 	4 		Chunk ID 			varies
			0x04 	4 		Chunk Data Size 	depends on contained text
			0x0c 			Text
			*/
		public UInt32 CuePointID;
		private String _Text;

		public String Text
		{
			get
			{
				if (_Text.Length == 0)
					return string.Empty;
				return _Text + '\0';
			}

			set
			{
				string tmp = value;
				_Text = tmp;
			}
		}

		public override uint Size
		{
			get
			{
				var size = (UInt32) Text.Length;
				return size;
			}
		}

		public override byte[] Data
		{
			get
			{
				uint size = (Size%2 == 0) ? Size : Size + 1;
				var output = new Byte[size];
				var wrt = new BinaryWriter(new MemoryStream(output), Encoding.ASCII);
				wrt.Write(Text.ToCharArray());
				while (wrt.BaseStream.Position < wrt.BaseStream.Length)
					wrt.Write('\0'); // "hidden" padding
				return output;
			}
			set { base.Data = value; }
		}
	}
}