using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	/// <summary>
	/// implements IMediaObject  (DirectX Media Object)
	/// implements IMFTransform (Media Foundation Transform)
	/// On Windows XP, it is always an MM (if present at all)
	/// </summary>
	[ComImport, Guid("bbeea841-0a63-4f52-a7ab-a9b3a84ed38a")]
	internal class WindowsMediaMp3DecoderComObject
	{
	}
}