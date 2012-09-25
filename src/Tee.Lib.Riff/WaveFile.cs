using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave.WaveFormats;
using NAudio.Wave.WaveStreams;
using Tee.Lib.Riff.Riff;

namespace Tee.Lib.Riff
{
	namespace Wave
	{
		public class WaveFile : RiffFile
		{
			public List<Region> Regions;
			private List<Marker> _markers;

			public WaveFile(String Filename)
				: base(Filename)
			{
				EnumerateMarkers();
			}

			public WaveFile(String Filename, bool ReadOnly)
				: base(Filename, ReadOnly)
			{
				EnumerateMarkers();
			}

			public void EnumerateMarkers()
			{
				_markers = new List<Marker>();

				// enumerate markers
				var cueChunk = (CkCue)GetChunk<CkCue>();
				if (cueChunk == null)
					return;
				foreach (CuePoint cp in cueChunk.CuePoints)
				{
					var mk = new Marker(cp);
					_markers.Add(mk);
					var listChunk = (CkList)GetChunk<CkList>();
					if (listChunk == null)
						continue;
					foreach (ListChunk lc in listChunk.Chunks)
					{
						if (lc.GetType() == typeof(LiCkInfoLabl))
						{
							var me = lc as LiCkInfoLabl;
							if (me != null && me.CuePointID == cp.ID)
							{
								mk.LablChunk = (LiCkInfoLabl)lc;
								break;
							}
						}
					}
				}
			}

			public Marker AddMarker(UInt32 SamplePos, String Label)
			{
				Marker mk = AddMarker(SamplePos);
				CkList listChunk = (CkList)GetChunk<CkList>() ?? (CkList)AddChunk(CkType.LIST);
				var lb = new LiCkInfoLabl(mk.CuePoint, Label);
				listChunk.Chunks.Add(lb);
				return mk;
			}

			public Marker AddMarker(UInt32 SamplePos)
			{
				CkCue cueChunk = (CkCue)GetChunk<CkCue>() ?? (CkCue)AddChunk(CkType.cue);
				CuePoint cp = cueChunk.AddCuePoint(SamplePos);
				var mk = new Marker(cp);
				EnumerateMarkers();
				return mk;
			}

			public static void AddMarkers(String WaveFile, Dictionary<double, string> Markers)
			{
				var file = new RiffFile(WaveFile, false);

				var reader = new WaveFileReader(WaveFile);
				WaveFormat fmt = reader.WaveFormat;

				string temp1name = Path.GetDirectoryName(WaveFile) + Path.GetFileNameWithoutExtension(WaveFile) + "-temp" +
								   Path.GetExtension(WaveFile);

				// find old cue chunk, or add new
				var cueChunk = file.GetChunk<CkCue>() as CkCue;
				if (cueChunk == null)
				{
					cueChunk = file.AddChunk(CkType.cue) as CkCue;
				}
				else
					cueChunk.CuePoints.Clear();

				var listChunk = file.GetChunk<CkList>() as CkList;
				if (listChunk == null || listChunk.TypeID != LiCkType.adtl)
				{
					listChunk = file.AddChunk(CkType.LIST) as CkList;
					if (listChunk != null) listChunk.TypeID = LiCkType.adtl;
				}

				if (listChunk != null) listChunk.Chunks.Clear();

				uint cueCounter = 0;

				foreach (var pair in Markers)
				{
					var samplePos = (uint)(pair.Key * fmt.SampleRate);
					var cp = new CuePoint(samplePos) { ID = ++cueCounter };
					if (cueChunk != null) cueChunk.CuePoints.Add(cp);
					var labl = new LiCkInfoLabl(cp, pair.Value);
					if (listChunk != null) listChunk.Chunks.Add(labl);
				}
				reader.Close();
				reader.Dispose();
				file.Save(temp1name);
				file.Close();

				File.Replace(temp1name, WaveFile, null);
			}

			public Region AddRegion(UInt32 Start, UInt32 Length)
			{
				// cp
				CkCue cueChunk = (CkCue)GetChunk<CkCue>() ?? (CkCue)AddChunk(CkType.cue);
				var cp = new CuePoint(Start) { ID = (UInt32)cueChunk.CuePoints.Count + 1 };
				cueChunk.CuePoints.Add(cp);

				// ltxt entry
				CkList listChunk = (CkList)GetChunk<CkList>() ?? (CkList)AddChunk(CkType.LIST);
				var ltxtChunk = new LiCkLtxt(cp.ID, Length);
				listChunk.Chunks.Add(ltxtChunk);

				// create the region
				var newRegion = new Region(cp) { Length = Length, LtxtChunk = ltxtChunk };
				return newRegion;
			}

			public Region AddRegion(UInt32 Start, UInt32 Length, string Label)
			{
				Region newRegion = AddRegion(Start, Length);
				// label
				var listChunk = GetChunk<CkList>() as CkList;
				var newLabl = new LiCkInfoLabl(newRegion.CuePoint, Label);
				if (listChunk != null) listChunk.Chunks.Add(newLabl);
				return newRegion;
			}

			public static void StripPadding(string WaveFile, int Seconds)
			{
				var reader = new WaveFileReader(WaveFile);
				var strTarget = new BinaryWriter(new MemoryStream());
				WaveFormat fmt = reader.WaveFormat;

				string temp1name = Path.GetDirectoryName(WaveFile) + Path.DirectorySeparatorChar +
								   Path.GetFileNameWithoutExtension(WaveFile) + "-temp." + Path.GetExtension(WaveFile);
				string temp2name = Path.GetDirectoryName(WaveFile) + Path.DirectorySeparatorChar +
								   Path.GetFileNameWithoutExtension(WaveFile) + "-old." + Path.GetExtension(WaveFile);
				int samples = fmt.SampleRate * Seconds;
				int sampleSize = (fmt.BitsPerSample / 8) * fmt.Channels;
				int padBytes = samples * sampleSize;
				var bufsize = (int)(reader.Length - (padBytes * 2));
				var buf = new byte[bufsize];
				int newSampleCount = bufsize / sampleSize;

				reader.Seek(padBytes, SeekOrigin.Begin);
				strTarget.Write(reader.Read(buf, 0, buf.Length));
				reader.Close();
				reader.Dispose();

				var file = new RiffFile(WaveFile, false);
				file.GetChunk<CkData>().Data = buf;

				// adjust marker positions, remove if outside
				foreach (Chunk ch in file.Chunks)
				{
					if (ch.GetType() == typeof(CkCue))
					{
						var me = ch as CkCue;
						//foreach (CuePoint cp in me.CuePoints)
						if (me != null)
							for (int i = me.CuePoints.Count - 1; i >= 0; i--)
							{
								CuePoint cp = me.CuePoints[i];
								cp.Position -= (uint)samples;
								cp.SampleOffset -= (uint)samples;
								if (cp.Position > newSampleCount)
									me.CuePoints.Remove(cp);
							}
					}

						// trim down region lengths (ugly, but necessary due to Vegas rounding rather than truncating)
					else if (ch is CkList)
					{
						var me = ch as CkList;
						foreach (ListChunk lch in me.Chunks)
						{
							if (lch.GetType() == typeof(LiCkLtxt))
							{
								var lt = lch as LiCkLtxt;
								if (lt != null && lt.SampleLength > newSampleCount)
								{
									lt.SampleLength = (uint)newSampleCount;
								}
							}
							else if (lch.GetType() == typeof(LiCkInfoTCOD))
							{
								var tch = lch as LiCkInfoTCOD;
								if (tch != null) tch.Position += (uint)samples;
							}
							else if (lch.GetType() == typeof(LiCkInfoTCDO))
							{
								var tch = lch as LiCkInfoTCDO;
								if (tch != null) tch.Position -= (uint)samples;
							}
						}
					}
				}
				file.Save(temp1name);
				File.Move(WaveFile, temp2name);
				File.Move(temp1name, WaveFile);
				File.Delete(temp2name);
			}
		}
	}
}