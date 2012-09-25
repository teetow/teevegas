using System.Runtime.InteropServices;

namespace NAudio.Wave.MmeInterop
{
	/// <summary>
	/// WaveInCapabilities structure (based on WAVEINCAPS from mmsystem.h)
	/// http://msdn.microsoft.com/en-us/library/ms713726(VS.85).aspx
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct WaveInCapabilities
	{
		/// <summary>
		/// wMid
		/// </summary>
		private readonly short manufacturerId;

		/// <summary>
		/// wPid
		/// </summary>
		private readonly short productId;

		/// <summary>
		/// vDriverVersion
		/// </summary>
		private readonly int driverVersion;

		/// <summary>
		/// Product Name (szPname)
		/// </summary>
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxProductNameLength)] private readonly string productName;

		/// <summary>
		/// Supported formats (bit flags) dwFormats 
		/// </summary>
		private readonly SupportedWaveFormat supportedFormats;

		/// <summary>
		/// Supported channels (1 for mono 2 for stereo) (wChannels)
		/// Seems to be set to -1 on a lot of devices
		/// </summary>
		private readonly short channels;

		/// <summary>
		/// wReserved1
		/// </summary>
		private readonly short reserved;

		private const int MaxProductNameLength = 32;

		/// <summary>
		/// Number of channels supported
		/// </summary>
		public int Channels
		{
			get { return channels; }
		}

		/// <summary>
		/// The product name
		/// </summary>
		public string ProductName
		{
			get { return productName; }
		}
	}
}