namespace Tee.Cmd.MediaManager
{
	sealed partial class FormMediaManager
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
			this.cmMediaManager = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.selectEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.markSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyfilePathsToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bsMediaListItems = new System.Windows.Forms.BindingSource(this.components);
			this.dgMedia = new System.Windows.Forms.DataGridView();
			this.mediaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.useCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cmMediaManager.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bsMediaListItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgMedia)).BeginInit();
			this.SuspendLayout();
			// 
			// cmMediaManager
			// 
			this.cmMediaManager.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.toolStripSeparator3,
            this.copyToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.selectEventsToolStripMenuItem,
            this.markSelectedToolStripMenuItem,
            this.copyfilePathsToClipboardToolStripMenuItem});
			this.cmMediaManager.Name = "cmMediaManager";
			this.cmMediaManager.Size = new System.Drawing.Size(206, 242);
			this.cmMediaManager.Opening += new System.ComponentModel.CancelEventHandler(this.cmMediaManager_Opening);
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.refreshToolStripMenuItem.Text = "&Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(202, 6);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.copyToolStripMenuItem.Text = "&Copy to folder";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
			// 
			// renameToolStripMenuItem
			// 
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.renameToolStripMenuItem.Text = "Re&name";
			this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.deleteToolStripMenuItem.Text = "R&emove from project";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(202, 6);
			// 
			// selectEventsToolStripMenuItem
			// 
			this.selectEventsToolStripMenuItem.Name = "selectEventsToolStripMenuItem";
			this.selectEventsToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.selectEventsToolStripMenuItem.Text = "&Select events on timeline";
			this.selectEventsToolStripMenuItem.Click += new System.EventHandler(this.selectEventsToolStripMenuItem_Click);
			// 
			// markSelectedToolStripMenuItem
			// 
			this.markSelectedToolStripMenuItem.Name = "markSelectedToolStripMenuItem";
			this.markSelectedToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.markSelectedToolStripMenuItem.Text = "Select &media from timeline";
			this.markSelectedToolStripMenuItem.Click += new System.EventHandler(this.markSelectedToolStripMenuItem_Click);
			// 
			// copyfilePathsToClipboardToolStripMenuItem
			// 
			this.copyfilePathsToClipboardToolStripMenuItem.Name = "copyfilePathsToClipboardToolStripMenuItem";
			this.copyfilePathsToClipboardToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
			this.copyfilePathsToClipboardToolStripMenuItem.Text = "Copy &file paths to clipboard";
			this.copyfilePathsToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyfilePathsToClipboardToolStripMenuItem_Click);
			// 
			// bsMediaListItems
			// 
			this.bsMediaListItems.AllowNew = true;
			this.bsMediaListItems.DataSource = typeof(MediaListItem);
			// 
			// dgMedia
			// 
			this.dgMedia.AllowUserToAddRows = false;
			this.dgMedia.AllowUserToDeleteRows = false;
			this.dgMedia.AllowUserToResizeRows = false;
			this.dgMedia.AutoGenerateColumns = false;
			this.dgMedia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgMedia.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mediaDataGridViewTextBoxColumn,
            this.useCountDataGridViewTextBoxColumn});
			this.dgMedia.ContextMenuStrip = this.cmMediaManager;
			this.dgMedia.DataSource = this.bsMediaListItems;
			this.dgMedia.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgMedia.Location = new System.Drawing.Point(0, 0);
			this.dgMedia.Name = "dgMedia";
			this.dgMedia.ReadOnly = true;
			this.dgMedia.RowHeadersVisible = false;
			this.dgMedia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgMedia.Size = new System.Drawing.Size(644, 257);
			this.dgMedia.TabIndex = 1;
			this.dgMedia.Click += new System.EventHandler(this.dgMedia_LostFocus);
			this.dgMedia.LostFocus += new System.EventHandler(this.dgMedia_LostFocus);
			// 
			// mediaDataGridViewTextBoxColumn
			// 
			this.mediaDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.mediaDataGridViewTextBoxColumn.DataPropertyName = "FilePath";
			this.mediaDataGridViewTextBoxColumn.HeaderText = "File";
			this.mediaDataGridViewTextBoxColumn.Name = "mediaDataGridViewTextBoxColumn";
			this.mediaDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// useCountDataGridViewTextBoxColumn
			// 
			this.useCountDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.useCountDataGridViewTextBoxColumn.DataPropertyName = "UseCount";
			this.useCountDataGridViewTextBoxColumn.HeaderText = "Use count";
			this.useCountDataGridViewTextBoxColumn.Name = "useCountDataGridViewTextBoxColumn";
			this.useCountDataGridViewTextBoxColumn.ReadOnly = true;
			this.useCountDataGridViewTextBoxColumn.Width = 81;
			// 
			// FormMediaManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dgMedia);
			this.Name = "FormMediaManager";
			this.Size = new System.Drawing.Size(644, 257);
			this.cmMediaManager.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bsMediaListItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgMedia)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip cmMediaManager;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectEventsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem markSelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem copyfilePathsToClipboardToolStripMenuItem;
		private System.Windows.Forms.BindingSource bsMediaListItems;
		private System.Windows.Forms.DataGridView dgMedia;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.DataGridViewTextBoxColumn mediaDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn useCountDataGridViewTextBoxColumn;

	}
}