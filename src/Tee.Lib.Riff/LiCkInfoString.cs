using System;
using System.IO;
using System.Text;

namespace Tee.Lib.Riff
{
	public class LiCkInfoString : LabeledListChunk
	{
		/*
			Offset 	Size 	Description 		Value
			0x00 	4 		Chunk ID 			varies
			0x04 	4 		Chunk Data Size 	depends on contained data
			0x0c 			data
			*/

		public LiCkInfoString(Byte[] Indata)
		{
			var DataReader = new BinaryReader(new MemoryStream(Indata), Encoding.ASCII);
			Text = DataReader.ReadStringNullTerm();
		}
	}
}