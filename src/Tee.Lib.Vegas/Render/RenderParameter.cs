using System;

namespace Tee.Lib.Vegas.Render
{
	public abstract class RenderParameter
	{
		public string Name;
		public bool IsDefault;

		public object _value;

		public object Value
		{
			get { return _value; }
			set { _value = value; }
		}
	}

	public class RenderParameter<DataType> : RenderParameter
	{
		public new DataType Value
		{
			get { return (DataType)_value; }
			set { _value = value; }
		}

		public RenderParameter()
		{
		}

		public RenderParameter(String Name, DataType Value = default(DataType), bool IsDefault = true)
		{
			this.Name = Name;
			this.Value = Value;
			this.IsDefault = IsDefault;
		}

		public Type GetDataType()
		{
			return Value.GetType();
		}

		public override string ToString()
		{
			return String.Format("{0} ({1})={2}", Name, Value.GetType(), Value);
		}
	}
}