using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Endpoint Hardware Support
	/// </summary>
	[Flags]
	public enum EEndpointHardwareSupport
	{
		/// <summary>
		/// Volume
		/// </summary>
		Volume = 0x00000001,

		/// <summary>
		/// Mute
		/// </summary>
		Mute = 0x00000002,

		/// <summary>
		/// Meter
		/// </summary>
		Meter = 0x00000004
	}
}