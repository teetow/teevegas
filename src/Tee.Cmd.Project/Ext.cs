using System;
using System.IO;
using ScriptPortal.Vegas;

namespace Tee.Cmd.Project
{
	internal static class Ext
	{
		/// <summary>
		/// Tries it darndest to get a usable name from a TrackEvent.
		/// </summary>
		/// <param name="Event">The TrackEvent in question</param>
		/// <returns>Name if one exists, String.Emtpy if everything else fails.</returns>
		internal static string GetName(this TrackEvent Event)
		{
			if (Event.Name != null)
				return Event.Name;
			if (Event.ActiveTake.Name != null)
				return Event.ActiveTake.Name;
			if (Event.ActiveTake.MediaPath != null)
				return Path.GetFileNameWithoutExtension(Event.ActiveTake.MediaPath);
			return String.Empty;
		}
	}
}