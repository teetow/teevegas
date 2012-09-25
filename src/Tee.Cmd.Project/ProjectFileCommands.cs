using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Sony.Vegas;

namespace Tee.Cmd.Project
{
	internal class ProjectFileCommands
	{
		private Vegas myVegas;

		private readonly CustomCommand ProjectFileNewVersionCommand = new CustomCommand(CommandCategory.Edit,
																						ProjectFileStrings.FileNewVersionMenuTitle);

		private readonly CustomCommand ProjectFileParentCommand = new CustomCommand(CommandCategory.Edit, ProjectFileStrings.MenuParent);

		internal void ProjectFileInit(Vegas Vegas, ref ArrayList CustomCommands)
		{
			myVegas = Vegas;
			ProjectFileNewVersionCommand.DisplayName = ProjectFileStrings.FileNewVersionWindowTitle;
			ProjectFileNewVersionCommand.MenuItemName = ProjectFileStrings.FileNewVersionMenuTitle;
			ProjectFileNewVersionCommand.Invoked += ProjectFileNewVersionCommand_Invoked;

			ProjectFileParentCommand.AddChild(ProjectFileNewVersionCommand);

			CustomCommands.Add(ProjectFileParentCommand);
			CustomCommands.Add(ProjectFileNewVersionCommand);
		}

		private void ProjectFileNewVersionCommand_Invoked(object sender, EventArgs e)
		{
			var selected =
				(from currentTrack in myVegas.Project.Tracks from ev in currentTrack.Events where ev.Selected select ev)
					.ToList();

			var files = selected.GroupBy(p => p.ActiveTake.MediaPath);

			using (var undo = new UndoBlock("Create new file"))
			{
				foreach (var file in files)
				{
					string fnOrig = file.Key;
					string fnBase = Path.GetDirectoryName(fnOrig) + Path.DirectorySeparatorChar;
					string fnFile = Path.GetFileNameWithoutExtension(fnOrig);
					if (fnFile == null) throw new ArgumentNullException("fnFile");
					string fnExt = Path.GetExtension(fnOrig);
					string newfilename;
					int counter = 1;
					int numDigits = 3;

					// check if we're already part of a sequence
					var fileEndingRegex = new Regex(@"_(?<counter>\d+)$");
					var m = fileEndingRegex.Match(Path.GetFileNameWithoutExtension(fnFile));
					if (m.Success)
					{
						string prevCtr = m.Groups["counter"].Value;
						counter = Convert.ToInt16(prevCtr);
						numDigits = prevCtr.Length;
						fnFile = fnFile.Substring(0, fnFile.Length - numDigits);
					}
					else
						fnFile = fnFile + "_";

					do
					{
						string tail = counter++.ToString().PadLeft(numDigits, '0');
						newfilename = fnBase + fnFile + tail + fnExt;
					} while (File.Exists(newfilename));

					File.Copy(fnOrig, newfilename);
					Media newMedia = myVegas.Project.MediaPool.AddMedia(newfilename);

					foreach (TrackEvent ev in file)
					{
						ev.ActiveTake.Media.ReplaceWith(newMedia);
					}
				}
			}
		}
	}
}