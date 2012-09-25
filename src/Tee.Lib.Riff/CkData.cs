using System;
using System.IO;
using System.Text;
using Tee.Lib.Riff.Riff;

namespace Tee.Lib.Riff
{
	public sealed class CkData : Chunk
	{
		public CkData(Byte[] indata)
		{
			ID = CkType.data;
			var dataReader = new BinaryReader(new MemoryStream(indata), Encoding.ASCII);
			Data = dataReader.ReadBytes(indata.Length);
		}
	}
}