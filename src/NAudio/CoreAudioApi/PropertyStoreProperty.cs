namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Property Store Property
	/// </summary>
	public class PropertyStoreProperty
	{
		private readonly PropertyKey propertyKey;
		private PropVariant propertyValue;

		internal PropertyStoreProperty(PropertyKey key, PropVariant value)
		{
			propertyKey = key;
			propertyValue = value;
		}

		/// <summary>
		/// Property Key
		/// </summary>
		public PropertyKey Key
		{
			get { return propertyKey; }
		}

		/// <summary>
		/// Property Value
		/// </summary>
		public object Value
		{
			get { return propertyValue.Value; }
		}
	}
}