using System;
using System.IO;
using System.Text;

namespace NAudio.FileFormats.SoundFont
{
	internal class PresetBuilder : StructureBuilder
	{
		private Preset lastPreset;

		public override int Length
		{
			get { return 38; }
		}

		public Preset[] Presets
		{
			get { return (Preset[]) data.ToArray(typeof (Preset)); }
		}

		public override object Read(BinaryReader br)
		{
			var p = new Preset();
			string s = Encoding.ASCII.GetString(br.ReadBytes(20));
			if (s.IndexOf('\0') >= 0)
			{
				s = s.Substring(0, s.IndexOf('\0'));
			}
			p.Name = s;
			p.PatchNumber = br.ReadUInt16();
			p.Bank = br.ReadUInt16();
			p.startPresetZoneIndex = br.ReadUInt16();
			p.library = br.ReadUInt32();
			p.genre = br.ReadUInt32();
			p.morphology = br.ReadUInt32();
			if (lastPreset != null)
				lastPreset.endPresetZoneIndex = (ushort) (p.startPresetZoneIndex - 1);
			data.Add(p);
			lastPreset = p;
			return p;
		}

		public override void Write(BinaryWriter bw, object o)
		{
			var p = (Preset) o;
			//bw.Write(p.---);
		}

		public void LoadZones(Zone[] presetZones)
		{
			// don't do the last preset, which is simply EOP
			for (int preset = 0; preset < data.Count - 1; preset++)
			{
				var p = (Preset) data[preset];
				p.Zones = new Zone[p.endPresetZoneIndex - p.startPresetZoneIndex + 1];
				Array.Copy(presetZones, p.startPresetZoneIndex, p.Zones, 0, p.Zones.Length);
			}
			// we can get rid of the EOP record now
			data.RemoveAt(data.Count - 1);
		}
	}
}