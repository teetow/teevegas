using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NAudio.Gui
{
	/// <summary>
	/// Windows Forms control for painting audio waveforms
	/// </summary>
	public partial class WaveformPainter : Control
	{
		private readonly List<float> samples = new List<float>(1000);
		private Pen foregroundPen;
		private int insertPos;
		private int maxSamples;

		/// <summary>
		/// Constructs a new instance of the WaveFormPainter class
		/// </summary>
		public WaveformPainter()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
			         ControlStyles.OptimizedDoubleBuffer, true);
			InitializeComponent();
			OnForeColorChanged(EventArgs.Empty);
			OnResize(EventArgs.Empty);
		}

		/// <summary>
		/// On Resize
		/// </summary>
		protected override void OnResize(EventArgs e)
		{
			maxSamples = Width;
			base.OnResize(e);
		}

		/// <summary>
		/// On ForeColor Changed
		/// </summary>
		/// <param name="e"></param>
		protected override void OnForeColorChanged(EventArgs e)
		{
			foregroundPen = new Pen(ForeColor);
			base.OnForeColorChanged(e);
		}

		/// <summary>
		/// Add Max Value
		/// </summary>
		/// <param name="maxSample"></param>
		public void AddMax(float maxSample)
		{
			if (samples.Count <= maxSamples)
			{
				samples.Add(maxSample);
			}
			else
			{
				samples[insertPos] = maxSample;
			}
			insertPos++;
			insertPos %= maxSamples;

			Invalidate();
		}

		/// <summary>
		/// On Paint
		/// </summary>
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			for (int x = 0; x < Width; x++)
			{
				float lineHeight = Height*GetSample(x - Width + insertPos);
				float y1 = (Height - lineHeight)/2;
				pe.Graphics.DrawLine(foregroundPen, x, y1, x, y1 + lineHeight);
			}
		}

		private float GetSample(int index)
		{
			if (index < 0)
				index += maxSamples;
			if (index >= 0 & index < samples.Count)
				return samples[index];
			return 0;
		}
	}
}