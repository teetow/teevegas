using System;
using System.Windows.Forms;
using Tee.Lib.Vegas.Render;

namespace Tee.Scr.RegionRender
{
	partial class RegionRenderForm
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.includeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.startDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.formatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.templateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.renderItemViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnRender = new System.Windows.Forms.Button();
			this.lbStatus = new System.Windows.Forms.Label();
			this.pbProgress = new System.Windows.Forms.ProgressBar();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOptions = new System.Windows.Forms.Button();
			this.cbSelection = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.renderItemViewBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.includeDataGridViewCheckBoxColumn,
            this.startDataGridViewTextBoxColumn,
            this.lengthDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.formatDataGridViewTextBoxColumn,
            this.templateDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.renderItemViewBindingSource;
			this.dataGridView1.Location = new System.Drawing.Point(12, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(718, 240);
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
			// 
			// includeDataGridViewCheckBoxColumn
			// 
			this.includeDataGridViewCheckBoxColumn.DataPropertyName = "Include";
			this.includeDataGridViewCheckBoxColumn.FillWeight = 20F;
			this.includeDataGridViewCheckBoxColumn.HeaderText = "Include";
			this.includeDataGridViewCheckBoxColumn.Name = "includeDataGridViewCheckBoxColumn";
			this.includeDataGridViewCheckBoxColumn.ReadOnly = true;
			this.includeDataGridViewCheckBoxColumn.Width = 20;
			// 
			// startDataGridViewTextBoxColumn
			// 
			this.startDataGridViewTextBoxColumn.DataPropertyName = "Start";
			this.startDataGridViewTextBoxColumn.HeaderText = "Start";
			this.startDataGridViewTextBoxColumn.Name = "startDataGridViewTextBoxColumn";
			this.startDataGridViewTextBoxColumn.ReadOnly = true;
			this.startDataGridViewTextBoxColumn.Width = 80;
			// 
			// lengthDataGridViewTextBoxColumn
			// 
			this.lengthDataGridViewTextBoxColumn.DataPropertyName = "Length";
			this.lengthDataGridViewTextBoxColumn.HeaderText = "Length";
			this.lengthDataGridViewTextBoxColumn.Name = "lengthDataGridViewTextBoxColumn";
			this.lengthDataGridViewTextBoxColumn.ReadOnly = true;
			this.lengthDataGridViewTextBoxColumn.Width = 80;
			// 
			// nameDataGridViewTextBoxColumn
			// 
			this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
			this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
			this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
			this.nameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// formatDataGridViewTextBoxColumn
			// 
			this.formatDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.formatDataGridViewTextBoxColumn.DataPropertyName = "Format";
			this.formatDataGridViewTextBoxColumn.HeaderText = "Format";
			this.formatDataGridViewTextBoxColumn.Name = "formatDataGridViewTextBoxColumn";
			this.formatDataGridViewTextBoxColumn.ReadOnly = true;
			this.formatDataGridViewTextBoxColumn.Width = 64;
			// 
			// templateDataGridViewTextBoxColumn
			// 
			this.templateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.templateDataGridViewTextBoxColumn.DataPropertyName = "Template";
			this.templateDataGridViewTextBoxColumn.HeaderText = "Template";
			this.templateDataGridViewTextBoxColumn.Name = "templateDataGridViewTextBoxColumn";
			this.templateDataGridViewTextBoxColumn.ReadOnly = true;
			this.templateDataGridViewTextBoxColumn.Width = 76;
			// 
			// renderItemViewBindingSource
			// 
			this.renderItemViewBindingSource.DataSource = typeof(Tee.Scr.RegionRender.RenderItemView);
			this.renderItemViewBindingSource.Sort = "";
			this.renderItemViewBindingSource.DataError += new System.Windows.Forms.BindingManagerDataErrorEventHandler(this.renderItemViewBindingSource_DataError);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRefresh.Location = new System.Drawing.Point(93, 258);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(75, 23);
			this.btnRefresh.TabIndex = 2;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = true;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_ClickRefresh);
			// 
			// btnRender
			// 
			this.btnRender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRender.Location = new System.Drawing.Point(655, 258);
			this.btnRender.Name = "btnRender";
			this.btnRender.Size = new System.Drawing.Size(75, 23);
			this.btnRender.TabIndex = 0;
			this.btnRender.Text = "Render";
			this.btnRender.UseVisualStyleBackColor = true;
			this.btnRender.Click += new System.EventHandler(this.btnRender_Click);
			// 
			// lbStatus
			// 
			this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lbStatus.AutoSize = true;
			this.lbStatus.Location = new System.Drawing.Point(374, 263);
			this.lbStatus.Name = "lbStatus";
			this.lbStatus.Size = new System.Drawing.Size(180, 13);
			this.lbStatus.TabIndex = 99;
			this.lbStatus.Text = "If you can read this, you are Batman.";
			// 
			// pbProgress
			// 
			this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pbProgress.Location = new System.Drawing.Point(272, 258);
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = new System.Drawing.Size(96, 23);
			this.pbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbProgress.TabIndex = 99;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(574, 258);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOptions
			// 
			this.btnOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOptions.Location = new System.Drawing.Point(12, 258);
			this.btnOptions.Name = "btnOptions";
			this.btnOptions.Size = new System.Drawing.Size(75, 23);
			this.btnOptions.TabIndex = 2;
			this.btnOptions.Text = "Options...";
			this.btnOptions.UseVisualStyleBackColor = true;
			this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
			// 
			// cbSelection
			// 
			this.cbSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbSelection.AutoSize = true;
			this.cbSelection.Location = new System.Drawing.Point(174, 262);
			this.cbSelection.Name = "cbSelection";
			this.cbSelection.Size = new System.Drawing.Size(92, 17);
			this.cbSelection.TabIndex = 100;
			this.cbSelection.Text = "Selection only";
			this.cbSelection.UseVisualStyleBackColor = true;
			this.cbSelection.CheckedChanged += new System.EventHandler(this.cbSelection_CheckedChanged);
			this.cbSelection.Click += new System.EventHandler(this.cbSelection_Click);
			// 
			// RegionRenderForm
			// 
			this.AcceptButton = this.btnRender;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(742, 293);
			this.Controls.Add(this.cbSelection);
			this.Controls.Add(this.pbProgress);
			this.Controls.Add(this.lbStatus);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnRender);
			this.Controls.Add(this.btnOptions);
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.dataGridView1);
			this.MinimumSize = new System.Drawing.Size(750, 320);
			this.Name = "RegionRenderForm";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.renderItemViewBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnRender;
		private System.Windows.Forms.BindingSource renderItemViewBindingSource;
		private System.Windows.Forms.DataGridViewCheckBoxColumn includeDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn startDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn lengthDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn formatDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn templateDataGridViewTextBoxColumn;
		private System.Windows.Forms.Label lbStatus;
		private System.Windows.Forms.ProgressBar pbProgress;
		private Button btnCancel;
		private Button btnOptions;
		private CheckBox cbSelection;
	}
}