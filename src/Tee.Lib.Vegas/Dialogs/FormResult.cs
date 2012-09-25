using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tee.Lib.Vegas.Dialogs
{
	public partial class FormResult : Form
	{
		public FormResult()
		{
			InitializeComponent();
		}

		public void AddText(string p)
		{
			rtbText.Text += p;
		}

		private void btnCopyToClipboard_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(rtbText.Text);
		}
	}
}
