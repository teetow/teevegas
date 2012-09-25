namespace NAudio.Wave.Asio
{
	internal enum ASIOMessageSelector
	{
		kAsioSelectorSupported = 1, // selector in <value>, returns 1L if supported,
		kAsioEngineVersion, // returns engine (host) asio implementation version,
		kAsioResetRequest, // request driver reset. if accepted, this
		kAsioBufferSizeChange, // not yet supported, will currently always return 0L.
		kAsioResyncRequest, // the driver went out of sync, such that
		kAsioLatenciesChanged, // the drivers latencies have changed. The engine
		kAsioSupportsTimeInfo, // if host returns true here, it will expect the
		kAsioSupportsTimeCode, // 
		kAsioMMCCommand, // unused - value: number of commands, message points to mmc commands
		kAsioSupportsInputMonitor, // kAsioSupportsXXX return 1 if host supports this
		kAsioSupportsInputGain, // unused and undefined
		kAsioSupportsInputMeter, // unused and undefined
		kAsioSupportsOutputGain, // unused and undefined
		kAsioSupportsOutputMeter, // unused and undefined
		kAsioOverload, // driver detected an overload
	}
}