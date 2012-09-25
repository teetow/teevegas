using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.Dmo
{
	// http://msdn.microsoft.com/en-us/library/ff819509%28VS.85%29.aspx
	// CLSID_CMP3DecMediaObject

	/// <summary>
	/// Windows Media MP3 Decoder (as a DMO)
	/// WORK IN PROGRESS - DO NOT USE!
	/// </summary>
	public class WindowsMediaMp3Decoder : IDisposable
	{
		private WindowsMediaMp3DecoderComObject mediaComObject;
		private MediaObject mediaObject;
		private IPropertyStore propertyStoreInterface;
		//IWMResamplerProps resamplerPropsInterface;

		/// <summary>
		/// Creates a new Resampler based on the DMO Resampler
		/// </summary>
		public WindowsMediaMp3Decoder()
		{
			mediaComObject = new WindowsMediaMp3DecoderComObject();
			mediaObject = new MediaObject((IMediaObject) mediaComObject);
			propertyStoreInterface = (IPropertyStore) mediaComObject;
			//resamplerPropsInterface = (IWMResamplerProps)mediaComObject;
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
			/*if(resamplerPropsInterface != null)
            {
                Marshal.ReleaseComObject(resamplerPropsInterface);
                resamplerPropsInterface = null;
            }*/
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