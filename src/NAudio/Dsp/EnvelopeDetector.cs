using System;
using System.Diagnostics;

namespace NAudio.Dsp
{
	internal class EnvelopeDetector
	{
		private double coeff;
		private double ms;
		private double sampleRate;

		public EnvelopeDetector() : this(1.0, 44100.0)
		{
		}

		public EnvelopeDetector(double ms, double sampleRate)
		{
			Debug.Assert(sampleRate > 0.0);
			Debug.Assert(ms > 0.0);
			this.sampleRate = sampleRate;
			this.ms = ms;
			setCoef();
		}

		public double TimeConstant
		{
			get { return ms; }
			set
			{
				Debug.Assert(value > 0.0);
				ms = value;
				setCoef();
			}
		}


		public double SampleRate
		{
			get { return sampleRate; }
			set
			{
				Debug.Assert(value > 0.0);
				sampleRate = value;
				setCoef();
			}
		}

		public void run(double inValue, ref double state)
		{
			state = inValue + coeff*(state - inValue);
		}

		private void setCoef()
		{
			coeff = Math.Exp(-1.0/(0.001*ms*sampleRate));
		}
	}
}