namespace Tee.Scr.RegionRender
{
	partial class RenderParamsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenderParamsForm));
			this.label1 = new System.Windows.Forms.Label();
			this.tbTargetDir = new System.Windows.Forms.TextBox();
			this.btnTargetDirManager = new System.Windows.Forms.Button();
			this.cmbAudioTemplate = new System.Windows.Forms.ComboBox();
			this.cmbAudioRenderer = new System.Windows.Forms.ComboBox();
			this.lbAudioRenderer = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.cmbVideoTemplate = new System.Windows.Forms.ComboBox();
			this.cmbVideoRenderer = new System.Windows.Forms.ComboBox();
			this.cbDoVideo = new System.Windows.Forms.CheckBox();
			this.cbDoStems = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tbNamingMask = new System.Windows.Forms.TextBox();
			this.lbNamingMaskExample = new System.Windows.Forms.Label();
			this.cbNoEmpty = new System.Windows.Forms.CheckBox();
			this.cbDoReadonly = new System.Windows.Forms.CheckBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnResetWindow = new System.Windows.Forms.Button();
			this.cbDoPadding = new System.Windows.Forms.CheckBox();
			this.tbPaddingAmount = new System.Windows.Forms.TextBox();
			this.lbPaddingPreview = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.tbCounterDigits = new System.Windows.Forms.TextBox();
			this.lbCtrDigits = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.btnResetProject = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.btnTargetDirBrowse = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 197);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(127, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Place rendered files here:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbTargetDir
			// 
			this.tbTargetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbTargetDir.Location = new System.Drawing.Point(153, 194);
			this.tbTargetDir.Name = "tbTargetDir";
			this.tbTargetDir.Size = new System.Drawing.Size(254, 20);
			this.tbTargetDir.TabIndex = 3;
			this.tbTargetDir.TextChanged += new System.EventHandler(this.tbTargetDir_TextChanged);
			// 
			// btnTargetDirManager
			// 
			this.btnTargetDirManager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTargetDirManager.Enabled = false;
			this.btnTargetDirManager.Location = new System.Drawing.Point(338, 218);
			this.btnTargetDirManager.Name = "btnTargetDirManager";
			this.btnTargetDirManager.Size = new System.Drawing.Size(101, 23);
			this.btnTargetDirManager.TabIndex = 4;
			this.btnTargetDirManager.Text = "Manage tags...";
			this.btnTargetDirManager.UseVisualStyleBackColor = true;
			this.btnTargetDirManager.Visible = false;
			this.btnTargetDirManager.Click += new System.EventHandler(this.btnTargetManagerBrowse_Click);
			// 
			// cmbAudioTemplate
			// 
			this.cmbAudioTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbAudioTemplate.FormattingEnabled = true;
			this.cmbAudioTemplate.Location = new System.Drawing.Point(153, 52);
			this.cmbAudioTemplate.Name = "cmbAudioTemplate";
			this.cmbAudioTemplate.Size = new System.Drawing.Size(286, 21);
			this.cmbAudioTemplate.TabIndex = 5;
			this.cmbAudioTemplate.SelectedIndexChanged += new System.EventHandler(this.cmbAudioTemplate_SelectedIndexChanged);
			// 
			// cmbAudioRenderer
			// 
			this.cmbAudioRenderer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbAudioRenderer.FormattingEnabled = true;
			this.cmbAudioRenderer.Location = new System.Drawing.Point(153, 25);
			this.cmbAudioRenderer.Name = "cmbAudioRenderer";
			this.cmbAudioRenderer.Size = new System.Drawing.Size(286, 21);
			this.cmbAudioRenderer.TabIndex = 5;
			this.cmbAudioRenderer.SelectedIndexChanged += new System.EventHandler(this.cmbAudioRenderer_SelectedIndexChanged);
			// 
			// lbAudioRenderer
			// 
			this.lbAudioRenderer.AutoSize = true;
			this.lbAudioRenderer.Location = new System.Drawing.Point(58, 28);
			this.lbAudioRenderer.Name = "lbAudioRenderer";
			this.lbAudioRenderer.Size = new System.Drawing.Size(85, 13);
			this.lbAudioRenderer.TabIndex = 0;
			this.lbAudioRenderer.Text = "Audio file format:";
			this.lbAudioRenderer.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(31, 55);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Audio format template:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(58, 121);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Video file format:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(31, 148);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(112, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Video format template:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// cmbVideoTemplate
			// 
			this.cmbVideoTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbVideoTemplate.FormattingEnabled = true;
			this.cmbVideoTemplate.Location = new System.Drawing.Point(153, 145);
			this.cmbVideoTemplate.Name = "cmbVideoTemplate";
			this.cmbVideoTemplate.Size = new System.Drawing.Size(286, 21);
			this.cmbVideoTemplate.TabIndex = 5;
			this.cmbVideoTemplate.SelectedIndexChanged += new System.EventHandler(this.cmbVideoTemplate_SelectedIndexChanged);
			// 
			// cmbVideoRenderer
			// 
			this.cmbVideoRenderer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cmbVideoRenderer.FormattingEnabled = true;
			this.cmbVideoRenderer.Location = new System.Drawing.Point(153, 118);
			this.cmbVideoRenderer.Name = "cmbVideoRenderer";
			this.cmbVideoRenderer.Size = new System.Drawing.Size(286, 21);
			this.cmbVideoRenderer.TabIndex = 5;
			this.cmbVideoRenderer.SelectedIndexChanged += new System.EventHandler(this.cmbVideoRenderer_SelectedIndexChanged);
			// 
			// cbDoVideo
			// 
			this.cbDoVideo.AutoSize = true;
			this.cbDoVideo.Location = new System.Drawing.Point(153, 95);
			this.cbDoVideo.Name = "cbDoVideo";
			this.cbDoVideo.Size = new System.Drawing.Size(111, 17);
			this.cbDoVideo.TabIndex = 6;
			this.cbDoVideo.Text = "Render video files";
			this.cbDoVideo.UseVisualStyleBackColor = true;
			this.cbDoVideo.CheckedChanged += new System.EventHandler(this.cbDoVideo_CheckedChanged);
			// 
			// cbDoStems
			// 
			this.cbDoStems.AutoSize = true;
			this.cbDoStems.Location = new System.Drawing.Point(153, 289);
			this.cbDoStems.Name = "cbDoStems";
			this.cbDoStems.Size = new System.Drawing.Size(91, 17);
			this.cbDoStems.TabIndex = 5;
			this.cbDoStems.Text = "Render stems";
			this.cbDoStems.UseVisualStyleBackColor = true;
			this.cbDoStems.CheckedChanged += new System.EventHandler(this.cbDoStems_CheckedChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(9, 223);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(134, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Name files using this mask:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// tbNamingMask
			// 
			this.tbNamingMask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbNamingMask.Location = new System.Drawing.Point(153, 220);
			this.tbNamingMask.Name = "tbNamingMask";
			this.tbNamingMask.Size = new System.Drawing.Size(179, 20);
			this.tbNamingMask.TabIndex = 7;
			this.tbNamingMask.TextChanged += new System.EventHandler(this.tbNamingMask_TextChanged);
			// 
			// lbNamingMaskExample
			// 
			this.lbNamingMaskExample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbNamingMaskExample.AutoSize = true;
			this.lbNamingMaskExample.Location = new System.Drawing.Point(442, 223);
			this.lbNamingMaskExample.Name = "lbNamingMaskExample";
			this.lbNamingMaskExample.Size = new System.Drawing.Size(16, 13);
			this.lbNamingMaskExample.TabIndex = 8;
			this.lbNamingMaskExample.Text = "...";
			// 
			// cbNoEmpty
			// 
			this.cbNoEmpty.AutoSize = true;
			this.cbNoEmpty.Location = new System.Drawing.Point(153, 312);
			this.cbNoEmpty.Name = "cbNoEmpty";
			this.cbNoEmpty.Size = new System.Drawing.Size(143, 17);
			this.cbNoEmpty.TabIndex = 5;
			this.cbNoEmpty.Text = "Do not render empty files";
			this.cbNoEmpty.UseVisualStyleBackColor = true;
			this.cbNoEmpty.CheckedChanged += new System.EventHandler(this.cbNoEmpty_CheckedChanged);
			// 
			// cbDoReadonly
			// 
			this.cbDoReadonly.AutoSize = true;
			this.cbDoReadonly.Location = new System.Drawing.Point(153, 335);
			this.cbDoReadonly.Name = "cbDoReadonly";
			this.cbDoReadonly.Size = new System.Drawing.Size(138, 17);
			this.cbDoReadonly.TabIndex = 5;
			this.cbDoReadonly.Text = "Overwrite read-only files";
			this.cbDoReadonly.UseVisualStyleBackColor = true;
			this.cbDoReadonly.CheckedChanged += new System.EventHandler(this.cbDoReadonly_CheckedChanged);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(419, 438);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(101, 23);
			this.btnOK.TabIndex = 10;
			this.btnOK.Text = "Write settings";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnResetWindow
			// 
			this.btnResetWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnResetWindow.Location = new System.Drawing.Point(12, 438);
			this.btnResetWindow.Name = "btnResetWindow";
			this.btnResetWindow.Size = new System.Drawing.Size(101, 23);
			this.btnResetWindow.TabIndex = 10;
			this.btnResetWindow.Text = "Reset this window";
			this.btnResetWindow.UseVisualStyleBackColor = true;
			this.btnResetWindow.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// cbDoPadding
			// 
			this.cbDoPadding.AutoSize = true;
			this.cbDoPadding.Location = new System.Drawing.Point(153, 358);
			this.cbDoPadding.Name = "cbDoPadding";
			this.cbDoPadding.Size = new System.Drawing.Size(125, 17);
			this.cbDoPadding.TabIndex = 5;
			this.cbDoPadding.Text = "Pad rendered files by";
			this.cbDoPadding.UseVisualStyleBackColor = true;
			this.cbDoPadding.CheckedChanged += new System.EventHandler(this.cbDoPadding_CheckedChanged);
			// 
			// tbPaddingAmount
			// 
			this.tbPaddingAmount.Location = new System.Drawing.Point(284, 356);
			this.tbPaddingAmount.Name = "tbPaddingAmount";
			this.tbPaddingAmount.Size = new System.Drawing.Size(49, 20);
			this.tbPaddingAmount.TabIndex = 11;
			this.tbPaddingAmount.TextChanged += new System.EventHandler(this.tbPaddingAmount_TextChanged);
			// 
			// lbPaddingPreview
			// 
			this.lbPaddingPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbPaddingPreview.AutoSize = true;
			this.lbPaddingPreview.Location = new System.Drawing.Point(442, 362);
			this.lbPaddingPreview.Name = "lbPaddingPreview";
			this.lbPaddingPreview.Size = new System.Drawing.Size(16, 13);
			this.lbPaddingPreview.TabIndex = 12;
			this.lbPaddingPreview.Text = "...";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(97, 290);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(46, 13);
			this.label8.TabIndex = 9;
			this.label8.Text = "Options:";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(338, 438);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 10;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// tbCounterDigits
			// 
			this.tbCounterDigits.Location = new System.Drawing.Point(179, 246);
			this.tbCounterDigits.Name = "tbCounterDigits";
			this.tbCounterDigits.Size = new System.Drawing.Size(21, 20);
			this.tbCounterDigits.TabIndex = 7;
			this.tbCounterDigits.TextChanged += new System.EventHandler(this.tbCounterDigits_TextChanged);
			// 
			// lbCtrDigits
			// 
			this.lbCtrDigits.AutoSize = true;
			this.lbCtrDigits.Location = new System.Drawing.Point(150, 249);
			this.lbCtrDigits.Name = "lbCtrDigits";
			this.lbCtrDigits.Size = new System.Drawing.Size(26, 13);
			this.lbCtrDigits.TabIndex = 6;
			this.lbCtrDigits.Text = "Use";
			this.lbCtrDigits.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(203, 249);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(102, 13);
			this.label7.TabIndex = 6;
			this.label7.Text = "digits in file counters";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnResetProject
			// 
			this.btnResetProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnResetProject.Location = new System.Drawing.Point(119, 438);
			this.btnResetProject.Name = "btnResetProject";
			this.btnResetProject.Size = new System.Drawing.Size(101, 23);
			this.btnResetProject.TabIndex = 10;
			this.btnResetProject.Text = "Reset project";
			this.btnResetProject.UseVisualStyleBackColor = true;
			this.btnResetProject.Click += new System.EventHandler(this.btnResetProject_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(339, 359);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(47, 13);
			this.label9.TabIndex = 13;
			this.label9.Text = "seconds";
			// 
			// btnTargetDirBrowse
			// 
			this.btnTargetDirBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTargetDirBrowse.Image = ((System.Drawing.Image)(resources.GetObject("btnTargetDirBrowse.Image")));
			this.btnTargetDirBrowse.Location = new System.Drawing.Point(413, 192);
			this.btnTargetDirBrowse.Name = "btnTargetDirBrowse";
			this.btnTargetDirBrowse.Size = new System.Drawing.Size(26, 23);
			this.btnTargetDirBrowse.TabIndex = 4;
			this.btnTargetDirBrowse.UseVisualStyleBackColor = true;
			this.btnTargetDirBrowse.Click += new System.EventHandler(this.btnTargetDirBrowse_Click);
			// 
			// RenderParamsForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(532, 473);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.lbPaddingPreview);
			this.Controls.Add(this.tbPaddingAmount);
			this.Controls.Add(this.btnResetProject);
			this.Controls.Add(this.btnResetWindow);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.cbDoVideo);
			this.Controls.Add(this.lbNamingMaskExample);
			this.Controls.Add(this.lbAudioRenderer);
			this.Controls.Add(this.cbNoEmpty);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cmbVideoRenderer);
			this.Controls.Add(this.cbDoPadding);
			this.Controls.Add(this.cbDoReadonly);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbCounterDigits);
			this.Controls.Add(this.tbNamingMask);
			this.Controls.Add(this.cmbVideoTemplate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cmbAudioRenderer);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.lbCtrDigits);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.cmbAudioTemplate);
			this.Controls.Add(this.cbDoStems);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnTargetDirBrowse);
			this.Controls.Add(this.btnTargetDirManager);
			this.Controls.Add(this.tbTargetDir);
			this.MinimumSize = new System.Drawing.Size(540, 500);
			this.Name = "RenderParamsForm";
			this.Text = "Project Rendering Parameters";
			this.Load += new System.EventHandler(this.RenderParamsForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbTargetDir;
		private System.Windows.Forms.Button btnTargetDirManager;
		private System.Windows.Forms.ComboBox cmbAudioTemplate;
		private System.Windows.Forms.ComboBox cmbAudioRenderer;
		private System.Windows.Forms.Label lbAudioRenderer;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cmbVideoTemplate;
		private System.Windows.Forms.ComboBox cmbVideoRenderer;
		private System.Windows.Forms.CheckBox cbDoVideo;
		private System.Windows.Forms.CheckBox cbDoStems;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbNamingMask;
		private System.Windows.Forms.Label lbNamingMaskExample;
		private System.Windows.Forms.CheckBox cbNoEmpty;
		private System.Windows.Forms.CheckBox cbDoReadonly;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnResetWindow;
		private System.Windows.Forms.CheckBox cbDoPadding;
		private System.Windows.Forms.TextBox tbPaddingAmount;
		private System.Windows.Forms.Label lbPaddingPreview;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TextBox tbCounterDigits;
		private System.Windows.Forms.Label lbCtrDigits;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnResetProject;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnTargetDirBrowse;
	}
}