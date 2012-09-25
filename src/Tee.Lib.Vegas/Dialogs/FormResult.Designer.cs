namespace Tee.Lib.Vegas.Dialogs
{
	partial class FormResult
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
			this.rtbText = new System.Windows.Forms.RichTextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCopyToClipboard = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// rtbText
			// 
			this.rtbText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtbText.Location = new System.Drawing.Point(12, 12);
			this.rtbText.Name = "rtbText";
			this.rtbText.Size = new System.Drawing.Size(416, 144);
			this.rtbText.TabIndex = 0;
			this.rtbText.Text = "";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(353, 162);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "Close";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCopyToClipboard
			// 
			this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCopyToClipboard.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCopyToClipboard.Location = new System.Drawing.Point(12, 162);
			this.btnCopyToClipboard.Name = "btnCopyToClipboard";
			this.btnCopyToClipboard.Size = new System.Drawing.Size(156, 23);
			this.btnCopyToClipboard.TabIndex = 2;
			this.btnCopyToClipboard.Text = "Copy to clipboard";
			this.btnCopyToClipboard.UseVisualStyleBackColor = true;
			this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
			// 
			// FormResult
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(440, 197);
			this.Controls.Add(this.btnCopyToClipboard);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.rtbText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(271, 95);
			this.Name = "FormResult";
			this.Text = "FormResult";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox rtbText;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCopyToClipboard;
	}
}