using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace NAudio.Utils
{
	/// <summary>
	/// Helper class for registering Windows Explorer File associations
	/// </summary>
	public class FileAssociations
	{
		[DllImport("shell32.dll")]
		private static extern void SHChangeNotify(HChangeNotifyEventID wEventId,
		                                          HChangeNotifyFlags uFlags,
		                                          IntPtr dwItem1,
		                                          IntPtr dwItem2);

		/// <summary>
		/// Determines if the specified file type is registered
		/// </summary>
		/// <param name="extension">The file extension including the leading period (e.g. ".wav")</param>
		/// <returns>True if it is registered</returns>
		public static bool IsFileTypeRegistered(string extension)
		{
			RegistryKey key = Registry.ClassesRoot.OpenSubKey(extension);
			if (key == null)
				return false;
			key.Close();
			return true;
		}

		/// <summary>
		/// Gets the HKCR key name for the extenstion
		/// </summary>
		/// <param name="extension">The file extension including the leading period (e.g. ".wav")</param>
		/// <returns>The HKCR key name or null if not registered</returns>
		public static string GetFileTypeKey(string extension)
		{
			RegistryKey key = Registry.ClassesRoot.OpenSubKey(extension);
			string fileTypeKey = null;
			if (key != null)
			{
				fileTypeKey = (string) key.GetValue(null);
				key.Close();
			}
			return fileTypeKey;
		}

		/// <summary>
		/// Registers a file type in Windows
		/// </summary>
		/// <param name="extension">Extension include leading '.'</param>
		/// <param name="description">The description of this file type</param>
		/// <param name="iconPath">Null for no icon or e.g c:\windows\regedit.exe,0</param>
		public static void RegisterFileType(string extension, string description, string iconPath)
		{
			if (IsFileTypeRegistered(extension))
				throw new ArgumentException(extension + "is already registered");

			RegistryKey key = Registry.ClassesRoot.CreateSubKey(extension);
			string fileKey = extension.Substring(1) + "File";
			key.SetValue(null, fileKey);
			key.Close();
			key = Registry.ClassesRoot.CreateSubKey(fileKey);
			key.SetValue(null, description);
			key.Close();
			if (iconPath != null)
			{
				key = Registry.ClassesRoot.CreateSubKey(fileKey + "\\DefaultIcon");
				key.SetValue(null, iconPath);
				key.Close();
			}
			SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
		}

		/// <summary>
		/// Adds a new explorer context menu action for this file type
		/// Will overwrite any existing action with this key
		/// </summary>
		/// <param name="extension">The file extension (include the leading dot)</param>
		/// <param name="actionKey">A unique key for this action</param>
		/// <param name="actionDescription">What will appear on the context menu</param>
		/// <param name="command">The command to execute</param>
		public static void AddAction(string extension, string actionKey, string actionDescription, string command)
		{
			// command e.g. notepad.exe "%1"
			string fileTypeKey = GetFileTypeKey(extension);
			if (fileTypeKey == null)
				throw new ArgumentException(extension + "is not a registered file type");
			RegistryKey key = Registry.ClassesRoot.CreateSubKey(fileTypeKey + "\\shell\\" + actionKey);
			key.SetValue(null, actionDescription);
			key.Close();

			key = Registry.ClassesRoot.CreateSubKey(fileTypeKey + "\\shell\\" + actionKey + "\\command");
			key.SetValue(null, command);
			key.Close();
			SHChangeNotify(HChangeNotifyEventID.SHCNE_ASSOCCHANGED, HChangeNotifyFlags.SHCNF_IDLIST, IntPtr.Zero, IntPtr.Zero);
		}

		/// <summary>
		/// Removes an explorer context menu action from a file extension
		/// </summary>
		/// <param name="extension">The file extension (include the leading dot)</param>
		/// <param name="actionKey">The unique key used to register this action</param>
		public static void RemoveAction(string extension, string actionKey)
		{
			string fileTypeKey = GetFileTypeKey(extension);
			if (fileTypeKey == null)
			{
				return;
				//throw new ArgumentException(extension + "is not a registered file type");
			}
			Registry.ClassesRoot.DeleteSubKey(fileTypeKey + "\\shell\\" + actionKey + "\\command");
			Registry.ClassesRoot.DeleteSubKey(fileTypeKey + "\\shell\\" + actionKey);
		}

		// TODO: add ourselves as an "Open With" application
		// TODO: better error handling
		// TODO: unregistering stuff
		// TODO: set default action
	}

	#region enum HChangeNotifyEventID

	#endregion // enum HChangeNotifyEventID

	#region public enum HChangeNotifyFlags

	#endregion // enum HChangeNotifyFlags
}