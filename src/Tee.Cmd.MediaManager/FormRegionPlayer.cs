using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ScriptPortal.Vegas;

namespace Tee.Cmd.MediaManager
{
	/*/
		public partial class FormRegionPlayer : UserControl
		{
			Sony.Vegas.Vegas myVegas = null;
			public FormRegionPlayer()
			{
				InitializeComponent();
			}
	/*/

	public sealed partial class FormRegionPlayer : DockableControl
	{
		private readonly List<RegionView> regions = new List<RegionView>();

		public FormRegionPlayer()
			: base(Strings.RegionPlayerWindowTitle)
		{
			InitializeComponent();
			DisplayName = Strings.RegionPlayerWindowTitle;
			Init();
		}

		public override DockWindowStyle DefaultDockWindowStyle
		{
			get { return DockWindowStyle.Docked; }
		}

		public override bool AutoSize
		{
			get { return true; }
		}

		protected override void OnLoaded(EventArgs args)
		{
			RefreshGrid();
			AttachToVegasEvents();
			base.OnLoaded(args);
		}

		protected override void OnClosed(EventArgs args)
		{
			DetachFromVegasEvents();
			base.OnClosed(args);
		}

		//*/

		public void Init()
		{
			RefreshRegions();
			regionViewBindingSource = new BindingSource();
			var sortableList = new SortableBindingList<RegionView>(regions);
			regionViewBindingSource.DataSource = sortableList;

			dataGridView1.DataSource = regionViewBindingSource;
			RefreshGrid();
		}

		private void RefreshRegions()
		{
			regions.Clear();
			foreach (Region rgn in myVegas.Project.Regions)
			{
				regions.Add(new RegionView(rgn));
			}
		}

		private void RefreshGrid()
		{
			regionViewBindingSource.ResetBindings(false);
			//dataGridView1.ReadOnly = true;
		}

		private void PlayRegion(Timecode pos, Timecode len)
		{
			myVegas.Transport.CursorPosition = pos;
			myVegas.Transport.SelectionStart = pos;
			myVegas.Transport.SelectionLength = len;
			myVegas.Transport.ViewCursor(true);
			myVegas.Transport.Play();
		}

		public void AttachToVegasEvents()
		{
			VisibleChanged -= EventVisibleChanged;
			VisibleChanged += EventVisibleChanged;

			myVegas.ProjectOpening -= EventProjectOpening;
			myVegas.ProjectOpening += EventProjectOpening;

			myVegas.ProjectClosed -= EventProjectClosed;
			myVegas.ProjectClosed += EventProjectClosed;

			myVegas.ProjectOpened -= EventProjectOpened;
			myVegas.ProjectOpened += EventProjectOpened;

			myVegas.MarkersChanged -= myVegas_MarkersChanged;
			myVegas.MarkersChanged += myVegas_MarkersChanged;
		}

		public void DetachFromVegasEvents()
		{
			VisibleChanged -= EventVisibleChanged;
			myVegas.ProjectOpening -= EventProjectOpening;
			//myVegas.ProjectOpened -= EventProjectClosed;
			//myVegas.ProjectClosed -= EventProjectOpened;
			//myVegas.ProjectOpened -= EventProjectOpened;
			myVegas.ProjectClosed -= EventProjectClosed;

			myVegas.MarkersChanged -= myVegas_MarkersChanged;
		}

		private void EventVisibleChanged(object sender, EventArgs e)
		{
			var mm = (FormRegionPlayer)sender;

			if (!mm.Visible)
				DetachFromVegasEvents();
		}

		private void EventProjectOpening(object sender, ProjectOpeningEventArgs args)
		{
			DetachFromVegasEvents();
		}

		private void EventProjectOpened(object sender, EventArgs e)
		{
			RefreshGrid();
			AttachToVegasEvents();
		}

		private void EventProjectClosed(object sender, EventArgs e)
		{
			RefreshGrid();
			DetachFromVegasEvents();
		}

		private void myVegas_MarkersChanged(object sender, EventArgs e)
		{
			RefreshRegions();
			RefreshGrid();
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;
			DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
			var view = row.DataBoundItem as RegionView;
			if (view != null) PlayRegion(view.Position, view.Length);
		}

		private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			if (e.Row.DataBoundItem == null)
				return;
			var view = e.Row.DataBoundItem as RegionView;
			if (view != null)
			{
				Region reg = view.Region;
				// ReSharper disable UnusedVariable
				using (var undo = new UndoBlock("Remove region")) // ReSharper restore UnusedVariable
				{
					myVegas.Project.Regions.Remove(reg);
				}
			}
		}

		private void regionViewBindingSource_CurrentChanged(object sender, EventArgs e)
		{
			var me = sender as BindingSource;
			if (me == null || (me.Current == null || !dataGridView1.Focused))
				return;

			var view = me.Current as RegionView;
			if (view == null)
				return;

			PlayRegion(view.Position, view.Length);
		}
	}
}