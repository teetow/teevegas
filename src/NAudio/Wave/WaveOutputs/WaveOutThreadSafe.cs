using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using NAudio.Wave.MmeInterop;
using NAudio.Wave.WaveStreams;

namespace NAudio.Wave.WaveOutputs
{
	// BIG TODO: how to report errors back from functions?

	/// <summary>
	/// Represents a wave out device
	/// </summary>
	public class WaveOutThreadSafe : IWavePlayer
	{
		private readonly Queue<WaveOutAction> actionQueue;
		private readonly WaveInterop.WaveCallback callback;
		private readonly int desiredLatency;
		private readonly int devNumber;
		private readonly Thread waveOutThread;
		private readonly AutoResetEvent workAvailable;
		private WaveOutBuffer[] buffers;
		private bool buffersQueued;
		private IntPtr hWaveOut;
		private int numBuffers;
		private PlaybackState playbackState;
		private float volume = 1;
		private IWaveProvider waveStream;

		/// <summary>
		/// Opens a WaveOut device
		/// </summary>
		/// <param name="devNumber">This is the device number to open. 
		/// This must be between 0 and <see>DeviceCount</see> - 1.</param>
		/// <param name="desiredLatency">The number of milliseconds of audio to read before 
		/// streaming to the audio device. This will be broken into 3 buffers</param>
		public WaveOutThreadSafe(int devNumber, int desiredLatency)
		{
			this.devNumber = devNumber;
			this.desiredLatency = desiredLatency;
			callback = Callback;
			actionQueue = new Queue<WaveOutAction>();
			workAvailable = new AutoResetEvent(false);
			waveOutThread = new Thread(ThreadProc);
			waveOutThread.Start();
		}

		/// <summary>
		/// Returns the number of Wave Out devices available in the system
		/// </summary>
		public static Int32 DeviceCount
		{
			get { return WaveInterop.waveOutGetNumDevs(); }
		}

		#region IWavePlayer Members

		/// <summary>
		/// Playback stopped
		/// </summary>
		public event EventHandler PlaybackStopped;

		/// <summary>
		/// Initialises the WaveOut device
		/// </summary>
		/// <param name="waveProvider">Wave provider to play</param>
		public void Init(IWaveProvider waveProvider)
		{
			if (Thread.CurrentThread.ManagedThreadId != waveOutThread.ManagedThreadId)
			{
				lock (actionQueue)
				{
					actionQueue.Enqueue(new WaveOutAction(WaveOutFunction.Init, waveStream));
					workAvailable.Set();
				}
				return;
			}

			waveStream = waveProvider;
			int bufferSize = waveProvider.WaveFormat.ConvertLatencyToByteSize(desiredLatency);
			//waveStream.GetReadSize((desiredLatency + 2) / 3);
			numBuffers = 3;

			MmException.Try(
				WaveInterop.waveOutOpen(out hWaveOut, (IntPtr) devNumber, waveStream.WaveFormat, callback, IntPtr.Zero,
				                        WaveInterop.CallbackFunction), "waveOutOpen");

			buffers = new WaveOutBuffer[numBuffers];
			playbackState = PlaybackState.Stopped;
			var waveOutLock = new object();
			for (int n = 0; n < numBuffers; n++)
			{
				buffers[n] = new WaveOutBuffer(hWaveOut, bufferSize, waveStream, waveOutLock);
			}
		}

		/// <summary>
		/// Start playing the audio from the WaveStream
		/// </summary>
		public void Play()
		{
			if (playbackState != PlaybackState.Playing)
			{
				playbackState = PlaybackState.Playing;

				if (Thread.CurrentThread.ManagedThreadId != waveOutThread.ManagedThreadId)
				{
					lock (actionQueue)
					{
						actionQueue.Enqueue(new WaveOutAction(WaveOutFunction.Play, null));
						workAvailable.Set();
					}
					return;
				}
			}

			if (!buffersQueued)
			{
				Pause(); // to avoid a deadlock - we don't want two waveOutWrites at once
				for (int n = 0; n < numBuffers; n++)
				{
					Debug.Assert(buffers[n].InQueue == false, "Adding a buffer that was already queued on play");
					buffers[n].OnDone();
				}
				buffersQueued = true;
			}
			Resume();
		}

		/// <summary>
		/// Pause the audio
		/// </summary>
		public void Pause()
		{
			if (Thread.CurrentThread.ManagedThreadId != waveOutThread.ManagedThreadId)
			{
				lock (actionQueue)
				{
					actionQueue.Enqueue(new WaveOutAction(WaveOutFunction.Pause, null));
					workAvailable.Set();
				}
				return;
			}

			MmResult result = WaveInterop.waveOutPause(hWaveOut);
			if (result != MmResult.NoError)
				throw new MmException(result, "waveOutPause");
			playbackState = PlaybackState.Paused;
		}

		/// <summary>
		/// Stop and reset the WaveOut device
		/// </summary>
		public void Stop()
		{
			if (Thread.CurrentThread.ManagedThreadId != waveOutThread.ManagedThreadId)
			{
				lock (actionQueue)
				{
					actionQueue.Enqueue(new WaveOutAction(WaveOutFunction.Stop, null));
					workAvailable.Set();
				}
				return;
			}

			playbackState = PlaybackState.Stopped;
			buffersQueued = false;
			MmResult result = WaveInterop.waveOutReset(hWaveOut);
			if (result != MmResult.NoError)
				throw new MmException(result, "waveOutReset");
		}

		/// <summary>
		/// Playback State
		/// </summary>
		public PlaybackState PlaybackState
		{
			get { return playbackState; }
		}


		/// <summary>
		/// Volume for this device 1.0 is full scale
		/// </summary>
		public float Volume
		{
			get { return volume; }
			set
			{
				volume = value;
				float left = volume;
				float right = volume;
				int stereoVolume = (int) (left*0xFFFF) + ((int) (right*0xFFFF) << 16);
				lock (actionQueue)
				{
					actionQueue.Enqueue(new WaveOutAction(WaveOutFunction.SetVolume, stereoVolume));
					workAvailable.Set();
				}
			}
		}

		#endregion

		/// <summary>
		/// Retrieves the capabilities of a waveOut device
		/// </summary>
		/// <param name="devNumber">Device to test</param>
		/// <returns>The WaveOut device capabilities</returns>
		public static WaveOutCapabilities GetCapabilities(int devNumber)
		{
			var caps = new WaveOutCapabilities();
			int structSize = Marshal.SizeOf(caps);
			MmException.Try(WaveInterop.waveOutGetDevCaps((IntPtr) devNumber, out caps, structSize), "waveOutGetDevCaps");
			return caps;
		}

		private void ThreadProc()
		{
			while (true)
			{
				workAvailable.WaitOne();
				bool loop = true;
				while (loop)
				{
					WaveOutAction waveOutAction = null;
					lock (actionQueue)
					{
						if (actionQueue.Count > 0)
						{
							waveOutAction = actionQueue.Dequeue();
						}
					}
					if (waveOutAction != null)
					{
						try
						{
							switch (waveOutAction.Function)
							{
								case WaveOutFunction.Init:
									Init((WaveStream) waveOutAction.Data);
									break;
								case WaveOutFunction.Play:
									Play();
									break;
								case WaveOutFunction.Pause:
									Pause();
									break;
								case WaveOutFunction.Resume:
									Resume();
									break;
								case WaveOutFunction.Stop:
									Stop();
									break;
								case WaveOutFunction.BufferDone:
									OnBufferDone((WaveOutBuffer) waveOutAction.Data);
									break;
								case WaveOutFunction.Exit:
									Exit();
									return;
								case WaveOutFunction.SetVolume:
									SetVolume((int) waveOutAction.Data);
									break;
							}
						}
						catch (Exception e)
						{
							string s = e.ToString();
							if (waveOutAction.Function == WaveOutFunction.Exit)
							{
								return;
							}
						}
					}
					else
					{
						loop = false;
					}
				}
			}
		}

		/// <summary>
		/// Resume playing after a pause from the same position
		/// </summary>
		public void Resume()
		{
			if (Thread.CurrentThread.ManagedThreadId != waveOutThread.ManagedThreadId)
			{
				lock (actionQueue)
				{
					actionQueue.Enqueue(new WaveOutAction(WaveOutFunction.Resume, null));
					workAvailable.Set();
				}
				return;
			}

			MmResult result = WaveInterop.waveOutRestart(hWaveOut);
			if (result != MmResult.NoError)
				throw new MmException(result, "waveOutRestart");
			playbackState = PlaybackState.Playing;
		}

		private void Exit()
		{
			if (hWaveOut != IntPtr.Zero)
			{
				Stop();
				WaveInterop.waveOutClose(hWaveOut);
				hWaveOut = IntPtr.Zero;
			}

			if (buffers != null)
			{
				for (int n = 0; n < numBuffers; n++)
				{
					buffers[n].Dispose();
				}
				buffers = null;
			}
		}

		private void SetVolume(int stereoVolume)
		{
			MmException.Try(WaveInterop.waveOutSetVolume(hWaveOut, stereoVolume), "waveOutSetVolume");
		}

		// made non-static so that playing can be stopped here
		private void Callback(IntPtr hWaveOut, WaveInterop.WaveMessage uMsg, IntPtr dwUser, WaveHeader wavhdr,
		                      IntPtr dwReserved)
		{
			if (uMsg == WaveInterop.WaveMessage.WaveOutDone)
			{
				// check that we're not here through pressing stop
				var hBuffer = (GCHandle) wavhdr.userData;
				var buffer = (WaveOutBuffer) hBuffer.Target;

				lock (actionQueue)
				{
					actionQueue.Enqueue(new WaveOutAction(WaveOutFunction.BufferDone, buffer));
					workAvailable.Set();
				}

				// n.b. this was wrapped in an exception handler, but bug should be fixed now
			}
		}

		private void OnBufferDone(WaveOutBuffer buffer)
		{
			if (playbackState == PlaybackState.Playing)
			{
				if (!buffer.OnDone())
				{
					playbackState = PlaybackState.Stopped;
					RaisePlaybackStopped();
				}
			}
		}

		private void RaisePlaybackStopped()
		{
			if (PlaybackStopped != null)
			{
				PlaybackStopped(this, EventArgs.Empty);
			}
		}

		#region Dispose Pattern

		/// <summary>
		/// Closes this WaveOut device
		/// </summary>
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			Dispose(true);
		}

		/// <summary>
		/// Closes the WaveOut device and disposes of buffers
		/// </summary>
		/// <param name="disposing">True if called from <see>Dispose</see></param>
		protected void Dispose(bool disposing)
		{
			lock (actionQueue)
			{
				actionQueue.Clear();
				actionQueue.Enqueue(new WaveOutAction(WaveOutFunction.Exit, null));
				workAvailable.Set();
			}
			waveOutThread.Join();
		}

		/// <summary>
		/// Finalizer. Only called when user forgets to call <see>Dispose</see>
		/// </summary>
		~WaveOutThreadSafe()
		{
			Debug.Assert(false, "WaveOut device was not closed");
			Dispose(false);
		}

		#endregion
	}
}