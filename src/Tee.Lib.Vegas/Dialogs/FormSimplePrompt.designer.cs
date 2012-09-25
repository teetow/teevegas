namespace Tee.Lib.Vegas.Dialogs
{
	public partial class FormSimplePrompt
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
			this.tbUserData = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblPrompt = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblDescription = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbUserData
			// 
			this.tbUserData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbUserData.Location = new System.Drawing.Point(12, 25);
			this.tbUserData.Name = "tbUserData";
			this.tbUserData.Size = new System.Drawing.Size(344, 20);
			this.tbUserData.TabIndex = 0;
			this.tbUserData.TextChanged += new System.EventHandler(this.tbUserData_TextChanged);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(244, 57);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(53, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// lblPrompt
			// 
			this.lblPrompt.AutoSize = true;
			this.lblPrompt.Location = new System.Drawing.Point(9, 9);
			this.lblPrompt.Name = "lblPrompt";
			this.lblPrompt.Size = new System.Drawing.Size(30, 13);
			this.lblPrompt.TabIndex = 2;
			this.lblPrompt.Text = "Data";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(303, 57);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(53, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// lblDescription
			// 
			this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblDescription.AutoEllipsis = true;
			this.lblDescription.AutoSize = true;
			this.lblDescription.Location = new System.Drawing.Point(12, 62);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(124, 13);
			this.lblDescription.TabIndex = 4;
			this.lblDescription.Text = "Enter the requested data";
			// 
			// FormSimplePrompt
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(368, 92);
			this.ControlBox = false;
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblPrompt);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tbUserData);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSimplePrompt";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Enter data";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.TextBox tbUserData;
		public System.Windows.Forms.Button btnOK;
		public System.Windows.Forms.Label lblPrompt;
		private System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.Label lblDescription;
	}
}