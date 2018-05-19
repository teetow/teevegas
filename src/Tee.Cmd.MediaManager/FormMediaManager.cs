using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptPortal.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Dialogs;

namespace Tee.Cmd.MediaManager
{
	/*/
		public partial class FormMediaManager : UserControl
		{
	/*/

	public sealed partial class FormMediaManager : DockableControl
	{
		public Vegas MyVegas;
		private List<MediaListItem> _medialistItems;

		public FormMediaManager()
			: base(Strings.WindowTitle)
		{
			InitializeComponent();
			DisplayName = Strings.WindowTitle;
			Init();
		}

		public override DockWindowStyle DefaultDockWindowStyle
		{
			get { return DockWindowStyle.Docked; }
		}

		public override bool AutoSize
		{
			get { return true; }
		}

		protected override void OnLoaded(EventArgs args)
		{
			PopulateMediaListItems();
			base.OnLoaded(args);
		}

		//*/

		public void Init()
		{
			_medialistItems = new List<MediaListItem>();
			PopulateMediaListItems();
			bsMediaListItems.DataSource = _medialistItems;
		}

		public void PopulateMediaListItems()
		{
			_medialistItems.Clear();
			foreach (Media m in myVegas.Project.MediaPool)
			{
				_medialistItems.Add(new MediaListItem(m));
			}
			RefreshGrid();
		}

		private void dgMedia_LostFocus(object sender, EventArgs e)
		{
			cmMediaManager.Close();
		}

		public void RefreshGrid()
		{
			try
			{
				bsMediaListItems.SuspendBinding();
				bsMediaListItems.ResetBindings(false);
				bsMediaListItems.ResumeBinding();
			}
			catch (Exception e)
			{
				myVegas.DebugOut(e.Message);
			}
			//*/
		}

		private List<Media> GetSelectedMedia()
		{
			return
				(from DataGridViewRow row in dgMedia.SelectedRows
				 select row.DataBoundItem as MediaListItem
					 into item
					 select item.Media).ToList();
		}

		private string ResolveFilePath(string source, string target)
		{
			string folderPath = Path.GetDirectoryName(target);

			if (folderPath != null && (File.Exists(target) || Directory.Exists(folderPath)))
				// path to existing file / directory?
				return Path.GetFullPath(target);

			if (Path.GetPathRoot(target) == string.Empty) // just filename?
			{
				target = Path.GetDirectoryName(source) + Path.DirectorySeparatorChar + target;
			}

			if (Path.GetExtension(target) == string.Empty)
			{
				target = Path.ChangeExtension(target, Path.GetExtension(source));
			}
			return target;
		}

		private static string ResolveFolderPath(string TargetPath, bool CreateIfNotExist = true)
		{
			string userPath = Path.GetFullPath(TargetPath);

			if (Path.GetPathRoot(userPath) == string.Empty) // jibberish?
			{
				return string.Empty;
			}

			string folderName = Path.GetFullPath(Path.GetDirectoryName(userPath));
			string targetPath = userPath;

			if (File.Exists(userPath)) // file?
			{
				targetPath = folderName; // use only folder part
			}

			if (Directory.Exists(targetPath))
				return targetPath;

			if (!Directory.Exists(targetPath) && CreateIfNotExist) // non-existing folder?
			{
				Directory.CreateDirectory(targetPath); // create it first
			}
			else
			{
				return string.Empty;
			}

			return targetPath;
		}

		private static void CopyOrRedirect(MediaPool pool, Media media, string path)
		{
			string target = Path.GetFullPath(path);
			if (Path.GetFullPath(media.FilePath) == target)
				return;

			// copy the file
			if (!File.Exists(target))
				File.Copy(media.FilePath, target);
			Media newMedia = pool.AddMedia(target);
			media.ReplaceWith(newMedia);
			string mediaKey = media.KeyString;
			if (media.UseCount == 0)
				pool.Remove(mediaKey);
		}

		private void RenameMedia(Media Media, string TargetPath)
		{
			Media sourceMedia = Media;
			MediaPool pool = myVegas.Project.MediaPool;
			string sourceFile = sourceMedia.FilePath;

			try
			{
				var placeholder = new Media(TargetPath + ".temp");
				foreach (MediaStream stream in sourceMedia.Streams)
				{
					if (stream is VideoStream)
						placeholder.CreateOfflineStream(MediaType.Video);
					else if (stream is AudioStream)
						placeholder.CreateOfflineStream(MediaType.Audio);
				}
				sourceMedia.ReplaceWith(placeholder);
				pool.Remove(sourceFile);
				File.Move(sourceFile, TargetPath);
				placeholder.ReplaceWith(new Media(TargetPath));
				pool.Remove(placeholder.FilePath);
			}
			catch (Exception Ex)
			{
				myVegas.DebugOut(Ex.Message);
			}
		}

		/***********************************************************************************
		 *								 UI EVENTS
		 ***********************************************************************************/

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PopulateMediaListItems();
		}

		private void selectEventsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<Media> selectedMedia = GetSelectedMedia();

			using (var undo = new UndoBlock("Select media events"))
			{
				List<TrackEvent> events = MyVegas.Project.GetAllEvents();
				foreach (TrackEvent ev in events)
				{
					ev.Selected = selectedMedia.Find(delegate(Media media)
														{
															if (ev.Takes.Count == 0) return false;
															return media == ev.ActiveTake.Media;
														}) != null;
				}
			}
		}

		private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MediaPool pool = MyVegas.Project.MediaPool;
			List<Media> selectedMedia = GetSelectedMedia();

			using (var undo = new UndoBlock("Copy media to folder"))
			{
				var MovePrompt = new FormSimplePrompt("Enter destination", "Enter the destination folder here",
													  "Non-existing folder will be created.");
				DialogResult rslt = MovePrompt.ShowDialog();
				if (rslt != DialogResult.OK)
					return;

				string targetPath = ResolveFolderPath(MovePrompt.tbUserData.Text);
				if (targetPath == string.Empty)
				{
					MessageBox.Show("Weird path supplied. Please enter a path in this format: C:\\Path\\");
					return;
				}
				// show zee bar!
				bool YesToAll = false;
				bool NoToAll = false;
				var progress = new FormProgressDialog("Copying files...");
				progress.SetBounds(0, selectedMedia.Count);
				progress.Show();
				foreach (Media media in selectedMedia)
				{
					progress.PerformStep();
					string finalFileName = targetPath + Path.DirectorySeparatorChar + Path.GetFileName(media.FilePath);
					if (File.Exists(finalFileName) && !YesToAll)
					{
						if (NoToAll)
							continue;
						string Prompt =
							String.Format(
								"{0}\nRe-target this media to the specified file? If the file content differs, this may destabilize your project. \n{1}",
								media.FilePath, finalFileName);
						var dlgRetarget = new YesNoAll(Prompt, "Re-target", "Leave");
						DialogResult rsltRetarget = dlgRetarget.ShowDialog();

						if (rsltRetarget == DialogResult.Yes)
						{
							if (dlgRetarget.ToAll)
								YesToAll = true;
						}
						else
						{
							if (dlgRetarget.ToAll)
								NoToAll = true;
							continue;
						}
					}
					progress.AddLog(String.Format("Copying {0} -> {1}...", media.FilePath, finalFileName));
					progress.Refresh();
					CopyOrRedirect(pool, media, finalFileName);
					progress.AddLog("...Done.");
				}
				progress.Close();
			}
			PopulateMediaListItems();
		}

		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<Media> selectedMedia = GetSelectedMedia();

			using (var undo = new UndoBlock("Rename media"))
			{
				foreach (Media media in selectedMedia)
				{
					var MovePrompt = new FormSimplePrompt("Enter destination", "Enter the target filename",
														  "Note: you can enter an existing file.") { tbUserData = { Text = Path.GetFileName(media.FilePath) } };
					DialogResult rslt = MovePrompt.ShowDialog();
					if (rslt != DialogResult.OK)
						continue;

					string target = MovePrompt.tbUserData.Text;
					if (File.Exists(target))
					{
						DialogResult RetargetRslt =
							MessageBox.Show(
								"Re-target this media to the specified file? If the file content differs, this may destabilize your project.",
								"Target file exists", MessageBoxButtons.YesNo);
						if (RetargetRslt != DialogResult.Yes)
						{
							continue;
						}
					}

					target = ResolveFilePath(media.FilePath, target);
#if DEBUG
					DialogResult go = MessageBox.Show(String.Format("RENAME:\n{0}\nTO\n{1}", media.FilePath, target), "Do it?",
													  MessageBoxButtons.YesNo);
					if (go != DialogResult.Yes)
						continue;

#endif
					RenameMedia(media, target);
				}
			}
			PopulateMediaListItems();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MediaPool pool = MyVegas.Project.MediaPool;
			List<Media> selectedMedia = GetSelectedMedia();

			using (var undo = new UndoBlock("Delete media"))
			{
				foreach (Media media in selectedMedia)
				{
					var killsheet = new List<TrackEvent>();
					if (media.UseCount != 0)
					{
						DialogResult rslt = MessageBox.Show("Really remove this media? All associated events or takes will be removed.",
															"Media still in use", MessageBoxButtons.YesNo);
						{
							if (rslt != DialogResult.Yes)
								continue;
							foreach (TrackEvent ev in MyVegas.Project.GetAllEvents())
							{
								if (ev.Takes.Count == 1 && ev.ActiveTake.Media == media)
								{
									killsheet.Add(ev);
									continue;
								}
								foreach (Take take in ev.Takes)
								{
									if (take.Media == media)
									{
										ev.Takes.Remove(take);
									}
								}
							}
						}
						foreach (TrackEvent ev in killsheet)
						{
							var parentTrack = ev.Track;
							parentTrack.Events.Remove(ev);
						}
					}

					string mediaKey = media.KeyString;
					pool.Remove(mediaKey);
				}
			}
			PopulateMediaListItems();
		}

		private void cmMediaManager_Opening(object sender, CancelEventArgs e)
		{
			var selMedia = GetSelectedMedia();
			var items = cmMediaManager.Items.Find("renameToolStripMenuItem", true);
			if (items.Length == 0)
				return;
			ToolStripItem renameItem = items[0];
			if (selMedia.Count == 1)
			{
				renameItem.Enabled = true;
				return;
			}
			renameItem.Enabled = false;
		}

		private void markSelectedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<TrackEvent> selEvents = MyVegas.Project.GetSelectedEvents();
			var selMediaOnTimeline = new List<Media>();
			foreach (Take take in selEvents.SelectMany(ev => ev.Takes.Where(take => !selMediaOnTimeline.Contains(take.Media))))
			{
				selMediaOnTimeline.Add(take.Media);
			}
			foreach (DataGridViewRow row in dgMedia.Rows)
			{
				var item = row.DataBoundItem as MediaListItem;
				if (item == null) continue;
				Media media = item.Media;
				row.Selected = selMediaOnTimeline.Contains(media);
			}
		}

		private void copyfilePathsToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var me = sender as DataGridView;
			var str = new StringBuilder();
			if (me == null)
				return;
			foreach (MediaListItem item in from DataGridViewRow row in me.SelectedRows select row.DataBoundItem as MediaListItem)
			{
				str.AppendLine(item.Media.FilePath);
			}
			Clipboard.SetText(str.ToString());
		}
	}
}