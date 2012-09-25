using System;

namespace NAudio.Dmo
{
	/// <summary>
	/// Contains the name and CLSID of a DirectX Media Object
	/// </summary>
	public class DmoDescriptor
	{
		/// <summary>
		/// Initializes a new instance of DmoDescriptor
		/// </summary>
		public DmoDescriptor(string name, Guid clsid)
		{
			Name = name;
			Clsid = clsid;
		}

		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Clsid
		/// </summary>
		public Guid Clsid { get; private set; }
	}
}