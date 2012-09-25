using System;
using System.IO;
using System.Text;
using Tee.Lib.Riff.Riff;

namespace Tee.Lib.Riff
{
	public class CkFmt : Chunk
	{
		/*
			0x00 	4 	Chunk ID						"fmt " (0x666D7420)
			0x04 	4 	Chunk Data Size 				16 + extra format bytes
			0x08 	2 	Compression code 				1 - 65,535
			0x0a 	2 	Number of channels				1 - 65,535
			0x0c 	4 	Sample rate 					1 - 0xFFFFFFFF
			0x10 	4 	Average bytes per second		1 - 0xFFFFFFFF
			0x14 	2 	Block align 					1 - 65,535
			0x16 	2 	Significant bits per sample 	2 - 65,535
			0x18 	2 	Extra format bytes 				0 - 65,535
			0x1a 	x	Extra format bytes
			*/

		public UInt32 AvgBytesPerSecond;
		public UInt16 BlockAlign;
		public UInt16 CompressionCode;
		public UInt16 ExtraFormatBytes;
		public Byte[] ExtraFormatData;
		public UInt16 NumChannels;
		public UInt32 SampleRate;
		public UInt16 SignificantBitsPerSample;

		public CkFmt(Byte[] indata)
		{
			ID = CkType.fmt;
			var DataReader = new BinaryReader(new MemoryStream(indata), Encoding.ASCII);

			CompressionCode = DataReader.ReadUInt16();
			NumChannels = DataReader.ReadUInt16();
			SampleRate = DataReader.ReadUInt32();
			AvgBytesPerSecond = DataReader.ReadUInt32();
			BlockAlign = DataReader.ReadUInt16();
			SignificantBitsPerSample = DataReader.ReadUInt16();
			if (DataReader.BaseStream.Position >= DataReader.BaseStream.Length)
				return;
			ExtraFormatBytes = DataReader.ReadUInt16();
			ExtraFormatData = DataReader.ReadBytes(ExtraFormatBytes);
		}

		public override UInt32 Size
		{
			get { return (UInt32) 16 + ExtraFormatBytes; }
		}

		public override byte[] Data
		{
			get
			{
				var Output = new Byte[Size];
				var dataWriter = new BinaryWriter(new MemoryStream(Output));
				dataWriter.Write(CompressionCode);
				dataWriter.Write(NumChannels);
				dataWriter.Write(SampleRate);
				dataWriter.Write(AvgBytesPerSecond);
				dataWriter.Write(BlockAlign);
				dataWriter.Write(SignificantBitsPerSample);
				if (ExtraFormatBytes != 0)
				{
					dataWriter.Write(ExtraFormatBytes);
					dataWriter.Write(ExtraFormatData);
				}
				return Output;
			}
			set { base.Data = value; }
		}
	}
}