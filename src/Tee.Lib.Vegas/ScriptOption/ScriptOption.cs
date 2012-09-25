using System;
using Microsoft.Win32;

namespace Tee.Lib.Vegas.ScriptOption
{
	//public class ScriptOptionGeneric<T>
	//{
	//    private readonly RegistryKey _regKey;
	//    private readonly string _parentKey;

	//    public T Value { get; set; }

	//    public string RegistryKeyName { get; set; }

	//    public ScriptOptionGeneric(string ParentScriptKey, string KeyName, T DefaultValue = default(T))
	//    {
	//        // get registry key for Keyname
	//        _parentKey = ParentScriptKey;
	//        RegistryKeyName = KeyName;
	//        _regKey = FindRegistryKey(KeyName);

	//        // get and parse value, store in Value
	//    }

	//    private RegistryKey FindRegistryKey(string ScriptName)
	//    {
	//        var HKCU = Registry.CurrentUser.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadWriteSubTree);
	//        if (HKCU != null)
	//        {
	//            var vegasKey = HKCU.CreateSubKey(String.Format("{0}\\{1}\\{2}", ScriptOptionStrings.CompanyParentKey, _parentKey, ScriptName));
	//            return vegasKey;
	//        }
	//        return null;
	//    }
	//}

	public class ScriptOption
	{
		private readonly RegistryKey _registryKey;
		private string _value;

		public readonly string Name;
		public readonly string Script;
		public Type Type;

		public ScriptOption(string ScriptKey, string OptionKey, object DefaultValue = default(object))
		{
			this.Script = ScriptKey;
			Name = OptionKey;

			_registryKey = FindRegistryKey();

			if (DefaultValue != null)
				Type = DefaultValue.GetType();

			ReadValueFromRegistry();

			if (_value == null)
			{
				_value = DefaultValue.ToString();

				WriteValueToRegistry();
			}
		}

		private RegistryKey FindRegistryKey()
		{
			RegistryKey HKCU = Registry.CurrentUser.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadWriteSubTree);
			if (HKCU != null)
			{
				RegistryKey vegasKey =
					HKCU.CreateSubKey(String.Format("{0}\\{1}\\{2}", ScriptOptionStrings.CompanyParentKey, ScriptOptionStrings.ProductParentKey, Script));
				return vegasKey;
			}
			return null;
		}

		internal object GetValue()
		{
			return _value;
		}

		internal void SetValue(object Value)
		{
			_value = Value.ToString();
		}

		internal void WriteValueToRegistry()
		{
			if (_registryKey == null)
				return;
			_registryKey.SetValue(Name, _value);
		}

		internal void ReadValueFromRegistry()
		{
			if (_registryKey == null)
				return;

			object RegObj = _registryKey.GetValue(Name);

			if (RegObj != null)
				_value = RegObj.ToString();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}