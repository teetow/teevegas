using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tee.Lib.Riff.Riff;

namespace Tee.Lib.Riff
{
	public sealed class CkList : Chunk
	{
		/*
			Offset 	Size 	Description 		Value
			0x00 	4 		Chunk ID 			"list" (0x6C696E74)
			0x04 	4 		Chunk InData Size 	depends on contained text
			0x08 	4 		Type ID 			"adtl" (0x6164746C)
			0x0c 			List of Text Labels and Names
			*/

		public List<ListChunk> Chunks;
		public String TypeID;


		public CkList()
		{
			Init();
		}


		public CkList(Byte[] Indata)
		{
			Init();
			var DataReader = new BinaryReader(new MemoryStream(Indata), Encoding.ASCII);
			TypeID = new String(DataReader.ReadChars(4));
			switch (TypeID)
			{
				case (LiCkType.adtl):
					byte[] adtldata = DataReader.ReadBytes((int) DataReader.BaseStream.Length);
					ReadAdtlChunk(adtldata);
					break;
				case (LiCkType.INFO):
					byte[] infodata = DataReader.ReadBytes((int) DataReader.BaseStream.Length);
					ReadInfoChunk(infodata);
					break;
				default:
					Data = DataReader.ReadBytes((int) DataReader.BaseStream.Length);
					break;
			}
		}

		public override UInt32 Size
		{
			get
			{
				UInt32 size = 4;
				if (Chunks == null)
					return size;
				foreach (ListChunk chunk in Chunks)
				{
					uint chunkSize = 8 + chunk.Size;
					if (chunkSize%2 != 0)
						chunkSize += 1;
					size += chunkSize;
				}
				return size;
			}
		}

		public override byte[] Data
		{
			get
			{
				var Out = new Byte[Size];
				var wrt = new BinaryWriter(new MemoryStream(Out));
				wrt.Write(TypeID.ToCharArray());
				foreach (ListChunk ch in Chunks)
				{
					wrt.Write(ch.ID.ToCharArray());
					wrt.Write(ch.Size);
					wrt.Write(ch.Data);
				}
				return Out;
			}
			set { base.Data = value; }
		}

		public void Init()
		{
			ID = CkType.LIST;
			Chunks = new List<ListChunk>();
			TypeID = LiCkType.adtl;
		}


		public void ReadAdtlChunk(Byte[] InData)
		{
			Chunks = new List<ListChunk>();
			var dataReader = new BinaryReader(new MemoryStream(InData), Encoding.ASCII);
			while (dataReader.BaseStream.Position < dataReader.BaseStream.Length)
			{
				var type = new String(dataReader.ReadChars(4));
				uint size = dataReader.ReadUInt32();
				if (size%2 != 0) // account for padding
					size += 1;
				byte[] ChunkData = dataReader.ReadBytes((int) size);
				ListChunk newChunk = null;
				switch (type)
				{
					case LiCkAdtlType.labl:
						newChunk = new LiCkInfoLabl(ChunkData);
						break;
					case LiCkAdtlType.note:
						newChunk = new LiCkNote(ChunkData);
						break;
					case LiCkAdtlType.ltxt:
						newChunk = new LiCkLtxt(ChunkData);
						break;
				}
				if (newChunk != null)
					Chunks.Add(newChunk);
			}
		}

		private void ReadInfoChunk(byte[] InData)
		{
			Chunks = new List<ListChunk>();
			var dataReader = new BinaryReader(new MemoryStream(InData), Encoding.ASCII);
			while (dataReader.BaseStream.Position < dataReader.BaseStream.Length)
			{
				var type = new String(dataReader.ReadChars(4));
				uint size = dataReader.ReadUInt32();
				if (size%2 != 0) // account for padding
					size += 1;
				byte[] ChunkData = dataReader.ReadBytes((int) size);
				LabeledListChunk newChunk;
				switch (type)
				{
					case LiCkInfoType.TCDO:
						newChunk = new LiCkInfoTCDO(ChunkData) {ID = type};
						break;
					case LiCkInfoType.TCOD:
						newChunk = new LiCkInfoTCOD(ChunkData) {ID = type};
						break;
					default:
						newChunk = new LiCkInfoString(ChunkData) {ID = type};
						break;
				}
				Chunks.Add(newChunk);
			}
		}
	}
}