using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NAudio.Wave.MmeInterop;

namespace NAudio.Midi
{
	/// <summary>
	/// Represents a MIDI in device
	/// </summary>
	public class MidiIn : IDisposable
	{
		private readonly MidiInterop.MidiInCallback callback;
		private readonly IntPtr hMidiIn = IntPtr.Zero;
		private bool disposed;

		/// <summary>
		/// Opens a specified MIDI in device
		/// </summary>
		/// <param name="deviceNo">The device number</param>
		public MidiIn(int deviceNo)
		{
			callback = Callback;
			MmException.Try(
				MidiInterop.midiInOpen(out hMidiIn, (IntPtr) deviceNo, callback, IntPtr.Zero, MidiInterop.CALLBACK_FUNCTION),
				"midiInOpen");
		}

		/// <summary>
		/// Gets the number of MIDI input devices available in the system
		/// </summary>
		public static int NumberOfDevices
		{
			get { return MidiInterop.midiInGetNumDevs(); }
		}

		#region IDisposable Members

		/// <summary>
		/// Closes this MIDI in device
		/// </summary>
		public void Dispose()
		{
			GC.KeepAlive(callback);
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		/// <summary>
		/// Called when a MIDI message is received
		/// </summary>
		public event EventHandler<MidiInMessageEventArgs> MessageReceived;

		/// <summary>
		/// An invalid MIDI message
		/// </summary>
		public event EventHandler<MidiInMessageEventArgs> ErrorReceived;

		/// <summary>
		/// Closes this MIDI in device
		/// </summary>
		public void Close()
		{
			Dispose();
		}

		/// <summary>
		/// Start the MIDI in device
		/// </summary>
		public void Start()
		{
			MmException.Try(MidiInterop.midiInStart(hMidiIn), "midiInStart");
		}

		/// <summary>
		/// Stop the MIDI in device
		/// </summary>
		public void Stop()
		{
			MmException.Try(MidiInterop.midiInStop(hMidiIn), "midiInStop");
		}

		/// <summary>
		/// Reset the MIDI in device
		/// </summary>
		public void Reset()
		{
			MmException.Try(MidiInterop.midiInReset(hMidiIn), "midiInReset");
		}

		private void Callback(IntPtr midiInHandle, MidiInterop.MidiInMessage message, IntPtr userData,
		                      IntPtr messageParameter1, IntPtr messageParameter2)
		{
			switch (message)
			{
				case MidiInterop.MidiInMessage.Open:
					// message Parameter 1 & 2 are not used
					break;
				case MidiInterop.MidiInMessage.Data:
					// parameter 1 is packed MIDI message
					// parameter 2 is milliseconds since MidiInStart
					if (MessageReceived != null)
					{
						MessageReceived(this, new MidiInMessageEventArgs(messageParameter1.ToInt32(), messageParameter2.ToInt32()));
					}
					break;
				case MidiInterop.MidiInMessage.Error:
					// parameter 1 is invalid MIDI message
					if (ErrorReceived != null)
					{
						ErrorReceived(this, new MidiInMessageEventArgs(messageParameter1.ToInt32(), messageParameter2.ToInt32()));
					}
					break;
				case MidiInterop.MidiInMessage.Close:
					// message Parameter 1 & 2 are not used
					break;
				case MidiInterop.MidiInMessage.LongData:
					// parameter 1 is pointer to MIDI header
					// parameter 2 is milliseconds since MidiInStart
					break;
				case MidiInterop.MidiInMessage.LongError:
					// parameter 1 is pointer to MIDI header
					// parameter 2 is milliseconds since MidiInStart
					break;
				case MidiInterop.MidiInMessage.MoreData:
					// parameter 1 is packed MIDI message
					// parameter 2 is milliseconds since MidiInStart
					break;
			}
		}

		/// <summary>
		/// Gets the MIDI in device info
		/// </summary>
		public static MidiInCapabilities DeviceInfo(int midiInDeviceNumber)
		{
			var caps = new MidiInCapabilities();
			int structSize = Marshal.SizeOf(caps);
			MmException.Try(MidiInterop.midiInGetDevCaps((IntPtr) midiInDeviceNumber, out caps, structSize), "midiInGetDevCaps");
			return caps;
		}

		/// <summary>
		/// Closes the MIDI out device
		/// </summary>
		/// <param name="disposing">True if called from Dispose</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				//if(disposing) Components.Dispose();
				MidiInterop.midiInClose(hMidiIn);
			}
			disposed = true;
		}

		/// <summary>
		/// Cleanup
		/// </summary>
		~MidiIn()
		{
			Debug.Assert(false, "MIDI In was not finalised");
			Dispose(false);
		}
	}
}