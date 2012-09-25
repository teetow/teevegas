using System;

namespace NAudio.Wave.Asio
{
	// -------------------------------------------------------------------------------
	// Structures used by the ASIODriver and ASIODriverExt
	// -------------------------------------------------------------------------------

	// -------------------------------------------------------------------------------
	// Error and Exceptions
	// -------------------------------------------------------------------------------

	/// <summary>
	/// ASIO common Exception.
	/// </summary>
	internal class ASIOException : Exception
	{
		private ASIOError error;

		public ASIOException()
		{
		}

		public ASIOException(string message)
			: base(message)
		{
		}

		public ASIOException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public ASIOError Error
		{
			get { return error; }
			set
			{
				error = value;
				Data["ASIOError"] = error;
			}
		}

		/// <summary>
		/// Gets the name of the error.
		/// </summary>
		/// <param name="error">The error.</param>
		/// <returns>the name of the error</returns>
		public static String getErrorName(ASIOError error)
		{
			return Enum.GetName(typeof (ASIOError), error);
		}
	}

	// -------------------------------------------------------------------------------
	// Channel Info, Buffer Info
	// -------------------------------------------------------------------------------

	// -------------------------------------------------------------------------------
	// Time structures
	// -------------------------------------------------------------------------------

	// -------------------------------------------------------------------------------
	// Callbacks
	// -------------------------------------------------------------------------------
}