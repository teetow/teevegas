using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tee.Lib.Riff.Riff;

namespace Tee.Lib.Riff
{
	public class RiffFile
	{
		public String ID;

		private List<Chunk> m_Chunks;
		public String m_Format;

		public RiffFile(String Filename)
		{
			Init(Filename, true);
		}

		public RiffFile(String Filename, bool ReadOnly)
		{
			Init(Filename, ReadOnly);
		}

		public UInt32 Size
		{
			get { return m_Chunks.Aggregate<Chunk, uint>(4, (current, ch) => current + (ch.Size + 8)); }
		}

		public IEnumerable<Chunk> Chunks
		{
			get { return m_Chunks; }
		}

		private void Init(string Filename, bool ReadOnly)
		{
			BinaryReader fileReader;
			FileStream fileStream;
			try
			{
				fileStream = new FileStream(Filename, FileMode.Open);
				fileReader = new BinaryReader(fileStream, Encoding.ASCII);
			}
			catch
			{
				return;
			}
			ID = new String(fileReader.ReadChars(4));
			fileReader.ReadUInt32();
			m_Format = new String(fileReader.ReadChars(4));
			m_Chunks = new List<Chunk>();

			while (fileReader.BaseStream.Position < fileReader.BaseStream.Length)
			{
				var id = new String(fileReader.ReadChars(4));
				uint sizeInner = fileReader.ReadUInt32();
				Chunk newChunk = null;
				Byte[] chunkData;
				switch (id)
				{
					case CkType.fmt:
						chunkData = fileReader.ReadBytes((int) sizeInner);
						newChunk = new CkFmt(chunkData);
						break;
					case CkType.data:
						if (ReadOnly)
						{
							fileReader.BaseStream.Seek(sizeInner, SeekOrigin.Current);
						}
						else
						{
							chunkData = fileReader.ReadBytes((int) sizeInner);
							newChunk = new CkData(chunkData);
						}
						break;
					case CkType.cue:
						chunkData = fileReader.ReadBytes((int) sizeInner);
						newChunk = new CkCue(chunkData);
						break;
					case CkType.LIST:
						chunkData = fileReader.ReadBytes((int) sizeInner);
						newChunk = new CkList(chunkData);
						break;
					default:
						Console.WriteLine(String.Format("An unknown chunk {0} was found.", id));
						chunkData = fileReader.ReadBytes((int) sizeInner);
						newChunk = new CkUnknown(chunkData, id);
						break;
				}
				if (newChunk != null)
					m_Chunks.Add(newChunk);
			}
			fileStream.Close();
			fileStream.Dispose();
			fileReader.Close();
		}

		public void Save(string Filename)
		{
			var outWriter = new BinaryWriter(new FileStream(Filename, FileMode.Create, FileAccess.Write));
			outWriter.Write(ID.ToCharArray());
			outWriter.Write(Size);
			outWriter.Write(m_Format.ToCharArray());
			outWriter.Flush();

			foreach (Chunk ch in m_Chunks)
			{
				outWriter.Write(ch.ID.ToCharArray());
				outWriter.Write(ch.Size);
				outWriter.Write(ch.Data);
			}
			outWriter.Close();
		}

		public Chunk GetChunk(String ChunkID)
		{
			return m_Chunks.FirstOrDefault(ch => ch.ID.ToLower() == ChunkID.ToLower());
		}

		public Chunk GetChunk<T>()
		{
			if (m_Chunks == null)
				return null;
			return m_Chunks.FirstOrDefault(ch => ch.GetType() == typeof (T));
		}


		public Chunk AddChunk(string ChunkType)
		{
			switch (ChunkType)
			{
				case CkType.cue:
					var cue = new CkCue();
					m_Chunks.Add(cue);
					return cue;
				case CkType.LIST:
					var list = new CkList();
					m_Chunks.Add(list);
					return list;
			}
			return null;
		}

		public void Close()
		{
		}
	}
}