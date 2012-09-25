using System;

namespace NAudio.FileFormats.SoundFont
{
	/// <summary>
	/// A SoundFont Preset
	/// </summary>
	public class Preset
	{
		private ushort bank;
		internal ushort endPresetZoneIndex;
		internal uint genre;
		internal uint library;
		internal uint morphology;
		private string name;
		private ushort patchNumber;
		internal ushort startPresetZoneIndex;

		/// <summary>
		/// Preset name
		/// </summary>
		public string Name
		{
			get { return name; }
			set
			{
				// TODO: validate
				name = value;
			}
		}

		/// <summary>
		/// Patch Number
		/// </summary>
		public ushort PatchNumber
		{
			get { return patchNumber; }
			set
			{
				// TODO: validate
				patchNumber = value;
			}
		}

		/// <summary>
		/// Bank number
		/// </summary>
		public ushort Bank
		{
			get { return bank; }
			set
			{
				// 0 - 127, GM percussion bank is 128
				// TODO: validate
				bank = value;
			}
		}

		/// <summary>
		/// Zones
		/// </summary>
		public Zone[] Zones { get; set; }

		/// <summary>
		/// <see cref="object.ToString"/>
		/// </summary>
		public override string ToString()
		{
			return String.Format("{0}-{1} {2}", bank, patchNumber, name);
		}
	}
}