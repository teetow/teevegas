using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// from Propidl.h.
	/// http://msdn.microsoft.com/en-us/library/aa380072(VS.85).aspx
	/// contains a union so we have to do an explicit layout
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct PropVariant
	{
		[FieldOffset(0)] private readonly short vt;
		[FieldOffset(2)] private readonly short wReserved1;
		[FieldOffset(4)] private readonly short wReserved2;
		[FieldOffset(6)] private readonly short wReserved3;
		[FieldOffset(8)] private readonly sbyte cVal;
		[FieldOffset(8)] private readonly byte bVal;
		[FieldOffset(8)] private readonly short iVal;
		[FieldOffset(8)] private readonly ushort uiVal;
		[FieldOffset(8)] private readonly int lVal;
		[FieldOffset(8)] private readonly uint ulVal;
		[FieldOffset(8)] private readonly int intVal;
		[FieldOffset(8)] private readonly uint uintVal;
		[FieldOffset(8)] private readonly long hVal;
		[FieldOffset(8)] private readonly long uhVal;
		[FieldOffset(8)] private readonly float fltVal;
		[FieldOffset(8)] private readonly double dblVal;
		[FieldOffset(8)] private readonly bool boolVal;
		[FieldOffset(8)] private readonly int scode;
		//CY cyVal;
		[FieldOffset(8)] private readonly DateTime date;
		[FieldOffset(8)] private readonly FILETIME filetime;
		//CLSID* puuid;
		//CLIPDATA* pclipdata;
		//BSTR bstrVal;
		//BSTRBLOB bstrblobVal;
		[FieldOffset(8)] private Blob blobVal;
		//LPSTR pszVal;
		[FieldOffset(8)] private readonly IntPtr pwszVal; //LPWSTR 
		//IUnknown* punkVal;
		/*IDispatch* pdispVal;
        IStream* pStream;
        IStorage* pStorage;
        LPVERSIONEDSTREAM pVersionedStream;
        LPSAFEARRAY parray;
        CAC cac;
        CAUB caub;
        CAI cai;
        CAUI caui;
        CAL cal;
        CAUL caul;
        CAH cah;
        CAUH cauh;
        CAFLT caflt;
        CADBL cadbl;
        CABOOL cabool;
        CASCODE cascode;
        CACY cacy;
        CADATE cadate;
        CAFILETIME cafiletime;
        CACLSID cauuid;
        CACLIPDATA caclipdata;
        CABSTR cabstr;
        CABSTRBLOB cabstrblob;
        CALPSTR calpstr;
        CALPWSTR calpwstr;
        CAPROPVARIANT capropvar;
        CHAR* pcVal;
        UCHAR* pbVal;
        SHORT* piVal;
        USHORT* puiVal;
        LONG* plVal;
        ULONG* pulVal;
        INT* pintVal;
        UINT* puintVal;
        FLOAT* pfltVal;
        DOUBLE* pdblVal;
        VARIANT_BOOL* pboolVal;
        DECIMAL* pdecVal;
        SCODE* pscode;
        CY* pcyVal;
        DATE* pdate;
        BSTR* pbstrVal;
        IUnknown** ppunkVal;
        IDispatch** ppdispVal;
        LPSAFEARRAY* pparray;
        PROPVARIANT* pvarVal;
        */

		/// <summary>
		/// Helper method to gets blob data
		/// </summary>
		private byte[] GetBlob()
		{
			var Result = new byte[blobVal.Length];
			Marshal.Copy(blobVal.Data, Result, 0, Result.Length);
			return Result;
		}

		/// <summary>
		/// Property value
		/// </summary>
		public object Value
		{
			get
			{
				var ve = (VarEnum) vt;
				switch (ve)
				{
					case VarEnum.VT_I1:
						return bVal;
					case VarEnum.VT_I2:
						return iVal;
					case VarEnum.VT_I4:
						return lVal;
					case VarEnum.VT_I8:
						return hVal;
					case VarEnum.VT_INT:
						return iVal;
					case VarEnum.VT_UI4:
						return ulVal;
					case VarEnum.VT_LPWSTR:
						return Marshal.PtrToStringUni(pwszVal);
					case VarEnum.VT_BLOB:
						return GetBlob();
				}
				throw new NotImplementedException("PropVariant " + ve.ToString());
			}
		}
	}
}