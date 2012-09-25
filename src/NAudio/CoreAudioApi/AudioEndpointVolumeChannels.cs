using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Endpoint Volume Channels
	/// </summary>
	public class AudioEndpointVolumeChannels
	{
		private readonly IAudioEndpointVolume _AudioEndPointVolume;
		private readonly AudioEndpointVolumeChannel[] _Channels;

		internal AudioEndpointVolumeChannels(IAudioEndpointVolume parent)
		{
			int ChannelCount;
			_AudioEndPointVolume = parent;

			ChannelCount = Count;
			_Channels = new AudioEndpointVolumeChannel[ChannelCount];
			for (int i = 0; i < ChannelCount; i++)
			{
				_Channels[i] = new AudioEndpointVolumeChannel(_AudioEndPointVolume, i);
			}
		}

		/// <summary>
		/// Channel Count
		/// </summary>
		public int Count
		{
			get
			{
				int result;
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume.GetChannelCount(out result));
				return result;
			}
		}

		/// <summary>
		/// Indexer - get a specific channel
		/// </summary>
		public AudioEndpointVolumeChannel this[int index]
		{
			get { return _Channels[index]; }
		}
	}
}