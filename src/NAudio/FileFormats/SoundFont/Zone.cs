using System;

namespace NAudio.FileFormats.SoundFont
{
	/// <summary>
	/// A SoundFont zone
	/// </summary>
	public class Zone
	{
		internal ushort generatorCount;
		internal ushort generatorIndex;
		internal ushort modulatorCount;
		internal ushort modulatorIndex;

		/// <summary>
		/// Modulators for this Zone
		/// </summary>
		public Modulator[] Modulators { get; set; }

		/// <summary>
		/// Generators for this Zone
		/// </summary>
		public Generator[] Generators { get; set; }

		/// <summary>
		/// <see cref="object.ToString"/>
		/// </summary>
		public override string ToString()
		{
			return String.Format("Zone {0} Gens:{1} {2} Mods:{3}", generatorCount, generatorIndex,
			                     modulatorCount, modulatorIndex);
		}
	}
}