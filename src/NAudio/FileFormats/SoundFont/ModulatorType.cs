using System;

namespace NAudio.FileFormats.SoundFont
{
	/// <summary>
	/// Modulator Type
	/// </summary>
	public class ModulatorType
	{
		private readonly ControllerSourceEnum controllerSource;
		private readonly bool midiContinuousController;
		private readonly ushort midiContinuousControllerNumber;
		private readonly SourceTypeEnum sourceType;
		private bool direction;
		private bool polarity;

		internal ModulatorType(ushort raw)
		{
			// TODO: map this to fields
			polarity = ((raw & 0x0200) == 0x0200);
			direction = ((raw & 0x0100) == 0x0100);
			midiContinuousController = ((raw & 0x0080) == 0x0080);
			sourceType = (SourceTypeEnum) ((raw & (0xFC00)) >> 10);

			controllerSource = (ControllerSourceEnum) (raw & 0x007F);
			midiContinuousControllerNumber = (ushort) (raw & 0x007F);
		}

		/// <summary>
		/// <see cref="object.ToString"/>
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (midiContinuousController)
				return String.Format("{0} CC{1}", sourceType, midiContinuousControllerNumber);
			else
				return String.Format("{0} {1}", sourceType, controllerSource);
		}
	}
}