using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Endpoint Volume Volume Range
	/// </summary>
	public class AudioEndpointVolumeVolumeRange
	{
		private readonly float _VolumeIncrementdB;
		private readonly float _VolumeMaxdB;
		private readonly float _VolumeMindB;

		internal AudioEndpointVolumeVolumeRange(IAudioEndpointVolume parent)
		{
			Marshal.ThrowExceptionForHR(parent.GetVolumeRange(out _VolumeMindB, out _VolumeMaxdB, out _VolumeIncrementdB));
		}

		/// <summary>
		/// Minimum Decibels
		/// </summary>
		public float MinDecibels
		{
			get { return _VolumeMindB; }
		}

		/// <summary>
		/// Maximum Decibels
		/// </summary>
		public float MaxDecibels
		{
			get { return _VolumeMaxdB; }
		}

		/// <summary>
		/// Increment Decibels
		/// </summary>
		public float IncrementDecibels
		{
			get { return _VolumeIncrementdB; }
		}
	}
}