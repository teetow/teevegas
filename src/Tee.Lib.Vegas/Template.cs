using System.Collections.Generic;
using System.Linq;
using ScriptPortal.Vegas;

namespace Tee.Lib.Vegas
{
	public static class Template
	{
		public static int Scan(string Needle, string Haystack)
		{
			Needle = StripNonAlpha(Needle).ToLower();
			Haystack = StripNonAlpha(Haystack).ToLower();

			int NeedlePos = 0, HayPos = 0, BestPos = 0;

			while (NeedlePos < Needle.Length && HayPos < Haystack.Length)
			{
				if (Needle[NeedlePos] == Haystack[HayPos])
				{
					BestPos++;
					NeedlePos++;
					HayPos++;
					//continue;
				}
				else
					HayPos++;
			}
			return BestPos;
		}

		public static string StripNonAlpha(string Needle)
		{
			string result = string.Empty;
			if (Needle != null)
			{
				result = Needle.Where(char.IsLetterOrDigit).Aggregate(result, (current, c) => current + c);
			}
			return result;
		}

		public static Renderer FindBestRendererByName(this IEnumerable<Renderer> RendererCollection, string RendererName)
		{
			var bestRenderer = new KeyValuePair<int, Renderer>();

			foreach (Renderer curRenderer in RendererCollection)
			{
				int curScore = Scan(RendererName, curRenderer.Name);

				if (bestRenderer.Value == null || curScore > bestRenderer.Key)
					bestRenderer = new KeyValuePair<int, Renderer>(curScore, curRenderer);
			}
			return bestRenderer.Value;
		}

		public static RenderTemplate FindBestTemplateByName(this RenderTemplates TemplateCollection, string TemplateName)
		{
			// instant escape on null
			if (TemplateName == null || TemplateName.Equals(string.Empty))
				return null;

			// quick escape if we have a perfect match
			RenderTemplate exactMatch = TemplateCollection.FindByName(TemplateName);
			if (exactMatch != null && exactMatch.Name.Equals(TemplateName))
				return exactMatch;

			var bestTemplate = new KeyValuePair<int, RenderTemplate>();

			foreach (RenderTemplate curTemplate in TemplateCollection)
			{
				int curScore = Scan(TemplateName, curTemplate.Name);

				if (bestTemplate.Value == null || curScore > bestTemplate.Key)
					bestTemplate = new KeyValuePair<int, RenderTemplate>(curScore, curTemplate);
			}
			return bestTemplate.Value;
		}
	}
}