﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NAudio.Gui
{
	/// <summary>
	/// Implements a rudimentary volume meter
	/// </summary>
	public partial class VolumeMeter : Control
	{
		private float amplitude;
		private Brush foregroundBrush;

		/// <summary>
		/// Basic volume meter
		/// </summary>
		public VolumeMeter()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
			         ControlStyles.OptimizedDoubleBuffer, true);
			MinDb = -60;
			MaxDb = 18;
			Amplitude = 0;
			Orientation = Orientation.Vertical;
			InitializeComponent();
			OnForeColorChanged(EventArgs.Empty);
		}

		/// <summary>
		/// Current Value
		/// </summary>
		[DefaultValue(-3.0)]
		public float Amplitude
		{
			get { return amplitude; }
			set
			{
				amplitude = value;
				Invalidate();
			}
		}

		/// <summary>
		/// Minimum decibels
		/// </summary>
		[DefaultValue(-60.0)]
		public float MinDb { get; set; }

		/// <summary>
		/// Maximum decibels
		/// </summary>
		[DefaultValue(18.0)]
		public float MaxDb { get; set; }

		/// <summary>
		/// Meter orientation
		/// </summary>
		[DefaultValue(Orientation.Vertical)]
		public Orientation Orientation { get; set; }

		/// <summary>
		/// On Fore Color Changed
		/// </summary>
		protected override void OnForeColorChanged(EventArgs e)
		{
			foregroundBrush = new SolidBrush(ForeColor);
			base.OnForeColorChanged(e);
		}

		/// <summary>
		/// Paints the volume meter
		/// </summary>
		protected override void OnPaint(PaintEventArgs pe)
		{
			//base.OnPaint(pe);


			pe.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);

			double db = 20*Math.Log10(Amplitude);
			if (db < MinDb)
				db = MinDb;
			if (db > MaxDb)
				db = MaxDb;
			double percent = (db - MinDb)/(MaxDb - MinDb);

			int width = Width - 2;
			int height = Height - 2;
			if (Orientation == Orientation.Horizontal)
			{
				width = (int) (width*percent);

				pe.Graphics.FillRectangle(foregroundBrush, 1, 1, width, height);
			}
			else
			{
				height = (int) (height*percent);
				pe.Graphics.FillRectangle(foregroundBrush, 1, Height - 1 - height, width, height);
			}

			/*
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            string dbValue = String.Format("{0:F2} dB", db);
            if(Double.IsNegativeInfinity(db))
            {
                dbValue = "-\x221e db"; // -8 dB
            }

            pe.Graphics.DrawString(dbValue, this.Font,
                Brushes.Black, this.ClientRectangle, format);*/
		}
	}
}