using System.Text.RegularExpressions;

namespace NAudio.Wave.WaveStreams
{
	/// <summary>
	/// Holds information on a cue: a labeled position within a Wave file
	/// </summary>
	public class Cue
	{
		/// <summary>
		/// Creates a Cue based on a sample position and label 
		/// </summary>
		/// <param name="position"></param>
		/// <param name="label"></param>
		public Cue(int position, string label)
		{
			Position = position;
			if (label == null)
			{
				label = "";
			}
			Label = Regex.Replace(label, @"[^\u0000-\u00FF]", "");
		}

		/// <summary>
		/// Cue position in samples
		/// </summary>
		public int Position { get; private set; }

		/// <summary>
		/// Label of the cue
		/// </summary>
		public string Label { get; private set; }
	}
}