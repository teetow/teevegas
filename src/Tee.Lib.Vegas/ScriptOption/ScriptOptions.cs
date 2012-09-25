namespace Tee.Lib.Vegas.ScriptOption
{
	public static class ScriptOptions
	{
		internal static ScriptOptionCollection GetTeeVegasOptions(string Script)
		{
			var Coll = new ScriptOptionCollection(Script);
			Coll.GetOptionsFromRegistry();
			return Coll;
		}

		public static bool GetValue(string Script, string Option, bool DefaultValue)
		{
			return bool.Parse(GetOptionObject(Script, Option, DefaultValue).ToString());
		}

		public static string GetValue(string Script, string Option, string DefaultValue)
		{
			return GetOptionObject(Script, Option, DefaultValue).ToString();
		}

		internal static object GetOptionObject(string Script, string Option, object DefaultValue)
		{
			ScriptOptionCollection Coll = GetTeeVegasOptions(Script);
			ScriptOption Opt = Coll.GetOption(Option, DefaultValue.ToString());
			return Opt.GetValue();
		}

		public static void SetValue<T>(string Script, string Option, T Value)
		{
			var Opt = new ScriptOption(Script, Option, Value);
			Opt.SetValue(Value);
			Opt.WriteValueToRegistry();
		}
	}
}