using System.IO;

namespace NAudio.FileFormats.SoundFont
{
	internal class GeneratorBuilder : StructureBuilder
	{
		public override int Length
		{
			get { return 4; }
		}

		public Generator[] Generators
		{
			get { return (Generator[]) data.ToArray(typeof (Generator)); }
		}

		public override object Read(BinaryReader br)
		{
			var g = new Generator();
			g.GeneratorType = (GeneratorEnum) br.ReadUInt16();
			g.UInt16Amount = br.ReadUInt16();
			data.Add(g);
			return g;
		}

		public override void Write(BinaryWriter bw, object o)
		{
			//Zone z = (Zone) o;
			//bw.Write(p.---);
		}

		public void Load(Instrument[] instruments)
		{
			foreach (Generator g in Generators)
			{
				if (g.GeneratorType == GeneratorEnum.Instrument)
				{
					g.Instrument = instruments[g.UInt16Amount];
				}
			}
		}

		public void Load(SampleHeader[] sampleHeaders)
		{
			foreach (Generator g in Generators)
			{
				if (g.GeneratorType == GeneratorEnum.SampleID)
				{
					g.SampleHeader = sampleHeaders[g.UInt16Amount];
				}
			}
		}
	}
}