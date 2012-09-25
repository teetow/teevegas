namespace NAudio.Dsp
{
	internal class AttRelEnvelope
	{
		// DC offset to prevent denormal
		protected const double DC_OFFSET = 1.0E-25;


		private readonly EnvelopeDetector attack;
		private readonly EnvelopeDetector release;

		public AttRelEnvelope(double att_ms, double rel_ms, double sampleRate)
		{
			attack = new EnvelopeDetector(att_ms, sampleRate);
			release = new EnvelopeDetector(rel_ms, sampleRate);
		}

		public double Attack
		{
			get { return attack.TimeConstant; }
			set { attack.TimeConstant = value; }
		}

		public double Release
		{
			get { return release.TimeConstant; }
			set { release.TimeConstant = value; }
		}

		public double SampleRate
		{
			get { return attack.SampleRate; }
			set { attack.SampleRate = release.SampleRate = value; }
		}

		public void Run(double inValue, ref double state)
		{
			// assumes that:
			// positive delta = attack
			// negative delta = release
			// good for linear & log values
			if (inValue > state)
				attack.run(inValue, ref state); // attack
			else
				release.run(inValue, ref state); // release
		}
	}
}