using System.ComponentModel;
using System.Windows.Forms;

namespace NAudio.Gui
{
	/// <summary>
	/// The Arranger control
	/// </summary>
	public class Arranger : UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private readonly Container components;

		/// <summary>
		/// Creates a new arranger control
		/// </summary>
		public Arranger(Container Components)
		{
			components = Components;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Arranger
			// 
			this.Name = "Arranger";
			this.Size = new System.Drawing.Size(472, 376);
		}

		#endregion
	}
}