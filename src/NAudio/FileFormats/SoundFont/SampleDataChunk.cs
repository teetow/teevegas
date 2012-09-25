using System;

namespace NAudio.FileFormats.SoundFont
{
	internal class SampleDataChunk
	{
		private readonly byte[] sampleData;

		public SampleDataChunk(RiffChunk chunk)
		{
			string header = chunk.ReadChunkID();
			if (header != "sdta")
			{
				throw new ApplicationException(String.Format("Not a sample data chunk ({0})", header));
			}
			sampleData = chunk.GetData();
		}

		public byte[] SampleData
		{
			get { return sampleData; }
		}
	}
}

// end of namespace