using System.IO;
using System.Text;

namespace NAudio.FileFormats.SoundFont
{
	internal class SampleHeaderBuilder : StructureBuilder
	{
		public override int Length
		{
			get { return 46; }
		}

		public SampleHeader[] SampleHeaders
		{
			get { return (SampleHeader[]) data.ToArray(typeof (SampleHeader)); }
		}

		public override object Read(BinaryReader br)
		{
			var sh = new SampleHeader();
			string s = Encoding.ASCII.GetString(br.ReadBytes(20));
			if (s.IndexOf('\0') >= 0)
			{
				s = s.Substring(0, s.IndexOf('\0'));
			}

			sh.SampleName = s;
			sh.Start = br.ReadUInt32();
			sh.End = br.ReadUInt32();
			sh.StartLoop = br.ReadUInt32();
			sh.EndLoop = br.ReadUInt32();
			sh.SampleRate = br.ReadUInt32();
			sh.OriginalPitch = br.ReadByte();
			sh.PitchCorrection = br.ReadSByte();
			sh.SampleLink = br.ReadUInt16();
			sh.SFSampleLink = (SFSampleLink) br.ReadUInt16();
			data.Add(sh);
			return sh;
		}

		public override void Write(BinaryWriter bw, object o)
		{
			var sh = (SampleHeader) o;
			//bw.Write(p.---);
		}

		internal void RemoveEOS()
		{
			data.RemoveAt(data.Count - 1);
		}
	}
}