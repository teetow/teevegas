using System;
using System.Windows.Forms;
using ScriptPortal.Vegas;

namespace Tee.Lib.Vegas.Dialogs
{
	public class FormTimeEntry : FormSimplePrompt
	{
		public FormTimeEntry(string Caption = "Time entry", string Prompt = "Enter time", string DataLabel = "Time")
		{
			Text = Caption;
			lblPrompt.Text = Prompt;
			lblDescription.Text = DataLabel;
			tbUserData.TextChanged += UpdateTimePreview;
		}

		public static Timecode GetUserTime(String Caption = "Time entry", String Prompt = "Enter time", String DataLabel = "Time")
		{
			var myForm = new FormTimeEntry(Caption, Prompt, DataLabel);
			DialogResult rslt = myForm.ShowDialog();

			if (rslt != DialogResult.OK)
				return null;

			Timecode parsedTC = Timecode.FromPositionString(myForm.tbUserData.Text, RulerFormat.Unknown);

			if (parsedTC == null)
				return null;

			return parsedTC;
		}

		private void UpdateTimePreview(object sender, EventArgs e)
		{
			var tb = sender as TextBox;
			if (tb == null)
				return;
			var parent = tb.Parent as FormSimplePrompt;
			if (parent == null)
				return;
			Label lbInfo = parent.lblDescription;
			Timecode parsedTC = null;
			try
			{
				parsedTC = Timecode.FromPositionString(tb.Text, RulerFormat.Unknown);
			}
			catch
			{
				lbInfo.Text = "";
			}
			if (parsedTC != null)
			{
				lbInfo.Text = parsedTC.ToPositionString(RulerFormat.Unknown);
			}
		}
	}
}