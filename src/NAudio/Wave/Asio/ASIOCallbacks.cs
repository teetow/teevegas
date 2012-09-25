using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Asio
{
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ASIOCallbacks
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate void ASIOBufferSwitchCallBack(int doubleBufferIndex, bool directProcess);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate void ASIOSampleRateDidChangeCallBack(double sRate);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate int ASIOAsioMessageCallBack(ASIOMessageSelector selector, int value, IntPtr message, IntPtr opt);

		// return ASIOTime*
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		internal delegate IntPtr ASIOBufferSwitchTimeInfoCallBack(
			IntPtr asioTimeParam, int doubleBufferIndex, bool directProcess);

		//        internal delegate IntPtr ASIOBufferSwitchTimeInfoCallBack(ref ASIOTime asioTimeParam, int doubleBufferIndex, bool directProcess);

		//	void (*bufferSwitch) (long doubleBufferIndex, ASIOBool directProcess);
		public ASIOBufferSwitchCallBack pbufferSwitch;
		//    void (*sampleRateDidChange) (ASIOSampleRate sRate);
		public ASIOSampleRateDidChangeCallBack psampleRateDidChange;
		//	long (*asioMessage) (long selector, long value, void* message, double* opt);
		public ASIOAsioMessageCallBack pasioMessage;
		//	ASIOTime* (*bufferSwitchTimeInfo) (ASIOTime* params, long doubleBufferIndex, ASIOBool directProcess);
		public ASIOBufferSwitchTimeInfoCallBack pbufferSwitchTimeInfo;
	}
}