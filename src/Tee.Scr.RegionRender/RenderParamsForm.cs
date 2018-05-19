using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ScriptPortal.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Render;
using Tee.Lib.Vegas.ScriptOption;

namespace Tee.Scr.RegionRender
{
	public partial class RenderParamsForm : Form
	{
		private readonly Vegas myVegas;
		private readonly RenderParamSet _renderParams;
		private string FavoritesConfFile;

		public RenderParamSet UserRenderParams
		{
			get
			{
				var set = new RenderParamSet();

				set.AddIfNotDefault(RenderTags.AudioFmt, cmbAudioRenderer.Text);
				set.AddIfNotDefault(RenderTags.AudioTpl, cmbAudioTemplate.Text);
				set.AddIfNotDefault(RenderTags.VideoTpl, cmbVideoRenderer.Text);
				set.AddIfNotDefault(RenderTags.VideoTpl, cmbVideoTemplate.Text);

				set.AddIfNotDefault(RenderTags.DoReadonly, cbDoReadonly.Checked);
				set.AddIfNotDefault(RenderTags.DoStems, cbDoStems.Checked);
				set.AddIfNotDefault(RenderTags.DoVideo, cbDoVideo.Checked);
				set.AddIfNotDefault(RenderTags.NoEmpty, cbNoEmpty.Checked);

				set.AddIfNotDefault(RenderTags.NamingMask, tbNamingMask.Text);
				set.AddIfNotDefault(RenderTags.TargetDir, tbTargetDir.Text);
				set.AddIfNotDefault(RenderTags.CounterDigits, long.Parse(tbCounterDigits.Text));
				set.AddIfNotDefault(RenderTags.DoPadding, cbDoPadding.Checked);
				if (set.GetParam<bool>(RenderTags.DoPadding)) // only write padding if we're actually padding
				{
					int paddingSeconds;
					if (int.TryParse(tbPaddingAmount.Text, out paddingSeconds))
						set.AddIfNotDefault(RenderTags.PaddingAmt, paddingSeconds);
				}
				return set;
			}
		}

		private readonly List<Renderer> _audioRenderers = new List<Renderer>();
		private readonly List<Renderer> _videoRenderers = new List<Renderer>();

		public RenderParamsForm(Vegas Vegas, RenderParamSet RenderParams)
		{
			myVegas = Vegas;
			_renderParams = RenderParams;

			InitializeComponent();
			InitCustomTags();
			InitRenderSelectors();
		}

		private void InitCustomTags()
		{
			FavoritesConfFile = ScriptOptions.GetValue(RegionRenderStrings.ScriptName, RegionRenderStrings.CustomTagConfigFile, RegionRenderStrings.CustomTagConfigFileDefault);
			var dirs = new RenderRootDirSet(FavoritesConfFile);
		}

		private void RenderParamsForm_Load(object sender, EventArgs e)
		{
			SetInitialValues();
		}

		private void InitRenderSelectors()
		{
			foreach (var renderer in myVegas.Renderers)
			{
				//if (renderer.Templates.Any(item => item.VideoStreamCount != 0))
				foreach (var tpl in renderer.Templates)
				{
					if (tpl.VideoStreamCount != 0)
					{
						_videoRenderers.Add(renderer);
					}
					else
					{
						_audioRenderers.Add(renderer);
					}
				}
			}

			cmbAudioRenderer.DataSource = _audioRenderers.Select(item => item.Name).ToList();
			cmbVideoRenderer.DataSource = _videoRenderers.Select(item => item.Name).ToList();
		}

		private void SetInitialValues()
		{
			cmbAudioRenderer.Text = _renderParams.GetParam<string>(RenderTags.AudioFmt);
			cmbAudioTemplate.Text = _renderParams.GetParam<string>(RenderTags.AudioTpl);
			cmbVideoRenderer.Text = _renderParams.GetParam<string>(RenderTags.VideoFmt);
			cmbVideoTemplate.Text = _renderParams.GetParam<string>(RenderTags.VideoTpl);

			tbTargetDir.Text = _renderParams.GetParam<string>(RenderTags.TargetDir);

			cbDoReadonly.Checked = _renderParams.GetParam<bool>(RenderTags.DoReadonly);
			cbDoReadonly_CheckedChanged(cbDoReadonly, null);

			cbDoStems.Checked = _renderParams.GetParam<bool>(RenderTags.DoStems);
			cbDoStems_CheckedChanged(cbDoStems, null);

			cbDoVideo.Checked = _renderParams.GetParam<bool>(RenderTags.DoVideo);
			cbDoVideo_CheckedChanged(cbDoVideo, null);

			cbNoEmpty.Checked = _renderParams.GetParam<bool>(RenderTags.NoEmpty);
			cbNoEmpty_CheckedChanged(cbNoEmpty, null);

			cbDoPadding.Checked = _renderParams.GetParam<bool>(RenderTags.DoPadding);
			cbDoPadding_CheckedChanged(cbDoPadding, null);

			tbNamingMask.Text = _renderParams.GetParam<string>(RenderTags.NamingMask);
			tbCounterDigits.Text = _renderParams.GetParam<long>(RenderTags.CounterDigits).ToString();

			int padding = _renderParams.GetParam<int>(RenderTags.PaddingAmt);
			tbPaddingAmount.Text = padding.ToString();

			cmbVideoRenderer.Enabled = cbDoVideo.Checked;
			cmbVideoTemplate.Enabled = cbDoVideo.Checked;
			tbPaddingAmount.Enabled = cbDoPadding.Checked;
		}

		private void HighlightDefaultValues()
		{
			Color defaultColor = Color.Blue;
			Color userColor = SystemColors.ControlText;

			cmbAudioRenderer.ForeColor = (cmbAudioRenderer.Text == _renderParams.GetParam<string>(RenderTags.AudioFmt) ? defaultColor : userColor);
			cmbAudioTemplate.ForeColor = (cmbAudioTemplate.Text == _renderParams.GetParam<string>(RenderTags.AudioTpl) ? defaultColor : userColor);
			cmbVideoRenderer.ForeColor = (cmbVideoRenderer.Text == _renderParams.GetParam<string>(RenderTags.VideoFmt) ? defaultColor : userColor);
			cmbVideoTemplate.ForeColor = (cmbVideoTemplate.Text == _renderParams.GetParam<string>(RenderTags.VideoTpl) ? defaultColor : userColor);

			cbDoPadding.ForeColor = (cbDoPadding.Checked == _renderParams.GetParam<bool>(RenderTags.DoPadding) ? defaultColor : userColor);
			cbDoReadonly.ForeColor = (cbDoReadonly.Checked == _renderParams.GetParam<bool>(RenderTags.DoReadonly) ? defaultColor : userColor);
			cbDoStems.ForeColor = (cbDoStems.Checked == _renderParams.GetParam<bool>(RenderTags.DoStems) ? defaultColor : userColor);
			cbDoVideo.ForeColor = (cbDoVideo.Checked == _renderParams.GetParam<bool>(RenderTags.DoVideo) ? defaultColor : userColor);
			cbNoEmpty.ForeColor = (cbNoEmpty.Checked == _renderParams.GetParam<bool>(RenderTags.NoEmpty) ? defaultColor : userColor);

			tbNamingMask.ForeColor = (tbNamingMask.Text == _renderParams.GetParam<string>(RenderTags.NamingMask) ? defaultColor : userColor);
			tbTargetDir.ForeColor = (tbTargetDir.Text == _renderParams.GetParam<string>(RenderTags.TargetDir) ? defaultColor : userColor);
			tbCounterDigits.ForeColor = (tbCounterDigits.Text == _renderParams.GetParam<long>(RenderTags.CounterDigits).ToString() ? defaultColor : userColor);

			int paramPaddingAmt = _renderParams.GetParam<int>(RenderTags.PaddingAmt);
			int userPaddingAmt;
			int.TryParse(tbPaddingAmount.Text, out userPaddingAmt);

			tbPaddingAmount.ForeColor = (paramPaddingAmt == userPaddingAmt) ? defaultColor : userColor;
		}

		private void UpdateNamingPreview()
		{
			const string region = "MyRegion";
			const string track = "MyTrack";
			const string bus = "MyBus";
			const int ctr = 1;

			string output = tbNamingMask.Text.ToLowerInvariant();
			output = output.Replace(RenderTags.Format(RenderTags.Region), region);
			output = output.Replace(RenderTags.Format(RenderTags.Track), track);
			output = output.Replace(RenderTags.Format(RenderTags.Bus), bus);
			string counterFormat = "{0:d" + tbCounterDigits.Text + "}";
			output = output.Replace(RenderTags.Format(RenderTags.Counter), String.Format(counterFormat, ctr));
			lbNamingMaskExample.Text = output;
		}

		private void tbPaddingAmount_TextChanged(object sender, EventArgs e)
		{
			Timecode tc = Timecode.FromString(tbPaddingAmount.Text, RulerFormat.Seconds);
			if (tc == null)
				return;
			lbPaddingPreview.Text = tc.ToString();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cmbAudioRenderer_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbAudioRenderer.SelectedValue == null)
				return;

			var r = myVegas.Renderers.FindByName(cmbAudioRenderer.Text);
			r.Templates.Refresh();
			cmbAudioTemplate.DataSource = r.Templates.Select(item => item.Name).ToList();

			HighlightDefaultValues();
		}

		private void cmbVideoRenderer_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbVideoRenderer.SelectedValue == null)
				return;

			var r = myVegas.Renderers.FindByName(cmbVideoRenderer.Text);
			r.Templates.Refresh();
			cmbVideoTemplate.DataSource = r.Templates.Select(item => item.Name).ToList();

			HighlightDefaultValues();
		}

		private void cmbAudioTemplate_SelectedIndexChanged(object sender, EventArgs e)
		{
			HighlightDefaultValues();
		}

		private void cmbVideoTemplate_SelectedIndexChanged(object sender, EventArgs e)
		{
			HighlightDefaultValues();
		}

		private void cbDoPadding_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox me = sender as CheckBox;
			tbPaddingAmount.Enabled = me.Checked;
			HighlightDefaultValues();
		}

		private void cbDoVideo_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox me = sender as CheckBox;
			cmbVideoRenderer.Enabled = me.Checked;
			cmbVideoTemplate.Enabled = me.Checked;
			HighlightDefaultValues();
		}

		private void cbDoStems_CheckedChanged(object sender, EventArgs e)
		{
			HighlightDefaultValues();
		}

		private void cbNoEmpty_CheckedChanged(object sender, EventArgs e)
		{
			HighlightDefaultValues();
		}

		private void cbDoReadonly_CheckedChanged(object sender, EventArgs e)
		{
			HighlightDefaultValues();
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			SetInitialValues();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void tbCounterDigits_TextChanged(object sender, EventArgs e)
		{
			UpdateNamingPreview();
			HighlightDefaultValues();
		}

		private void tbNamingMask_TextChanged(object sender, EventArgs e)
		{
			UpdateNamingPreview();
			HighlightDefaultValues();
		}

		private void tbTargetDir_TextChanged(object sender, EventArgs e)
		{
			UpdateNamingPreview();
			HighlightDefaultValues();
		}

		private void btnResetProject_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes != MessageBox.Show("This will clear all render markers from your project. Really reset?", "Really reset project?", MessageBoxButtons.YesNo))
				return;

			var killSheet = new List<CommandMarker>();
			using (UndoBlock undo = new UndoBlock("Clear project render markers"))
			{
				killSheet.AddRange(myVegas.Project.CommandMarkers.Where(cmk => cmk.CommandType.ToString().ContainsRenderTag(false)));
				foreach (var cmk in killSheet)
				{
					myVegas.Project.CommandMarkers.Remove(cmk);
				}
			}

			_renderParams.ResetToDefault();
			SetInitialValues();
			UpdateNamingPreview();
			HighlightDefaultValues();
		}

		private void btnTargetManagerBrowse_Click(object sender, EventArgs e)
		{
			var frm = new RenderRootDirManagerForm(FavoritesConfFile);
			frm.ShowDialog();
			InitCustomTags();
		}

		private void btnTargetDirBrowse_Click(object sender, EventArgs e)
		{
			string lastTargetDir = ScriptOptions.GetValue(RegionRenderStrings.ScriptName, RegionRenderStrings.LastTargetDir, RegionRenderStrings.LastTargetDirDefault);
			string initialDir = Path.GetDirectoryName(lastTargetDir);
			if (string.IsNullOrEmpty(initialDir) || !Directory.Exists(initialDir))
				initialDir = @"C:\";

			var dlg = new FolderBrowserDialog();
			dlg.RootFolder = Environment.SpecialFolder.MyComputer;
			dlg.SelectedPath = initialDir;
			dlg.Description = "Choose target directory";
			if (dlg.ShowDialog() != DialogResult.OK)
				return;
			ScriptOptions.SetValue(RegionRenderStrings.ScriptName, RegionRenderStrings.LastTargetDir, dlg.SelectedPath);
			tbTargetDir.Text = dlg.SelectedPath;
		}
	}
}