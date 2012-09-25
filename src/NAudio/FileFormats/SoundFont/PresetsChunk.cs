using System;
using System.Text;

namespace NAudio.FileFormats.SoundFont
{
	/// <summary>
	/// Class to read the SoundFont file presets chunk
	/// </summary>
	public class PresetsChunk
	{
		private readonly GeneratorBuilder instrumentZoneGenerators = new GeneratorBuilder();
		private readonly ModulatorBuilder instrumentZoneModulators = new ModulatorBuilder();
		private readonly ZoneBuilder instrumentZones = new ZoneBuilder();
		private readonly InstrumentBuilder instruments = new InstrumentBuilder();
		private readonly PresetBuilder presetHeaders = new PresetBuilder();
		private readonly GeneratorBuilder presetZoneGenerators = new GeneratorBuilder();
		private readonly ModulatorBuilder presetZoneModulators = new ModulatorBuilder();
		private readonly ZoneBuilder presetZones = new ZoneBuilder();
		private readonly SampleHeaderBuilder sampleHeaders = new SampleHeaderBuilder();

		internal PresetsChunk(RiffChunk chunk)
		{
			string header = chunk.ReadChunkID();
			if (header != "pdta")
			{
				throw new ApplicationException(String.Format("Not a presets data chunk ({0})", header));
			}

			RiffChunk c;
			while ((c = chunk.GetNextSubChunk()) != null)
			{
				switch (c.ChunkID)
				{
					case "PHDR":
					case "phdr":
						c.GetDataAsStructureArray(presetHeaders);
						break;
					case "PBAG":
					case "pbag":
						c.GetDataAsStructureArray(presetZones);
						break;
					case "PMOD":
					case "pmod":
						c.GetDataAsStructureArray(presetZoneModulators);
						break;
					case "PGEN":
					case "pgen":
						c.GetDataAsStructureArray(presetZoneGenerators);
						break;
					case "INST":
					case "inst":
						c.GetDataAsStructureArray(instruments);
						break;
					case "IBAG":
					case "ibag":
						c.GetDataAsStructureArray(instrumentZones);
						break;
					case "IMOD":
					case "imod":
						c.GetDataAsStructureArray(instrumentZoneModulators);
						break;
					case "IGEN":
					case "igen":
						c.GetDataAsStructureArray(instrumentZoneGenerators);
						break;
					case "SHDR":
					case "shdr":
						c.GetDataAsStructureArray(sampleHeaders);
						break;
					default:
						throw new ApplicationException(String.Format("Unknown chunk type {0}", c.ChunkID));
				}
			}

			// now link things up
			instrumentZoneGenerators.Load(sampleHeaders.SampleHeaders);
			instrumentZones.Load(instrumentZoneModulators.Modulators, instrumentZoneGenerators.Generators);
			instruments.LoadZones(instrumentZones.Zones);
			presetZoneGenerators.Load(instruments.Instruments);
			presetZones.Load(presetZoneModulators.Modulators, presetZoneGenerators.Generators);
			presetHeaders.LoadZones(presetZones.Zones);
			sampleHeaders.RemoveEOS();
		}

		/// <summary>
		/// The Presets contained in this chunk
		/// </summary>
		public Preset[] Presets
		{
			get { return presetHeaders.Presets; }
		}

		/// <summary>
		/// The instruments contained in this chunk
		/// </summary>
		public Instrument[] Instruments
		{
			get { return instruments.Instruments; }
		}

		/// <summary>
		/// The sample headers contained in this chunk
		/// </summary>
		public SampleHeader[] SampleHeaders
		{
			get { return sampleHeaders.SampleHeaders; }
		}

		/// <summary>
		/// <see cref="object.ToString"/>
		/// </summary>
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("Preset Headers:\r\n");
			foreach (Preset p in presetHeaders.Presets)
			{
				sb.AppendFormat("{0}\r\n", p);
			}
			sb.Append("Instruments:\r\n");
			foreach (Instrument i in instruments.Instruments)
			{
				sb.AppendFormat("{0}\r\n", i);
			}
			return sb.ToString();
		}
	}
}