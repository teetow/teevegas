using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Multimedia Device Collection
	/// </summary>
	public class MMDeviceCollection : IEnumerable<MMDevice>
	{
		private readonly IMMDeviceCollection _MMDeviceCollection;

		internal MMDeviceCollection(IMMDeviceCollection parent)
		{
			_MMDeviceCollection = parent;
		}

		/// <summary>
		/// Device count
		/// </summary>
		public int Count
		{
			get
			{
				int result;
				Marshal.ThrowExceptionForHR(_MMDeviceCollection.GetCount(out result));
				return result;
			}
		}

		/// <summary>
		/// Get device by index
		/// </summary>
		/// <param name="index">Device index</param>
		/// <returns>Device at the specified index</returns>
		public MMDevice this[int index]
		{
			get
			{
				IMMDevice result;
				_MMDeviceCollection.Item(index, out result);
				return new MMDevice(result);
			}
		}

		#region IEnumerable<MMDevice> Members

		/// <summary>
		/// Get Enumerator
		/// </summary>
		/// <returns>Device enumerator</returns>
		public IEnumerator<MMDevice> GetEnumerator()
		{
			for (int index = 0; index < Count; index++)
			{
				yield return this[index];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}