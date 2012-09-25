using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tee.Lib.Vegas
{
	public class ErrorLog
	{
		private readonly List<string> _entries = new List<string>();

		public ErrorLog()
		{
			_entries.Clear();
		}

		public int Count
		{
			get { return _entries.Count; }
		}

		public void Push(string msg)
		{
			_entries.Add(msg);
		}

		public void PushOnce(string Entry)
		{
			if (_entries.Any(cMsg => cMsg == Entry))
			{
				return;
			}
			_entries.Add(Entry);
		}

		public String GetMerged()
		{
			var output = new StringBuilder();
			foreach (string entry in _entries)
			{
				output.AppendLine(entry);
			}
			return output.ToString();
		}
	}
}