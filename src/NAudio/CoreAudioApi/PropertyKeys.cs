using System;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Property Keys
	/// </summary>
	public static class PropertyKeys
	{
		/// <summary>
		/// PKEY_DeviceInterface_FriendlyName
		/// </summary>
		public static readonly Guid PKEY_DeviceInterface_FriendlyName = new Guid(0xa45c254e, 0xdf1c, 0x4efd, 0x80, 0x20, 0x67,
		                                                                         0xd1, 0x46, 0xa8, 0x50, 0xe0);

		/// <summary>
		/// PKEY_AudioEndpoint_FormFactor
		/// </summary>
		public static readonly Guid PKEY_AudioEndpoint_FormFactor = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0,
		                                                                     0xc0, 0xff, 0xee, 0x7f, 0x0e);

		/// <summary>
		/// PKEY_AudioEndpoint_ControlPanelPageProvider
		/// </summary>
		public static readonly Guid PKEY_AudioEndpoint_ControlPanelPageProvider = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c,
		                                                                                   0x23, 0xe0, 0xc0, 0xff, 0xee, 0x7f,
		                                                                                   0x0e);

		/// <summary>
		/// PKEY_AudioEndpoint_Association
		/// </summary>
		public static readonly Guid PKEY_AudioEndpoint_Association = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0,
		                                                                      0xc0, 0xff, 0xee, 0x7f, 0x0e);

		/// <summary>
		/// PKEY_AudioEndpoint_PhysicalSpeakers
		/// </summary>
		public static readonly Guid PKEY_AudioEndpoint_PhysicalSpeakers = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23,
		                                                                           0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);

		/// <summary>
		/// PKEY_AudioEndpoint_GUID
		/// </summary>
		public static readonly Guid PKEY_AudioEndpoint_GUID = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0, 0xc0,
		                                                               0xff, 0xee, 0x7f, 0x0e);

		/// <summary>
		/// PKEY_AudioEndpoint_Disable_SysFx 
		/// </summary>
		public static readonly Guid PKEY_AudioEndpoint_Disable_SysFx = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0,
		                                                                        0xc0, 0xff, 0xee, 0x7f, 0x0e);

		/// <summary>
		/// PKEY_AudioEndpoint_FullRangeSpeakers 
		/// </summary>
		public static readonly Guid PKEY_AudioEndpoint_FullRangeSpeakers = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23,
		                                                                            0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);

		/// <summary>
		/// PKEY_AudioEngine_DeviceFormat 
		/// </summary>
		public static readonly Guid PKEY_AudioEngine_DeviceFormat = new Guid(0xf19f064d, 0x82c, 0x4e27, 0xbc, 0x73, 0x68, 0x82,
		                                                                     0xa1, 0xbb, 0x8e, 0x4c);

		/// <summary>
		/// PKEY _Devie_FriendlyName
		/// </summary>
		public static readonly Guid PKEY_Device_FriendlyName = new Guid(0x026e516e, 0xb814, 0x414b, 0x83, 0xcd, 0x85, 0x6d,
		                                                                0x6f, 0xef, 0x48, 0x22);
	}
}