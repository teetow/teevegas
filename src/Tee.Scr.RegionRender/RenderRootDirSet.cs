using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Tee.Scr.RegionRender
{
	[Serializable]
	internal class RenderRootDirSet
	{
		private readonly List<RenderRootDir> _rootDirs = new List<RenderRootDir>();

		public List<RenderRootDir> RootDirs
		{
			get { return _rootDirs; }
		}

		private readonly string _confFile;

		private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
		{
			Formatting = Formatting.Indented,
			TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
		};

		public RenderRootDirSet(string ConfFile)
		{
			_confFile = ConfFile;
			if (File.Exists(_confFile))
				LoadFromFile();
		}

		public void LoadFromFile()
		{
			_rootDirs.Clear();
			if (!File.Exists(_confFile))
				return;
			using (var reader = new StreamReader(_confFile))
			{
				string jsonData = reader.ReadToEnd();
				try
				{
					var fileRootDirs = JsonConvert.DeserializeObject<List<RenderRootDir>>(jsonData, _serializerSettings);
					_rootDirs.AddRange(fileRootDirs);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		public void SaveToFile()
		{
			if (string.IsNullOrEmpty(_confFile))
				return;

			string confDir = Path.GetDirectoryName(_confFile);

			if (!Directory.Exists(confDir))
				Directory.CreateDirectory(confDir);

			string serializedOutput = JsonConvert.SerializeObject(_rootDirs, _serializerSettings);
			using (var wr = new StreamWriter(_confFile) { AutoFlush = true })
			{
				wr.Write(serializedOutput);
			}
		}

		public object RootDirTitles
		{
			get
			{
				return _rootDirs.Select(dir => dir.Name).ToList();
			}
		}
	}
}