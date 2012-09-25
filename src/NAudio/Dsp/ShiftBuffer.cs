namespace NAudio.Dsp
{
	/// <summary>
	/// A shift buffer
	/// </summary>
	public class ShiftBuffer
	{
		private readonly double[][] list;
		private readonly int size;
		private int insertPos;

		/// <summary>
		/// creates a new shift buffer
		/// </summary>
		public ShiftBuffer(int size)
		{
			list = new double[size][];
			insertPos = 0;
			this.size = size;
		}

		/// <summary>
		/// Return samples from the buffer
		/// </summary>
		public double[] this[int index]
		{
			get { return list[(size + insertPos - index)%size]; }
		}

		/// <summary>
		/// Add samples to the buffer
		/// </summary>
		public void Add(double[] buffer)
		{
			list[insertPos] = buffer;
			insertPos = (insertPos + 1)%size;
		}
	}
}