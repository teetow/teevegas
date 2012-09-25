using System;

namespace NAudio.Midi
{
	/// <summary>
	/// MIDI In Message Information
	/// </summary>
	public class MidiInMessageEventArgs : EventArgs
	{
		private readonly int message;
		private readonly MidiEvent midiEvent;
		private readonly int timestamp;

		/// <summary>
		/// Create a new MIDI In Message EventArgs
		/// </summary>
		/// <param name="message"></param>
		/// <param name="timestamp"></param>
		public MidiInMessageEventArgs(int message, int timestamp)
		{
			this.message = message;
			this.timestamp = timestamp;
			try
			{
				midiEvent = MidiEvent.FromRawMessage(message);
			}
			catch (Exception)
			{
				// don't worry too much - might be an invalid message
			}
		}

		/// <summary>
		/// The Raw message received from the MIDI In API
		/// </summary>
		public int RawMessage
		{
			get { return message; }
		}

		/// <summary>
		/// The raw message interpreted as a MidiEvent
		/// </summary>
		public MidiEvent MidiEvent
		{
			get { return midiEvent; }
		}

		/// <summary>
		/// The timestamp in milliseconds for this message
		/// </summary>
		public int Timestamp
		{
			get { return timestamp; }
		}
	}
}