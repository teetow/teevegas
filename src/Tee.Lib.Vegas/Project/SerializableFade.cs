using System;
using Sony.Vegas;

namespace Tee.Lib.Vegas.Project
{
	[Serializable]
	public class SerializableFade
	{
		public string CurveType;
		public string ReciprocalCurve;
		public long LengthNanos;
		public float Gain;

		public SerializableFade(Fade fade)
		{
			CurveType = fade.Curve.ToString();
			ReciprocalCurve = fade.ReciprocalCurve.ToString();
			LengthNanos = fade.Length.Nanos;
			Gain = fade.Gain;
		}
	}
}