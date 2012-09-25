using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sony.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Render;
using Tee.Lib.Vegas.ScriptOption;

namespace Tee.Scr.RegionRender
{
	public partial class RegionRenderForm : Form
	{
		private readonly Vegas myVegas;

		internal RegionRenderForm(Vegas Vegas, RenderSet RenderSet)
		{
			myVegas = Vegas;
			_renderSet = RenderSet;
			InitializeComponent();
			Init();
		}

		private readonly RenderSet _renderSet = new RenderSet();
		private readonly List<RenderItemView> _renderViews = new List<RenderItemView>();
		private readonly ErrorLog _errorLog = new ErrorLog();

		internal void SetProgressBounds(int Max, int Min = 0)
		{
			pbProgress.Maximum = Max;
			pbProgress.Minimum = Min;
		}

		internal void SetProgress(int Progress)
		{
			pbProgress.Value = Progress;
		}

		internal void SetStatus(string Status)
		{
			lbStatus.Text = Status;
		}

		private void Init()
		{
			renderItemViewBindingSource.DataSource = _renderViews;
			_renderSet.ReportProgressInvoke = SetProgress;
			_renderSet.SetProgressBoundsInvoke = SetProgressBounds;
			_renderSet.SetProgressStatusInvoke = SetStatus;
			_renderSet.ErrorLogInvoke = ErrorLogInvoke;
			if (myVegas.GetSelectedRegions() == null || myVegas.GetSelectedRegions().Count == 0)
			{
				cbSelection.Enabled = false;
			}
			else
			{
				cbSelection.Checked = ScriptOptions.GetValue(RegionRenderStrings.ScriptName, RegionRenderStrings.SelectionOnlyOptionName, false);
				_renderSet.UseSelection = cbSelection.Checked;
			}
			PopulateRenderItems();
		}

		private void ErrorLogInvoke(string Status)
		{
			_errorLog.Push(Status);
		}

		private void PopulateRenderItems()
		{
			ClearRenderItems();
			ShowProgressBar();
			_renderSet.UseSelection = cbSelection.Checked;
			_renderSet.Build(myVegas);
			UpdateGrid();
			HideProgressBar();
		}

		private void HideProgressBar()
		{
			pbProgress.Hide();
			lbStatus.Hide();
		}

		private void ShowProgressBar()
		{
			pbProgress.Show();
			lbStatus.Show();
		}

		private void btnRefresh_ClickRefresh(object sender, EventArgs e)
		{
			PopulateRenderItems();
		}

		private void ClearRenderItems()
		{
			renderItemViewBindingSource.Clear();
			_renderViews.Clear();
			_renderSet.Clear();
		}

		private void UpdateGrid()
		{
			_renderViews.AddRange(_renderSet.Select(item => new RenderItemView(item)));
			_renderViews.Sort(delegate(RenderItemView a, RenderItemView b)
			{
				var aStart = Timecode.FromPositionString(a.Start, RulerFormat.Unknown);
				var bStart = Timecode.FromPositionString(b.Start, RulerFormat.Unknown);

				if (aStart > bStart)
					return 1;
				if (aStart < bStart)
					return -1;
				return 0;
			});
			renderItemViewBindingSource.ResetBindings(true);
		}

		private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			var me = sender as DataGridView;
			me.Rows.Clear();
		}

		private void renderItemViewBindingSource_DataError(object sender, BindingManagerDataErrorEventArgs e)
		{
			var me = sender as BindingSource;
			me.Clear();
		}

		private void btnRender_Click(object sender, EventArgs e)
		{
			ShowProgressBar();
			_renderSet.Render(myVegas);
			HideProgressBar();
			if (_errorLog.Count > 0)
			{
				var rsltDlg = new Lib.Vegas.Dialogs.FormResult();
				rsltDlg.AddText(_errorLog.GetMerged());
				rsltDlg.ShowDialog();
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnOptions_Click(object sender, EventArgs e)
		{
			EntryPoint.RunSetup(myVegas);
		}

		private void cbSelection_CheckedChanged(object sender, EventArgs e)
		{
			var me = sender as CheckBox;
			ScriptOptions.SetValue(RegionRenderStrings.ScriptName, RegionRenderStrings.SelectionOnlyOptionName, me.Checked);
			_renderSet.UseSelection = cbSelection.Checked;
			PopulateRenderItems();
		}

		private void cbSelection_Click(object sender, EventArgs e)
		{
		}
	}
}