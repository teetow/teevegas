using System;
using System.Runtime.InteropServices;
using NAudio.Mixer;
using NAudio.Wave.WaveFormats;
using NAudio.Wave.WaveInputs;
using NAudio.Wave.WaveStreams;

namespace NAudio.Wave.MmeInterop
{
	/// <summary>
	/// Allows recording using the Windows waveIn APIs
	/// Events are raised as recorded buffers are made available
	/// </summary>
	public class WaveIn : IWaveIn
	{
		private readonly WaveInterop.WaveCallback callback;
		private WaveInBuffer[] buffers;
		private WaveCallbackInfo callbackInfo;
		private volatile bool recording;
		private IntPtr waveInHandle;

		/// <summary>
		/// Prepares a Wave input device for recording
		/// </summary>
		public WaveIn()
			: this(WaveCallbackInfo.NewWindow())
		{
		}

		/// <summary>
		/// Creates a WaveIn device using the specified window handle for callbacks
		/// </summary>
		/// <param name="windowHandle">A valid window handle</param>
		public WaveIn(IntPtr windowHandle)
			: this(WaveCallbackInfo.ExistingWindow(windowHandle))
		{
		}

		/// <summary>
		/// Prepares a Wave input device for recording
		/// </summary>
		public WaveIn(WaveCallbackInfo callbackInfo)
		{
			DeviceNumber = 0;
			WaveFormat = new WaveFormat(8000, 16, 1);
			BufferMilliseconds = 100;
			NumberOfBuffers = 3;
			callback = Callback;
			this.callbackInfo = callbackInfo;
			callbackInfo.Connect(callback);
		}

		/// <summary>
		/// Returns the number of Wave In devices available in the system
		/// </summary>
		public static int DeviceCount
		{
			get { return WaveInterop.waveInGetNumDevs(); }
		}

		/// <summary>
		/// Milliseconds for the buffer. Recommended value is 100ms
		/// </summary>
		public int BufferMilliseconds { get; set; }

		/// <summary>
		/// Number of Buffers to use (usually 2 or 3)
		/// </summary>
		public int NumberOfBuffers { get; set; }

		/// <summary>
		/// The device number to use
		/// </summary>
		public int DeviceNumber { get; set; }

		#region IWaveIn Members

		/// <summary>
		/// Indicates recorded data is available 
		/// </summary>
		public event EventHandler<WaveInEventArgs> DataAvailable;

		/// <summary>
		/// Indicates that all recorded data has now been received.
		/// </summary>
		public event EventHandler RecordingStopped;

		/// <summary>
		/// Start recording
		/// </summary>
		public void StartRecording()
		{
			OpenWaveInDevice();
			if (recording)
				throw new InvalidOperationException("Already recording");
			MmException.Try(WaveInterop.waveInStart(waveInHandle), "waveInStart");
			recording = true;
		}

		/// <summary>
		/// Stop recording
		/// </summary>
		public void StopRecording()
		{
			recording = false;
			MmException.Try(WaveInterop.waveInStop(waveInHandle), "waveInStop");
			//MmException.Try(WaveInterop.waveInReset(waveInHandle), "waveInReset");      
			// Don't actually close yet so we get the last buffer
		}

		/// <summary>
		/// WaveFormat we are recording in
		/// </summary>
		public WaveFormat WaveFormat { get; set; }

		/// <summary>
		/// Dispose method
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		/// <summary>
		/// Retrieves the capabilities of a waveIn device
		/// </summary>
		/// <param name="devNumber">Device to test</param>
		/// <returns>The WaveIn device capabilities</returns>
		public static WaveInCapabilities GetCapabilities(int devNumber)
		{
			var caps = new WaveInCapabilities();
			int structSize = Marshal.SizeOf(caps);
			MmException.Try(WaveInterop.waveInGetDevCaps((IntPtr) devNumber, out caps, structSize), "waveInGetDevCaps");
			return caps;
		}

		private void CreateBuffers()
		{
			// Default to three buffers of 100ms each
			int bufferSize = BufferMilliseconds*WaveFormat.AverageBytesPerSecond/1000;

			buffers = new WaveInBuffer[NumberOfBuffers];
			for (int n = 0; n < buffers.Length; n++)
			{
				buffers[n] = new WaveInBuffer(waveInHandle, bufferSize);
			}
		}

		/// <summary>
		/// Called when we get a new buffer of recorded data
		/// </summary>
		private void Callback(IntPtr waveInHandle, WaveInterop.WaveMessage message, IntPtr userData, WaveHeader waveHeader,
		                      IntPtr reserved)
		{
			if (message == WaveInterop.WaveMessage.WaveInData)
			{
				var hBuffer = (GCHandle) waveHeader.userData;
				var buffer = (WaveInBuffer) hBuffer.Target;

				if (DataAvailable != null)
				{
					DataAvailable(this, new WaveInEventArgs(buffer.Data, buffer.BytesRecorded));
				}
				if (recording)
				{
					buffer.Reuse();
				}
				else
				{
					if (RecordingStopped != null)
					{
						RecordingStopped(this, EventArgs.Empty);
					}
				}
			}
		}

		private void OpenWaveInDevice()
		{
			CloseWaveInDevice();
			MmResult result;
			result = callbackInfo.WaveInOpen(out waveInHandle, DeviceNumber, WaveFormat, callback);
			MmException.Try(result, "waveInOpen");
			CreateBuffers();
		}

		/// <summary>
		/// Dispose pattern
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (recording)
					StopRecording();
				CloseWaveInDevice();
				if (callbackInfo != null)
				{
					callbackInfo.Disconnect();
					callbackInfo = null;
				}
			}
		}

		private void CloseWaveInDevice()
		{
			// Some drivers need the reset to properly release buffers
			WaveInterop.waveInReset(waveInHandle);
			if (buffers != null)
			{
				for (int n = 0; n < buffers.Length; n++)
				{
					buffers[n].Dispose();
				}
				buffers = null;
			}
			WaveInterop.waveInClose(waveInHandle);
			waveInHandle = IntPtr.Zero;
		}

		/// <summary>
		/// Microphone Level
		/// </summary>
		public MixerLine GetMixerLine()
		{
			// TODO use mixerGetID instead to see if this helps with XP
			MixerLine mixerLine;
			if (waveInHandle != IntPtr.Zero)
			{
				mixerLine = new MixerLine(waveInHandle, 0, MixerFlags.WaveInHandle);
			}
			else
			{
				mixerLine = new MixerLine((IntPtr) DeviceNumber, 0, MixerFlags.WaveIn);
			}
			return mixerLine;
		}
	}
}