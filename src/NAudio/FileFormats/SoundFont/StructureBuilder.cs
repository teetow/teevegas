using System.Collections;
using System.IO;

namespace NAudio.FileFormats.SoundFont
{
	/// <summary>
	/// base class for structures that can read themselves
	/// </summary>
	internal abstract class StructureBuilder
	{
		protected ArrayList data;

		public StructureBuilder()
		{
			Reset();
		}

		public abstract int Length { get; }

		public object[] Data
		{
			get { return data.ToArray(); }
		}

		public abstract object Read(BinaryReader br);
		public abstract void Write(BinaryWriter bw, object o);

		public void Reset()
		{
			data = new ArrayList();
		}
	}
}