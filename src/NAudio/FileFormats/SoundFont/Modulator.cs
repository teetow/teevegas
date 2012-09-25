using System;

namespace NAudio.FileFormats.SoundFont
{
	/// <summary>
	/// Modulator
	/// </summary>
	public class Modulator
	{
		private short amount;
		private GeneratorEnum destinationGenerator;
		private ModulatorType sourceModulationAmount;
		private ModulatorType sourceModulationData;
		private TransformEnum sourceTransform;

		/// <summary>
		/// Source Modulation data type
		/// </summary>
		public ModulatorType SourceModulationData
		{
			get { return sourceModulationData; }
			set { sourceModulationData = value; }
		}

		/// <summary>
		/// Destination generator type
		/// </summary>
		public GeneratorEnum DestinationGenerator
		{
			get { return destinationGenerator; }
			set { destinationGenerator = value; }
		}

		/// <summary>
		/// Amount
		/// </summary>
		public short Amount
		{
			get { return amount; }
			set { amount = value; }
		}

		/// <summary>
		/// Source Modulation Amount Type
		/// </summary>
		public ModulatorType SourceModulationAmount
		{
			get { return sourceModulationAmount; }
			set { sourceModulationAmount = value; }
		}

		/// <summary>
		/// Source Transform Type
		/// </summary>
		public TransformEnum SourceTransform
		{
			get { return sourceTransform; }
			set { sourceTransform = value; }
		}

		/// <summary>
		/// <see cref="object.ToString"/>
		/// </summary>
		public override string ToString()
		{
			return String.Format("Modulator {0} {1} {2} {3} {4}",
			                     sourceModulationData, destinationGenerator,
			                     amount, sourceModulationAmount, sourceTransform);
		}
	}
}