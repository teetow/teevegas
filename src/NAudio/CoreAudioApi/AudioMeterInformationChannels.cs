using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Meter Information Channels
	/// </summary>
	public class AudioMeterInformationChannels
	{
		private readonly IAudioMeterInformation _AudioMeterInformation;

		internal AudioMeterInformationChannels(IAudioMeterInformation parent)
		{
			_AudioMeterInformation = parent;
		}

		/// <summary>
		/// Metering Channel Count
		/// </summary>
		public int Count
		{
			get
			{
				int result;
				Marshal.ThrowExceptionForHR(_AudioMeterInformation.GetMeteringChannelCount(out result));
				return result;
			}
		}

		/// <summary>
		/// Get Peak value
		/// </summary>
		/// <param name="index">Channel index</param>
		/// <returns>Peak value</returns>
		public float this[int index]
		{
			get
			{
				var peakValues = new float[Count];
				GCHandle Params = GCHandle.Alloc(peakValues, GCHandleType.Pinned);
				Marshal.ThrowExceptionForHR(_AudioMeterInformation.GetChannelsPeakValues(peakValues.Length,
				                                                                         Params.AddrOfPinnedObject()));
				Params.Free();
				return peakValues[index];
			}
		}
	}
}