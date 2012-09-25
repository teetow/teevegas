using System;
using System.IO;
using System.Text;

namespace Tee.Lib.Riff
{
	public class BEXT

	{
		public Char[] Description;
		public Char[] OriginationDate;
		public Char[] OriginationTime;
		public Char[] Originator;
		public Char[] OriginatorReference;
		public Char[] Reserved;
		public UInt32 TimeReferenceHigh;
		public UInt32 TimeReferenceLow;
		public Char[] UMID;
		public ushort Version;

		public BEXT(Byte[] Indata)
		{
			var DataReader = new BinaryReader(new MemoryStream(Indata), Encoding.ASCII);

			Description = DataReader.ReadChars(256);
			Originator = DataReader.ReadChars(32);
			OriginatorReference = DataReader.ReadChars(32);
			OriginationDate = DataReader.ReadChars(10);
			OriginationTime = DataReader.ReadChars(8);
			TimeReferenceLow = DataReader.ReadUInt32();
			TimeReferenceHigh = DataReader.ReadUInt32();
			Version = DataReader.ReadUInt16();
			UMID = DataReader.ReadChars(64);
			Reserved = DataReader.ReadChars(190);
		}
	}
}