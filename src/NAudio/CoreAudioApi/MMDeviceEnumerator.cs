using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// MM Device Enumerator
	/// </summary>
	public class MMDeviceEnumerator
	{
		private readonly IMMDeviceEnumerator _realEnumerator;

		/// <summary>
		/// Creates a new MM Device Enumerator
		/// </summary>
		public MMDeviceEnumerator()
		{
			if (Environment.OSVersion.Version.Major < 6)
			{
				throw new NotSupportedException("This functionality is only supported on Windows Vista or newer.");
			}
			_realEnumerator = new MMDeviceEnumeratorComObject() as IMMDeviceEnumerator;
		}

		/// <summary>
		/// Enumerate Audio Endpoints
		/// </summary>
		/// <param name="dataFlow">Desired DataFlow</param>
		/// <param name="dwStateMask">State Mask</param>
		/// <returns>Device Collection</returns>
		public MMDeviceCollection EnumerateAudioEndPoints(DataFlow dataFlow, DeviceState dwStateMask)
		{
			IMMDeviceCollection result;
			Marshal.ThrowExceptionForHR(_realEnumerator.EnumAudioEndpoints(dataFlow, dwStateMask, out result));
			return new MMDeviceCollection(result);
		}

		/// <summary>
		/// Get Default Endpoint
		/// </summary>
		/// <param name="dataFlow">Data Flow</param>
		/// <param name="role">Role</param>
		/// <returns>Device</returns>
		public MMDevice GetDefaultAudioEndpoint(DataFlow dataFlow, Role role)
		{
			IMMDevice _Device = null;
			Marshal.ThrowExceptionForHR((_realEnumerator).GetDefaultAudioEndpoint(dataFlow, role, out _Device));
			return new MMDevice(_Device);
		}

		/// <summary>
		/// Get device by ID
		/// </summary>
		/// <param name="ID">Device ID</param>
		/// <returns>Device</returns>
		public MMDevice GetDevice(string ID)
		{
			IMMDevice _Device = null;
			Marshal.ThrowExceptionForHR((_realEnumerator).GetDevice(ID, out _Device));
			return new MMDevice(_Device);
		}
	}
}