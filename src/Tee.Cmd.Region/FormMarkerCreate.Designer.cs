namespace Tee.Cmd.Region
{
	partial class FormMarkerCreate
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
			this.numHowMany = new System.Windows.Forms.NumericUpDown();
			this.tbInterval = new System.Windows.Forms.TextBox();
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lbTimecodePreview = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numHowMany)).BeginInit();
			this.SuspendLayout();
			// 
			// numHowMany
			// 
			this.numHowMany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.numHowMany.Location = new System.Drawing.Point(119, 12);
			this.numHowMany.Name = "numHowMany";
			this.numHowMany.Size = new System.Drawing.Size(156, 20);
			this.numHowMany.TabIndex = 2;
			// 
			// tbInterval
			// 
			this.tbInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbInterval.Location = new System.Drawing.Point(119, 38);
			this.tbInterval.Name = "tbInterval";
			this.tbInterval.Size = new System.Drawing.Size(156, 20);
			this.tbInterval.TabIndex = 3;
			this.tbInterval.TextChanged += new System.EventHandler(this.tbInterval_TextChanged);
			// 
			// btnCreate
			// 
			this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnCreate.Location = new System.Drawing.Point(119, 74);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(75, 23);
			this.btnCreate.TabIndex = 4;
			this.btnCreate.Text = "Create";
			this.btnCreate.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(200, 74);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "How many?";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(86, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "At what interval?";
			// 
			// lbTimecodePreview
			// 
			this.lbTimecodePreview.AutoSize = true;
			this.lbTimecodePreview.Location = new System.Drawing.Point(12, 79);
			this.lbTimecodePreview.Name = "lbTimecodePreview";
			this.lbTimecodePreview.Size = new System.Drawing.Size(10, 13);
			this.lbTimecodePreview.TabIndex = 7;
			this.lbTimecodePreview.Text = " ";
			// 
			// FormMarkerCreate
			// 
			this.AcceptButton = this.btnCreate;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(287, 109);
			this.ControlBox = false;
			this.Controls.Add(this.lbTimecodePreview);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnCreate);
			this.Controls.Add(this.tbInterval);
			this.Controls.Add(this.numHowMany);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormMarkerCreate";
			this.Text = "FormMarkerCreate";
			((System.ComponentModel.ISupportInitialize)(this.numHowMany)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numHowMany;
		private System.Windows.Forms.TextBox tbInterval;
		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lbTimecodePreview;
	}
}