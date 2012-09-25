using System;
using System.IO;
using System.Text;

namespace Tee.Lib.Riff
{
	public sealed class CkUnknown : Chunk
	{
		public CkUnknown(Byte[] indata, String ID)
		{
			this.ID = ID;
			if (indata == null)
				return;
			var DataReader = new BinaryReader(new MemoryStream(indata), Encoding.ASCII);
			Data = DataReader.ReadBytes(indata.Length);
		}
	}
}