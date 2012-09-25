namespace NAudio.Wave.WaveOutputs
{
	internal class WaveOutAction
	{
		private readonly object data;
		private readonly WaveOutFunction function;

		public WaveOutAction(WaveOutFunction function, object data)
		{
			this.function = function;
			this.data = data;
		}

		public WaveOutFunction Function
		{
			get { return function; }
		}

		public object Data
		{
			get { return data; }
		}
	}
}