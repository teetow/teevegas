using System;
using System.IO;
using System.Text;

namespace Tee.Lib.Riff
{
	/// <summary>
	/// EndTimecode
	/// </summary>
	public class LiCkInfoTCDO : LabeledListChunk
	{
		public LiCkInfoTCDO(Byte[] Indata)
		{
			var DataReader = new BinaryReader(new MemoryStream(Indata), Encoding.ASCII);
			Text = DataReader.ReadStringNullTerm();
		}

		public uint Position
		{
			get
			{
				var posStr = new string(Encoding.ASCII.GetChars(Data));
				uint pos;
				if (uint.TryParse(posStr, out pos))
				{
					return pos;
				}
				return 0;
			}
			set
			{
				string valStr = value.ToString();
				byte[] val = Encoding.ASCII.GetBytes(valStr);
				Data = val;
			}
		}
	}
}