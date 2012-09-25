using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// MM Device
	/// </summary>
	public class MMDevice
	{
		#region Variables

		private readonly IMMDevice deviceInterface;
		private AudioEndpointVolume _AudioEndpointVolume;
		private AudioMeterInformation _AudioMeterInformation;
		private PropertyStore _PropertyStore;

		#endregion

		#region Guids

		private static Guid IID_IAudioMeterInformation = new Guid("C02216F6-8C67-4B5B-9D00-D008E73E0064");
		private static Guid IID_IAudioEndpointVolume = new Guid("5CDF2C82-841E-4546-9722-0CF74078229A");
		private static Guid IID_IAudioClient = new Guid("1CB9AD4C-DBFA-4c32-B178-C2F568A703B2");

		#endregion

		#region Init

		private void GetPropertyInformation()
		{
			IPropertyStore propstore;
			Marshal.ThrowExceptionForHR(deviceInterface.OpenPropertyStore(StorageAccessMode.Read, out propstore));
			_PropertyStore = new PropertyStore(propstore);
		}

		private AudioClient GetAudioClient()
		{
			object result;
			Marshal.ThrowExceptionForHR(deviceInterface.Activate(ref IID_IAudioClient, ClsCtx.ALL, IntPtr.Zero, out result));
			return new AudioClient(result as IAudioClient);
		}

		private void GetAudioMeterInformation()
		{
			object result;
			Marshal.ThrowExceptionForHR(deviceInterface.Activate(ref IID_IAudioMeterInformation, ClsCtx.ALL, IntPtr.Zero,
			                                                     out result));
			_AudioMeterInformation = new AudioMeterInformation(result as IAudioMeterInformation);
		}

		private void GetAudioEndpointVolume()
		{
			object result;
			Marshal.ThrowExceptionForHR(deviceInterface.Activate(ref IID_IAudioEndpointVolume, ClsCtx.ALL, IntPtr.Zero,
			                                                     out result));
			_AudioEndpointVolume = new AudioEndpointVolume(result as IAudioEndpointVolume);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Audio Client
		/// </summary>
		public AudioClient AudioClient
		{
			get
			{
				// now makes a new one each call to allow caller to manage when to dispose
				// n.b. should probably not be a property anymore
				return GetAudioClient();
			}
		}

		/// <summary>
		/// Audio Meter Information
		/// </summary>
		public AudioMeterInformation AudioMeterInformation
		{
			get
			{
				if (_AudioMeterInformation == null)
					GetAudioMeterInformation();

				return _AudioMeterInformation;
			}
		}

		/// <summary>
		/// Audio Endpoint Volume
		/// </summary>
		public AudioEndpointVolume AudioEndpointVolume
		{
			get
			{
				if (_AudioEndpointVolume == null)
					GetAudioEndpointVolume();

				return _AudioEndpointVolume;
			}
		}

		/// <summary>
		/// Properties
		/// </summary>
		public PropertyStore Properties
		{
			get
			{
				if (_PropertyStore == null)
					GetPropertyInformation();
				return _PropertyStore;
			}
		}

		/// <summary>
		/// Friendly name for the endpoint
		/// </summary>
		public string FriendlyName
		{
			get
			{
				if (_PropertyStore == null)
				{
					GetPropertyInformation();
				}
				if (_PropertyStore.Contains(PropertyKeys.PKEY_DeviceInterface_FriendlyName))
				{
					return (string) _PropertyStore[PropertyKeys.PKEY_DeviceInterface_FriendlyName].Value;
				}
				else
					return "Unknown";
			}
		}

		/// <summary>
		/// Friendly name of device
		/// </summary>
		public string DeviceFriendlyName
		{
			get
			{
				if (_PropertyStore == null)
				{
					GetPropertyInformation();
				}
				if (_PropertyStore.Contains(PropertyKeys.PKEY_Device_FriendlyName))
				{
					return (string) _PropertyStore[PropertyKeys.PKEY_Device_FriendlyName].Value;
				}
				else
				{
					return "Unknown";
				}
			}
		}

		/// <summary>
		/// Device ID
		/// </summary>
		public string ID
		{
			get
			{
				string Result;
				Marshal.ThrowExceptionForHR(deviceInterface.GetId(out Result));
				return Result;
			}
		}

		/// <summary>
		/// Data Flow
		/// </summary>
		public DataFlow DataFlow
		{
			get
			{
				DataFlow Result;
				var ep = deviceInterface as IMMEndpoint;
				ep.GetDataFlow(out Result);
				return Result;
			}
		}

		/// <summary>
		/// Device State
		/// </summary>
		public DeviceState State
		{
			get
			{
				DeviceState Result;
				Marshal.ThrowExceptionForHR(deviceInterface.GetState(out Result));
				return Result;
			}
		}

		#endregion

		#region Constructor

		internal MMDevice(IMMDevice realDevice)
		{
			deviceInterface = realDevice;
		}

		#endregion

		/// <summary>
		/// To string
		/// </summary>
		public override string ToString()
		{
			return FriendlyName;
		}
	}
}