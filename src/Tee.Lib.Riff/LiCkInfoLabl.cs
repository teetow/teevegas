using System;
using System.IO;
using System.Text;

namespace Tee.Lib.Riff
{
	public class LiCkInfoLabl : LabeledListChunk
	{
		/*
			Offset 	Size 	Description 		Value
			0x00 	4 		Chunk ID 			"labl" (0x6C61626C)
			0x04 	4 		Chunk Data Size 	depends on contained text
			0x08 	4 		Cue Point ID 		0 - 0xFFFFFFFF
			0x0c 			Text
			*/

		public LiCkInfoLabl(Byte[] Indata)
		{
			ID = LiCkAdtlType.labl;
			var DataReader = new BinaryReader(new MemoryStream(Indata), Encoding.ASCII);
			CuePointID = DataReader.ReadUInt32();
			Text = DataReader.ReadStringNullTerm();
		}

		public LiCkInfoLabl(CuePoint CuePoint, String Label)
		{
			CuePointID = CuePoint.ID;
			ID = LiCkAdtlType.labl;
			Text = Label;
		}

		public override uint Size
		{
			get
			{
				uint size = 4 + (uint) Text.Length;
				return size;
			}
		}

		public override byte[] Data
		{
			get
			{
				uint dataSize = (Size%2 == 0) ? Size : Size + 1;
				var outData = new byte[dataSize];
				var outWriter = new BinaryWriter(new MemoryStream(outData, true));
				outWriter.Write(CuePointID);
				outWriter.Write(Encoding.ASCII.GetBytes(Text));
				outWriter.Flush();
				return outData;
			}
			set { base.Data = value; }
		}
	}
}