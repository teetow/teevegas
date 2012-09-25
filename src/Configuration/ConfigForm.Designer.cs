namespace Configuration
{
	partial class ConfigForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
			this.cbAppSrc = new System.Windows.Forms.ComboBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.rtbInfo = new System.Windows.Forms.RichTextBox();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lbStatus = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lbScriptDirsExist = new System.Windows.Forms.Label();
			this.btnCreateDirs = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.lbJunctionExists = new System.Windows.Forms.Label();
			this.btnJunction = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cbAppSrc
			// 
			this.cbAppSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbAppSrc.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
			this.cbAppSrc.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbAppSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAppSrc.FormattingEnabled = true;
			this.cbAppSrc.Location = new System.Drawing.Point(210, 93);
			this.cbAppSrc.Name = "cbAppSrc";
			this.cbAppSrc.Size = new System.Drawing.Size(310, 21);
			this.cbAppSrc.TabIndex = 2;
			this.cbAppSrc.SelectedIndexChanged += new System.EventHandler(this.cbAppSrc_SelectedIndexChanged);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(445, 286);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// rtbInfo
			// 
			this.rtbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtbInfo.Location = new System.Drawing.Point(12, 178);
			this.rtbInfo.Name = "rtbInfo";
			this.rtbInfo.Size = new System.Drawing.Size(508, 101);
			this.rtbInfo.TabIndex = 4;
			this.rtbInfo.TabStop = false;
			this.rtbInfo.Text = "";
			// 
			// btnRefresh
			// 
			this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRefresh.Location = new System.Drawing.Point(12, 285);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 3;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.Location = new System.Drawing.Point(12, 12);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(508, 67);
			this.richTextBox1.TabIndex = 99;
			this.richTextBox1.TabStop = false;
			this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 13);
			this.label1.TabIndex = 34;
			this.label1.Text = "Vegas folder to use:";
			// 
			// lbStatus
			// 
			this.lbStatus.AutoSize = true;
			this.lbStatus.Location = new System.Drawing.Point(157, 209);
			this.lbStatus.Name = "lbStatus";
			this.lbStatus.Size = new System.Drawing.Size(0, 13);
			this.lbStatus.TabIndex = 100;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 125);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(162, 13);
			this.label3.TabIndex = 101;
			this.label3.Text = "Are there script directories there?";
			// 
			// lbScriptDirsExist
			// 
			this.lbScriptDirsExist.AutoSize = true;
			this.lbScriptDirsExist.Location = new System.Drawing.Point(207, 125);
			this.lbScriptDirsExist.Name = "lbScriptDirsExist";
			this.lbScriptDirsExist.Size = new System.Drawing.Size(16, 13);
			this.lbScriptDirsExist.TabIndex = 102;
			this.lbScriptDirsExist.Text = "...";
			// 
			// btnCreateDirs
			// 
			this.btnCreateDirs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCreateDirs.Location = new System.Drawing.Point(445, 120);
			this.btnCreateDirs.Name = "btnCreateDirs";
			this.btnCreateDirs.Size = new System.Drawing.Size(75, 23);
			this.btnCreateDirs.TabIndex = 103;
			this.btnCreateDirs.Text = "Create";
			this.btnCreateDirs.UseVisualStyleBackColor = true;
			this.btnCreateDirs.Click += new System.EventHandler(this.btnCreateDirs_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 154);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(176, 13);
			this.label5.TabIndex = 101;
			this.label5.Text = "Do they contain links to TeeVegas?";
			// 
			// lbJunctionExists
			// 
			this.lbJunctionExists.AutoSize = true;
			this.lbJunctionExists.Location = new System.Drawing.Point(207, 154);
			this.lbJunctionExists.Name = "lbJunctionExists";
			this.lbJunctionExists.Size = new System.Drawing.Size(16, 13);
			this.lbJunctionExists.TabIndex = 102;
			this.lbJunctionExists.Text = "...";
			// 
			// btnJunction
			// 
			this.btnJunction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnJunction.Location = new System.Drawing.Point(445, 149);
			this.btnJunction.Name = "btnJunction";
			this.btnJunction.Size = new System.Drawing.Size(75, 23);
			this.btnJunction.TabIndex = 103;
			this.btnJunction.Text = "Create";
			this.btnJunction.UseVisualStyleBackColor = true;
			this.btnJunction.Click += new System.EventHandler(this.btnJunction_Click);
			// 
			// ConfigForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(532, 321);
			this.Controls.Add(this.btnJunction);
			this.Controls.Add(this.btnCreateDirs);
			this.Controls.Add(this.lbJunctionExists);
			this.Controls.Add(this.lbScriptDirsExist);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lbStatus);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbAppSrc);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.rtbInfo);
			this.Controls.Add(this.btnClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(540, 230);
			this.Name = "ConfigForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TeeVegas Configuration";
			this.Load += new System.EventHandler(this.ConfigFormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.RichTextBox rtbInfo;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.ComboBox cbAppSrc;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lbStatus;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lbScriptDirsExist;
		private System.Windows.Forms.Button btnCreateDirs;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lbJunctionExists;
		private System.Windows.Forms.Button btnJunction;
	}
}

