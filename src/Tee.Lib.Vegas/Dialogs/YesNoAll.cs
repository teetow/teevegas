using System;
using System.Windows.Forms;

namespace Tee.Lib.Vegas.Dialogs
{
	public partial class YesNoAll : Form
	{
		public bool ToAll { get { return cbToAll.Checked; } }

		public YesNoAll(String Caption, String YesButton, String NoButton)
		{
			InitializeComponent();
			Init(Caption, YesButton, NoButton);
		}

		private void Init(string Caption, string YesButton = "Yes", string NoButton = "No")
		{
			label1.Text = Caption;
			btnYes.Text = YesButton;
			btnNo.Text = NoButton;
		}
	}
}