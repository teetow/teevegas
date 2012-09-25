using System;

namespace NAudio.Dsp
{
	internal class BiQuadFilter
	{
		// coefficients
		private double a0;
		private double a1;
		private double a2;
		private double b0;
		private double b1;
		private double b2;

		private BiQuadFilter()
		{
		}

		public void Transform(float[] inBuffer, float[] outBuffer)
		{
			float[] x = inBuffer;
			float[] y = outBuffer;

			for (int n = 0; n < inBuffer.Length; n++)
			{
				y[n] = (float) (
				               	(b0/a0)*x[n] + (b1/a0)*x[n - 1] + (b2/a0)*x[n - 2]
				               	- (a1/a0)*y[n - 1] - (a2/a0)*y[n - 2]);
			}
		}

		/// <summary>
		/// H(s) = 1 / (s^2 + s/Q + 1)
		/// </summary>
		public static BiQuadFilter LowPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			double w0 = 2*Math.PI*cutoffFrequency/sampleRate;
			double cosw0 = Math.Cos(w0);
			double alpha = Math.Sin(w0)/(2*q);

			var filter = new BiQuadFilter();

			filter.b0 = (1 - cosw0)/2;
			filter.b1 = 1 - cosw0;
			filter.b2 = (1 - cosw0)/2;
			filter.a0 = 1 + alpha;
			filter.a1 = -2*cosw0;
			filter.a2 = 1 - alpha;
			return filter;
		}

		/// <summary>
		/// H(s) = s^2 / (s^2 + s/Q + 1)
		/// </summary>
		public static BiQuadFilter HighPassFilter(float sampleRate, float cutoffFrequency, float q)
		{
			double w0 = 2*Math.PI*cutoffFrequency/sampleRate;
			double cosw0 = Math.Cos(w0);
			double alpha = Math.Sin(w0)/(2*q);

			var filter = new BiQuadFilter();

			filter.b0 = (1 + Math.Cos(w0))/2;
			filter.b1 = -(1 + Math.Cos(w0));
			filter.b2 = (1 + Math.Cos(w0))/2;
			filter.a0 = 1 + alpha;
			filter.a1 = -2*Math.Cos(w0);
			filter.a2 = 1 - alpha;
			return filter;
		}

		/// <summary>
		/// H(s) = s / (s^2 + s/Q + 1)  (constant skirt gain, peak gain = Q)
		/// </summary>
		public static BiQuadFilter BandPassFilterConstantSkirtGain(float sampleRate, float centreFrequency, float q)
		{
			double w0 = 2*Math.PI*centreFrequency/sampleRate;
			double cosw0 = Math.Cos(w0);
			double sinw0 = Math.Sin(w0);
			double alpha = sinw0/(2*q);

			var filter = new BiQuadFilter();
			filter.b0 = sinw0/2; // =   Q*alpha
			filter.b1 = 0;
			filter.b2 = -sinw0/2; // =  -Q*alpha
			filter.a0 = 1 + alpha;
			filter.a1 = -2*cosw0;
			filter.a2 = 1 - alpha;
			return filter;
		}

		/// <summary>
		/// H(s) = (s/Q) / (s^2 + s/Q + 1)      (constant 0 dB peak gain)
		/// </summary>
		public static BiQuadFilter BandPassFilterConstantPeakGain(float sampleRate, float centreFrequency, float q)
		{
			double w0 = 2*Math.PI*centreFrequency/sampleRate;
			double cosw0 = Math.Cos(w0);
			double sinw0 = Math.Sin(w0);
			double alpha = sinw0/(2*q);

			var filter = new BiQuadFilter();
			filter.b0 = alpha;
			filter.b1 = 0;
			filter.b2 = -alpha;
			filter.a0 = 1 + alpha;
			filter.a1 = -2*cosw0;
			filter.a2 = 1 - alpha;
			return filter;
		}

		/// <summary>
		/// H(s) = (s^2 + 1) / (s^2 + s/Q + 1)
		/// </summary>
		public static BiQuadFilter NotchFilter(float sampleRate, float centreFrequency, float q)
		{
			double w0 = 2*Math.PI*centreFrequency/sampleRate;
			double cosw0 = Math.Cos(w0);
			double sinw0 = Math.Sin(w0);
			double alpha = sinw0/(2*q);

			var filter = new BiQuadFilter();
			filter.b0 = 1;
			filter.b1 = -2*cosw0;
			filter.b2 = 1;
			filter.a0 = 1 + alpha;
			filter.a1 = -2*cosw0;
			filter.a2 = 1 - alpha;
			return filter;
		}

		/// <summary>
		/// H(s) = (s^2 - s/Q + 1) / (s^2 + s/Q + 1)
		/// </summary>
		public static BiQuadFilter AllPassFilter(float sampleRate, float centreFrequency, float q)
		{
			double w0 = 2*Math.PI*centreFrequency/sampleRate;
			double cosw0 = Math.Cos(w0);
			double sinw0 = Math.Sin(w0);
			double alpha = sinw0/(2*q);

			var filter = new BiQuadFilter();
			filter.b0 = 1 - alpha;
			filter.b1 = -2*cosw0;
			filter.b2 = 1 + alpha;
			filter.a0 = 1 + alpha;
			filter.a1 = -2*cosw0;
			filter.a2 = 1 - alpha;
			return filter;
		}

		/// <summary>
		/// H(s) = (s^2 + s*(A/Q) + 1) / (s^2 + s/(A*Q) + 1)
		/// </summary>
		public static BiQuadFilter PeakingEQ(float sampleRate, float centreFrequency, float q, float dbGain)
		{
			double w0 = 2*Math.PI*centreFrequency/sampleRate;
			double cosw0 = Math.Cos(w0);
			double sinw0 = Math.Sin(w0);
			double alpha = sinw0/(2*q);
			double A = Math.Pow(10, dbGain/40); // TODO: should we square root this value?

			var filter = new BiQuadFilter();
			filter.b0 = 1 + alpha*A;
			filter.b1 = -2*cosw0;
			filter.b2 = 1 - alpha*A;
			filter.a0 = 1 + alpha/A;
			filter.a1 = -2*cosw0;
			filter.a2 = 1 - alpha/A;
			return filter;
		}

		/// <summary>
		/// H(s) = A * (s^2 + (sqrt(A)/Q)*s + A)/(A*s^2 + (sqrt(A)/Q)*s + 1)
		/// </summary>
		/// <param name="sampleRate"></param>
		/// <param name="cutoffFrequency"></param>
		/// <param name="shelfSlope">a "shelf slope" parameter (for shelving EQ only).  
		/// When S = 1, the shelf slope is as steep as it can be and remain monotonically
		/// increasing or decreasing gain with frequency.  The shelf slope, in dB/octave, 
		/// remains proportional to S for all other values for a fixed f0/Fs and dBgain.</param>
		/// <param name="dbGain">Gain in decibels</param>
		public static BiQuadFilter LowShelf(float sampleRate, float cutoffFrequency, float shelfSlope, float dbGain)
		{
			double w0 = 2*Math.PI*cutoffFrequency/sampleRate;
			double cosw0 = Math.Cos(w0);
			double sinw0 = Math.Sin(w0);
			double A = Math.Pow(10, dbGain/40); // TODO: should we square root this value?
			double alpha = sinw0/2*Math.Sqrt((A + 1/A)*(1/shelfSlope - 1) + 2);
			double temp = 2*Math.Sqrt(A)*alpha;
			var filter = new BiQuadFilter();
			filter.b0 = A*((A + 1) - (A - 1)*cosw0 + temp);
			filter.b1 = 2*A*((A - 1) - (A + 1)*cosw0);
			filter.b2 = A*((A + 1) - (A - 1)*cosw0 - temp);
			filter.a0 = (A + 1) + (A - 1)*cosw0 + temp;
			filter.a1 = -2*((A - 1) + (A + 1)*cosw0);
			filter.a2 = (A + 1) + (A - 1)*cosw0 - temp;
			return filter;
		}

		/// <summary>
		/// H(s) = A * (A*s^2 + (sqrt(A)/Q)*s + 1)/(s^2 + (sqrt(A)/Q)*s + A)
		/// </summary>
		/// <param name="sampleRate"></param>
		/// <param name="cutoffFrequency"></param>
		/// <param name="shelfSlope"></param>
		/// <param name="dbGain"></param>
		/// <returns></returns>
		public static BiQuadFilter HighShelf(float sampleRate, float cutoffFrequency, float shelfSlope, float dbGain)
		{
			double w0 = 2*Math.PI*cutoffFrequency/sampleRate;
			double cosw0 = Math.Cos(w0);
			double sinw0 = Math.Sin(w0);
			double A = Math.Pow(10, dbGain/40); // TODO: should we square root this value?
			double alpha = sinw0/2*Math.Sqrt((A + 1/A)*(1/shelfSlope - 1) + 2);
			double temp = 2*Math.Sqrt(A)*alpha;

			var filter = new BiQuadFilter();
			filter.b0 = A*((A + 1) + (A - 1)*cosw0 + temp);
			filter.b1 = -2*A*((A - 1) + (A + 1)*cosw0);
			filter.b2 = A*((A + 1) + (A - 1)*cosw0 - temp);
			filter.a0 = (A + 1) - (A - 1)*cosw0 + temp;
			filter.a1 = 2*((A - 1) - (A + 1)*cosw0);
			filter.a2 = (A + 1) - (A - 1)*cosw0 - temp;
			return filter;
		}
	}
}