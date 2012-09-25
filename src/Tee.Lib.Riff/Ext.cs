using System.IO;
using System.Text;

namespace Tee.Lib.Riff
{
	public static class Ext
	{
		public static string ReadStringNullTerm(this BinaryReader Reader)
		{
			var str = new StringBuilder();
			while (Reader.BaseStream.Position < Reader.BaseStream.Length && Reader.PeekChar() != '\0')
			{
				str.Append(Reader.ReadChar());
			}
			return str.ToString();
		}
	}
}