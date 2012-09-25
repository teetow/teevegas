using System;
using System.IO;
using System.Text;

namespace Tee.Lib.Riff
{
	public class LiCkLtxt : LabeledListChunk
	{
		/*
			Offset 	Size 	Description 		Value
			0x00 	4 		Chunk ID 			"ltxt" (0x6C747874)
			0x04 	4 		Chunk Data Size 	depends on contained text
			0x08 	4 		Cue Point ID 		0 - 0xFFFFFFFF
			0x0c 	4 		Sample Length 		0 - 0xFFFFFFFF
			0x10 	4 		Purpose ID 			0 - 0xFFFFFFFF
			0x12 	2 		Country 			0 - 0xFFFF
			0x14 	2 		Language 			0 - 0xFFFF
			0x16 	2 		Dialect 			0 - 0xFFFF
			0x18 	2 		Code Page 			0 - 0xFFFF
			0x1A 			Text
			*/

		public UInt16 CodePage;
		public UInt16 Country;
		public UInt16 Dialect;
		public UInt16 Language;
		public String PurposeID;
		public UInt32 SampleLength;

		public LiCkLtxt()
		{
			Init();
		}

		public LiCkLtxt(UInt32 CuePointID, UInt32 Length)
		{
			Init();
			this.CuePointID = CuePointID;
			SampleLength = Length;
			PurposeID = LiCkAdtlType.rgn;
		}

		public LiCkLtxt(Byte[] Indata)
		{
			/*
				0x00 	4 	Chunk ID 	"ltxt" (0x6C747874)
				0x04 	4 	Chunk Data Size 	depends on contained text
				0x08 	4 	Cue Point ID 	0 - 0xFFFFFFFF
				0x0c 	4 	Sample Length 	0 - 0xFFFFFFFF
				0x10 	4 	Purpose ID 	0 - 0xFFFFFFFF
				0x12 	2 	Country 	0 - 0xFFFF
				0x14 	2 	Language 	0 - 0xFFFF
				0x16 	2 	Dialect 	0 - 0xFFFF
				0x18 	2 	Code Page 	0 - 0xFFFF
				0x1A 		Text
				 */
			Init();
			var DataReader = new BinaryReader(new MemoryStream(Indata), Encoding.ASCII);

			CuePointID = DataReader.ReadUInt32();
			SampleLength = DataReader.ReadUInt32();
			PurposeID = new String(DataReader.ReadChars(4));
			Country = DataReader.ReadUInt16();
			Language = DataReader.ReadUInt16();
			Dialect = DataReader.ReadUInt16();
			CodePage = DataReader.ReadUInt16();
			Text = DataReader.ReadStringNullTerm();
		}

		public override uint Size
		{
			get
			{
				uint size = 20 + (UInt32) Text.Length;
				return size;
			}
		}

		public override byte[] Data
		{
			get
			{
				var Output = new Byte[(Size%2) == 0 ? Size : Size + 1];
				var wrt = new BinaryWriter(new MemoryStream(Output));
				wrt.Write(CuePointID);
				wrt.Write(SampleLength);
				wrt.Write(PurposeID.ToCharArray());
				wrt.Write(Country);
				wrt.Write(Language);
				wrt.Write(Dialect);
				wrt.Write(CodePage);
				wrt.Write(Text.ToCharArray());
				return Output;
			}
		}

		public void Init()
		{
			ID = LiCkAdtlType.ltxt;
		}
	}
}