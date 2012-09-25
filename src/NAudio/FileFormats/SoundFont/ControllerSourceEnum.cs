namespace NAudio.FileFormats.SoundFont
{
	/// <summary>
	/// Controller Sources
	/// </summary>
	public enum ControllerSourceEnum
	{
		/// <summary>
		/// No Controller
		/// </summary>
		NoController = 0,

		/// <summary>
		/// Note On Velocity
		/// </summary>
		NoteOnVelocity = 2,

		/// <summary>
		/// Note On Key Number
		/// </summary>
		NoteOnKeyNumber = 3,

		/// <summary>
		/// Poly Pressure
		/// </summary>
		PolyPressure = 10,

		/// <summary>
		/// Channel Pressure
		/// </summary>
		ChannelPressure = 13,

		/// <summary>
		/// Pitch Wheel
		/// </summary>
		PitchWheel = 14,

		/// <summary>
		/// Pitch Wheel Sensitivity
		/// </summary>
		PitchWheelSensitivity = 16
	}
}