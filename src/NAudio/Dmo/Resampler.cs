using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.Dmo
{
	/// <summary>
	/// Resampler
	/// </summary>
	public class Resampler : IDisposable
	{
		private ResamplerMediaComObject mediaComObject;
		private MediaObject mediaObject;
		private IPropertyStore propertyStoreInterface;
		private IWMResamplerProps resamplerPropsInterface;

		/// <summary>
		/// Creates a new Resampler based on the DMO Resampler
		/// </summary>
		public Resampler()
		{
			mediaComObject = new ResamplerMediaComObject();
			mediaObject = new MediaObject((IMediaObject) mediaComObject);
			propertyStoreInterface = (IPropertyStore) mediaComObject;
			resamplerPropsInterface = (IWMResamplerProps) mediaComObject;
		}

		/// <summary>
		/// Media Object
		/// </summary>
		public MediaObject MediaObject
		{
			get { return mediaObject; }
		}

		#region IDisposable Members

		/// <summary>
		/// Dispose code - experimental at the moment
		/// Was added trying to track down why Resampler crashes NUnit
		/// This code not currently being called by ResamplerDmoStream
		/// </summary>
		public void Dispose()
		{
			if (propertyStoreInterface != null)
			{
				Marshal.ReleaseComObject(propertyStoreInterface);
				propertyStoreInterface = null;
			}
			if (resamplerPropsInterface != null)
			{
				Marshal.ReleaseComObject(resamplerPropsInterface);
				resamplerPropsInterface = null;
			}
			if (mediaObject != null)
			{
				mediaObject.Dispose();
				mediaObject = null;
			}
			if (mediaComObject != null)
			{
				Marshal.ReleaseComObject(mediaComObject);
				mediaComObject = null;
			}
		}

		#endregion
	}
}