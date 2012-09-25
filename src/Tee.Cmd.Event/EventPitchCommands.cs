using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Sony.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Dialogs;

namespace Tee.Cmd.Event
{
	public class EventPitchCommands
	{
		private const decimal MagicRatio = 1.0594630943592952645618252949463m; // MM... Magic numbers ftw

		private readonly CustomCommand CmdEventPitchDnOct = new CustomCommand(CommandCategory.Edit, "Pitch -1&2");
		private readonly CustomCommand CmdEventPitchDnOne = new CustomCommand(CommandCategory.Edit, "Pitch &-1");
		private readonly CustomCommand CmdEventPitchParent = new CustomCommand(CommandCategory.Edit, "Event &Pitch");
		private readonly CustomCommand CmdEventPitchReset = new CustomCommand(CommandCategory.Edit, "Pitch &Reset");
		private readonly CustomCommand CmdEventPitchSet = new CustomCommand(CommandCategory.Edit, "Pitch &Set");
		private readonly CustomCommand CmdEventPitchUpOct = new CustomCommand(CommandCategory.Edit, "Pitch +&12");
		private readonly CustomCommand CmdEventPitchUpOne = new CustomCommand(CommandCategory.Edit, "Pitch &+1");
		private Vegas myVegas;

		internal void EventPitchInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;

			// Pitch
			CmdEventPitchUpOne.Invoked += EventPitchUpOne_Invoke;
			CmdEventPitchUpOct.Invoked += EventPitchUpOct_Invoke;
			CmdEventPitchDnOne.Invoked += EventPitchDnOne_Invoke;
			CmdEventPitchDnOct.Invoked += EventPitchDnOct_Invoke;
			CmdEventPitchReset.Invoked += EventPitchReset_Invoke;
			CmdEventPitchSet.Invoked += EventPitchSet_Invoke;

			CmdEventPitchParent.AddChild(CmdEventPitchUpOne);
			CmdEventPitchParent.AddChild(CmdEventPitchUpOct);
			CmdEventPitchParent.AddChild(CmdEventPitchDnOne);
			CmdEventPitchParent.AddChild(CmdEventPitchDnOct);
			CmdEventPitchParent.AddChild(CmdEventPitchReset);
			CmdEventPitchParent.AddChild(CmdEventPitchSet);

			CustomCommands.Add(CmdEventPitchParent);
			CustomCommands.Add(CmdEventPitchUpOne);
			CustomCommands.Add(CmdEventPitchUpOct);
			CustomCommands.Add(CmdEventPitchReset);
			CustomCommands.Add(CmdEventPitchDnOne);
			CustomCommands.Add(CmdEventPitchDnOct);
			CustomCommands.Add(CmdEventPitchSet);
		}

		private void EventPitchUpOne_Invoke(object sender, EventArgs e)
		{
			EventPitchChange(1.0m);
		}

		private void EventPitchUpOct_Invoke(object sender, EventArgs e)
		{
			EventPitchChange(12.0m);
		}

		private void EventPitchReset_Invoke(object sender, EventArgs e)
		{
			EventPitchChangeSet(0.0m);
		}

		private void EventPitchDnOne_Invoke(object sender, EventArgs e)
		{
			EventPitchChange(-1.0m);
		}

		private void EventPitchDnOct_Invoke(object sender, EventArgs e)
		{
			EventPitchChange(-12.0m);
		}

		private void EventPitchSet_Invoke(object sender, EventArgs e)
		{
			var prompt = new FormSimplePrompt("Enter pitch", "Pitch", "Enter the pitch in semitones");
			prompt.OnEvalInput += ParsePitchString;
			if (prompt.ShowDialog() == DialogResult.Cancel)
				return;
			decimal pitch;
			bool success = decimal.TryParse(ParsePitchString(prompt.tbUserData.Text), out pitch);
			if (!success)
				return;
			EventPitchChangeSet(pitch);
		}

		private void EventPitchChangeSet(decimal Semitones)
		{
			var selected = myVegas.Project.GetSelectedEvents();
			EventPitchChangeSet(selected, Semitones);
		}

		private void EventPitchChangeSet(IEnumerable<TrackEvent> Events, decimal Semitones)
		{
			using (var undo = new UndoBlock("Set pitch to " + Semitones.ToString()))
			{
				foreach (var ev in Events)
				{
					var rateChangeFactor = Math.Pow((double)MagicRatio, (double)Semitones);
					var oldRate = ev.PlaybackRate;
					ev.AdjustPlaybackRate(rateChangeFactor, true);

					ev.Length = Timecode.FromNanos((long)(ev.Length.Nanos / (ev.PlaybackRate / oldRate)));
				}
			}
		}

		private void EventPitchChange(decimal Semitones)
		{
			var events = myVegas.Project.GetSelectedEvents();
			EventPitchChange(events, Semitones);
		}

		private void EventPitchChange(IEnumerable<TrackEvent> Events, decimal Semitones)
		{
			using (var undo = new UndoBlock("Pitch " + Semitones.ToString()))
			{
				var rateChangeFactor = (decimal)Math.Pow((double)MagicRatio, (double)Semitones);

				foreach (var ev in Events)
				{
					decimal newRate = Math.Round((decimal)ev.PlaybackRate * rateChangeFactor, 6);
					Timecode newLength = Timecode.FromNanos((long)(ev.Length.Nanos / rateChangeFactor));

					// boundary checks / clamps
					if (newRate < 0.0m)
						return;

					if ((Math.Round(newRate, 3) > 4.0m) || (Math.Round(newRate, 3) < 0.25m))
						return;

					ev.AdjustPlaybackRate((double)newRate, true);
					ev.Length = newLength;
				}
			}
		}

		public string ParsePitchString(string Entry)
		{
			decimal result;
			bool success = decimal.TryParse(Entry, out result);
			if (!success)
			{
				return "Enter a positive or negative pitch value.";
			}
			if (result < -24 || result > 24)
			{
				return "Enter a value between -24 and 24.";
			}
			return result.ToString();
		}
	}
}