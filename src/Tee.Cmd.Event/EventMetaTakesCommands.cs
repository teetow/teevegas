using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptPortal.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Dialogs;

namespace Tee.Cmd.Event
{
	public class EventMetaTakesCommands
	{
		private readonly CustomCommand MetaTakeGroupNextCommand =
			new CustomCommand(CommandCategory.Edit, "Take Group Ne&xt");

		private readonly CustomCommand MetaTakeGroupPrevCommand =
			new CustomCommand(CommandCategory.Edit, "Take Group Previo&us");

		// CustomCommand MetaTakesGetOffsetCommand = new CustomCommand(CommandCategory.Edit, "Offset &Get");
		private readonly CustomCommand MetaTakesDistFirstCommand =
			new CustomCommand(CommandCategory.Edit, "Distribute &First");

		private readonly CustomCommand MetaTakesDistRevCommand =
			new CustomCommand(CommandCategory.Edit, "Distribute Re&verse");
		private readonly CustomCommand MetaTakesDistRndCommand =
			new CustomCommand(CommandCategory.Edit, "Distribute Ran&dom");

		private readonly CustomCommand MetaTakesDistSeqCommand =
			new CustomCommand(CommandCategory.Edit, "Distribute Se&quence");

		private readonly CustomCommand MetaTakesFirstCommand = new CustomCommand(CommandCategory.Edit, "Take R&eset");
		private readonly CustomCommand MetaTakesNextCommand = new CustomCommand(CommandCategory.Edit, "Take &Next");
		private readonly CustomCommand MetaTakesParent = new CustomCommand(CommandCategory.Edit, "Meta&Takes");
		private readonly CustomCommand MetaTakesPrevCommand = new CustomCommand(CommandCategory.Edit, "Take &Previous");
		private readonly CustomCommand MetaTakesResetOffsetCommand = new CustomCommand(CommandCategory.Edit, "Offset &Reset");
		private readonly CustomCommand MetaTakesSetOffsetCommand = new CustomCommand(CommandCategory.Edit, "Offset &Set");

		private readonly CustomCommand MetaTakesSplitEventCommand = new CustomCommand(CommandCategory.Edit,
																					  "Spl&it Event by MetaTakes");

		private Vegas myVegas;

		public void MetaTakesInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;
			//*/
			MetaTakesFirstCommand.Invoked += MetaTakesFirst_Invoke;
			MetaTakesNextCommand.Invoked += MetaTakesNext_Invoke;
			MetaTakesPrevCommand.Invoked += MetaTakesPrev_Invoke;
			MetaTakeGroupNextCommand.Invoked += MetaTakeGroupNextCommand_Invoked;
			MetaTakeGroupPrevCommand.Invoked += MetaTakeGroupPrevCommand_Invoked;
			//MetaTakesGetOffsetCommand.Invoked += new EventHandler(MetaTakesGetOffset_invoke);
			MetaTakesSetOffsetCommand.Invoked += MetaTakesSetOffset_invoke;
			MetaTakesResetOffsetCommand.Invoked += MetaTakesResetOffset_invoke;
			MetaTakesSplitEventCommand.Invoked += MetaTakesSplit_Invoke;
			MetaTakesDistFirstCommand.Invoked += MetaTakesDistFirstCommand_Invoked;
			MetaTakesDistSeqCommand.Invoked += MetaTakesDistSeqCommand_Invoked;
			MetaTakesDistRndCommand.Invoked += MetaTakesDistRndCommand_Invoked;
			MetaTakesDistRevCommand.Invoked += MetaTakesDistRevCommand_Invoked;

			MetaTakesParent.AddChild(MetaTakesFirstCommand);
			MetaTakesParent.AddChild(MetaTakesNextCommand);
			MetaTakesParent.AddChild(MetaTakesPrevCommand);
			MetaTakesParent.AddChild(MetaTakeGroupNextCommand);
			MetaTakesParent.AddChild(MetaTakeGroupPrevCommand);
			//MetaTakesParent.AddChild(MetaTakesGetOffsetCommand);
			MetaTakesParent.AddChild(MetaTakesSetOffsetCommand);
			MetaTakesParent.AddChild(MetaTakesResetOffsetCommand);
			MetaTakesParent.AddChild(MetaTakesSplitEventCommand);
			MetaTakesParent.AddChild(MetaTakesDistSeqCommand);
			MetaTakesParent.AddChild(MetaTakesDistRndCommand);
			MetaTakesParent.AddChild(MetaTakesDistRevCommand);

			CustomCommands.Add(MetaTakesParent);
			CustomCommands.Add(MetaTakesFirstCommand);
			CustomCommands.Add(MetaTakesNextCommand);
			CustomCommands.Add(MetaTakesPrevCommand);
			CustomCommands.Add(MetaTakeGroupNextCommand);
			CustomCommands.Add(MetaTakeGroupPrevCommand);
			//CustomCommands.Add(MetaTakesGetOffsetCommand);
			CustomCommands.Add(MetaTakesSetOffsetCommand);
			CustomCommands.Add(MetaTakesResetOffsetCommand);
			CustomCommands.Add(MetaTakesSplitEventCommand);
			CustomCommands.Add(MetaTakesDistSeqCommand);
			CustomCommands.Add(MetaTakesDistRndCommand);
			CustomCommands.Add(MetaTakesDistRevCommand);
			//*/
		}

		private void MetaTakesFirst_Invoke(object sender, EventArgs e)
		{
			Distribute(SequenceMode.Reset);
		}

		private void MetaTakesNext_Invoke(object sender, EventArgs e)
		{
			Distribute(SequenceMode.Next);
		}

		private void MetaTakesPrev_Invoke(object sender, EventArgs e)
		{
			Distribute(SequenceMode.Prev);
		}

		private void MetaTakeGroupNextCommand_Invoked(object sender, EventArgs e)
		{
			Distribute(SequenceMode.NextGroup);
		}

		private void MetaTakeGroupPrevCommand_Invoked(object sender, EventArgs e)
		{
			Distribute(SequenceMode.PrevGroup);
		}

		private void MetaTakesSetOffset_invoke(object sender, EventArgs e)
		{
			SetMarkerOffset();
		}

		private void MetaTakesResetOffset_invoke(object sender, EventArgs e)
		{
			ResetMarkerOffset();
		}

		private void MetaTakesSplit_Invoke(object sender, EventArgs e)
		{
			SplitEvent();
		}

		private void MetaTakesDistFirstCommand_Invoked(object sender, EventArgs e)
		{
			Distribute(SequenceMode.First);
		}

		private void MetaTakesDistRndCommand_Invoked(object sender, EventArgs e)
		{
			Distribute(SequenceMode.Random);
		}

		private void MetaTakesDistSeqCommand_Invoked(object sender, EventArgs e)
		{
			Distribute(SequenceMode.Sequential);
		}

		private void MetaTakesDistRevCommand_Invoked(object sender, EventArgs e)
		{
			Distribute(SequenceMode.Reverse);
		}

		private void ResetMarkerOffset()
		{
			List<TrackEvent> selected = myVegas.Project.GetSelectedEvents();

			using (var undo = new UndoBlock("Reset marker offset"))
			{
				foreach (TrackEvent ev in selected)
				{
					MetaMarker mmk = ev.FindCurrentMetaMarker();
					if (mmk == null)
						continue;
					ev.ActiveTake.Offset = mmk.GlobalMarkerPosition;
				}
			}
		}

		private void SplitEvent()
		{
			List<TrackEvent> Events = myVegas.Project.GetSelectedEvents();
			if (Events.Count == 0)
				return;

			using (var undo = new UndoBlock("Split Events by MetaTakes"))
			{
				foreach (TrackEvent ev in Events)
				{
					TrackEvent curEv = ev;
					foreach (MediaMarker mk in ev.ActiveTake.Media.Markers)
					{
						if (curEv == null)
							break; // dun goofed
						var mmk = new MetaMarker(mk, curEv);

						if (!mmk.IsWithinEventBounds)
							continue;
						curEv = curEv.Split(mmk.GlobalMarkerOffset);
					}
				}
			}
		}

		private void SetMarkerOffset()
		{
			using (var undo = new UndoBlock("Set Marker Offset"))
			{
				Timecode ofs = FormTimeEntry.GetUserTime();
				foreach (TrackEvent ev in myVegas.Project.GetSelectedEvents())
				{
					SetCurrentMarkerOffset(ev, ofs);
				}
			}
		}

		private void SetCurrentMarkerOffset(TrackEvent Event, Timecode Offset)
		{
			MetaMarker mmk = Event.FindCurrentMetaMarker();
			if (mmk == null)
				return;
			Event.ActiveTake.Offset = mmk.LocalMarkerOffset - Offset;
		}

		public void SetMetaTake(TrackEvent TrackEvent, int MarkerIndex)
		{
			Marker mk = TrackEvent.FindCurrentMarker();
			if (mk == null)
				return;
			Marker targetMk;
			try
			{
				targetMk = TrackEvent.ActiveTake.Media.Markers[MarkerIndex];
			}
			catch
			{
				return;
			}
			SetMetaTake(TrackEvent, targetMk);
		}

		public void SetMetaTake(TrackEvent TrackEvent, Marker TargetMarker)
		{
			MetaMarker currentMmk = TrackEvent.FindCurrentMetaMarker();
			if (currentMmk == null)
				return;

			var targetMmk = new MetaMarker(TargetMarker, TrackEvent);
			TrackEvent.ActiveTake.Offset = targetMmk.GlobalMarkerPosition - currentMmk.GlobalMarkerOffset;
		}

		private void Distribute(SequenceMode sequenceMode)
		{
			List<TrackEvent> events = myVegas.Project.GetSelectedEvents();

			using (var undo = new UndoBlock("Distribute: " + sequenceMode.ToString()))
			{
				if (sequenceMode == SequenceMode.Reset)
				{
					foreach (TrackEvent ev in events)
					{
						if (ev.ActiveTake.Media.Markers.Count > 0)
							SetMetaTake(ev, 0);
					}
				}
				else if (sequenceMode == SequenceMode.First)
				{
					foreach (TrackEvent ev in events)
					{
						Marker mkCur = ev.FindCurrentMarker();
						if (mkCur == null)
							continue;
						List<Marker> siblings =
							ev.ActiveTake.Media.Markers.Cast<Marker>().Where(
								mk => mkCur.Label.ToLowerInvariant() == mk.Label.ToLowerInvariant()).ToList();
						if (siblings.Count <= 1 || mkCur == siblings[0])
							continue;
						SetMetaTake(ev, siblings[0]);
					}
				}

				else if (sequenceMode == SequenceMode.Sequential)
				{
					var used = new List<Marker>();

					foreach (TrackEvent ev in events)
					{
						Marker mkCur = ev.FindCurrentMarker();
						if (mkCur == null)
							continue;

						List<Marker> siblings =
							ev.ActiveTake.Media.Markers.Cast<Marker>().Where(
								mk => mkCur.Label.ToLowerInvariant() == mk.Label.ToLowerInvariant()).ToList();
						if (siblings.Count <= 1)
							continue;

						List<Marker> usedSiblings = used.FindAll(item => item.Label.ToLowerInvariant() == mkCur.Label.ToLowerInvariant());
						// reset when full
						if (usedSiblings.Count >= siblings.Count)
						{
							used.RemoveAll(item => item.Label.ToLowerInvariant() == mkCur.Label.ToLowerInvariant());
							usedSiblings.Clear();
						}

						bool done = false;
						Marker next = null;
						int curid = siblings.FindIndex(item => item.Position == mkCur.Position);
						int offset = 0;
						while (!done)
						{
							next = siblings[(curid + offset++) % siblings.Count];
							if (usedSiblings.Find(item => item.Position == next.Position) == null)
							{
								done = true;
							}
						}
						if (next != null)
						{
							SetMetaTake(ev, next);
							used.Add(next);
						}
					}
				}
				else if (sequenceMode == SequenceMode.Reverse)
				{
					List<int> seq =
						(from ev in events select ev.FindCurrentMarker() into curMk where curMk != null select curMk.Index).ToList();
					int i = seq.Count - 1;
					foreach (TrackEvent ev in events)
					{
						SetMetaTake(ev, seq[i--]);
					}
				}
				else if (sequenceMode == SequenceMode.Random)
				{
					var rnd = new Random();
					int last = 0;
					int next = 0;
					foreach (TrackEvent ev in events)
					{
						Marker mkCur = ev.FindCurrentMarker();
						if (mkCur == null)
							continue;
						List<Marker> siblings =
							ev.ActiveTake.Media.Markers.Cast<Marker>().Where(
								mk => mkCur.Label.ToLowerInvariant() == mk.Label.ToLowerInvariant()).ToList();
						while (next == last)
						{
							next = rnd.Next(0, ev.ActiveTake.Media.Markers.Count);
							if (siblings.Count > 1)
							{
								Marker trg = ev.ActiveTake.Media.Markers[next];
								if (mkCur.Label.ToLowerInvariant() != trg.Label.ToLowerInvariant())
								{
									next = last; // force the loop to run once more
								}
							}
							else // we have only one variation
							{
								next = -1;
								break;
							}
						}
						if (next > 0)
							SetMetaTake(ev, next);
						last = next;
					}
				}
				else if (sequenceMode == SequenceMode.Next)
				{
					foreach (TrackEvent ev in events)
					{
						Marker mkCur = ev.FindCurrentMarker();
						if (mkCur == null)
							continue;
						List<int> siblings = (from Marker mk in ev.ActiveTake.Media.Markers
											  where mkCur.Label != null && mkCur.Label.ToLowerInvariant() == mk.Label.ToLowerInvariant()
											  select mk.Index).ToList();
						int curMkSibID = siblings.IndexOf(mkCur.Index);
						int nextMkID = (curMkSibID + 1) % siblings.Count;
						Marker nextMk = ev.ActiveTake.Media.Markers[siblings[nextMkID]];
						SetMetaTake(ev, nextMk);
					}
				}
				else if (sequenceMode == SequenceMode.Prev)
				{
					foreach (TrackEvent ev in events)
					{
						Marker mkCur = ev.FindCurrentMarker();
						if (mkCur == null)
							continue;
						List<int> siblings = (from Marker mk in ev.ActiveTake.Media.Markers
											  where mkCur.Label != null && mkCur.Label.ToLowerInvariant() == mk.Label.ToLowerInvariant()
											  select mk.Index).ToList();
						int curMkSibID = siblings.IndexOf(mkCur.Index);
						int nextMkID = (curMkSibID - 1) % siblings.Count;
						if (nextMkID < 0)
							nextMkID += siblings.Count;
						Marker nextMk = ev.ActiveTake.Media.Markers[siblings[nextMkID]];
						SetMetaTake(ev, nextMk);
					}
				}
				else if (sequenceMode == SequenceMode.NextGroup || sequenceMode == SequenceMode.PrevGroup)
				{
					int groupJumpDirection = 1;
					if (sequenceMode == SequenceMode.PrevGroup)
					{
						groupJumpDirection = -1;
					}
					foreach (TrackEvent ev in events)
					{
						var groups = new List<string>();
						Marker mkCur = ev.FindCurrentMarker();

						if (mkCur == null)
							continue;
						foreach (MediaMarker mk in ev.ActiveTake.Media.Markers)
						{
							if (mk.Label != null && !groups.Contains(mk.Label.ToLowerInvariant()))
							{
								groups.Add(mk.Label.ToLowerInvariant());
							}
						}
						string grpNext = string.Empty;
						for (int i = 0; i < groups.Count; i++)
						{
							if (groups[i] == mkCur.Label.ToLowerInvariant())
							{
								grpNext = groups[(groups.Count + i + groupJumpDirection) % (groups.Count)];
								break;
							}
						}
						Marker mkNext =
							ev.ActiveTake.Media.Markers.FirstOrDefault(
								mk => mk.Label != null && mk.Label.ToLowerInvariant() == grpNext.ToLowerInvariant());
						if (mkNext == null)
							return;
						SetMetaTake(ev, mkNext);
					}
				}
			}
		}

		private enum SequenceMode
		{
			None = 0,
			Reset,
			First,
			Sequential,
			Random,
			Reverse,
			Next,
			Prev,
			NextGroup,
			PrevGroup
		}
	}
}