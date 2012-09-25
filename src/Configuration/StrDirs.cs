using System;
using System.Collections.Generic;

namespace Configuration
{
	public class StrDirs
	{
		public static List<string> AppDirs = new List<string>
		{
		    String.Format(@"{0}\Sony\Vegas Pro\",
		                    Environment.GetFolderPath(
		                    Environment.SpecialFolder.CommonApplicationData))
		    ,
		    String.Format(@"{0}\",
		                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))
		};
	}
}