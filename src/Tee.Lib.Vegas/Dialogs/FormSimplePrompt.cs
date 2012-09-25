using System;
using System.Windows.Forms;

namespace Tee.Lib.Vegas.Dialogs
{
	public partial class FormSimplePrompt : Form
	{
		public delegate string EvalInputHandler(string Text);

		public EvalInputHandler OnEvalInput;

		public FormSimplePrompt(string Title = "Data entry", string Prompt = "Enter data", string Description = "Data")
		{
			InitializeComponent();
			Text = Title;
			lblPrompt.Text = Prompt;
			lblDescription.Text = Description;
		}

		public override sealed string Text
		{
			get { return base.Text; }
			set { base.Text = value; }
		}

		private void tbUserData_TextChanged(object sender, EventArgs e)
		{
			if (OnEvalInput == null)
				return;

			var me = sender as TextBox;
			if (me != null) lblDescription.Text = OnEvalInput(me.Text);
		}
	}
}