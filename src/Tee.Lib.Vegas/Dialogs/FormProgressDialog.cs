using System;
using System.Windows.Forms;

namespace Tee.Lib.Vegas.Dialogs
{
	public partial class FormProgressDialog : Form
	{
		private readonly TimeSpan RefreshInterval = TimeSpan.FromMilliseconds(400);
		public bool CancelPressed;
		private DateTime LastRefresh = DateTime.Now;
		private int StepCounter;

		public FormProgressDialog(string Caption)
		{
			Init(Caption, String.Empty);
		}

		public FormProgressDialog(string Caption, string Status)
		{
			Init(Caption, Status);
		}

		public string Log
		{
			get { return tbInfo.Text; }
		}

		private void Init(string Caption, string Status)
		{
			InitializeComponent();
			StepCounter = 0;
			btnCancel.DialogResult = DialogResult.Cancel;
			SetCaption(Caption);
			SetStatus(Status);
			Refresh();
		}


		public void AddLog(string Message)
		{
			tbInfo.Text += Environment.NewLine + Message;
			tbInfo.ScrollToEnd();
		}

		public void SetStatus(string Status)
		{
			tbInfo.Text = Status;
			if (DateTime.Now - LastRefresh > RefreshInterval)
			{
				Refresh();
				LastRefresh = DateTime.Now;
			}
		}

		public void SetCaption(string Caption)
		{
			Text = Caption;
			Refresh();
		}

		public void SetBounds(int Min, int Max)
		{
			StepCounter = 0;
			pbProgress.Minimum = Min;
			pbProgress.Maximum = Max;
			pbProgress.Value = Min;
			Refresh();
		}

		public void PerformStep()
		{
			pbProgress.SuspendLayout();
			pbProgress.PerformStep();
		}

		public void SetProgress(int Progress)
		{
			pbProgress.Value = Progress;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			CancelPressed = true;
			lbStatus.Text = "Cancelling... Please wait.";
			Close();
		}

		public void PerformDeferredStep()
		{
			StepCounter++;
			var stepSize = (int) (pbProgress.Maximum/10.0m);
			if (StepCounter > stepSize)
			{
				if (pbProgress.Value + StepCounter > pbProgress.Maximum)
				{
					pbProgress.Value = pbProgress.Maximum;
				}
				else
				{
					pbProgress.Value += StepCounter;
				}
				StepCounter = 0;
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
		}
	}
}