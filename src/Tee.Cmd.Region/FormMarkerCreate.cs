using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ScriptPortal.Vegas;

namespace Tee.Cmd.Region
{
	public partial class FormMarkerCreate : Form
	{
		public FormMarkerCreate()
		{
			InitializeComponent();
		}

		private void tbInterval_TextChanged(object sender, EventArgs e)
		{
			var tb = sender as TextBox;
			if (tb == null)
				return;
			var parent = tb.Parent as FormMarkerCreate;
			if (parent == null)
				return;
			Label lbInfo = parent.lbTimecodePreview;
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

		public static KeyValuePair<int, Timecode>? GetAmountAndTime()
		{
			var form = new FormMarkerCreate();
			DialogResult rslt = form.ShowDialog();
			if (rslt != DialogResult.OK)
				return null;

			int num;
			if (!int.TryParse(form.numHowMany.Value.ToString(), out num))
			{
				return null;
			}

			Timecode tc = ParseTC(form.tbInterval.Text);
			if (tc == null)
				return null;
			return new KeyValuePair<int, Timecode>(num, tc);
		}

		private static Timecode ParseTC(string str)
		{
			Timecode parsed;
			try
			{
				parsed = Timecode.FromPositionString(str, RulerFormat.Unknown);
			}
			catch
			{
				return null;
			}

			return parsed;
		}
	}
}