using System;

namespace NAudio.Wave.Asio
{
	/// <summary>
	/// ASIODriverCapability holds all the information from the ASIODriver.
	/// Use ASIODriverExt to get the Capabilities
	/// </summary>
	internal class ASIODriverCapability
	{
		public int BufferGranularity;
		public int BufferMaxSize;
		public int BufferMinSize;
		public int BufferPreferredSize;
		public String DriverName;
		public ASIOChannelInfo[] InputChannelInfos;
		public int InputLatency;

		public int NbInputChannels;
		public int NbOutputChannels;
		public ASIOChannelInfo[] OutputChannelInfos;

		public int OutputLatency;

		public double SampleRate;
	}
}