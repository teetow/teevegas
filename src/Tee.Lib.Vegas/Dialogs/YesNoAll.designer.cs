namespace Tee.Lib.Vegas.Dialogs
{
	partial class YesNoAll
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
			this.btnYes = new System.Windows.Forms.Button();
			this.btnNo = new System.Windows.Forms.Button();
			this.cbToAll = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnYes
			// 
			this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnYes.Location = new System.Drawing.Point(156, 85);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(75, 23);
			this.btnYes.TabIndex = 0;
			this.btnYes.Text = "Yes";
			this.btnYes.UseVisualStyleBackColor = true;
			// 
			// btnNo
			// 
			this.btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
			this.btnNo.Location = new System.Drawing.Point(237, 85);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(75, 23);
			this.btnNo.TabIndex = 1;
			this.btnNo.Text = "No";
			this.btnNo.UseVisualStyleBackColor = true;
			// 
			// cbToAll
			// 
			this.cbToAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbToAll.AutoSize = true;
			this.cbToAll.Location = new System.Drawing.Point(12, 89);
			this.cbToAll.Name = "cbToAll";
			this.cbToAll.Size = new System.Drawing.Size(53, 17);
			this.cbToAll.TabIndex = 2;
			this.cbToAll.Text = "To All";
			this.cbToAll.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "...";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// YesNoAll
			// 
			this.AcceptButton = this.btnYes;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.CancelButton = this.btnNo;
			this.ClientSize = new System.Drawing.Size(324, 120);
			this.ControlBox = false;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbToAll);
			this.Controls.Add(this.btnNo);
			this.Controls.Add(this.btnYes);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "YesNoAll";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Warning";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnYes;
		private System.Windows.Forms.Button btnNo;
		private System.Windows.Forms.CheckBox cbToAll;
		private System.Windows.Forms.Label label1;
	}
}