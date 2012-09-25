using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tee.Lib.Riff.Riff;

namespace Tee.Lib.Riff
{
	public class CkCue : Chunk
	{
		/*
			Offset 	Size 	Description			Value
			0x00 	4 	Chunk ID 				"cue " (0x63756520)
			0x04 	4 	Chunk Data Size 		depends on Num Cue Points
			0x08 	4 	Num Cue Points 			number of cue points in list
			0x0c 	-	List of Cue Points
			*/
		public List<CuePoint> CuePoints;

		public CkCue(Byte[] Indata)
		{
			Init();
			var DataReader = new BinaryReader(new MemoryStream(Indata), Encoding.ASCII);
			DataReader.ReadUInt32();
			byte[] cuePointsData = DataReader.ReadBytes(Indata.Length);
			ReadCuePoints(cuePointsData);
		}

		public CkCue()
		{
			Init();
		}

		public override UInt32 Size
		{
			get { return (4 + (NumCuePoints*24)); }
		}

		public override byte[] Data
		{
			get
			{
				var Out = new Byte[Size];
				var wr = new BinaryWriter(new MemoryStream(Out));
				wr.Write(NumCuePoints);
				foreach (CuePoint cue in CuePoints)
				{
					wr.Write(cue.Data);
				}
				return Out;
			}
			set { base.Data = value; }
		}

		public UInt32 NumCuePoints
		{
			get
			{
				if (CuePoints == null)
					return 0;
				return (UInt32) CuePoints.Count;
			}
		}

		private void Init()
		{
			ID = CkType.cue;
			CuePoints = new List<CuePoint>();
		}


		private void ReadCuePoints(Byte[] Indata)
		{
			if (Indata.Length == 0)
				return;
			var dataReader = new BinaryReader(new MemoryStream(Indata), Encoding.ASCII);
			while (dataReader.BaseStream.Position < Indata.Length)
			{
				AddCuePoint(dataReader.ReadBytes(24));
			}
		}

		private void AddCuePoint(CuePoint CuePoint)
		{
			CuePoints.Add(CuePoint);
		}

		private void AddCuePoint(Byte[] CuePointData)
		{
			AddCuePoint(new CuePoint(CuePointData));
		}

		internal CuePoint AddCuePoint(uint SamplePos)
		{
			var cp = new CuePoint(SamplePos)
			         	{
			         		ID = NumCuePoints + 1,
			         		Position = SamplePos,
			         		DataChunkID = CkType.data,
			         		BlockStart = 0,
			         		ChunkStart = 0,
			         		SampleOffset = SamplePos
			         	};
			CuePoints.Add(cp);
			return cp;
		}
	}
}