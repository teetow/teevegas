using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ScriptPortal.Vegas;
using Tee.Lib.Riff.Wave;

namespace Tee.Lib.Vegas.Render
{
	public class RenderSet : List<RenderItem>
	{
		public bool Cancel;

		public bool UseSelection = false;

		public SetProgressBoundsDelegate SetProgressBoundsInvoke;
		public SetProgressDelegate ReportProgressInvoke;
		public SetProgressStatusDelegate SetProgressStatusInvoke;
		public ErrorLogDelegate ErrorLogInvoke;

		private void SetProgressBounds(int Max, int Min = 0)
		{
			if (SetProgressBoundsInvoke != null)
				SetProgressBoundsInvoke(Max, Min);
		}

		private void SetProgress(int Progress)
		{
			if (ReportProgressInvoke != null)
				ReportProgressInvoke(Progress);
		}

		private void SetProgressStatus(string Status)
		{
			if (SetProgressStatusInvoke != null)
				SetProgressStatusInvoke(Status);
		}

		private void ErrorLog(string Status)
		{
			if (ErrorLogInvoke != null)
				ErrorLogInvoke(Status);
		}

		public void Build(ScriptPortal.Vegas.Vegas Vegas)
		{
			Clear();
			if (Vegas.GetSelectedRegions() == null)
				UseSelection = false;
			BuildScanRegions(Vegas);
			BuildApplyCounters(Vegas);
			BuildApplyTemplates(Vegas);
			BuildMergeByFilename(Vegas);
		}

		private void BuildScanRegions(ScriptPortal.Vegas.Vegas Vegas)
		{
			var regions = UseSelection ? Vegas.GetSelectedRegions().Where(r => !r.Label.ContainsRenderTag(RenderTags.NoRender)).ToList() : Vegas.Project.Regions.Where(region => !region.Label.ContainsRenderTag(RenderTags.NoRender)).ToList();
			SetProgressBounds(regions.Count);
			SetProgressStatus(RenderStrings.StatusBuildingScanningRegions);

			for (int scanCounter = 0; scanCounter < regions.Count; scanCounter++)
			{
				SetProgress(scanCounter);
				if (Cancel)
					return;

				var reg = regions[scanCounter];

				var regionParams = Vegas.Project.GetParamsAt(reg.End);
				regionParams.ParseString(reg.Label);

				var tracks = Vegas.Project.Tracks.Where(trk => string.IsNullOrEmpty(trk.Name) || !trk.Name.ContainsRenderTag(RenderTags.NoRender)).ToList();
				if (regionParams.GetParam<bool>(RenderTags.NoEmpty))
				{
					tracks = tracks.Where(item => item.Events.Any(ev => ev.Start <= reg.End && ev.End >= reg.Position)).ToList();
				}
				if (!regionParams.GetParam<bool>(RenderTags.DoVideo))
				{
					tracks = tracks.Where(trk => !trk.IsVideo()).ToList();
				}

				// straight mode
				if (!regionParams.GetParam<bool>(RenderTags.DoStems))
				{
					var localParams = new RenderParamSet(regionParams);
					foreach (var trk in tracks)
					{
						localParams.ParseString(trk.Name);
						localParams.ParseString(trk.BusTrack.Name);
					}
					var ri = new RenderItem(reg, localParams);
					ri.Tracks.AddRange(tracks);

					Add(ri);
				}

				else // stem mode
				{
					foreach (var trk in tracks)
					{
						var localParams = new RenderParamSet(regionParams);

						localParams.ParseString(trk.Name);
						localParams.ParseString(trk.BusTrack.Name);
						var ri = new RenderItem(reg, localParams);
						ri.Tracks.Add(trk);

						Add(ri);
					}
				}
			}
		}

		private void BuildApplyCounters(ScriptPortal.Vegas.Vegas Vegas)
		{
			// pass II: counters
			var nameHitCount = new Dictionary<string, int>();
			var regions = Vegas.Project.Regions.Count > 0 ? Vegas.Project.Regions.ToList() : new List<Region>();
			SetProgressBounds(regions.Count);
			SetProgressStatus(RenderStrings.StatusBuildingApplyingCounters);

			for (int i = 0; i < Vegas.Project.Regions.Count; i++)
			{
				SetProgress(i);
				if (Cancel)
					return;

				var reg = Vegas.Project.Regions[i];
				if (string.IsNullOrEmpty(reg.Label))
					continue;

				string regionLabel = reg.Label.StripRenderTags().ToLowerInvariant();
				if (!nameHitCount.ContainsKey(regionLabel))
					nameHitCount[regionLabel] = 1;

				var items = this.Where(item => item.Region.EqualsRegion(reg));
				foreach (var item in items)
				{
					item.CounterIndex = nameHitCount[regionLabel];
				}
				nameHitCount[regionLabel]++;
			}
		}

		private void BuildApplyTemplates(ScriptPortal.Vegas.Vegas Vegas)
		{
			// pass III: rendertemplates
			SetProgressBounds(Count);
			SetProgressStatus(RenderStrings.StatusBuildingApplyingTemplates);
			for (int i = 0; i < Count; i++)
			{
				SetProgress(i);
				if (Cancel)
					return;

				var ri = this[i];
				if (ri.RenderParams.GetParam<bool>(RenderTags.DoVideo) && ri.Tracks.Any(trk => trk.IsVideo()))
				{
					var renderer = ri.RenderParams.GetParam<string>(RenderTags.VideoFmt);
					ri.RenderFormat = Vegas.Renderers.FindBestRendererByName(renderer);
					ri.RenderFormat.Templates.Refresh();
					var tpl = ri.RenderParams.GetParam<string>(RenderTags.VideoTpl);
					ri.RenderTemplate = ri.RenderFormat.Templates.FindBestTemplateByName(tpl);
				}
				else
				{
					var renderer = ri.RenderParams.GetParam<string>(RenderTags.AudioFmt);
					ri.RenderFormat = Vegas.Renderers.FindBestRendererByName(renderer);
					ri.RenderFormat.Templates.Refresh();
					var tpl = ri.RenderParams.GetParam<string>(RenderTags.AudioTpl);
					ri.RenderTemplate = ri.RenderFormat.Templates.FindBestTemplateByName(tpl);
				}
			}
		}

		private void BuildMergeByFilename(ScriptPortal.Vegas.Vegas Vegas)
		{
			SetProgressBounds(Count);
			SetProgressStatus(RenderStrings.StatusBuildingOptimizing);

			// merge by filename
			for (int i = 0; i < Count; i++)
			{
				SetProgress(i);
				if (Cancel)
					return;

				var killSheet = new List<RenderItem>();
				var ri = this[i];
				var siblings = this.Where(item => item != ri && item.FilePath == ri.FilePath);

				foreach (var sibling in siblings)
				{
					ri.MergeWith(sibling);
					killSheet.Add(sibling);
				}
				RemoveAll(killSheet.Contains);
			}
		}

		public void Render(ScriptPortal.Vegas.Vegas myVegas)
		{
			SetProgressBounds(Count);
			using (UndoBlock undo = new UndoBlock("Render tracks"))
			{
				for (int i = 0; i < Count; i++)
				{
					var ri = this[i];

					foreach (var trk in myVegas.Project.Tracks)
					{
						trk.Mute = !ri.Tracks.Contains(trk);
					}

					// padding
					if (ri.RenderParams.GetParam<bool>(RenderTags.DoPadding))
					{
						if (ri.RenderTemplate.RendererID != myVegas.Renderers.FindByName("Wave (Microsoft)").ID)
						{
							ErrorLog(
								String.Format(
									"The region {0} could not be padded. Padded rendering can only be performed on .WAV (PCM) files.",
									ri.Region.Label));
						}
						else
						{
							var paddingTime = Timecode.FromSeconds(ri.PaddingSeconds);
							if (ri.Start - paddingTime < myVegas.Project.Ruler.StartTime)
							{
								ErrorLog(String.Format(
									"The region {0} could not be padded. Move your region further into the project.", ri.Region.Label));
							}
							else
							{
								ri.Start -= paddingTime;
								ri.Length += paddingTime;
								ri.Length += paddingTime;
							}
						}
					}

					if (File.Exists(ri.FilePath) && ri.RenderParams.GetParam<bool>(RenderTags.DoReadonly))
					{
						// check readonly
						var attr = File.GetAttributes(ri.FilePath);
						if (attr.IsSet(FileAttributes.ReadOnly))
						{
							File.SetAttributes(ri.FilePath, attr & ~FileAttributes.ReadOnly);
						}
					}
					SetProgress(i);
					SetProgressStatus("Rendering " + ri.FilePath);
					RenderStatus status = myVegas.Render(ri.FilePath, ri.RenderTemplate, ri.Start, ri.Length);
					if (status != RenderStatus.Complete)
					{
						ErrorLog(String.Format("{0} raised error {1}", ri.FilePath, status.ToString()));
					}
					else
					{
						// file successfully rendered

						// strip padding
						if (ri.RenderParams.GetParam<bool>(RenderTags.DoPadding))
						{
							WaveFile.StripPadding(ri.FilePath, ri.PaddingSeconds);
						}
					}
				}
				foreach (ScriptPortal.Vegas.Track trk in myVegas.Project.Tracks)
				{
					trk.Mute = false;
				}
				undo.Cancel = true; // we didn't really do anything useful.
			}
		}
	}
}