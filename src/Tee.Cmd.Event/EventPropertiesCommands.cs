using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using ScriptPortal.Vegas;
using Tee.Lib.Riff.Wave;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Dialogs;
using Tee.Lib.Vegas.Project;

namespace Tee.Cmd.Event
{
	public class EventPropertiesCommands
	{
		private readonly CustomCommand EventPropertiesParent = new CustomCommand(CommandCategory.Edit, "Event P&roperties");

		private readonly CustomCommand EventPropertiesCopy = new CustomCommand(CommandCategory.Edit, "1Copy all properties");

		private readonly CustomCommand EventPropertiesPasteAll = new CustomCommand(CommandCategory.Edit, "2Paste all properties");
		private readonly CustomCommand EventPropertiesPasteFades = new CustomCommand(CommandCategory.Edit, "3Paste fades");
		private readonly CustomCommand EventPropertiesPastePitches = new CustomCommand(CommandCategory.Edit, "4Paste pitches");
		private readonly CustomCommand EventPropertiesPasteVolumes = new CustomCommand(CommandCategory.Edit, "5Paste gains");

		private readonly CustomCommand EventPropertiesMarkersCopy = new CustomCommand(CommandCategory.Edit, "6Copy markers");
		private readonly CustomCommand EventPropertiesMarkersPaste = new CustomCommand(CommandCategory.Edit, "7Paste markers");

		private readonly CustomCommand EventPropertiesGainReset = new CustomCommand(CommandCategory.Edit, "Reset Gain");
		private readonly CustomCommand EventPropertiesNormGainSet = new CustomCommand(CommandCategory.Edit, "9Set Normalization Gain");

		private readonly CustomCommand EventPropertiesTakeOffline = new CustomCommand(CommandCategory.Edit, "aSet Take to Offline");
		private Vegas myVegas;

		internal void EventPropertiesInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;
			// parent
			CustomCommands.Add(EventPropertiesParent);

			// properties copy
			EventPropertiesCopy.DisplayName = "&Copy all properties";
			EventPropertiesCopy.Invoked += EventPropertiesCopy_Invoke;
			CustomCommands.Add(EventPropertiesCopy);
			EventPropertiesParent.AddChild(EventPropertiesCopy);

			//properties paste
			EventPropertiesPasteAll.DisplayName = "Paste &all properties";
			EventPropertiesPasteFades.DisplayName = "Paste &fades";
			EventPropertiesPasteVolumes.DisplayName = "Paste &gains";
			EventPropertiesPastePitches.DisplayName = "Paste &pitches";
			EventPropertiesPasteAll.Invoked += EventPropertiesPaste_Invoke;
			EventPropertiesPasteFades.Invoked += EventPropertiesPasteFades_Invoked;
			EventPropertiesPasteVolumes.Invoked += EventPropertiesPasteVolumes_Invoked;
			EventPropertiesPastePitches.Invoked += EventPropertiesPastePitches_Invoked;
			CustomCommands.Add(EventPropertiesPasteAll);
			CustomCommands.Add(EventPropertiesPasteFades);
			CustomCommands.Add(EventPropertiesPasteVolumes);
			CustomCommands.Add(EventPropertiesPastePitches);
			EventPropertiesParent.AddChild(EventPropertiesPasteAll);
			EventPropertiesParent.AddChild(EventPropertiesPasteFades);
			EventPropertiesParent.AddChild(EventPropertiesPasteVolumes);
			EventPropertiesParent.AddChild(EventPropertiesPastePitches);

			// markers
			EventPropertiesMarkersCopy.DisplayName = "Cop&y markers";
			EventPropertiesMarkersPaste.DisplayName = "Pas&te markers";
			EventPropertiesMarkersCopy.Invoked += EventPropertiesMarkersCopy_Invoked;
			EventPropertiesMarkersPaste.Invoked += EventPropertiesMarkersPaste_Invoked;
			CustomCommands.Add(EventPropertiesMarkersCopy);
			CustomCommands.Add(EventPropertiesMarkersPaste);
			EventPropertiesParent.AddChild(EventPropertiesMarkersCopy);
			EventPropertiesParent.AddChild(EventPropertiesMarkersPaste);

			// gain
			EventPropertiesGainReset.DisplayName = "Reset &Gain";
			EventPropertiesNormGainSet.DisplayName = "Set &Normalization Gain";
			EventPropertiesTakeOffline.DisplayName = "Set Take to &Offline";
			EventPropertiesGainReset.Invoked += EventPropertiesGainReset_Invoked;
			EventPropertiesNormGainSet.Invoked += EventPropertiesNormGainSet_Invoked;
			EventPropertiesTakeOffline.Invoked += EventPropertiesTakeOffline_Invoked;
			CustomCommands.Add(EventPropertiesGainReset);
			CustomCommands.Add(EventPropertiesNormGainSet);
			CustomCommands.Add(EventPropertiesTakeOffline);
			EventPropertiesParent.AddChild(EventPropertiesGainReset);
			EventPropertiesParent.AddChild(EventPropertiesNormGainSet);
			EventPropertiesParent.AddChild(EventPropertiesTakeOffline);
		}

		private void EventPropertiesCopy_Invoke(object sender, EventArgs e)
		{
			EventPropertiesChange(EventPropertiesAdjustMethod.Copy);
		}

		private void EventPropertiesPaste_Invoke(object sender, EventArgs e)
		{
			EventPropertiesChange(EventPropertiesAdjustMethod.Paste);
		}

		private void EventPropertiesPastePitches_Invoked(object sender, EventArgs e)
		{
			EventPropertiesChange(EventPropertiesAdjustMethod.Paste, EventPropertiesFlags.PitchAmount);
		}

		private void EventPropertiesPasteVolumes_Invoked(object sender, EventArgs e)
		{
			EventPropertiesChange(EventPropertiesAdjustMethod.Paste, EventPropertiesFlags.Gain);
		}

		private void EventPropertiesPasteFades_Invoked(object sender, EventArgs e)
		{
			EventPropertiesChange(EventPropertiesAdjustMethod.Paste, EventPropertiesFlags.Fades);
		}

		private void EventPropertiesTakeOffline_Invoked(object sender, EventArgs e)
		{
			List<TrackEvent> selected = myVegas.Project.GetSelectedEvents();
			if (selected.Count == 0)
				return;

			using (var undo = new UndoBlock("Set takes offline"))
			{
				foreach (TrackEvent ev in selected)
				{
					Media offlineMedia = myVegas.Project.MediaPool.AddMedia("");
					if (ev.IsAudio())
					{
						//offlineMedia.CreateOfflineStream(MediaType.Audio);
						ev.AddTake(offlineMedia.GetAudioStreamByIndex(0), true);
					}
					if (ev.IsVideo())
					{
						//offlineMedia.CreateOfflineStream(MediaType.Video);
						ev.AddTake(offlineMedia.GetVideoStreamByIndex(0), true);
					}
				}
			}
		}

		private void EventPropertiesGainReset_Invoked(object sender, EventArgs e)
		{
			List<TrackEvent> selected = myVegas.Project.GetSelectedEvents();

			using (var undo = new UndoBlock("Reset Gain"))
			{
				foreach (TrackEvent ev in selected)
				{
					ev.SetGain(1.0f);
				}
			}
		}

		private void EventPropertiesNormGainSet_Invoked(object sender, EventArgs e)
		{
			List<TrackEvent> selected = myVegas.Project.GetSelectedEvents();
			if (selected.Count == 0)
				return;

			using (var undo = new UndoBlock("Set Normalization Gain"))
			{
				var dlg = new FormSimplePrompt("Enter gain", "Enter the gain in decibels",
											   "Enter a valid decimal number.");
				dlg.OnEvalInput += delegate(String Text)
										{
											float tryGain;
											if (float.TryParse(Text, out tryGain))
												return tryGain.ToString();
											return "Enter a valid decimal number.";
										};
				DialogResult rslt = dlg.ShowDialog();
				if (rslt != DialogResult.OK)
					return;
				float gain;

				if (!float.TryParse(dlg.tbUserData.Text, out gain))
					return;

				foreach (TrackEvent ev in selected)
				{
					if (ev.IsAudio())
					{
						var aev = ev as AudioEvent;
						aev.SetNormalizationGain(gain);
					}
				}
			}
		}

		private void EventPropertiesChange(EventPropertiesAdjustMethod Method, EventPropertiesFlags Flags = EventPropertiesFlags.All)
		{
			List<TrackEvent> Events = myVegas.Project.GetSelectedEvents();

			if (Events.Count == 0)
				return;

			if (Method == EventPropertiesAdjustMethod.Copy)
				EventPropertiesChangeCopy(Events[0]);

			else if (Method == EventPropertiesAdjustMethod.Paste)
				EventPropertiesChangePaste(Events, Flags);
		}

		private void EventPropertiesChangeCopy(TrackEvent SourceEvent)
		{
			var tpl = new EventPropertiesTemplate(SourceEvent);
			var fmt = new BinaryFormatter();
			var str = new MemoryStream();
			fmt.Serialize(str, tpl);
			byte[] bytes = str.GetBuffer();
			Clipboard.SetData(Strings.ClipboardTemplateTag, bytes);
		}

		private void EventPropertiesChangePaste(IEnumerable<TrackEvent> TargetEvents, EventPropertiesFlags Flags)
		{
			object clipboardData = Clipboard.GetData(Strings.ClipboardTemplateTag);
			if (clipboardData == null)
				return;
			var bytes = clipboardData as Byte[];
			var str = new MemoryStream(bytes);
			var fmt = new BinaryFormatter { Binder = new EventPropertiesTemplateBinder() };

			var tpl = (EventPropertiesTemplate)fmt.Deserialize(str);

			using (var undo = new UndoBlock("Paste event properties"))
			{
				foreach (TrackEvent ev in TargetEvents)
				{
					if (Flags.IsSet(EventPropertiesFlags.FadeIn))
						ev.FadeIn.Length = Timecode.FromNanos(tpl.FadeIn.LengthNanos);

					if (Flags.IsSet(EventPropertiesFlags.FadeOut))
						ev.FadeOut.Length = Timecode.FromNanos(tpl.FadeOut.LengthNanos);

					if (Flags.IsSet(EventPropertiesFlags.FadeInCurve))
						ev.FadeIn.Curve = (CurveType)Enum.Parse(typeof(CurveType), tpl.FadeIn.CurveType);

					if (Flags.IsSet(EventPropertiesFlags.FadeOutCurve))
						ev.FadeOut.Curve = (CurveType)Enum.Parse(typeof(CurveType), tpl.FadeOut.CurveType);

					if (Flags.IsSet(EventPropertiesFlags.Gain))
						ev.FadeIn.Gain = tpl.FadeIn.Gain;

					if (Flags.IsSet(EventPropertiesFlags.Loop))
						ev.Loop = tpl.Loop;

					if (Flags.IsSet(EventPropertiesFlags.Length))
						ev.Length = Timecode.FromNanos(tpl.LengthNanos);

					if (Flags.IsSet(EventPropertiesFlags.PitchAmount))
						ev.AdjustPlaybackRate(tpl.PlaybackRate, true);

					if (Flags.IsSet(EventPropertiesFlags.PitchMethod))
					{
						// Not supported by Vegas API yet
					}

					// broken, too unintuitive

					/*if (Flags.IsSet(EventPropertiesFlags.RegionOffset))
					{
						Region r = myVegas.FindSurroundingRegion(ev);

						// workaround to make sure we don't stack events within the same region
						if (r == null || r == LastSurroundingRegion)
							continue;

						ev.Start.Nanos = r.Position.Nanos + tpl.RegionOffsetNanos;
						LastSurroundingRegion = r;
					}*/
				}
			}
		}

		private void EventPropertiesMarkersCopy_Invoked(object sender, EventArgs e)
		{
			var markers = new Dictionary<double, string>();

			List<TrackEvent> selected = myVegas.Project.GetSelectedEvents(true);
			if (selected.Count == 0)
				return;

			TrackEvent selectedEvent = selected[0];
			if (!selectedEvent.IsAudio())
				return;

			Media media = selectedEvent.ActiveTake.Media;

			foreach (MediaMarker mk in media.Markers)
			{
				double posSeconds = (double)mk.Position.Nanos / 10000000; // ONE BILLION DOLLARS
				markers.Add(posSeconds, mk.Label);
			}

			Clipboard.SetData(Strings.ClipboardMarkersTag, markers);
		}

		private void EventPropertiesMarkersPaste_Invoked(object sender, EventArgs e)
		{
			object cbMarkers = Clipboard.GetData(Strings.ClipboardMarkersTag);
			if (cbMarkers == null)
				return;
			var markers = (Dictionary<double, string>)cbMarkers;

			List<TrackEvent> selected = myVegas.Project.GetSelectedEvents();

			using (var undo = new UndoBlock("Add markers to files (don't undo this)"))
			{
				foreach (TrackEvent ev in selected)
				{
					string dir = Path.GetDirectoryName(ev.ActiveTake.Media.FilePath) + Path.DirectorySeparatorChar;
					string filename = Path.GetFileName(ev.ActiveTake.Media.FilePath);

					if (filename != null && (!ev.IsAudio() || !filename.Contains(".wav"))) // wav files only, right now
						continue;

					string tempFileName = dir + Path.GetFileNameWithoutExtension(filename) + "_take02" + Path.GetExtension(filename);
					uint counter = 2;
					while (File.Exists(tempFileName))
					{
						tempFileName = dir + Path.GetFileNameWithoutExtension(filename) + "_take" + counter++.ToString().PadLeft(2, '0') +
									   Path.GetExtension(filename);
					}

					string fullpath = dir + filename;
					File.Copy(fullpath, tempFileName);
					ev.ActiveTake.Media.ReplaceWith(new Media(tempFileName));
					myVegas.Project.MediaPool.Remove(fullpath);

					try
					{
						var fileReader = new BinaryReader(new FileStream(fullpath, FileMode.Open), Encoding.ASCII);
						fileReader.Close();
					}
					catch
					{
						MessageBox.Show("File could not be opened. Try reloading Vegas.");
						undo.Cancel = true;
						return;
					}

					WaveFile.AddMarkers(fullpath, markers);

					ev.ActiveTake.Media.ReplaceWith(new Media(fullpath));
					myVegas.Project.MediaPool.Remove(tempFileName);
					File.Delete(tempFileName);
				}
			}
		}

		internal class Strings
		{
			public const string Length = "Length";
			public const string ClipboardMarkersTag = "TeeVegas Markers";
			public const string ClipboardTemplateTag = "TeeVegas Event Template";
			public const string FadeInTime = "FadeInTime";
			public const string FadeOutTime = "FadeOutTime";
			public const string FadeInCurve = "FadeInCurve";
			public const string FadeOutCurve = "FadeOutCurve";
		}
	}
}