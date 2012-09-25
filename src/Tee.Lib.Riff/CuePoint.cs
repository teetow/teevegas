using System;
using System.IO;
using System.Text;
using Tee.Lib.Riff.Riff;

namespace Tee.Lib.Riff
{
	public class CuePoint
	{
		/*
			Offset 	Size 	Description 	Value
			0x00 	4 		ID 				unique identification value
			0x04 	4 		Position 		play order position
			0x08 	4 		Data Chunk ID 	RIFF ID of corresponding data chunk
			0x0c 	4 		Chunk Start 	Byte Offset of Data Chunk *
			0x10 	4 		Block Start 	Byte Offset to sample of First Channel
			0x14 	4 		Sample Offset 	Byte Offset to sample byte of First Channel
			 */
		public UInt32 BlockStart;
		public UInt32 ChunkStart;
		public String DataChunkID;
		public UInt32 ID;
		public UInt32 Position;
		public UInt32 SampleOffset;

		public CuePoint(UInt32 Position)
		{
			DataChunkID = CkType.data;
			this.Position = Position;
			SampleOffset = Position;
		}

		public CuePoint(Byte[] Indata)
		{
			var DataStream = new MemoryStream(Indata);
			var DataReader = new BinaryReader(DataStream, Encoding.ASCII);

			ID = DataReader.ReadUInt32();
			Position = DataReader.ReadUInt32();
			DataChunkID = new String(DataReader.ReadChars(4));
			ChunkStart = DataReader.ReadUInt32();
			BlockStart = DataReader.ReadUInt32();
			SampleOffset = DataReader.ReadUInt32();
		}

		public Byte[] Data
		{
			get
			{
				var Output = new Byte[24];
				var wrt = new BinaryWriter(new MemoryStream(Output));
				wrt.Write(ID);
				wrt.Write(Position);
				wrt.Write(DataChunkID.ToCharArray());
				wrt.Write(ChunkStart);
				wrt.Write(BlockStart);
				wrt.Write(SampleOffset);
				return Output;
			}
		}
	}
}