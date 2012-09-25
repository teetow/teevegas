using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Tee.Lib.Vegas.ScriptOption
{
	public class ScriptOptionCollection : List<ScriptOption>
	{
		private readonly string _scriptKey;

		public ScriptOptionCollection(string Script)
		{
			_scriptKey = Script;
		}

		public void GetOptionsFromRegistry()
		{
			var HKCU = Registry.CurrentUser.OpenSubKey("Software", RegistryKeyPermissionCheck.ReadWriteSubTree);
			if (HKCU != null)
			{
				var vegasKey =
					HKCU.CreateSubKey(String.Format("{0}\\{1}\\{2}", ScriptOptionStrings.CompanyParentKey, ScriptOptionStrings.ProductParentKey, _scriptKey));
				if (vegasKey != null)
					foreach (string curKey in vegasKey.GetValueNames())
					{
						ScriptOption curOption = new ScriptOption(_scriptKey, curKey, vegasKey.GetValue(curKey).ToString());
						Add(curOption);
					}
			}
		}

		internal ScriptOption GetOption(string Option, string DefaultValue)
		{
			foreach (ScriptOption Opt in this)
			{
				if (Opt.Name.ToLower() == Option.ToLower() && Opt.Script == _scriptKey)
				{
					Opt.SetValue(DefaultValue);
					Opt.ReadValueFromRegistry();
					return Opt;
				}
			}
			var NewOpt = new ScriptOption(_scriptKey, Option, DefaultValue);
			Add(NewOpt);
			return NewOpt;
		}
	}
}