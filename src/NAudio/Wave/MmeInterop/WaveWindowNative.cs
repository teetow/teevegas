using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NAudio.Wave.MmeInterop
{
	internal class WaveWindowNative : NativeWindow
	{
		private readonly WaveInterop.WaveCallback waveCallback;

		public WaveWindowNative(WaveInterop.WaveCallback waveCallback)
		{
			this.waveCallback = waveCallback;
		}

		protected override void WndProc(ref Message m)
		{
			var message = (WaveInterop.WaveMessage) m.Msg;

			switch (message)
			{
				case WaveInterop.WaveMessage.WaveOutDone:
				case WaveInterop.WaveMessage.WaveInData:
					IntPtr hOutputDevice = m.WParam;
					var waveHeader = new WaveHeader();
					Marshal.PtrToStructure(m.LParam, waveHeader);
					waveCallback(hOutputDevice, message, IntPtr.Zero, waveHeader, IntPtr.Zero);
					break;
				case WaveInterop.WaveMessage.WaveOutOpen:
				case WaveInterop.WaveMessage.WaveOutClose:
				case WaveInterop.WaveMessage.WaveInClose:
				case WaveInterop.WaveMessage.WaveInOpen:
					waveCallback(m.WParam, message, IntPtr.Zero, null, IntPtr.Zero);
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}
	}
}