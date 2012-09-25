using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ASIOBufferInfo
	{
		public bool isInput; // on input:  ASIOTrue: input, else output
		public int channelNum; // on input:  channel index
		public IntPtr pBuffer0; // on output: double buffer addresses
		public IntPtr pBuffer1; // on output: double buffer addresses

		public IntPtr Buffer(int bufferIndex)
		{
			return (bufferIndex == 0) ? pBuffer0 : pBuffer1;
		}
	}
}