﻿using System;
using System.Runtime.InteropServices;
using NAudio.Wave.WaveFormats;

namespace NAudio.Dmo
{
	/// <summary>
	/// http://msdn.microsoft.com/en-us/library/aa929922.aspx
	/// DMO_MEDIA_TYPE 
	/// </summary>
	public struct DmoMediaType
	{
		private bool bFixedSizeSamples;
		private bool bTemporalCompression;
		private int cbFormat;
		private Guid formattype;
		private int lSampleSize;
		private Guid majortype;
		private IntPtr pUnk; // not used
		private IntPtr pbFormat;
		private Guid subtype;

		/// <summary>
		/// Major type
		/// </summary>
		public Guid MajorType
		{
			get { return majortype; }
		}

		/// <summary>
		/// Major type name
		/// </summary>
		public string MajorTypeName
		{
			get { return MediaTypes.GetMediaTypeName(majortype); }
		}

		/// <summary>
		/// Subtype
		/// </summary>
		public Guid SubType
		{
			get { return subtype; }
		}

		/// <summary>
		/// Subtype name
		/// </summary>
		public string SubTypeName
		{
			get
			{
				if (majortype == MediaTypes.MEDIATYPE_Audio)
				{
					return AudioMediaSubtypes.GetAudioSubtypeName(subtype);
				}
				return subtype.ToString();
			}
		}

		/// <summary>
		/// Fixed size samples
		/// </summary>
		public bool FixedSizeSamples
		{
			get { return bFixedSizeSamples; }
		}

		/// <summary>
		/// Sample size
		/// </summary>
		public int SampleSize
		{
			get { return lSampleSize; }
		}

		/// <summary>
		/// Format type
		/// </summary>
		public Guid FormatType
		{
			get { return formattype; }
		}

		/// <summary>
		/// Format type name
		/// </summary>
		public string FormatTypeName
		{
			get
			{
				if (formattype == DmoMediaTypeGuids.FORMAT_None)
				{
					return "None";
				}
				else if (formattype == Guid.Empty)
				{
					return "Null";
				}
				else if (formattype == DmoMediaTypeGuids.FORMAT_WaveFormatEx)
				{
					return "WaveFormatEx";
				}
				else
				{
					return FormatType.ToString();
				}
			}
		}

		/// <summary>
		/// Gets the structure as a Wave format (if it is one)
		/// </summary>        
		public WaveFormat GetWaveFormat()
		{
			if (formattype == DmoMediaTypeGuids.FORMAT_WaveFormatEx)
			{
				return WaveFormat.MarshalFromPtr(pbFormat);
			}
			else
			{
				throw new InvalidOperationException("Not a WaveFormat type");
			}
		}

		/// <summary>
		/// Sets this object up to point to a wave format
		/// </summary>
		/// <param name="waveFormat">Wave format structure</param>
		public void SetWaveFormat(WaveFormat waveFormat)
		{
			majortype = MediaTypes.MEDIATYPE_Audio;
			switch (waveFormat.Encoding)
			{
				case WaveFormatEncoding.Pcm:
					subtype = AudioMediaSubtypes.MEDIASUBTYPE_PCM;
					bFixedSizeSamples = true;
					break;
				case WaveFormatEncoding.IeeeFloat:
					subtype = AudioMediaSubtypes.MEDIASUBTYPE_IEEE_FLOAT;
					bFixedSizeSamples = true;
					break;
				case WaveFormatEncoding.MpegLayer3:
					subtype = AudioMediaSubtypes.WMMEDIASUBTYPE_MP3;
					break;
				default:
					throw new ArgumentException("Not a supported encoding");
			}
			formattype = DmoMediaTypeGuids.FORMAT_WaveFormatEx;
			if (cbFormat < Marshal.SizeOf(waveFormat))
				throw new InvalidOperationException("Not enough memory assigned for a WaveFormat structure");
			//Debug.Assert(cbFormat >= ,"Not enough space");
			Marshal.StructureToPtr(waveFormat, pbFormat, false);
		}
	}
}