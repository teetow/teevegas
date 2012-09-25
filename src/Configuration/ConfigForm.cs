using System;
using System.IO;
using System.Windows.Forms;

namespace Configuration
{
	public partial class ConfigForm : Form
	{
		public ConfigForm()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
		}

		private void ConfigFormLoad(object sender, EventArgs e)
		{
			LogWrite("This application uses JunctionPoint by Jeff Brown (Ingenio, Inc.)");
			if (StrDirs.AppDirs != null) cbAppSrc.Items.AddRange(StrDirs.AppDirs.ToArray());
			cbAppSrc.SelectedIndex = 0;
			EvalStatus();
		}

		private void EvalStatus()
		{
			VegasDirectoryStatus status = EvalScriptDir();

			if (status.IsSet(VegasDirectoryStatus.DirectoryExists))
			{
				lbScriptDirsExist.Text = Str.ScriptExistYes;
				btnCreateDirs.Enabled = false;
			}
			else
			{
				lbScriptDirsExist.Text = Str.ScriptExistNo;
				btnCreateDirs.Enabled = true;
			}

			if (status.IsSet(VegasDirectoryStatus.JunctionExists))
			{
				lbJunctionExists.Text = Str.ScriptExistYes;
				btnJunction.Enabled = false;
			}
			else
			{
				lbJunctionExists.Text = Str.ScriptExistNo;
				btnJunction.Enabled = true;
			}
		}

		private VegasDirectoryStatus EvalScriptDir()
		{
			// check for existing directories
			string appExtDir = Path.Combine(cbAppSrc.Text, Str.AppExtDirName);

			if (!Directory.Exists(appExtDir))
			{
				return VegasDirectoryStatus.None;
			}

			if (JunctionPoint.Exists(Path.Combine(appExtDir, Str.AppJunctionDirName)))
			{
				return VegasDirectoryStatus.DirectoryExists | VegasDirectoryStatus.JunctionExists;
			}
			return VegasDirectoryStatus.DirectoryExists;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			LogClear();
			EvalStatus();
		}

		private void cbAppSrc_SelectedIndexChanged(object sender, EventArgs e)
		{
			EvalStatus();
		}

		private void btnCreateDirs_Click(object sender, EventArgs e)
		{
			string vegasDir = cbAppSrc.Text;
			string appExtName = Path.Combine(vegasDir, Str.AppExtDirName);
			string scrName = Path.Combine(vegasDir, Str.ScrMenuDirName);

			try
			{
				if (!Directory.Exists(appExtName))
					Directory.CreateDirectory(appExtName);
				if (!Directory.Exists(scrName))
					Directory.CreateDirectory(scrName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnJunction_Click(object sender, EventArgs e)
		{
			string selectedDir;
			using (var dlg = new FolderBrowserDialog { SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) })
			{
				if (dlg.ShowDialog() != DialogResult.OK)
					return;

				selectedDir = dlg.SelectedPath;
			}
			// appext
			string vegasDir = cbAppSrc.Text;

			string appTarget = Path.Combine(selectedDir, Str.AppExtDirName);
			string appextDir = Path.Combine(vegasDir, Str.AppExtDirName);
			//appextDir = Path.Combine(appextDir, Str.AppJunctionDirName);

			string scrTarget = Path.Combine(selectedDir, Str.ScrMenuDirName);
			string scrDir = Path.Combine(vegasDir, Str.ScrMenuDirName);
			scrDir = Path.Combine(scrDir, Str.AppJunctionDirName);

			try
			{
				if (!Directory.Exists(appextDir) && !JunctionPoint.Exists(appextDir))
					CreateJunction(appextDir, appTarget);
				if (!Directory.Exists(scrDir) && !JunctionPoint.Exists(scrDir))
					CreateJunction(scrDir, scrTarget);
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void CreateJunction(string Source, string Dest)
		{
			if (string.IsNullOrEmpty(Source) || string.IsNullOrEmpty(Dest))
			{
				throw (new ArgumentNullException("Junction failed. One of the paths is empty."));
			}

			if (IsSubPath(Source, Dest))
			{
				throw new InvalidOperationException("Creating this junction would cause a circular reference.");
			}

			String Message = String.Format("A link to {0} will be created in {1}.\nReview this carefully before proceeding.",
										   Dest, Source);
			DialogResult Rslt = MessageBox.Show(Message, "Really continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

			if (Rslt != DialogResult.Yes)
				return;
			JunctionPoint.Create(Source, Dest, true);
			LogWrite(String.Format(@"{0} --> {1}", Source, Dest));
		}

		private bool IsSubPath(string Junction, string Target)
		{
			string[] jTwigs = Junction.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
			string[] tTwigs = Target.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

			if (tTwigs.Length > jTwigs.Length) // Target is deeper than Junction, cannot be circular
				return false;

			for (int i = 0; i < tTwigs.Length; i++)
			{
				string j = jTwigs[i].Replace("\\", "");
				string t = tTwigs[i].Replace("\\", "");
				if (!j.Equals(t, StringComparison.InvariantCultureIgnoreCase))
					return false;
			}
			return true;
		}

		private void LogClear()
		{
			rtbInfo.Clear();
		}

		private void LogWrite(string Entry)
		{
			rtbInfo.AppendText(Entry + Environment.NewLine);
			rtbInfo.Select(rtbInfo.Text.Length, 0);
			rtbInfo.ScrollToCaret();
		}
	}
}