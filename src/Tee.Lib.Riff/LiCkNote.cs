using System;
using System.IO;
using System.Text;

namespace Tee.Lib.Riff
{
	public class LiCkNote : LabeledListChunk
	{
		/*
			Offset 	Size 	Description 		Value
			0x00 	4 		Chunk ID 			"note" (0x6E6F7465)
			0x04 	4 		Chunk Data Size 	depends on contained text
			0x08 	4 		Cue Point ID		0 - 0xFFFFFFFF
			0x0C 			Text			
			*/

		public LiCkNote(Byte[] Indata)
		{
			ID = LiCkAdtlType.note;
			var DataReader = new BinaryReader(new MemoryStream(Indata), Encoding.ASCII);

			CuePointID = DataReader.ReadUInt32();
			Text = DataReader.ReadStringNullTerm();
		}
	}
}