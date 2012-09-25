using System;

namespace Tee.Lib.Vegas.Render
{
	public class RenderToken
	{
		private string _name;
		private string _tag;
		private string _value;

		public string Name
		{
			get { return _name; }
			set { _name = value.Trim().ToLower(); }
		}

		public string Tag
		{
			get { return _tag; }
			set { _tag = value.Trim().ToLower(); }
		}

		public string Value
		{
			get { return _value; }
			set { _value = value.Trim().ToLower(); }
		}

		public override string ToString()
		{
			return String.Format("{0} {1} = {2}", Name, Tag, Value);
		}
	}
}