using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Device State
	/// </summary>
	[Flags]
	public enum DeviceState
	{
		/// <summary>
		/// DEVICE_STATE_ACTIVE
		/// </summary>
		Active = 0x00000001,

		/// <summary>
		/// DEVICE_STATE_UNPLUGGED
		/// </summary>
		Unplugged = 0x00000002,

		/// <summary>
		/// DEVICE_STATE_NOTPRESENT 
		/// </summary>
		NotPresent = 0x00000004,

		/// <summary>
		/// DEVICE_STATEMASK_ALL
		/// </summary>
		All = 0x00000007
	}
}