namespace NAudio.Wave.WaveStreams
{
	internal class Mp3Index
	{
		public long FilePosition { get; set; }
		public int SamplePosition { get; set; }
		public int SampleCount { get; set; }
		public int ByteCount { get; set; }
	}
}