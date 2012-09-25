using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NAudio.Gui
{
	/// <summary>
	/// VolumeSlider control
	/// </summary>
	public class VolumeSlider : UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
#pragma warning disable 649
		private readonly Container components;
#pragma warning restore 649

		private float volume;

		/// <summary>
		/// Creates a new VolumeSlider control
		/// </summary>
		public VolumeSlider()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
		}

		/// <summary>
		/// The volume for this control
		/// </summary>
		public float Volume
		{
			get { return volume; }
			set
			{
				if (value < 0.0f)
					value = 0.0f;
				if (value > 1.0f)
					value = 1.0f;
				if (volume != value)
				{
					volume = value;
					if (VolumeChanged != null)
						VolumeChanged(this, EventArgs.Empty);
					Invalidate();
				}
			}
		}

		/// <summary>
		/// Volume changed event
		/// </summary>
		public event EventHandler VolumeChanged;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// <see cref="Control.OnPaint"/>
		/// </summary>
		protected override void OnPaint(PaintEventArgs pe)
		{
			var format = new StringFormat();
			format.LineAlignment = StringAlignment.Center;
			format.Alignment = StringAlignment.Center;

			pe.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
			pe.Graphics.FillRectangle(Brushes.LightGreen, 1, 1, (int) ((Width - 2)*volume), Height - 2);
			double db = 20*Math.Log10(Volume);
			string dbValue = String.Format("{0:F2} dB", db);
			/*if(Double.IsNegativeInfinity(db))
			{
				dbValue = "-\x221e db"; // -8 dB
			}*/

			pe.Graphics.DrawString(dbValue, Font,
			                       Brushes.Black, ClientRectangle, format);
			// Calling the base class OnPaint
			//base.OnPaint(pe);
		}

		/// <summary>
		/// <see cref="Control.OnMouseMove"/>
		/// </summary>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				SetVolumeFromMouse(e.X);
			}
			base.OnMouseMove(e);
		}

		/// <summary>
		/// <see cref="Control.OnMouseDown"/>
		/// </summary>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			SetVolumeFromMouse(e.X);
			base.OnMouseDown(e);
		}

		private void SetVolumeFromMouse(int x)
		{
			Volume = (float) x/Width;
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// VolumeSlider
			// 
			this.Name = "VolumeSlider";
			this.Size = new System.Drawing.Size(96, 16);
		}

		#endregion
	}
}