namespace Tee.Lib.Vegas.Dialogs
{
	partial class FormProgressDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pbProgress = new System.Windows.Forms.ProgressBar();
			this.tbInfo = new System.Windows.Forms.RichTextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lbStatus = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// pbProgress
			// 
			this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbProgress.Location = new System.Drawing.Point(12, 93);
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = new System.Drawing.Size(409, 23);
			this.pbProgress.Step = 1;
			this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbProgress.TabIndex = 0;
			// 
			// tbInfo
			// 
			this.tbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbInfo.Location = new System.Drawing.Point(12, 25);
			this.tbInfo.Name = "tbInfo";
			this.tbInfo.Size = new System.Drawing.Size(490, 62);
			this.tbInfo.TabIndex = 1;
			this.tbInfo.Text = "";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(427, 93);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Close";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lbStatus
			// 
			this.lbStatus.AutoSize = true;
			this.lbStatus.Location = new System.Drawing.Point(12, 8);
			this.lbStatus.Name = "lbStatus";
			this.lbStatus.Size = new System.Drawing.Size(37, 13);
			this.lbStatus.TabIndex = 3;
			this.lbStatus.Text = "Status";
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// FormProgressDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(514, 128);
			this.Controls.Add(this.lbStatus);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.tbInfo);
			this.Controls.Add(this.pbProgress);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormProgressDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Calculating...";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar pbProgress;
		private System.Windows.Forms.RichTextBox tbInfo;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lbStatus;
		private System.Windows.Forms.Timer timer1;
	}
}