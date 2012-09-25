using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Endpoint Volume Channel
	/// </summary>
	public class AudioEndpointVolumeChannel
	{
		private readonly IAudioEndpointVolume _AudioEndpointVolume;
		private readonly uint _Channel;

		internal AudioEndpointVolumeChannel(IAudioEndpointVolume parent, int channel)
		{
			_Channel = (uint) channel;
			_AudioEndpointVolume = parent;
		}

		/// <summary>
		/// Volume Level
		/// </summary>
		public float VolumeLevel
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(_AudioEndpointVolume.GetChannelVolumeLevel(_Channel, out result));
				return result;
			}
			set { Marshal.ThrowExceptionForHR(_AudioEndpointVolume.SetChannelVolumeLevel(_Channel, value, Guid.Empty)); }
		}

		/// <summary>
		/// Volume Level Scalar
		/// </summary>
		public float VolumeLevelScalar
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(_AudioEndpointVolume.GetChannelVolumeLevelScalar(_Channel, out result));
				return result;
			}
			set { Marshal.ThrowExceptionForHR(_AudioEndpointVolume.SetChannelVolumeLevelScalar(_Channel, value, Guid.Empty)); }
		}
	}
}