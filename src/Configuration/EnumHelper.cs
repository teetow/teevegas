using System;

namespace Configuration
{
	public static class EnumHelper
	{
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
	}
}