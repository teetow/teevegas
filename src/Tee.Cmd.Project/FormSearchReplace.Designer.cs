using System.Collections.Generic;

namespace Tee.Cmd.Project
{
	sealed partial class FormSearchReplace
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
			this.btnReplace = new System.Windows.Forms.Button();
			this.btnFind = new System.Windows.Forms.Button();
			this.tbFind = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbReplace = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gbInclude = new System.Windows.Forms.GroupBox();
			this.cbEvents = new System.Windows.Forms.CheckBox();
			this.cbCmdMarkers = new System.Windows.Forms.CheckBox();
			this.cbMarkers = new System.Windows.Forms.CheckBox();
			this.cbRegions = new System.Windows.Forms.CheckBox();
			this.cbTracknames = new System.Windows.Forms.CheckBox();
			this.gbResult = new System.Windows.Forms.GroupBox();
			this.dgResults = new System.Windows.Forms.DataGridView();
			this.bsResult = new System.Windows.Forms.BindingSource(this.components);
			this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colInclude = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.timecodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.labelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gbInclude.SuspendLayout();
			this.gbResult.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgResults)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bsResult)).BeginInit();
			this.SuspendLayout();
			// 
			// btnReplace
			// 
			this.btnReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReplace.Location = new System.Drawing.Point(448, 31);
			this.btnReplace.Name = "btnReplace";
			this.btnReplace.Size = new System.Drawing.Size(75, 23);
			this.btnReplace.TabIndex = 10;
			this.btnReplace.Text = "&Replace All";
			this.btnReplace.UseVisualStyleBackColor = true;
			this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
			// 
			// btnFind
			// 
			this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFind.Location = new System.Drawing.Point(448, 5);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(75, 23);
			this.btnFind.TabIndex = 9;
			this.btnFind.Text = "&Find All";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// tbFind
			// 
			this.tbFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbFind.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.tbFind.Location = new System.Drawing.Point(59, 7);
			this.tbFind.Name = "tbFind";
			this.tbFind.Size = new System.Drawing.Size(383, 20);
			this.tbFind.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 13);
			this.label1.TabIndex = 99;
			this.label1.Text = "Find:";
			// 
			// tbReplace
			// 
			this.tbReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tbReplace.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.tbReplace.Location = new System.Drawing.Point(59, 33);
			this.tbReplace.Name = "tbReplace";
			this.tbReplace.Size = new System.Drawing.Size(383, 20);
			this.tbReplace.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 99;
			this.label2.Text = "Replace:";
			// 
			// gbInclude
			// 
			this.gbInclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbInclude.Controls.Add(this.cbEvents);
			this.gbInclude.Controls.Add(this.cbCmdMarkers);
			this.gbInclude.Controls.Add(this.cbMarkers);
			this.gbInclude.Controls.Add(this.cbRegions);
			this.gbInclude.Controls.Add(this.cbTracknames);
			this.gbInclude.Location = new System.Drawing.Point(3, 59);
			this.gbInclude.Name = "gbInclude";
			this.gbInclude.Size = new System.Drawing.Size(520, 45);
			this.gbInclude.TabIndex = 99;
			this.gbInclude.TabStop = false;
			this.gbInclude.Text = "Include";
			// 
			// cbEvents
			// 
			this.cbEvents.AutoSize = true;
			this.cbEvents.Location = new System.Drawing.Point(329, 19);
			this.cbEvents.Name = "cbEvents";
			this.cbEvents.Size = new System.Drawing.Size(59, 17);
			this.cbEvents.TabIndex = 7;
			this.cbEvents.Text = "Events";
			this.cbEvents.UseVisualStyleBackColor = true;
			// 
			// cbCmdMarkers
			// 
			this.cbCmdMarkers.AutoSize = true;
			this.cbCmdMarkers.Checked = true;
			this.cbCmdMarkers.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbCmdMarkers.Location = new System.Drawing.Point(212, 19);
			this.cbCmdMarkers.Name = "cbCmdMarkers";
			this.cbCmdMarkers.Size = new System.Drawing.Size(111, 17);
			this.cbCmdMarkers.TabIndex = 6;
			this.cbCmdMarkers.Text = "CommandMarkers";
			this.cbCmdMarkers.UseVisualStyleBackColor = true;
			// 
			// cbMarkers
			// 
			this.cbMarkers.AutoSize = true;
			this.cbMarkers.Checked = true;
			this.cbMarkers.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbMarkers.Location = new System.Drawing.Point(142, 19);
			this.cbMarkers.Name = "cbMarkers";
			this.cbMarkers.Size = new System.Drawing.Size(64, 17);
			this.cbMarkers.TabIndex = 5;
			this.cbMarkers.Text = "Markers";
			this.cbMarkers.UseVisualStyleBackColor = true;
			// 
			// cbRegions
			// 
			this.cbRegions.AutoSize = true;
			this.cbRegions.Checked = true;
			this.cbRegions.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbRegions.Location = new System.Drawing.Point(71, 19);
			this.cbRegions.Name = "cbRegions";
			this.cbRegions.Size = new System.Drawing.Size(65, 17);
			this.cbRegions.TabIndex = 4;
			this.cbRegions.Text = "Regions";
			this.cbRegions.UseVisualStyleBackColor = true;
			// 
			// cbTracknames
			// 
			this.cbTracknames.AutoSize = true;
			this.cbTracknames.Checked = true;
			this.cbTracknames.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbTracknames.Location = new System.Drawing.Point(6, 19);
			this.cbTracknames.Name = "cbTracknames";
			this.cbTracknames.Size = new System.Drawing.Size(59, 17);
			this.cbTracknames.TabIndex = 3;
			this.cbTracknames.Text = "Tracks";
			this.cbTracknames.UseVisualStyleBackColor = true;
			// 
			// gbResult
			// 
			this.gbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.gbResult.Controls.Add(this.dgResults);
			this.gbResult.Location = new System.Drawing.Point(0, 110);
			this.gbResult.Name = "gbResult";
			this.gbResult.Size = new System.Drawing.Size(526, 188);
			this.gbResult.TabIndex = 99;
			this.gbResult.TabStop = false;
			this.gbResult.Text = "Result";
			// 
			// dgResults
			// 
			this.dgResults.AllowUserToAddRows = false;
			this.dgResults.AllowUserToDeleteRows = false;
			this.dgResults.AllowUserToResizeRows = false;
			this.dgResults.AutoGenerateColumns = false;
			this.dgResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colInclude,
            this.typeDataGridViewTextBoxColumn,
            this.timecodeDataGridViewTextBoxColumn,
            this.labelDataGridViewTextBoxColumn});
			this.dgResults.DataSource = this.bsResult;
			this.dgResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgResults.Location = new System.Drawing.Point(3, 16);
			this.dgResults.Name = "dgResults";
			this.dgResults.RowHeadersVisible = false;
			this.dgResults.Size = new System.Drawing.Size(520, 169);
			this.dgResults.TabIndex = 0;
			// 
			// bsResult
			// 
			this.bsResult.DataSource = typeof(SearchReplaceResult);
			// 
			// colID
			// 
			this.colID.HeaderText = "ID";
			this.colID.Name = "colID";
			this.colID.Visible = false;
			// 
			// colInclude
			// 
			this.colInclude.DataPropertyName = "Include";
			this.colInclude.FillWeight = 20F;
			this.colInclude.HeaderText = "?";
			this.colInclude.Name = "colInclude";
			this.colInclude.Width = 20;
			// 
			// typeDataGridViewTextBoxColumn
			// 
			this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
			this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
			this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
			this.typeDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// timecodeDataGridViewTextBoxColumn
			// 
			this.timecodeDataGridViewTextBoxColumn.DataPropertyName = "Timecode";
			this.timecodeDataGridViewTextBoxColumn.HeaderText = "Timecode";
			this.timecodeDataGridViewTextBoxColumn.Name = "timecodeDataGridViewTextBoxColumn";
			this.timecodeDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// labelDataGridViewTextBoxColumn
			// 
			this.labelDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.labelDataGridViewTextBoxColumn.DataPropertyName = "Label";
			this.labelDataGridViewTextBoxColumn.HeaderText = "Label";
			this.labelDataGridViewTextBoxColumn.Name = "labelDataGridViewTextBoxColumn";
			this.labelDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// FormSearchReplace
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbResult);
			this.Controls.Add(this.gbInclude);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbReplace);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbFind);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.btnReplace);
			this.MinimumSize = new System.Drawing.Size(433, 172);
			this.Name = "FormSearchReplace";
			this.Size = new System.Drawing.Size(529, 306);
			this.gbInclude.ResumeLayout(false);
			this.gbInclude.PerformLayout();
			this.gbResult.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgResults)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bsResult)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		
		private System.Windows.Forms.Button btnReplace;
		private System.Windows.Forms.Button btnFind;
		private System.Windows.Forms.TextBox tbFind;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbReplace;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox gbInclude;
		private System.Windows.Forms.CheckBox cbEvents;
		private System.Windows.Forms.CheckBox cbMarkers;
		private System.Windows.Forms.CheckBox cbRegions;
		private System.Windows.Forms.CheckBox cbTracknames;
		private System.Windows.Forms.CheckBox cbCmdMarkers;
		private System.Windows.Forms.GroupBox gbResult;
		private System.Windows.Forms.DataGridView dgResults;
		private System.Windows.Forms.BindingSource bsResult;
		private System.Windows.Forms.DataGridViewTextBoxColumn colID;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colInclude;
		private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn timecodeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn labelDataGridViewTextBoxColumn;
		private readonly List<SearchReplaceResult> SearchResults;
	}
}