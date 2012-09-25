namespace NAudio.Mixer
{
	/// <summary>
	/// Mixer control types
	/// </summary>
	public enum MixerControlType
	{
		/// <summary>Custom</summary>
		Custom = (MixerControlClass.Custom | MixerControlUnits.Custom),

		/// <summary>Boolean meter</summary>
		BooleanMeter = (MixerControlClass.Meter | MixerControlSubclass.MeterPolled | MixerControlUnits.Boolean),

		/// <summary>Signed meter</summary>
		SignedMeter = (MixerControlClass.Meter | MixerControlSubclass.MeterPolled | MixerControlUnits.Signed),

		/// <summary>Peak meter</summary>
		PeakMeter = (SignedMeter + 1),

		/// <summary>Unsigned meter</summary>
		UnsignedMeter = (MixerControlClass.Meter | MixerControlSubclass.MeterPolled | MixerControlUnits.Unsigned),

		/// <summary>Boolean</summary>
		Boolean = (MixerControlClass.Switch | MixerControlSubclass.SwitchBoolean | MixerControlUnits.Boolean),

		/// <summary>On Off</summary>
		OnOff = (Boolean + 1),

		/// <summary>Mute</summary>
		Mute = (Boolean + 2),

		/// <summary>Mono</summary>
		Mono = (Boolean + 3),

		/// <summary>Loudness</summary>
		Loudness = (Boolean + 4),

		/// <summary>Stereo Enhance</summary>
		StereoEnhance = (Boolean + 5),

		/// <summary>Button</summary>
		Button = (MixerControlClass.Switch | MixerControlSubclass.SwitchButton | MixerControlUnits.Boolean),

		/// <summary>Decibels</summary>
		Decibels = (MixerControlClass.Number | MixerControlUnits.Decibels),

		/// <summary>Signed</summary>
		Signed = (MixerControlClass.Number | MixerControlUnits.Signed),

		/// <summary>Unsigned</summary>
		Unsigned = (MixerControlClass.Number | MixerControlUnits.Unsigned),

		/// <summary>Percent</summary>
		Percent = (MixerControlClass.Number | MixerControlUnits.Percent),

		/// <summary>Slider</summary>
		Slider = (MixerControlClass.Slider | MixerControlUnits.Signed),

		/// <summary>Pan</summary>
		Pan = (Slider + 1),

		/// <summary>Q-sound pan</summary>
		QSoundPan = (Slider + 2),

		/// <summary>Fader</summary>
		Fader = (MixerControlClass.Fader | MixerControlUnits.Unsigned),

		/// <summary>Volume</summary>
		Volume = (Fader + 1),

		/// <summary>Bass</summary>
		Bass = (Fader + 2),

		/// <summary>Treble</summary>
		Treble = (Fader + 3),

		/// <summary>Equaliser</summary>
		Equalizer = (Fader + 4),

		/// <summary>Single Select</summary>
		SingleSelect = (MixerControlClass.List | MixerControlSubclass.ListSingle | MixerControlUnits.Boolean),

		/// <summary>Mux</summary>
		Mux = (SingleSelect + 1),

		/// <summary>Multiple select</summary>
		MultipleSelect = (MixerControlClass.List | MixerControlSubclass.ListMultiple | MixerControlUnits.Boolean),

		/// <summary>Mixer</summary>
		Mixer = (MultipleSelect + 1),

		/// <summary>Micro time</summary>
		MicroTime = (MixerControlClass.Time | MixerControlSubclass.TimeMicrosecs | MixerControlUnits.Unsigned),

		/// <summary>Milli time</summary>
		MilliTime = (MixerControlClass.Time | MixerControlSubclass.TimeMillisecs | MixerControlUnits.Unsigned),
	}
}