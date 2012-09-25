using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Meter Information
	/// </summary>
	public class AudioMeterInformation
	{
		private readonly IAudioMeterInformation _AudioMeterInformation;
		private readonly AudioMeterInformationChannels _Channels;
		private readonly EEndpointHardwareSupport _HardwareSupport;

		internal AudioMeterInformation(IAudioMeterInformation realInterface)
		{
			int HardwareSupp;

			_AudioMeterInformation = realInterface;
			Marshal.ThrowExceptionForHR(_AudioMeterInformation.QueryHardwareSupport(out HardwareSupp));
			_HardwareSupport = (EEndpointHardwareSupport) HardwareSupp;
			_Channels = new AudioMeterInformationChannels(_AudioMeterInformation);
		}

		/// <summary>
		/// Peak Values
		/// </summary>
		public AudioMeterInformationChannels PeakValues
		{
			get { return _Channels; }
		}

		/// <summary>
		/// Hardware Support
		/// </summary>
		public EEndpointHardwareSupport HardwareSupport
		{
			get { return _HardwareSupport; }
		}

		/// <summary>
		/// Master Peak Value
		/// </summary>
		public float MasterPeakValue
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(_AudioMeterInformation.GetPeakValue(out result));
				return result;
			}
		}
	}
}