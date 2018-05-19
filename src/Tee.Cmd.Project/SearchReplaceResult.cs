using ScriptPortal.Vegas;

namespace Tee.Cmd.Project
{
	public class SearchReplaceResult
	{
		private readonly object _HostItem;
		private readonly string _HostType;
		private readonly string _SearchPhrase;
		private bool _Include = true;

		public SearchReplaceResult(string SearchPhrase, object HostItem, string HostType)
		{
			_SearchPhrase = SearchPhrase;
			_HostType = HostType;

			switch (_HostType)
			{
				case "Sony.Vegas.Track":
					_HostItem = HostItem as Track;
					break;

				case "Sony.Vegas.TrackEvent":
					_HostItem = HostItem as TrackEvent;
					break;

				case "Sony.Vegas.Region":
					_HostItem = HostItem as Region;
					break;

				case "Sony.Vegas.Marker":
					_HostItem = HostItem as Marker;
					break;

				case "Sony.Vegas.CommandMarker":
					_HostItem = HostItem as CommandMarker;
					break;
			}
		}

		public bool Include
		{
			get { return _Include; }
			set { _Include = value; }
		}

		public void Replace(string ReplaceString)
		{
			switch (_HostType)
			{
				case "Sony.Vegas.Track":
					var trk = _HostItem as Track;
					if (trk != null) trk.Name = trk.Name.Replace(_SearchPhrase, ReplaceString);
					break;

				case "Sony.Vegas.TrackEvent":
					var curEvent = _HostItem as TrackEvent;
					if (curEvent != null)
						curEvent.Name = curEvent.Name == null
						                	? ReplaceString
						                	: curEvent.Name.ToLower().Replace(_SearchPhrase, ReplaceString);
					break;

				case "Sony.Vegas.Region":
					var curRegion = _HostItem as Region;
					if (curRegion != null) curRegion.Label = curRegion.Label.ToLower().Replace(_SearchPhrase, ReplaceString);
					break;

				case "Sony.Vegas.Marker":
					var curMarker = _HostItem as Marker;
					if (curMarker != null) curMarker.Label = curMarker.Label.ToLower().Replace(_SearchPhrase, ReplaceString);
					break;

				case "Sony.Vegas.CommandMarker":
					var curCommandMarker = _HostItem as CommandMarker;
					if (curCommandMarker != null)
						curCommandMarker.CommandParameter = curCommandMarker.CommandParameter.Replace(_SearchPhrase, ReplaceString);
					break;
			}
		}
	}
}