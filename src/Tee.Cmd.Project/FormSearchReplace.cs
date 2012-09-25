using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Sony.Vegas;
using Region = Sony.Vegas.Region;

namespace Tee.Cmd.Project
{
	/*/
				public partial class FormSearchReplace : UserControl
				{
	/*/

	public sealed partial class FormSearchReplace : DockableControl
	{
		public Vegas MyVegas;

		public FormSearchReplace()
			: base(ProjectSearchStrings.WindowTitle)
		{
			InitializeComponent();
			PersistDockWindowState = true;
			tbFind.KeyPress += tbFind_KeyPress;
			tbReplace.KeyPress += tbReplace_KeyPress;

			SearchResults = new List<SearchReplaceResult>();
			bsResult.DataSource = SearchResults;
			dgResults.DataSource = bsResult;
		}

		public override DockWindowStyle DefaultDockWindowStyle
		{
			get { return DockWindowStyle.Docked; }
		}

		public override Size DefaultFloatingSize
		{
			get { return new Size(640, 300); }
		}

		protected override void OnLoaded(EventArgs args)
		{
			base.OnLoaded(args);
			tbFind.Focus();
		}

		protected override void InitLayout()
		{
			base.InitLayout();
			tbFind.Focus();
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			if (Visible)
				tbFind.Focus();

			base.OnVisibleChanged(e);
		}

		//*/

		//-------------------------------------------------------------------------------------------------------------//

		private void PerformSearch()
		{
			SearchResults.Clear();

			if (string.IsNullOrEmpty(tbFind.Text))
				return;

			string searchString = tbFind.Text.ToLower();

			//tracks
			if (cbTracknames.Checked || cbEvents.Checked)
			{
				foreach (Track trk in MyVegas.Project.Tracks)
				{
					if (trk == null)
						continue;
					//trackevents
					if (cbEvents.Checked)
					{
						foreach (TrackEvent ev in trk.Events)
						{
							string name = ev.GetName();
							if (name != String.Empty && name.ToLowerInvariant().Contains(searchString))
								SearchResults.Add(new SearchReplaceResult(searchString, ev, typeof(TrackEvent).ToString()));
						}
					}

					if (cbTracknames.Checked && trk.Name != null && trk.Name.ToLower().Contains(searchString))
					{
						SearchResults.Add(new SearchReplaceResult(searchString, trk, typeof(Track).ToString()));
					}
				}
			}

			//regions
			if (cbRegions.Checked)
			{
				foreach (Region rgn in MyVegas.Project.Regions)
				{
					if (rgn.Label != null && rgn.Label.ToLower().Contains(searchString))
						SearchResults.Add(new SearchReplaceResult(searchString, rgn, rgn.GetType().ToString()));
				}
			}

			//markers
			if (cbMarkers.Checked)
			{
				foreach (Marker mrk in MyVegas.Project.Markers)
				{
					if (mrk.Label != null && mrk.Label.ToLower().Contains(searchString))
						SearchResults.Add(new SearchReplaceResult(searchString, mrk, mrk.GetType().ToString()));
				}
			}

			//commandmarkers
			if (cbCmdMarkers.Checked)
			{
				foreach (CommandMarker mrk in MyVegas.Project.CommandMarkers)
				{
					if (mrk.CommandParameter.ToLower().Contains(searchString))
						SearchResults.Add(new SearchReplaceResult(searchString, mrk, mrk.GetType().ToString()));
				}
			}
			RefreshGrid();
		}

		private void PerformReplace()
		{
			if (SearchResults.Count == 0)
				PerformSearch();

			if (SearchResults.Count == 0)
				return;

			string ReplaceString = tbReplace.Text;

			using (var undo = new UndoBlock("Search / replace"))
			{
				foreach (SearchReplaceResult rslt in SearchResults)
				{
					if (!rslt.Include)
						continue;
					rslt.Replace(ReplaceString);
				}
			}
			MyVegas.UpdateUI();

			// a bit dirty, but we want to update the search results.
			PerformSearch();
		}

		private void RefreshGrid()
		{
			bsResult.ResetBindings(false);
		}

		//
		// events
		//

		private void btnFind_Click(object sender, EventArgs e)
		{
			PerformSearch();
		}

		private void btnReplace_Click(object sender, EventArgs e)
		{
			PerformReplace();
		}

		private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Tab)
			{
				tbReplace.Focus();
				return;
			}

			if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return)
			{
				PerformSearch();
			}
		}

		private void tbReplace_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Tab)
			{
				tbFind.Focus();
				return;
			}

			if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return)
			{
				PerformReplace();
			}
		}
	}
}