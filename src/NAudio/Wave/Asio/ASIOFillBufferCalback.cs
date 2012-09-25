using System;

namespace NAudio.Wave.Asio
{
	/// <summary>
	/// Callback used by the ASIODriverExt to get wave data
	/// </summary>
	internal delegate void ASIOFillBufferCalback(IntPtr[] bufferChannels);
}