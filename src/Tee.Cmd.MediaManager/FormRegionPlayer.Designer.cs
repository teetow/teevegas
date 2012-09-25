namespace Tee.Cmd.MediaManager
{
	sealed partial class FormRegionPlayer
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
			this.positionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.labelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.regionViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.regionViewBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.positionDataGridViewTextBoxColumn,
            this.lengthDataGridViewTextBoxColumn,
            this.labelDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.regionViewBindingSource;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
			this.dataGridView1.Location = new System.Drawing.Point(0, 0);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(687, 416);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.TabStop = false;
			this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
			this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
			// 
			// positionDataGridViewTextBoxColumn
			// 
			this.positionDataGridViewTextBoxColumn.DataPropertyName = "Position";
			this.positionDataGridViewTextBoxColumn.HeaderText = "Position";
			this.positionDataGridViewTextBoxColumn.Name = "positionDataGridViewTextBoxColumn";
			// 
			// lengthDataGridViewTextBoxColumn
			// 
			this.lengthDataGridViewTextBoxColumn.DataPropertyName = "Length";
			this.lengthDataGridViewTextBoxColumn.HeaderText = "Length";
			this.lengthDataGridViewTextBoxColumn.Name = "lengthDataGridViewTextBoxColumn";
			// 
			// labelDataGridViewTextBoxColumn
			// 
			this.labelDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.labelDataGridViewTextBoxColumn.DataPropertyName = "Label";
			this.labelDataGridViewTextBoxColumn.HeaderText = "Label";
			this.labelDataGridViewTextBoxColumn.Name = "labelDataGridViewTextBoxColumn";
			// 
			// regionViewBindingSource
			// 
			this.regionViewBindingSource.DataSource = typeof(RegionView);
			this.regionViewBindingSource.CurrentChanged += new System.EventHandler(this.regionViewBindingSource_CurrentChanged);
			// 
			// FormRegionPlayer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridView1);
			this.Name = "FormRegionPlayer";
			this.Size = new System.Drawing.Size(687, 416);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.regionViewBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.BindingSource regionViewBindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn positionDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn lengthDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn labelDataGridViewTextBoxColumn;

	}
}
