using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ScriptPortal.Vegas;
using Tee.Lib.Vegas.Render;

namespace Tee.Lib.Vegas
{
	public static class Ext
	{
		// winforms
		public static void ScrollToEnd(this RichTextBox Rtb)
		{
			Rtb.SuspendLayout();
			Rtb.Select(Rtb.TextLength, 0);
			Rtb.ScrollToCaret();
			Rtb.ResumeLayout(true);
		}

		public static bool IsSet<T>(this Enum Type, T Value)
		{
			try
			{
				return (((int)(object)Type & (int)(object)Value) == (int)(object)Value);
			}
			catch
			{
				return false;
			}
		}

		public static T Set<T>(this Enum Type, T Value)
		{
			try
			{
				return (T)(object)(((int)(object)Type | (int)(object)Value));
			}
			catch (Exception ex)
			{
				throw new ArgumentException(
					String.Format(
						"Could not append value from enumerated type '{0}'.",
						typeof(T).Name
						), ex);
			}
		}

		public static T UnSet<T>(this Enum Type, T Value)
		{
			try
			{
				return (T)(object)(((int)(object)Type & ~(int)(object)Value));
			}
			catch (Exception ex)
			{
				throw new ArgumentException(
					String.Format(
						"Could not remove value from enumerated type '{0}'.",
						typeof(T).Name
						), ex);
			}
		}

		public static void SwapTimecode(ref Timecode X, ref Timecode Y)
		{
			Timecode temp = X;
			X = Y;
			Y = temp;
		}

		public static bool ContainsTime(this Region Region, Timecode Time, Timecode Length = null)
		{
			// I think I've accidentally been quite clever.
			if (Time <= Region.End && Time >= Region.Position)
			{
				if (Length != null)
				{
					if (Region.ContainsTime(Time + Length))
						return true;
				}
				return true;
			}
			return false;
		}

		///
		/// Rendering
		///

		public static RenderParamSet GetParamsAt(this ScriptPortal.Vegas.Project Project, Timecode Time)
		{
			var renderParams = new RenderParamSet();
			var cmarkers = Project.CommandMarkers.Where(mk => mk.Position <= Time);
			foreach (var mk in cmarkers)
			{
				renderParams.ParseCommandMarker(mk);
			}

			return renderParams;
		}

		public static bool ContainsRenderTag(this String str, string RenderTag)
		{
			return !string.IsNullOrEmpty(str) && str.ToLowerInvariant().Contains("{" + RenderTag.ToLowerInvariant() + "}");
		}

		public static bool ContainsRenderTag(this String str, bool IncludeBraces = true)
		{
			if (string.IsNullOrEmpty(str))
				return false;

			string s = str.ToLowerInvariant().Trim();
			return RenderTags.Tags.Any(tag => s.Contains(IncludeBraces ? RenderTags.Format(tag) : tag));
		}

		/// Returns a string cleaned of any rendertags
		public static string StripRenderTags(this string Input)
		{
			if (string.IsNullOrEmpty(Input))
				return String.Empty;

			Regex renderTagRegex = new Regex(@"\{(?<key>\w+?)[=: ](?<value>[^{]+?)\}", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
			return renderTagRegex.Replace(Input, "");
		}

		/// <summary>
		/// Returns the longest string that is common between two strings.
		/// </summary>
		/// <param name="Source">The source string (implied)</param>
		/// <param name="Comparison">The string to be compared</param>
		/// <param name="CaseSensitive">Whether the comparison should be case sensitive</param>
		/// <returns>A new string, </returns>
		public static string SharedString(this String Source, String Comparison, bool CaseSensitive = false)
		{
			if (Source == null || Comparison == null)
				return String.Empty;
			var root = new StringBuilder();
			int maxCheck = Math.Min(Source.Length, Comparison.Length);

			for (int i = 0; i < maxCheck; i++)
			{
				string srcChar = Source[i].ToString();
				string dstChar = Comparison[i].ToString();
				if (!CaseSensitive)
				{
					srcChar = srcChar.ToLower();
					dstChar = dstChar.ToLower();
				}

				if (srcChar == dstChar)
					root.Append(Source[i]);
				else
					break;
			}
			return root.ToString();
		}

		/// <summary>
		/// Returns the common root of a list of strings, e.g. SharedRoot({"foo", "foobar", "foobaz"}) would return "foo"
		/// </summary>
		/// <param name="Strings">A list of strings</param>
		/// <returns>The shared root string</returns>
		public static string SharedRoot(this List<string> Strings)
		{
			if (Strings.Count == 1)
				return Strings[0];
			string root = null;
			string sharedRoot = null;
			foreach (string s in Strings)
			{
				if (root == null)
				{
					root = s;
					continue;
				}
				sharedRoot = s.SharedString(root);
			}
			return sharedRoot;
		}

		public static string ToString(this MediaMarker Marker)
		{
			return String.Format("[{0}]{1}: {2}", Marker.Index, Marker.Position, Marker.Label);
		}

		public static string ToString(this Marker Marker)
		{
			return String.Format("[{0}]{1}: {2}", Marker.Index, Marker.Position, Marker.Label);
		}

		public static string ToString(this Region Marker)
		{
			return String.Format("[{0}]{1}: {2}", Marker.Index, Marker.Position, Marker.Label);
		}
	}
}