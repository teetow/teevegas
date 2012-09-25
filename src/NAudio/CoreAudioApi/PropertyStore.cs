using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Property Store class, only supports reading properties at the moment.
	/// </summary>
	public class PropertyStore
	{
		private readonly IPropertyStore storeInterface;

		/// <summary>
		/// Creates a new property store
		/// </summary>
		/// <param name="store">IPropertyStore COM interface</param>
		internal PropertyStore(IPropertyStore store)
		{
			storeInterface = store;
		}

		/// <summary>
		/// Property Count
		/// </summary>
		public int Count
		{
			get
			{
				int result;
				Marshal.ThrowExceptionForHR(storeInterface.GetCount(out result));
				return result;
			}
		}

		/// <summary>
		/// Gets property by index
		/// </summary>
		/// <param name="index">Property index</param>
		/// <returns>The property</returns>
		public PropertyStoreProperty this[int index]
		{
			get
			{
				PropVariant result;
				PropertyKey key = Get(index);
				Marshal.ThrowExceptionForHR(storeInterface.GetValue(ref key, out result));
				return new PropertyStoreProperty(key, result);
			}
		}

		/// <summary>
		/// Indexer by guid
		/// </summary>
		/// <param name="guid">Property guid</param>
		/// <returns>Property or null if not found</returns>
		public PropertyStoreProperty this[Guid guid]
		{
			get
			{
				PropVariant result;
				for (int i = 0; i < Count; i++)
				{
					PropertyKey key = Get(i);
					if (key.formatId == guid)
					{
						Marshal.ThrowExceptionForHR(storeInterface.GetValue(ref key, out result));
						return new PropertyStoreProperty(key, result);
					}
				}
				return null;
			}
		}

		/// <summary>
		/// Contains property guid
		/// </summary>
		/// <param name="guid">Looks for a specific Guid</param>
		/// <returns>True if found</returns>
		public bool Contains(Guid guid)
		{
			for (int i = 0; i < Count; i++)
			{
				PropertyKey key = Get(i);
				if (key.formatId == guid)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Gets property key at sepecified index
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Property key</returns>
		public PropertyKey Get(int index)
		{
			PropertyKey key;
			Marshal.ThrowExceptionForHR(storeInterface.GetAt(index, out key));
			return key;
		}

		/// <summary>
		/// Gets property value at specified index
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Property value</returns>
		public PropVariant GetValue(int index)
		{
			PropVariant result;
			PropertyKey key = Get(index);
			Marshal.ThrowExceptionForHR(storeInterface.GetValue(ref key, out result));
			return result;
		}
	}
}