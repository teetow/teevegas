using System;
using NAudio.Wave.WaveFormats;

namespace NAudio.Wave.MmeInterop
{
	/// <summary>
	/// Wave Callback Info
	/// </summary>
	public class WaveCallbackInfo
	{
		private WaveWindow waveOutWindow;
		private WaveWindowNative waveOutWindowNative;

		private WaveCallbackInfo(WaveCallbackStrategy strategy, IntPtr handle)
		{
			Strategy = strategy;
			Handle = handle;
		}

		/// <summary>
		/// Callback Strategy
		/// </summary>
		public WaveCallbackStrategy Strategy { get; private set; }

		/// <summary>
		/// Window Handle (if applicable)
		/// </summary>
		public IntPtr Handle { get; private set; }

		/// <summary>
		/// Sets up a new WaveCallbackInfo for function callbacks
		/// </summary>
		public static WaveCallbackInfo FunctionCallback()
		{
			return new WaveCallbackInfo(WaveCallbackStrategy.FunctionCallback, IntPtr.Zero);
		}

		/// <summary>
		/// Sets up a new WaveCallbackInfo to use a New Window
		/// IMPORTANT: only use this on the GUI thread
		/// </summary>
		public static WaveCallbackInfo NewWindow()
		{
			return new WaveCallbackInfo(WaveCallbackStrategy.NewWindow, IntPtr.Zero);
		}

		/// <summary>
		/// Sets up a new WaveCallbackInfo to use an existing window
		/// IMPORTANT: only use this on the GUI thread
		/// </summary>
		public static WaveCallbackInfo ExistingWindow(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException("Handle cannot be zero");
			}
			return new WaveCallbackInfo(WaveCallbackStrategy.ExistingWindow, handle);
		}

		internal void Connect(WaveInterop.WaveCallback callback)
		{
			if (Strategy == WaveCallbackStrategy.NewWindow)
			{
				waveOutWindow = new WaveWindow(callback);
				waveOutWindow.CreateControl();
				Handle = waveOutWindow.Handle;
			}
			else if (Strategy == WaveCallbackStrategy.ExistingWindow)
			{
				waveOutWindowNative = new WaveWindowNative(callback);
				waveOutWindowNative.AssignHandle(Handle);
			}
		}

		internal MmResult WaveOutOpen(out IntPtr waveOutHandle, int deviceNumber, WaveFormat waveFormat,
		                              WaveInterop.WaveCallback callback)
		{
			MmResult result;
			if (Strategy == WaveCallbackStrategy.FunctionCallback)
			{
				result = WaveInterop.waveOutOpen(out waveOutHandle, (IntPtr) deviceNumber, waveFormat, callback, IntPtr.Zero,
				                                 WaveInterop.CallbackFunction);
			}
			else
			{
				result = WaveInterop.waveOutOpenWindow(out waveOutHandle, (IntPtr) deviceNumber, waveFormat, Handle, IntPtr.Zero,
				                                       WaveInterop.CallbackWindow);
			}
			return result;
		}

		internal MmResult WaveInOpen(out IntPtr waveInHandle, int deviceNumber, WaveFormat waveFormat,
		                             WaveInterop.WaveCallback callback)
		{
			MmResult result;
			if (Strategy == WaveCallbackStrategy.FunctionCallback)
			{
				result = WaveInterop.waveInOpen(out waveInHandle, (IntPtr) deviceNumber, waveFormat, callback, IntPtr.Zero,
				                                WaveInterop.CallbackFunction);
			}
			else
			{
				result = WaveInterop.waveInOpenWindow(out waveInHandle, (IntPtr) deviceNumber, waveFormat, Handle, IntPtr.Zero,
				                                      WaveInterop.CallbackWindow);
			}
			return result;
		}

		internal void Disconnect()
		{
			if (waveOutWindow != null)
			{
				waveOutWindow.Close();
				waveOutWindow = null;
			}
			if (waveOutWindowNative != null)
			{
				waveOutWindowNative.ReleaseHandle();
				waveOutWindowNative = null;
			}
		}
	}
}