using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace NAudio.Wave.Asio
{
	/// <summary>
	/// Main ASIODriver Class. To use this class, you need to query first the GetASIODriverNames() and
	/// then use the GetASIODriverByName to instantiate the correct ASIODriver.
	/// This is the first ASIODriver binding fully implemented in C#!
	/// 
	/// Contributor: Alexandre Mutel - email: alexandre_mutel at yahoo.fr
	/// </summary>
	internal class ASIODriver
	{
		private ASIODriverVTable asioDriverVTable;
		private IntPtr pASIOComObject;
		private IntPtr pinnedcallbacks;
		//GCHandle hThis;

		private ASIODriver()
		{
			// MRH experimental code
			//hThis = GCHandle.Alloc(this, GCHandleType.Pinned);
		}

		/*~ASIODriver()
        {
            hThis.Free();
        }*/

		/// <summary>
		/// Gets the ASIO driver names installed.
		/// </summary>
		/// <returns>a list of driver names. Use this name to GetASIODriverByName</returns>
		public static String[] GetASIODriverNames()
		{
			RegistryKey regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ASIO");
			var names = new string[0];
			if (regKey != null)
			{
				names = regKey.GetSubKeyNames();
				regKey.Close();
			}
			return names;
		}

		/// <summary>
		/// Instantiate a ASIODriver given its name.
		/// </summary>
		/// <param name="name">The name of the driver</param>
		/// <returns>an ASIODriver instance</returns>
		public static ASIODriver GetASIODriverByName(String name)
		{
			RegistryKey regKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ASIO\\" + name);
			if (regKey == null)
			{
				throw new ArgumentException(String.Format("Driver Name {0} doesn't exist", name));
			}
			string guid = regKey.GetValue("CLSID").ToString();
			return GetASIODriverByGuid(new Guid(guid));
		}

		/// <summary>
		/// Instantiate the ASIO driver by GUID.
		/// </summary>
		/// <param name="guid">The GUID.</param>
		/// <returns>an ASIODriver instance</returns>
		public static ASIODriver GetASIODriverByGuid(Guid guid)
		{
			var driver = new ASIODriver();
			driver.initFromGuid(guid);
			return driver;
		}

		/// <summary>
		/// Inits the ASIODriver..
		/// </summary>
		/// <param name="sysHandle">The sys handle.</param>
		/// <returns></returns>
		public bool init(IntPtr sysHandle)
		{
			return asioDriverVTable.init(pASIOComObject, sysHandle);
		}

		/// <summary>
		/// Gets the name of the driver.
		/// </summary>
		/// <returns></returns>
		public String getDriverName()
		{
			var name = new StringBuilder(256);
			asioDriverVTable.getDriverName(pASIOComObject, name);
			return name.ToString();
		}

		/// <summary>
		/// Gets the driver version.
		/// </summary>
		/// <returns></returns>
		public int getDriverVersion()
		{
			return asioDriverVTable.getDriverVersion(pASIOComObject);
		}

		/// <summary>
		/// Gets the error message.
		/// </summary>
		/// <returns></returns>
		public String getErrorMessage()
		{
			var errorMessage = new StringBuilder(256);
			asioDriverVTable.getErrorMessage(pASIOComObject, errorMessage);
			return errorMessage.ToString();
		}

		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void start()
		{
			handleException(asioDriverVTable.start(pASIOComObject), "start");
		}

		/// <summary>
		/// Stops this instance.
		/// </summary>
		public ASIOError stop()
		{
			return asioDriverVTable.stop(pASIOComObject);
		}

		/// <summary>
		/// Gets the channels.
		/// </summary>
		/// <param name="numInputChannels">The num input channels.</param>
		/// <param name="numOutputChannels">The num output channels.</param>
		public void getChannels(out int numInputChannels, out int numOutputChannels)
		{
			handleException(asioDriverVTable.getChannels(pASIOComObject, out numInputChannels, out numOutputChannels),
			                "getChannels");
		}

		/// <summary>
		/// Gets the latencies.
		/// </summary>
		/// <param name="inputLatency">The input latency.</param>
		/// <param name="outputLatency">The output latency.</param>
		public void getLatencies(out int inputLatency, out int outputLatency)
		{
			handleException(asioDriverVTable.getLatencies(pASIOComObject, out inputLatency, out outputLatency), "getLatencies");
		}

		/// <summary>
		/// Gets the size of the buffer.
		/// </summary>
		/// <param name="minSize">Size of the min.</param>
		/// <param name="maxSize">Size of the max.</param>
		/// <param name="preferredSize">Size of the preferred.</param>
		/// <param name="granularity">The granularity.</param>
		public void getBufferSize(out int minSize, out int maxSize, out int preferredSize, out int granularity)
		{
			handleException(
				asioDriverVTable.getBufferSize(pASIOComObject, out minSize, out maxSize, out preferredSize, out granularity),
				"getBufferSize");
		}

		/// <summary>
		/// Determines whether this instance can use the specified sample rate.
		/// </summary>
		/// <param name="sampleRate">The sample rate.</param>
		/// <returns>
		/// 	<c>true</c> if this instance [can sample rate] the specified sample rate; otherwise, <c>false</c>.
		/// </returns>
		public bool canSampleRate(double sampleRate)
		{
			ASIOError error = asioDriverVTable.canSampleRate(pASIOComObject, sampleRate);
			if (error == ASIOError.ASE_NoClock)
			{
				return false;
			}
			if (error == ASIOError.ASE_OK)
			{
				return true;
			}
			handleException(error, "canSampleRate");
			return false;
		}

		/// <summary>
		/// Gets the sample rate.
		/// </summary>
		/// <returns></returns>
		public double getSampleRate()
		{
			double sampleRate;
			handleException(asioDriverVTable.getSampleRate(pASIOComObject, out sampleRate), "getSampleRate");
			return sampleRate;
		}

		/// <summary>
		/// Sets the sample rate.
		/// </summary>
		/// <param name="sampleRate">The sample rate.</param>
		public void setSampleRate(double sampleRate)
		{
			handleException(asioDriverVTable.setSampleRate(pASIOComObject, sampleRate), "setSampleRate");
		}

		/// <summary>
		/// Gets the clock sources.
		/// </summary>
		/// <param name="clocks">The clocks.</param>
		/// <param name="numSources">The num sources.</param>
		public void getClockSources(out long clocks, int numSources)
		{
			handleException(asioDriverVTable.getClockSources(pASIOComObject, out clocks, numSources), "getClockSources");
		}

		/// <summary>
		/// Sets the clock source.
		/// </summary>
		/// <param name="reference">The reference.</param>
		public void setClockSource(int reference)
		{
			handleException(asioDriverVTable.setClockSource(pASIOComObject, reference), "setClockSources");
		}

		/// <summary>
		/// Gets the sample position.
		/// </summary>
		/// <param name="samplePos">The sample pos.</param>
		/// <param name="timeStamp">The time stamp.</param>
		public void getSamplePosition(out long samplePos, ref ASIO64Bit timeStamp)
		{
			handleException(asioDriverVTable.getSamplePosition(pASIOComObject, out samplePos, ref timeStamp), "getSamplePosition");
		}

		/// <summary>
		/// Gets the channel info.
		/// </summary>
		/// <param name="channelNumber">The channel number.</param>
		/// <param name="trueForInputInfo">if set to <c>true</c> [true for input info].</param>
		/// <returns></returns>
		public ASIOChannelInfo getChannelInfo(int channelNumber, bool trueForInputInfo)
		{
			var info = new ASIOChannelInfo {channel = channelNumber, isInput = trueForInputInfo};
			handleException(asioDriverVTable.getChannelInfo(pASIOComObject, ref info), "getChannelInfo");
			return info;
		}

		/// <summary>
		/// Creates the buffers.
		/// </summary>
		/// <param name="bufferInfos">The buffer infos.</param>
		/// <param name="numChannels">The num channels.</param>
		/// <param name="bufferSize">Size of the buffer.</param>
		/// <param name="callbacks">The callbacks.</param>
		public void createBuffers(IntPtr bufferInfos, int numChannels, int bufferSize, ref ASIOCallbacks callbacks)
		{
			// next two lines suggested by droidi on codeplex issue tracker
			pinnedcallbacks = Marshal.AllocHGlobal(Marshal.SizeOf(callbacks));
			Marshal.StructureToPtr(callbacks, pinnedcallbacks, false);
			handleException(asioDriverVTable.createBuffers(pASIOComObject, bufferInfos, numChannels, bufferSize, ref callbacks),
			                "createBuffers");
		}

		/// <summary>
		/// Disposes the buffers.
		/// </summary>
		public ASIOError disposeBuffers()
		{
			ASIOError result = asioDriverVTable.disposeBuffers(pASIOComObject);
			Marshal.FreeHGlobal(pinnedcallbacks);
			return result;
		}

		/// <summary>
		/// Controls the panel.
		/// </summary>
		public void controlPanel()
		{
			handleException(asioDriverVTable.controlPanel(pASIOComObject), "controlPanel");
		}

		/// <summary>
		/// Futures the specified selector.
		/// </summary>
		/// <param name="selector">The selector.</param>
		/// <param name="opt">The opt.</param>
		public void future(int selector, IntPtr opt)
		{
			handleException(asioDriverVTable.future(pASIOComObject, selector, opt), "future");
		}

		/// <summary>
		/// Notifies OutputReady to the ASIODriver.
		/// </summary>
		/// <returns></returns>
		public ASIOError outputReady()
		{
			return asioDriverVTable.outputReady(pASIOComObject);
		}

		/// <summary>
		/// Releases this instance.
		/// </summary>
		public void ReleaseComASIODriver()
		{
			Marshal.Release(pASIOComObject);
		}

		/// <summary>
		/// Handles the exception. Throws an exception based on the error.
		/// </summary>
		/// <param name="error">The error to check.</param>
		/// <param name="methodName">Method name</param>
		private void handleException(ASIOError error, String methodName)
		{
			if (error != ASIOError.ASE_OK && error != ASIOError.ASE_SUCCESS)
			{
				var asioException =
					new ASIOException(String.Format("Error code [{0}] while calling ASIO method <{1}>",
					                                ASIOException.getErrorName(error), methodName));
				asioException.Error = error;
				throw asioException;
			}
		}

		/// <summary>
		/// Inits the vTable method from GUID. This is a tricky part of this class.
		/// </summary>
		/// <param name="ASIOGuid">The ASIO GUID.</param>
		private void initFromGuid(Guid ASIOGuid)
		{
			const uint CLSCTX_INPROC_SERVER = 1;
			// Start to query the virtual table a index 3 (init method of ASIODriver)
			const int INDEX_VTABLE_FIRST_METHOD = 3;

			// Pointer to the ASIO object
			// USE CoCreateInstance instead of builtin COM-Class instantiation,
			// because the ASIODriver expect to have the ASIOGuid used for both COM Object and COM interface
			// The CoCreateInstance is working only in STAThread mode.
			int hresult = CoCreateInstance(ref ASIOGuid, IntPtr.Zero, CLSCTX_INPROC_SERVER, ref ASIOGuid, out pASIOComObject);
			if (hresult != 0)
			{
				throw new COMException("Unable to instantiate ASIO. Check if STAThread is set", hresult);
			}

			// The first pointer at the adress of the ASIO Com Object is a pointer to the
			// C++ Virtual table of the object.
			// Gets a pointer to VTable.
			IntPtr pVtable = Marshal.ReadIntPtr(pASIOComObject);

			// Instantiate our Virtual table mapping
			asioDriverVTable = new ASIODriverVTable();

			// This loop is going to retrieve the pointer from the C++ VirtualTable
			// and attach an internal delegate in order to call the method on the COM Object.
			FieldInfo[] fieldInfos = typeof (ASIODriverVTable).GetFields();
			for (int i = 0; i < fieldInfos.Length; i++)
			{
				FieldInfo fieldInfo = fieldInfos[i];
				// Read the method pointer from the VTable
				IntPtr pPointerToMethodInVTable = Marshal.ReadIntPtr(pVtable, (i + INDEX_VTABLE_FIRST_METHOD)*IntPtr.Size);
				// Instantiate a delegate
				object methodDelegate = Marshal.GetDelegateForFunctionPointer(pPointerToMethodInVTable, fieldInfo.FieldType);
				// Store the delegate in our C# VTable
				fieldInfo.SetValue(asioDriverVTable, methodDelegate);
			}
		}

		[DllImport("ole32.Dll")]
		private static extern int CoCreateInstance(ref Guid clsid,
		                                           IntPtr inner,
		                                           uint context,
		                                           ref Guid uuid,
		                                           out IntPtr rReturnedComObject);

		#region Nested type: ASIODriverVTable

		/// <summary>
		/// Internal VTable structure to store all the delegates to the C++ COM method.
		/// </summary>
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		private class ASIODriverVTable
		{
			//3  virtual ASIOBool init(void *sysHandle) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate bool ASIOInit(IntPtr _pUnknown, IntPtr sysHandle);

			public readonly ASIOInit init;
			//4  virtual void getDriverName(char *name) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate void ASIOgetDriverName(IntPtr _pUnknown, StringBuilder name);

			public readonly ASIOgetDriverName getDriverName;
			//5  virtual long getDriverVersion() = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate int ASIOgetDriverVersion(IntPtr _pUnknown);

			public readonly ASIOgetDriverVersion getDriverVersion;
			//6  virtual void getErrorMessage(char *string) = 0;	
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate void ASIOgetErrorMessage(IntPtr _pUnknown, StringBuilder errorMessage);

			public readonly ASIOgetErrorMessage getErrorMessage;
			//7  virtual ASIOError start() = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOstart(IntPtr _pUnknown);

			public readonly ASIOstart start;
			//8  virtual ASIOError stop() = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOstop(IntPtr _pUnknown);

			public readonly ASIOstop stop;
			//9  virtual ASIOError getChannels(long *numInputChannels, long *numOutputChannels) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetChannels(IntPtr _pUnknown, out int numInputChannels, out int numOutputChannels);

			public readonly ASIOgetChannels getChannels;
			//10  virtual ASIOError getLatencies(long *inputLatency, long *outputLatency) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetLatencies(IntPtr _pUnknown, out int inputLatency, out int outputLatency);

			public readonly ASIOgetLatencies getLatencies;
			//11 virtual ASIOError getBufferSize(long *minSize, long *maxSize, long *preferredSize, long *granularity) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetBufferSize(
				IntPtr _pUnknown, out int minSize, out int maxSize, out int preferredSize, out int granularity);

			public readonly ASIOgetBufferSize getBufferSize;
			//12 virtual ASIOError canSampleRate(ASIOSampleRate sampleRate) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOcanSampleRate(IntPtr _pUnknown, double sampleRate);

			public readonly ASIOcanSampleRate canSampleRate;
			//13 virtual ASIOError getSampleRate(ASIOSampleRate *sampleRate) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetSampleRate(IntPtr _pUnknown, out double sampleRate);

			public readonly ASIOgetSampleRate getSampleRate;
			//14 virtual ASIOError setSampleRate(ASIOSampleRate sampleRate) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOsetSampleRate(IntPtr _pUnknown, double sampleRate);

			public readonly ASIOsetSampleRate setSampleRate;
			//15 virtual ASIOError getClockSources(ASIOClockSource *clocks, long *numSources) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetClockSources(IntPtr _pUnknown, out long clocks, int numSources);

			public readonly ASIOgetClockSources getClockSources;
			//16 virtual ASIOError setClockSource(long reference) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOsetClockSource(IntPtr _pUnknown, int reference);

			public readonly ASIOsetClockSource setClockSource;
			//17 virtual ASIOError getSamplePosition(ASIOSamples *sPos, ASIOTimeStamp *tStamp) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetSamplePosition(IntPtr _pUnknown, out long samplePos, ref ASIO64Bit timeStamp);

			public readonly ASIOgetSamplePosition getSamplePosition;
			//18 virtual ASIOError getChannelInfo(ASIOChannelInfo *info) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOgetChannelInfo(IntPtr _pUnknown, ref ASIOChannelInfo info);

			public readonly ASIOgetChannelInfo getChannelInfo;
			//19 virtual ASIOError createBuffers(ASIOBufferInfo *bufferInfos, long numChannels, long bufferSize, ASIOCallbacks *callbacks) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			//            public delegate ASIOError ASIOcreateBuffers(IntPtr _pUnknown, ref ASIOBufferInfo[] bufferInfos, int numChannels, int bufferSize, ref ASIOCallbacks callbacks);
			public delegate ASIOError ASIOcreateBuffers(
				IntPtr _pUnknown, IntPtr bufferInfos, int numChannels, int bufferSize, ref ASIOCallbacks callbacks);

			public readonly ASIOcreateBuffers createBuffers;
			//20 virtual ASIOError disposeBuffers() = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOdisposeBuffers(IntPtr _pUnknown);

			public readonly ASIOdisposeBuffers disposeBuffers;
			//21 virtual ASIOError controlPanel() = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOcontrolPanel(IntPtr _pUnknown);

			public readonly ASIOcontrolPanel controlPanel;
			//22 virtual ASIOError future(long selector,void *opt) = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOfuture(IntPtr _pUnknown, int selector, IntPtr opt);

			public readonly ASIOfuture future;
			//23 virtual ASIOError outputReady() = 0;
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate ASIOError ASIOoutputReady(IntPtr _pUnknown);

			public readonly ASIOoutputReady outputReady;
		}

		#endregion
	}
}