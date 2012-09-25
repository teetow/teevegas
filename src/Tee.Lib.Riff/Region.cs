using System;

namespace Tee.Lib.Riff
{
	public class Region : Marker
	{
		public LiCkLtxt LtxtChunk;

		public Region(CuePoint CuePoint)
			: base(CuePoint)
		{
		}

		public UInt32 Length
		{
			get
			{
				if (LtxtChunk == null)
					return 0;
				return LtxtChunk.SampleLength;
			}
			set
			{
				if (LtxtChunk == null)
					return;
				LtxtChunk.SampleLength = value;
			}
		}
	}
}