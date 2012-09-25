using System;

namespace NAudio.Mixer
{
	/// <summary>
	/// Custom Mixer control
	/// </summary>
	public class CustomMixerControl : MixerControl
	{
		internal CustomMixerControl(MixerInterop.MIXERCONTROL mixerControl, IntPtr mixerHandle, MixerFlags mixerHandleType,
		                            int nChannels)
		{
			this.mixerControl = mixerControl;
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.nChannels = nChannels;
			mixerControlDetails = new MixerInterop.MIXERCONTROLDETAILS();
			GetControlDetails();
		}

		/// <summary>
		/// Get the data for this custom control
		/// </summary>
		/// <param name="pDetails">pointer to memory to receive data</param>
		protected override void GetDetails(IntPtr pDetails)
		{
		}

		// TODO: provide a way of getting / setting data
	}
}