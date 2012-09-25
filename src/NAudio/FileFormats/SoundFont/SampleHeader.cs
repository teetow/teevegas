namespace NAudio.FileFormats.SoundFont
{
	/// <summary>
	/// A SoundFont Sample Header
	/// </summary>
	public class SampleHeader
	{
		/// <summary>
		/// End offset
		/// </summary>
		public uint End;

		/// <summary>
		/// End loop point
		/// </summary>
		public uint EndLoop;

		/// <summary>
		/// Original pitch
		/// </summary>
		public byte OriginalPitch;

		/// <summary>
		/// Pitch correction
		/// </summary>
		public sbyte PitchCorrection;

		/// <summary>
		/// SoundFont Sample Link Type
		/// </summary>
		public SFSampleLink SFSampleLink;

		/// <summary>
		/// Sample Link
		/// </summary>
		public ushort SampleLink;

		/// <summary>
		/// The sample name
		/// </summary>
		public string SampleName;

		/// <summary>
		/// Sample Rate
		/// </summary>
		public uint SampleRate;

		/// <summary>
		/// Start offset
		/// </summary>
		public uint Start;

		/// <summary>
		/// Start loop point
		/// </summary>
		public uint StartLoop;

		/// <summary>
		/// <see cref="object.ToString"/>
		/// </summary>
		public override string ToString()
		{
			return SampleName;
		}
	}
}