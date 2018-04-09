using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptPortal.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Dialogs;

namespace Tee.Cmd.Event
{
	public class EventEdgeCommands
	{
		private readonly CustomCommand EventEdgeParent = new CustomCommand(CommandCategory.Edit, "Event E&dges");

		private readonly CustomCommand EventEdgeLftAdj = new CustomCommand(CommandCategory.Edit, "Edge &Left Adjust");
		private readonly CustomCommand EventEdgeLftAuto = new CustomCommand(CommandCategory.Edit, "Edge L&eft Auto");
		private readonly CustomCommand EventEdgeLftCursor = new CustomCommand(CommandCategory.Edit, "Edge Le&ft At Cursor");

		private readonly CustomCommand EventEdgeRgtAdj = new CustomCommand(CommandCategory.Edit, "Edge &Right Adjust");
		private readonly CustomCommand EventEdgeRgtAuto = new CustomCommand(CommandCategory.Edit, "Edge R&ight Auto");
		private readonly CustomCommand EventEdgeRgtCursor = new CustomCommand(CommandCategory.Edit, "Edge Ri&ght at Cursor");
		private Vegas myVegas;

		internal void EventEdgeInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;
			CustomCommands.Add(EventEdgeParent);

			EventEdgeLftAuto.Invoked += EventEdgeLftAuto_Invoked;
			EventEdgeLftCursor.Invoked += EventEdgeLftCr_Invoke;
			EventEdgeLftAdj.Invoked += EventEdgeLftIn_Invoke;
			EventEdgeParent.AddChild(EventEdgeLftAuto);
			EventEdgeParent.AddChild(EventEdgeLftCursor);
			EventEdgeParent.AddChild(EventEdgeLftAdj);
			CustomCommands.Add(EventEdgeLftAdj);
			CustomCommands.Add(EventEdgeLftAuto);
			CustomCommands.Add(EventEdgeLftCursor);

			EventEdgeRgtAuto.Invoked += EventEdgeRgtAuto_Invoked;
			EventEdgeRgtCursor.Invoked += EventEdgeRgtCr_Invoke;
			EventEdgeRgtAdj.Invoked += EventEdgeRgtIn_Invoke;

			EventEdgeParent.AddChild(EventEdgeRgtAdj);
			EventEdgeParent.AddChild(EventEdgeRgtAuto);
			EventEdgeParent.AddChild(EventEdgeRgtCursor);

			CustomCommands.Add(EventEdgeRgtAdj);
			CustomCommands.Add(EventEdgeRgtAuto);
			CustomCommands.Add(EventEdgeRgtCursor);
		}

		private void EventEdgeLftIn_Invoke(object sender, EventArgs e)
		{
			EventEdgeChange(EventEdgeAdjustMethod.Left);
		}

		private void EventEdgeRgtIn_Invoke(object sender, EventArgs e)
		{
			EventEdgeChange(EventEdgeAdjustMethod.Right);
		}

		private void EventEdgeLftCr_Invoke(object sender, EventArgs e)
		{
			EventEdgeTrim(EventEdgeAdjustMethod.Left, myVegas.Cursor);
		}

		private void EventEdgeRgtCr_Invoke(object sender, EventArgs e)
		{
			EventEdgeTrim(EventEdgeAdjustMethod.Right, myVegas.Cursor);
		}

		private void EventEdgeRgtAuto_Invoked(object sender, EventArgs e)
		{
			List<TrackEvent> events = myVegas.Project.GetSelectedEvents(true);
			using (var undo = new UndoBlock("Automatically adjust right edges"))
			{
				for (int i = 0; i < events.Count - 1; i++) // don't adjust last event
				{
					Timecode distToNext = events[i + 1].Start - events[i].End;
					events[i].Length += distToNext;
				}
			}
		}

		private void EventEdgeLftAuto_Invoked(object sender, EventArgs e)
		{
			List<TrackEvent> events = myVegas.Project.GetSelectedEvents(true);
			using (var undo = new UndoBlock("Automatically adjust left edges"))
			{
				for (int i = events.Count - 1; i > 0; i--) // don't adjust first event
				{
					Timecode distToNext = events[i].Start - events[i - 1].End;
					events[i].Start -= distToNext;
					events[i].Length += distToNext;
					foreach (Take take in events[i].Takes)
					{
						take.Offset -= distToNext;
					}
				}
			}
		}

		private void EventEdgeChange(EventEdgeAdjustMethod Method)
		{
			var userAmount = FormTimeEntry.GetUserTime(String.Format("Adjust {0} edge by", Method.ToString()));
			if (userAmount == null || userAmount.Nanos == 0)
				return;

			using (var undo = new UndoBlock("Adjust Event Edge"))
			{
				var events = myVegas.Project.GetSelectedEvents();

				switch (Method)
				{
					case EventEdgeAdjustMethod.Left:
						foreach (var ev in events.Where(e => e.End + userAmount >= e.Start && e.Start - userAmount <= e.End)) // don't 'wrap' events
						{
							ev.End -= userAmount;
							foreach (Take take in ev.Takes)
							{
								take.Offset += userAmount;
							}
							ev.Start += userAmount;
						}
						break;

					case EventEdgeAdjustMethod.Right:
						foreach (var ev in events.Where(e => e.End + userAmount >= e.Start && e.Start - userAmount <= e.End))
						{
							ev.End += userAmount;
						}
						break;
				}
			}
		}

		private void EventEdgeTrim(EventEdgeAdjustMethod edgeAdjustMethod, Timecode Position)
		{
			List<TrackEvent> events = myVegas.Project.GetSelectedEvents();

			using (var undo = new UndoBlock("Set Event Edge"))
			{
				switch (edgeAdjustMethod)
				{
					case EventEdgeAdjustMethod.Left:
						foreach (var ev in events)
						{
							if (ev.Loop == false && Position <= ev.Start) // can't wrap non-looping
								continue;

							var amount = Position - ev.Start;

							if (Position <= ev.End)
							{
								ev.Start = ev.Start + amount;
								ev.End -= amount;

								foreach (var take in ev.Takes)
									take.Offset += amount;
							}
							else // wraparoo
							{
								var oldEnd = ev.End;
								var oldLength = ev.Length;
								ev.Start = oldEnd;
								ev.End = Position;
								foreach (var take in ev.Takes)
									take.Offset += oldLength;
							}
						}
						break;

					case EventEdgeAdjustMethod.Right:
						foreach (var ev in events)
						{
							if (ev.Loop == false && Position >= ev.End)
								continue;

							var amount = ev.End - Position;

							if (Position >= ev.Start)
							{
								ev.End -= amount;
							}
							else // wraparoo
							{
								var oldStart = ev.Start;
								var oldEnd = ev.End;
								ev.Start = Position;
								ev.End = oldStart;

								foreach (var take in ev.Takes)
									take.Offset -= ev.End - Position;
							}
						}
						break;
				}
			}
		}
	}
}