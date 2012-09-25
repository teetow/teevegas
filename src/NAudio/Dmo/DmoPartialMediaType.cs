using System;

namespace NAudio.Dmo
{
	/// <summary>
	/// DMO_PARTIAL_MEDIATYPE
	/// </summary>
	internal struct DmoPartialMediaType
	{
		public Guid Type { get; internal set; }

		public Guid Subtype { get; internal set; }
	}
}