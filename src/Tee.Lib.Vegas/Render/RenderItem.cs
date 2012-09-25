using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sony.Vegas;

namespace Tee.Lib.Vegas.Render
{
	public class RenderItem
	{
		// set up some defaults
		public RenderItem(Region Region, RenderParamSet Params)
		{
			this.Region = new RenderRegion(Region);
			RenderParams = Params;
		}

		public RenderParamSet RenderParams = new RenderParamSet();

		private Timecode _start;

		public Timecode Start
		{
			get
			{
				if (_start == null)
				{
					_start = (Region != null) ? Region.Position : Timecode.FromSeconds(0);
				}
				return _start;
			}
			set { _start = value; }
		}

		private Timecode _length;

		public Timecode Length
		{
			get
			{
				if (_length == null)
				{
					_length = (Region != null) ? Region.Length : Timecode.FromSeconds(0);
				}
				return _length;
			}
			set { _length = value; }
		}

		public Timecode End
		{
			get { return (Start + Length); }
		}

		public int PaddingSeconds
		{
			get
			{
				var paddingSeconds = RenderParams.GetParam<int>(RenderTags.PaddingAmt);
				return paddingSeconds;
			}
		}

		public Renderer RenderFormat { get; set; }

		public RenderTemplate RenderTemplate { get; set; }

		public RenderRegion Region;

		private string _regionLabel;

		public string RegionLabel
		{
			get
			{
				if (_regionLabel == null)
				{
					if (Region != null)
						_regionLabel = Region.Label.StripRenderTags();
				}
				return _regionLabel;
			}
		}

		private readonly List<Track> _tracks = new List<Track>();

		public List<Track> Tracks { get { return _tracks; } }

		public string TrackName { get { return (_tracks != null && Tracks.Count > 0) ? _tracks.First().Name.StripRenderTags() : string.Empty; } }

		private BusTrack _bus;

		public BusTrack Bus
		{
			get
			{
				if (_bus == null)
				{
					if (_tracks != null && Tracks.Count > 0)
						_bus = _tracks.First().BusTrack;
				}
				return _bus;
			}
		}

		public string BusName
		{
			get { return (Bus != null) ? Bus.Description : String.Empty; }
		}

		public int CounterIndex { get; set; }

		public string FilePath
		{
			get
			{
				// root + target + region
				string filepath = ApplyNamingMask(RenderParams.GetParam<string>(RenderTags.NamingMask));
				filepath = filepath.StripRenderTags();
				if (string.IsNullOrEmpty(RenderParams.GetParam<string>(RenderTags.RootDir)))
				{
					if (string.IsNullOrEmpty(RenderParams.GetParam<string>(RenderTags.TargetDir)))
					{
						filepath = Path.IsPathRooted(filepath) ? filepath : Path.Combine("C:\\", filepath);
					}
					else
						filepath = Path.Combine(RenderParams.GetParam<string>(RenderTags.TargetDir), filepath);
				}
				else
				{
					filepath = Path.Combine(RenderParams.GetParam<string>(RenderTags.RootDir), RenderParams.GetParam<string>(RenderTags.TargetDir));
				}

				if (RenderFormat != null)
				{
					filepath += RenderFormat.FileExtension.Replace("*", "");
				}
				return filepath;
			}
		}

		internal void MergeWith(RenderItem Donor)
		{
			RenderFormat = Donor.RenderFormat;
			RenderTemplate = Donor.RenderTemplate;
			RenderParams.MergeWith(Donor.RenderParams);
			Tracks.AddRange(Donor.Tracks);
		}

		internal void AddTracks(List<Track> TracksToAdd)
		{
			_tracks.AddRange(TracksToAdd);
		}

		internal string ApplyNamingMask(string NamingMask)
		{
			int iter = 0;
			while (NamingMask.ContainsRenderTag(RenderTags.Track) || NamingMask.ContainsRenderTag(RenderTags.Region) || NamingMask.ContainsRenderTag(RenderTags.Bus))
			{
				iter++;
				NamingMask = NamingMask.Replace(RenderTags.Format(RenderTags.Track), TrackName.StripRenderTags());
				NamingMask = NamingMask.Replace(RenderTags.Format(RenderTags.Region), RegionLabel.StripRenderTags());
				NamingMask = NamingMask.Replace(RenderTags.Format(RenderTags.Bus), BusName.StripRenderTags());
				if (iter > 3)
				{
					break;
				}
			}

			NamingMask = NamingMask.Replace(RenderTags.Format(RenderTags.Counter), String.Format("{0:d" + RenderParams.GetParam<long>(RenderTags.CounterDigits) + "}", CounterIndex));
			return NamingMask;
		}

		public override string ToString()
		{
			// region - start - end - track name - bus
			return String.Format("{0}:{1} ({2}) t: {3} b: {4}", Region.Label, Start, Length, TrackName, Bus.Name);
		}
	}
}