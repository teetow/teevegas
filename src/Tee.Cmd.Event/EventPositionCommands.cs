using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sony.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Dialogs;

namespace Tee.Cmd.Event
{
	public class EventPositionCommands
	{
		public enum EventPositionAlignMethod
		{
			None = 0,
			Left,
			Right,
			Center
		}

		public enum EventPositionModifyMethod
		{
			None = 0,
			Auto,
			Adjust,
			User
		}

		private readonly CustomCommand EventPosAlignByMarkers = new CustomCommand(CommandCategory.Edit, "Align by &Markers");
		private readonly CustomCommand EventPosAlignLeft = new CustomCommand(CommandCategory.Edit, "Align &Left");
		private readonly CustomCommand EventPosAlignRight = new CustomCommand(CommandCategory.Edit, "Align &Right");

		private readonly CustomCommand EventPosInterAuto = new CustomCommand(CommandCategory.Edit, "Interval &auto");
		private readonly CustomCommand EventPosInterDnSt = new CustomCommand(CommandCategory.Edit, "Interval &down");
		private readonly CustomCommand EventPosInterUpSt = new CustomCommand(CommandCategory.Edit, "Interval &up");
		private readonly CustomCommand EventPosInterUser = new CustomCommand(CommandCategory.Edit, "Inter&val set...");
		private readonly CustomCommand EventPosParent = new CustomCommand(CommandCategory.Edit, "Event P&osition");

		// Spacing
		private readonly CustomCommand EventPosSpaceAuto = new CustomCommand(CommandCategory.Edit, "S&pacing auto");
		private readonly CustomCommand EventPosSpaceUser = new CustomCommand(CommandCategory.Edit, "Spacing &set...");
		private Vegas myVegas;

		internal void EventPosInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;
			// Interval
			EventPosInterUpSt.Invoked += EventPosInterUpSt_Invoke;
			EventPosInterDnSt.Invoked += EventPosInterDnSt_Invoke;
			EventPosInterAuto.Invoked += EventPosInterAuto_Invoke;
			EventPosInterUser.Invoked += EventPosInterUser_Invoke;
			EventPosParent.AddChild(EventPosInterUpSt);
			EventPosParent.AddChild(EventPosInterDnSt);
			EventPosParent.AddChild(EventPosInterAuto);
			EventPosParent.AddChild(EventPosInterUser);

			// Space
			EventPosSpaceAuto.Invoked += EventPosSpaceAuto_Invoke;
			EventPosSpaceUser.Invoked += EventPosSpaceUser_Invoke;
			EventPosParent.AddChild(EventPosSpaceAuto);
			EventPosParent.AddChild(EventPosSpaceUser);

			// Align
			EventPosAlignLeft.Invoked += EventPosAlignLeft_Invoke;
			EventPosAlignRight.Invoked += EventPosAlignRight_Invoke;
			EventPosAlignByMarkers.Invoked += EventPosAlignByMarkers_Invoked;
			EventPosParent.AddChild(EventPosAlignLeft);
			EventPosParent.AddChild(EventPosAlignRight);
			EventPosParent.AddChild(EventPosAlignByMarkers);

			CustomCommands.Add(EventPosParent);
			CustomCommands.Add(EventPosInterUpSt);
			CustomCommands.Add(EventPosInterDnSt);
			CustomCommands.Add(EventPosInterAuto);
			CustomCommands.Add(EventPosInterUser);

			CustomCommands.Add(EventPosSpaceAuto);
			CustomCommands.Add(EventPosSpaceUser);

			CustomCommands.Add(EventPosAlignLeft);
			CustomCommands.Add(EventPosAlignRight);
			CustomCommands.Add(EventPosAlignByMarkers);
		}

		// Align

		private void EventPosInterUpSt_Invoke(object sender, EventArgs e)
		{
			EventPosInterChange(EventPositionModifyMethod.Adjust, Timecode.FromMilliseconds(250));
		}

		private void EventPosInterDnSt_Invoke(object sender, EventArgs e)
		{
			EventPosInterChange(EventPositionModifyMethod.Adjust, Timecode.FromMilliseconds(-250));
		}

		private void EventPosInterAuto_Invoke(object sender, EventArgs e)
		{
			EventPosInterChange(EventPositionModifyMethod.Auto);
		}

		private void EventPosInterUser_Invoke(object sender, EventArgs e)
		{
			EventPosInterChange(EventPositionModifyMethod.User);
		}

		private void EventPosSpaceAuto_Invoke(object sender, EventArgs e)
		{
			EventPosSpaceChange(EventPositionModifyMethod.Auto);
		}

		private void EventPosSpaceUser_Invoke(object sender, EventArgs e)
		{
			EventPosSpaceChange(EventPositionModifyMethod.User);
		}

		private void EventPosAlignLeft_Invoke(object sender, EventArgs e)
		{
			PosAlignLeft();
		}

		private void EventPosAlignRight_Invoke(object sender, EventArgs e)
		{
			PosAlignRight();
		}

		private void EventPosAlignByMarkers_Invoked(object sender, EventArgs e)
		{
			PosAlignByMarkers();
		}

		/// Position manipulation
		///
		private void EventPosInterChange(EventPositionModifyMethod Method, Timecode Interval)
		{
			List<TrackEvent> selectedEvents = myVegas.Project.GetSelectedEvents(true);
			if (selectedEvents.Count <= 1)
			{
				selectedEvents = GetEventsByTimeSelection();
				if (selectedEvents.Count <= 1)
					return;
			}

			TrackEvent FirstEvent = selectedEvents[0];
			TrackEvent LastEvent = selectedEvents[selectedEvents.Count - 1];

			if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				Interval = (Timecode.FromNanos(Interval.Nanos * 10));

			// Adjust
			if ((Method == EventPositionModifyMethod.Adjust))
			{
				using (var undo = new UndoBlock("Position: adjust"))
				{
					for (int EventCounter = 1; EventCounter < selectedEvents.Count; EventCounter++)
					{
						TrackEvent CurEvent = selectedEvents[EventCounter];

						if (CurEvent != null)
						{
							CurEvent.Start = CurEvent.Start + Timecode.FromNanos(Interval.Nanos * EventCounter);
						}
					}
				}
				return;
			}

			// Auto
			if (Method == EventPositionModifyMethod.Auto)
			{
				// get some stats.
				Timecode MediumInterval =
					Timecode.FromNanos((LastEvent.Start.Nanos - FirstEvent.Start.Nanos) / (selectedEvents.Count - 1));

				using (var undo = new UndoBlock("Position: auto"))
				{
					for (int EventCounter = 1; EventCounter < selectedEvents.Count - 1; EventCounter++)
					{
						TrackEvent CurEvent = selectedEvents[EventCounter];

						if (CurEvent != null)
						{
							CurEvent.Start = FirstEvent.Start + Timecode.FromNanos(MediumInterval.Nanos * EventCounter);
						}
					}
				}
				return;
			}

			// User
			if (Method == EventPositionModifyMethod.User)
			{
				Timecode UserInterval = FormTimeEntry.GetUserTime();
				if (UserInterval == null)
					return;

				using (var undo = new UndoBlock("Position: adjust"))
				{
					for (int EventCounter = 1; EventCounter < selectedEvents.Count; EventCounter++)
					{
						TrackEvent CurEvent = selectedEvents[EventCounter];

						if (CurEvent != null)
						{
							CurEvent.Start = FirstEvent.Start + Timecode.FromNanos(UserInterval.Nanos * EventCounter);
						}
					}
				}
			}
		}

		private List<TrackEvent> GetEventsByTimeSelection()
		{
			Timecode selStart = myVegas.Transport.SelectionStart;
			Timecode selEnd = selStart + myVegas.Transport.SelectionLength;
			if (selEnd < selStart)
			{
				Timecode temp = selStart;
				selStart = selEnd;
				selEnd = temp;
			}

			var selectedEvents = myVegas.Project.GetAllEvents().Where(item => item.Start < selEnd && item.End > selStart).ToList();
			selectedEvents = selectedEvents.SortByTime();
			return selectedEvents;
		}

		private void EventPosInterChange(EventPositionModifyMethod Method)
		{
			EventPosInterChange(Method, Timecode.FromMilliseconds(0));
		}

		/// Spacing manipulation
		///
		private void EventPosSpaceChange(EventPositionModifyMethod Method)
		{
			List<TrackEvent> TargetEvents = myVegas.Project.GetSelectedEvents(true);

			if (TargetEvents.Count <= 1)
			{
				TargetEvents = GetEventsByTimeSelection();
				if (TargetEvents.Count <= 1)
					return;
			}
			TrackEvent FirstEvent = TargetEvents[0];
			TrackEvent LastEvent = TargetEvents[TargetEvents.Count - 1];

			// Auto
			if (Method == EventPositionModifyMethod.Auto)
			{
				int _NumEvents = TargetEvents.Count;
				var MediaLength = new Timecode();

				MediaLength = TargetEvents.Aggregate(MediaLength, (current, CurrentEvent) => current + CurrentEvent.Length);

				Timecode TotalTime = LastEvent.End - FirstEvent.Start;
				Timecode Space = TotalTime - MediaLength;
				Timecode MediumInterval = Timecode.FromNanos((Space.Nanos / (_NumEvents - 1)));

				using (var undo = new UndoBlock("Spacing: auto"))
				{
					TrackEvent prevEvent = null;

					for (int eventCounter = 1; eventCounter < TargetEvents.Count; eventCounter++)
					{
						TrackEvent curEvent = TargetEvents[eventCounter];
						if (curEvent != null)
						{
							if (prevEvent != null) curEvent.Start = prevEvent.End + MediumInterval;
						}

						prevEvent = curEvent;
					}
				}
				return;
			}

			// User
			if (Method == EventPositionModifyMethod.User)
			{
				Timecode userInterval = FormTimeEntry.GetUserTime();
				if (userInterval == null)
					return;

				using (var undo = new UndoBlock("Spacing: user"))
				{
					TrackEvent prevEvent = null;

					for (int eventCounter = 0; eventCounter < TargetEvents.Count; eventCounter++)
					{
						TrackEvent curEvent = TargetEvents[eventCounter];

						if (curEvent != null)
						{
							if (prevEvent != null) curEvent.Start = prevEvent.End + userInterval;
						}

						prevEvent = curEvent;
					}
				}
			}
		}

		/// Align
		///
		private void PosAlignLeft()
		{
			List<TrackEvent> SelectedEvents = myVegas.Project.GetSelectedEvents(true);
			if (SelectedEvents.Count == 0)
				return;
			Timecode targetTime = SelectedEvents.First().Start;

			using (var undo = new UndoBlock("Align Events Left"))
			{
				foreach (TrackEvent curEvent in SelectedEvents)
				{
					curEvent.Start = targetTime;
				}
			}
		}

		private void PosAlignRight()
		{
			List<TrackEvent> SelectedEvents = myVegas.Project.GetSelectedEvents();
			if (SelectedEvents.Count == 0)
				return;

			SelectedEvents.Sort((a, b) => { return Comparer<Timecode>.Default.Compare(a.End, b.End); });

			Timecode targetTime = SelectedEvents.First().End;

			using (var undo = new UndoBlock("Align Events Right"))
			{
				foreach (TrackEvent curEvent in SelectedEvents)
				{
					curEvent.Start = targetTime - curEvent.Length;
				}
			}
		}

		private void PosAlignByMarkers()
		{
			var selectedByTrack = myVegas.Project.GetSelectedEvents().GroupBy(item => item.Track);
			if (selectedByTrack.Count() < 2)
				return;

			var guideEvents = selectedByTrack.First();

			var punchpoints = (guideEvents.Select(Ev => new { Ev, mmk = Ev.FindCurrentMetaMarker() }).Select(T => T.Ev.Start + T.mmk.GlobalMarkerOffset)).ToList();

			selectedByTrack = selectedByTrack.Skip(1); // don't adjust the guide track

			using (var undo = new UndoBlock("Align by markers"))
			{
				foreach (var group in selectedByTrack)
				{
					var events = new List<TrackEvent>();
					events.AddRange(group);
					for (int i = 0; i <= events.Count - 1; i++)
					{
						if (i > punchpoints.Count - 1)
							break;
						TrackEvent ev = events[i];
						MetaMarker mmk = ev.FindCurrentMetaMarker();
						Timecode newMkPos = punchpoints[i];
						ev.Start = newMkPos - mmk.GlobalMarkerOffset;
					}
				}
			}
		}
	}
}