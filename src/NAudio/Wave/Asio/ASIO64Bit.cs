using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ASIO64Bit
	{
		public uint hi;
		public uint lo;
		// TODO: IMPLEMENT AN EASY WAY TO CONVERT THIS TO double  AND long
	};
}