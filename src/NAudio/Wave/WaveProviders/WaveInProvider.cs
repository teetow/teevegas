using NAudio.Wave.MmeInterop;
using NAudio.Wave.WaveFormats;
using NAudio.Wave.WaveInputs;
using NAudio.Wave.WaveOutputs;

namespace NAudio.Wave.WaveProviders
{
	/// <summary>
	/// Buffered WaveProvider taking source data from WaveIn
	/// </summary>
	public class WaveInProvider : IWaveProvider
	{
		private readonly BufferedWaveProvider bufferedWaveProvider;
		private readonly IWaveIn waveIn;

		/// <summary>
		/// Creates a new WaveInProvider
		/// n.b. Should make sure the WaveFormat is set correctly on IWaveIn before calling
		/// </summary>
		/// <param name="waveIn">The source of wave data</param>
		public WaveInProvider(IWaveIn waveIn)
		{
			this.waveIn = waveIn;
			waveIn.DataAvailable += waveIn_DataAvailable;
			bufferedWaveProvider = new BufferedWaveProvider(WaveFormat);
		}

		#region IWaveProvider Members

		/// <summary>
		/// Reads data from the WaveInProvider
		/// </summary>
		public int Read(byte[] buffer, int offset, int count)
		{
			return bufferedWaveProvider.Read(buffer, 0, count);
		}

		/// <summary>
		/// The WaveFormat
		/// </summary>
		public WaveFormat WaveFormat
		{
			get { return waveIn.WaveFormat; }
		}

		#endregion

		private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
		{
			bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
		}
	}
}