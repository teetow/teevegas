using System;
using System.IO;

namespace NAudio.FileFormats.SoundFont
{
	/// <summary>
	/// Represents a SoundFont
	/// </summary>
	public class SoundFont
	{
		private readonly InfoChunk info;
		private readonly PresetsChunk presetsChunk;
		private readonly SampleDataChunk sampleData;

		/// <summary>
		/// Loads a SoundFont from a file
		/// </summary>
		/// <param name="fileName">Filename of the SoundFont</param>
		public SoundFont(string fileName)
		{
			using (var sfFile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				RiffChunk riff = RiffChunk.GetTopLevelChunk(new BinaryReader(sfFile));
				if (riff.ChunkID == "RIFF")
				{
					string formHeader = riff.ReadChunkID();
					if (formHeader != "sfbk")
					{
						throw new ApplicationException(String.Format("Not a SoundFont ({0})", formHeader));
					}
					RiffChunk list = riff.GetNextSubChunk();
					if (list.ChunkID == "LIST")
					{
						//RiffChunk r = list.GetNextSubChunk();
						info = new InfoChunk(list);

						RiffChunk r = riff.GetNextSubChunk();
						sampleData = new SampleDataChunk(r);

						r = riff.GetNextSubChunk();
						presetsChunk = new PresetsChunk(r);
					}
					else
					{
						throw new ApplicationException(String.Format("Not info list found ({0})", list.ChunkID));
					}
				}
				else
				{
					throw new ApplicationException("Not a RIFF file");
				}
			}
		}

		/// <summary>
		/// The File Info Chunk
		/// </summary>
		public InfoChunk FileInfo
		{
			get { return info; }
		}

		/// <summary>
		/// The Presets
		/// </summary>
		public Preset[] Presets
		{
			get { return presetsChunk.Presets; }
		}

		/// <summary>
		/// The Instruments
		/// </summary>
		public Instrument[] Instruments
		{
			get { return presetsChunk.Instruments; }
		}

		/// <summary>
		/// The Sample Headers
		/// </summary>
		public SampleHeader[] SampleHeaders
		{
			get { return presetsChunk.SampleHeaders; }
		}

		/// <summary>
		/// The Sample Data
		/// </summary>
		public byte[] SampleData
		{
			get { return sampleData.SampleData; }
		}

		/// <summary>
		/// <see cref="object.ToString"/>
		/// </summary>
		public override string ToString()
		{
			return String.Format("Info Chunk:\r\n{0}\r\nPresets Chunk:\r\n{1}",
			                     info, presetsChunk);
		}

		// TODO: save / save as function
	}
}