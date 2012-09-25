using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	/// <summary>
	/// Audio Endpoint Volume
	/// </summary>
	public class AudioEndpointVolume : IDisposable
	{
		private readonly IAudioEndpointVolume _AudioEndPointVolume;
		private readonly AudioEndpointVolumeChannels _Channels;
		private readonly EEndpointHardwareSupport _HardwareSupport;
		private readonly AudioEndpointVolumeStepInformation _StepInformation;
		private readonly AudioEndpointVolumeVolumeRange _VolumeRange;
		private AudioEndpointVolumeCallback _CallBack;

		/// <summary>
		/// Creates a new Audio endpoint volume
		/// </summary>
		/// <param name="realEndpointVolume">IAudioEndpointVolume COM interface</param>
		internal AudioEndpointVolume(IAudioEndpointVolume realEndpointVolume)
		{
			uint HardwareSupp;

			_AudioEndPointVolume = realEndpointVolume;
			_Channels = new AudioEndpointVolumeChannels(_AudioEndPointVolume);
			_StepInformation = new AudioEndpointVolumeStepInformation(_AudioEndPointVolume);
			Marshal.ThrowExceptionForHR(_AudioEndPointVolume.QueryHardwareSupport(out HardwareSupp));
			_HardwareSupport = (EEndpointHardwareSupport) HardwareSupp;
			_VolumeRange = new AudioEndpointVolumeVolumeRange(_AudioEndPointVolume);
			_CallBack = new AudioEndpointVolumeCallback(this);
			Marshal.ThrowExceptionForHR(_AudioEndPointVolume.RegisterControlChangeNotify(_CallBack));
		}

		/// <summary>
		/// Volume Range
		/// </summary>
		public AudioEndpointVolumeVolumeRange VolumeRange
		{
			get { return _VolumeRange; }
		}

		/// <summary>
		/// Hardware Support
		/// </summary>
		public EEndpointHardwareSupport HardwareSupport
		{
			get { return _HardwareSupport; }
		}

		/// <summary>
		/// Step Information
		/// </summary>
		public AudioEndpointVolumeStepInformation StepInformation
		{
			get { return _StepInformation; }
		}

		/// <summary>
		/// Channels
		/// </summary>
		public AudioEndpointVolumeChannels Channels
		{
			get { return _Channels; }
		}

		/// <summary>
		/// Master Volume Level
		/// </summary>
		public float MasterVolumeLevel
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume.GetMasterVolumeLevel(out result));
				return result;
			}
			set { Marshal.ThrowExceptionForHR(_AudioEndPointVolume.SetMasterVolumeLevel(value, Guid.Empty)); }
		}

		/// <summary>
		/// Master Volume Level Scalar
		/// </summary>
		public float MasterVolumeLevelScalar
		{
			get
			{
				float result;
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume.GetMasterVolumeLevelScalar(out result));
				return result;
			}
			set { Marshal.ThrowExceptionForHR(_AudioEndPointVolume.SetMasterVolumeLevelScalar(value, Guid.Empty)); }
		}

		/// <summary>
		/// Mute
		/// </summary>
		public bool Mute
		{
			get
			{
				bool result;
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume.GetMute(out result));
				return result;
			}
			set { Marshal.ThrowExceptionForHR(_AudioEndPointVolume.SetMute(value, Guid.Empty)); }
		}

		#region IDisposable Members

		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			if (_CallBack != null)
			{
				Marshal.ThrowExceptionForHR(_AudioEndPointVolume.UnregisterControlChangeNotify(_CallBack));
				_CallBack = null;
			}
			GC.SuppressFinalize(this);
		}

		#endregion

		/// <summary>
		/// On Volume Notification
		/// </summary>
		public event AudioEndpointVolumeNotificationDelegate OnVolumeNotification;

		/// <summary>
		/// Volume Step Up
		/// </summary>
		public void VolumeStepUp()
		{
			Marshal.ThrowExceptionForHR(_AudioEndPointVolume.VolumeStepUp(Guid.Empty));
		}

		/// <summary>
		/// Volume Step Down
		/// </summary>
		public void VolumeStepDown()
		{
			Marshal.ThrowExceptionForHR(_AudioEndPointVolume.VolumeStepDown(Guid.Empty));
		}

		internal void FireNotification(AudioVolumeNotificationData NotificationData)
		{
			AudioEndpointVolumeNotificationDelegate del = OnVolumeNotification;
			if (del != null)
			{
				del(NotificationData);
			}
		}

		/// <summary>
		/// Finalizer
		/// </summary>
		~AudioEndpointVolume()
		{
			Dispose();
		}
	}
}