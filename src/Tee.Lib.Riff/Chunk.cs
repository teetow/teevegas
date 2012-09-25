using System;

namespace Tee.Lib.Riff
{
	public abstract class Chunk
	{
		public String ID;

		public virtual UInt32 Size
		{
			get
			{
				if (Data == null)
					return 0;
				var size = (UInt32) Data.Length;
				if (size%2 != 0) size++;
				return size;
			}
		}

		public virtual Byte[] Data { get; set; }
	}
}