using System;
using System.Runtime.Serialization;

namespace Tee.Lib.Vegas.Project
{
	public class EventPropertiesTemplateBinder : SerializationBinder
	{
		public override Type BindToType(string assemblyName, string typeName)
		{
			if (typeName.Contains("EventPropertiesTemplate"))
				return typeof(EventPropertiesTemplate);
			if (typeName.Contains("SerializableFade"))
				return typeof(SerializableFade);
			return typeof(object);
		}
	}
}