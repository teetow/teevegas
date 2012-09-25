using System;
using System.IO;

namespace NAudio.FileFormats.Map
{
	internal class MapBlockHeader
	{
		private int length; // surely this is length
		private int value2;
		private short value3;
		private short value4;

		public int Length
		{
			get { return length; }
		}

		public static MapBlockHeader Read(BinaryReader reader)
		{
			var header = new MapBlockHeader();
			header.length = reader.ReadInt32(); // usually first 2 bytes have a value
			header.value2 = reader.ReadInt32(); // usually 0
			header.value3 = reader.ReadInt16(); // 0,1,2,3
			header.value4 = reader.ReadInt16(); // 0x1017 (sometimes 0x1018
			return header;
		}

		public override string ToString()
		{
			return String.Format("{0} {1:X8} {2:X4} {3:X4}",
			                     length, value2, value3, value4);
		}
	}
}