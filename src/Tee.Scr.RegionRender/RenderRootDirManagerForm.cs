using System;
using System.Windows.Forms;

namespace Tee.Scr.RegionRender
{
	public partial class RenderRootDirManagerForm : Form
	{
		RenderRootDirSet _rootDirs;

		public RenderRootDirManagerForm(string ConfFile)
		{
			InitializeComponent();
			LoadOrInit(ConfFile);
		}

		private void LoadOrInit(string ConfFile)
		{
			_rootDirs = new RenderRootDirSet(ConfFile);
			renderRootDirBindingSource.DataSource = _rootDirs.RootDirs;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			_rootDirs.SaveToFile();
		}
	}
}