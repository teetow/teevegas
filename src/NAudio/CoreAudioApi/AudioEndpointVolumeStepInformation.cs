using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Endpoint Volume Step Information
	/// </summary>
	public class AudioEndpointVolumeStepInformation
	{
		private readonly uint _Step;
		private readonly uint _StepCount;

		internal AudioEndpointVolumeStepInformation(IAudioEndpointVolume parent)
		{
			Marshal.ThrowExceptionForHR(parent.GetVolumeStepInfo(out _Step, out _StepCount));
		}

		/// <summary>
		/// Step
		/// </summary>
		public uint Step
		{
			get { return _Step; }
		}

		/// <summary>
		/// StepCount
		/// </summary>
		public uint StepCount
		{
			get { return _StepCount; }
		}
	}
}