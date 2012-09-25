using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sony.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Dialogs;

namespace Tee.Cmd.Region
{
	internal class RegionMarkerCommands
	{
		private Vegas myVegas;

		private readonly CustomCommand MarkerCreateFromEventsCommand = new CustomCommand(CommandCategory.Edit, "Create from &Events");

		private readonly CustomCommand MarkerCreateMultiCommand = new CustomCommand(CommandCategory.Edit, "Create &Multiple");
		private readonly CustomCommand MarkerParentCommand = new CustomCommand(CommandCategory.Edit, "&Marker");
		private readonly CustomCommand MarkerSetIntervalCommand = new CustomCommand(CommandCategory.Edit, "Set inter&val");

		internal void MarkerInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;

			MarkerParentCommand.AddChild(MarkerCreateFromEventsCommand);
			MarkerParentCommand.AddChild(MarkerSetIntervalCommand);
			MarkerParentCommand.AddChild(MarkerCreateMultiCommand);
			MarkerCreateFromEventsCommand.Invoked += MarkerCreateFromEventsCommand_Invoked;
			MarkerSetIntervalCommand.Invoked += MarkerSetIntervalCommand_Invoked;
			MarkerCreateMultiCommand.Invoked += MarkerCreateMultiCommand_Invoked;

			CustomCommands.Add(MarkerParentCommand);
		}

		private void MarkerCreateMultiCommand_Invoked(object sender, EventArgs e)
		{
			KeyValuePair<int, Timecode>? data = FormMarkerCreate.GetAmountAndTime();
			if (data == null)
				return;

			int num = data.Value.Key;
			Timecode interval = data.Value.Value;

			using (var undo = new UndoBlock("Create multiple markers"))
			{
				for (int i = 0; i < num; i++)
				{
					Timecode pos = Timecode.FromNanos(myVegas.Transport.CursorPosition.Nanos + (interval.Nanos * i));
					myVegas.Project.Markers.Add(new Marker(pos));
				}
			}
		}

		private void MarkerSetIntervalCommand_Invoked(object sender, EventArgs e)
		{
			if (myVegas.Project.Markers.Count <= 0)
				return;

			var selectedMks = myVegas.GetSelectedMarkers();

			if (selectedMks.Count == 0 && myVegas.Project.Markers.Count != 0)
			{
				MessageBox.Show("Select a range of markers using the loop selection.");
				return;
			}

			Timecode time = FormTimeEntry.GetUserTime();

			if (time == null)
			{
				MessageBox.Show("You must enter a valid time.");
				return;
			}

			Timecode inc = null;

			using (var undo = new UndoBlock("Set marker interval"))
			{
				foreach (Marker mk in selectedMks)
				{
					if (inc == null)
					{
						inc = mk.Position;
					}
					mk.Position = inc;
					inc += time;
				}
			}
		}

		private void MarkerCreateFromEventsCommand_Invoked(object sender, EventArgs e)
		{
			List<TrackEvent> events = myVegas.Project.GetSelectedEvents();

			if (events.Count <= 0)
				return;

			using (var undo = new UndoBlock("Create Markers from events"))
			{
				foreach (TrackEvent ev in events)
				{
					myVegas.Project.Markers.Add(new Marker(ev.Start));
				}
			}
		}
	}
}