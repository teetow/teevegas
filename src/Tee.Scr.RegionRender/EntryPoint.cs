using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using ScriptPortal.Vegas;
using Tee.Lib.Vegas;
using Tee.Lib.Vegas.Render;

namespace Tee.Scr.RegionRender
{
	public class EntryPoint
	{
		public void FromVegas(Vegas Vegas, String ScriptFile, XmlDocument ScriptSettings, ScriptArgs Args)
		{
			if (ArgsHasSetup(Args)) // config mode
			{
				RunSetup(Vegas);
			}
			else
			{
				var renderset = new RenderSet();
				var form = new RegionRenderForm(Vegas, renderset);

				if (DialogResult.OK != form.ShowDialog())
					return;
				//renderset.Render(Vegas);
			}
		}

		public static void RunSetup(Vegas Vegas)
		{
			var nextRegion = Vegas.Project.NextRegion(Vegas.Transport.CursorPosition);
			var currentParams = Vegas.Project.GetParamsAt((nextRegion != null) ? nextRegion.Position : Vegas.Project.Length);
			var form = new RenderParamsForm(Vegas, currentParams);

			if (form.ShowDialog() != DialogResult.OK)
				return;

			int offsetCounter = 0;
			const long markerSpacing = 10000;
			Timecode startPos = Vegas.Transport.CursorPosition;
			Timecode currentPos = startPos;
			foreach (RenderParameter param in form.UserRenderParams.RenderParams)
			{
				// skip basedir for now TODO: FIX THIS
				if (param.Name == RenderTags.RootDir)
					continue;

				// skip params that are the same as previous
				var currentParam = currentParams.GetParam(param.Name);
				if (param.Value.Equals(currentParam.Value))
					continue;

				// find last marker of this type
				string paramName = param.Name;
				var sameTypeMarkers = Vegas.Project.CommandMarkers.Where(item => item.CommandType.ToString().Equals(paramName, StringComparison.InvariantCultureIgnoreCase));
				var sameRegionMarkers = sameTypeMarkers.Where(sibling => !Vegas.Project.RegionsBetween(sibling.Position, currentPos));
				CommandMarker updateCandidate = null;

				// dooo eeet
				foreach (var sibling in sameRegionMarkers)
				{
					updateCandidate = sibling;
				}
				if (updateCandidate != null)
				{
					updateCandidate.CommandParameter = param.Value.ToString();
				}
				else
				{
					CommandMarker mk = null;
					do
					{
						try
						{
							mk = new CommandMarker(currentPos, new MarkerCommandType(param.Name), param.Value.ToString());
						}
						catch (Exception)
						{
							currentPos += Timecode.FromNanos(markerSpacing * offsetCounter++);
						}
					} while (mk == null);

					Vegas.Project.CommandMarkers.Add(mk);
					currentPos = startPos + Timecode.FromNanos(markerSpacing * offsetCounter++);
				}
			}
		}

		private static bool ArgsHasSetup(ScriptArgs args)
		{
			return args.Cast<ScriptArg>().Any(derp => string.Equals(derp.Value, RegionRenderStrings.ScriptSetupArg, StringComparison.InvariantCultureIgnoreCase));
		}
	}
}