using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Volume Notification Data
	/// </summary>
	public class AudioVolumeNotificationData
	{
		private readonly float[] _ChannelVolume;
		private readonly int _Channels;
		private readonly Guid _EventContext;
		private readonly float _MasterVolume;
		private readonly bool _Muted;

		/// <summary>
		/// Audio Volume Notification Data
		/// </summary>
		/// <param name="eventContext"></param>
		/// <param name="muted"></param>
		/// <param name="masterVolume"></param>
		/// <param name="channelVolume"></param>
		public AudioVolumeNotificationData(Guid eventContext, bool muted, float masterVolume, float[] channelVolume)
		{
			_EventContext = eventContext;
			_Muted = muted;
			_MasterVolume = masterVolume;
			_Channels = channelVolume.Length;
			_ChannelVolume = channelVolume;
		}

		/// <summary>
		/// Event Context
		/// </summary>
		public Guid EventContext
		{
			get { return _EventContext; }
		}

		/// <summary>
		/// Muted
		/// </summary>
		public bool Muted
		{
			get { return _Muted; }
		}

		/// <summary>
		/// Master Volume
		/// </summary>
		public float MasterVolume
		{
			get { return _MasterVolume; }
		}

		/// <summary>
		/// Channels
		/// </summary>
		public int Channels
		{
			get { return _Channels; }
		}

		/// <summary>
		/// Channel Volume
		/// </summary>
		public float[] ChannelVolume
		{
			get { return _ChannelVolume; }
		}
	}
}