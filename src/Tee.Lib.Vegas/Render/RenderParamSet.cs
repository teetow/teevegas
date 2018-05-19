using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ScriptPortal.Vegas;

namespace Tee.Lib.Vegas.Render
{
	public class RenderParamSet
	{
		private readonly List<RenderParameter> _defaultParams = new List<RenderParameter>
		{
			new RenderParameter<string>(RenderTags.RootDir, String.Empty),
			new RenderParameter<string>(RenderTags.TargetDir, "C:\\"),
			new RenderParameter<string>(RenderTags.NamingMask,"{region}"),

			new RenderParameter<bool>(RenderTags.NoRender),
			new RenderParameter<bool>(RenderTags.NoEmpty),
			new RenderParameter<bool>(RenderTags.NoCounters),
			new RenderParameter<bool>(RenderTags.DoStems),
			new RenderParameter<bool>(RenderTags.DoVideo),
			new RenderParameter<bool>(RenderTags.DoReadonly),

			new RenderParameter<long>(RenderTags.CounterDigits, 3),

			new RenderParameter<bool>(RenderTags.DoPadding),
			new RenderParameter<int>(RenderTags.PaddingAmt, 1), // seconds

			new RenderParameter<string>(RenderTags.VideoFmt, "Video for Windows"),
			new RenderParameter<string>(RenderTags.VideoTpl, "Default Template"),
			new RenderParameter<string>(RenderTags.AudioFmt, "Wave (Microsoft)"),
			new RenderParameter<string>(RenderTags.AudioTpl, "Default Template")
		};

		private readonly List<RenderParameter> _userParams = new List<RenderParameter>();

		public RenderParamSet()
		{
		}

		public RenderParamSet(RenderParamSet regionParams)
		{
			foreach (RenderParameter param in regionParams.UserParams)
			{
				AddUserParam(param.Name, param.Value);
			}
		}

		public List<RenderParameter> RenderParams
		{
			get
			{
				var output = new List<RenderParameter>();
				foreach (var param in _defaultParams)
				{
					var p = _userParams.FirstOrDefault(item => item.Name == param.Name);
					output.Add(p ?? param);
				}
				return output;
			}
		}

		public List<RenderParameter> UserParams { get { return _userParams; } }

		public void ParseCommandMarker(CommandMarker mk)
		{
			string key = mk.CommandType.ToString().ToLowerInvariant();
			string value = mk.CommandParameter;
			ParseKeyValue(key, value);
		}

		internal void ParseString(string String)
		{
			if (string.IsNullOrEmpty(String))
				return;
			Regex renderTagRegex = new Regex(@"\{(?<key>\w+?)[=: ](?<value>[^{]+?)\}", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
			foreach (Match m in renderTagRegex.Matches(String))
			{
				string key = m.Groups["key"] != null ? m.Groups["key"].Value : null;
				string value = m.Groups["value"] != null ? m.Groups["value"].Value : null;
				if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
					continue;
				ParseKeyValue(key, value);
			}
		}

		public void ParseKeyValue(string Key, string Value)
		{
			switch (Key)
			{
				// string
				case RenderTags.RootDir:
				case RenderTags.TargetDir:
				case RenderTags.AudioFmt:
				case RenderTags.AudioTpl:
				case RenderTags.VideoFmt:
				case RenderTags.VideoTpl:
				case RenderTags.NamingMask:
					AddUserParam(Key, Value);
					break;

				// bool
				case RenderTags.NoRender:
				case RenderTags.DoStems:
				case RenderTags.DoVideo:
				case RenderTags.NoEmpty:
				case RenderTags.NoCounters:
				case RenderTags.DoReadonly:
				case RenderTags.DoPadding:
					bool boolParsed;
					if (bool.TryParse(Value, out boolParsed))
						AddUserParam(Key, boolParsed);

					break;

				// long
				case RenderTags.CounterDigits:
					long ctrDigitsParsed;
					if (long.TryParse(Value, out ctrDigitsParsed))
						AddUserParam(Key, ctrDigitsParsed);
					break;
				case RenderTags.PaddingAmt:
					int paddingAmtParsed;
					if (int.TryParse(Value, out paddingAmtParsed))
					{
						AddUserParam(Key, paddingAmtParsed);
					}
					break;
			}
		}

		public void AddIfNotDefault<T>(string Name, T Value)
		{
			var existingParam = _userParams.Find(item => item.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase)) as RenderParameter<T>;
			if (existingParam != null)
			{
				if (!existingParam.Value.Equals(Value))
				{
					existingParam.Value = Value;
					existingParam.IsDefault = false;
				}
			}
			else
			{
				var defaultParam = _defaultParams.Find(item => item.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase)) as RenderParameter<T>;
				if (defaultParam != null && defaultParam.Value.Equals(Value))
					return;
				AddUserParam(Name, Value);
			}
		}

		public void AddUserParam<T>(string Name, T Value)
		{
			var existingParam = _userParams.Find(item => item.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase)) as RenderParameter<T>;
			if (existingParam != null)
			{
				existingParam.Name = Name;
				existingParam.Value = Value;
				existingParam.IsDefault = false;
			}
			else
			{
				_userParams.Add(new RenderParameter<T> { Name = Name, Value = Value, IsDefault = false });
			}
		}

		public void MergeWith(RenderParamSet renderParamSet)
		{
			foreach (var param in renderParamSet.UserParams)
			{
				AddUserParam(param.Name, param.Value);
			}
		}

		public T GetParam<T>(string ParamName)
		{
			var param = RenderParams.Find(p => p.Name.Equals(ParamName, StringComparison.InvariantCultureIgnoreCase));
			if (param.Value == null)
				return default(T);
			return (T)param.Value;
		}

		public RenderParameter GetParam(string ParamName)
		{
			return RenderParams.Find(p => p.Name.Equals(ParamName, StringComparison.InvariantCultureIgnoreCase));
		}

		public void ResetToDefault()
		{
			_userParams.Clear();
		}
	}
}