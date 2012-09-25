namespace NAudio.Wave.Asio
{
	internal enum ASIOError
	{
		ASE_OK = 0, // This value will be returned whenever the call succeeded
		ASE_SUCCESS = 0x3f4847a0, // unique success return value for ASIOFuture calls
		ASE_NotPresent = -1000, // hardware input or output is not present or available
		ASE_HWMalfunction, // hardware is malfunctioning (can be returned by any ASIO function)
		ASE_InvalidParameter, // input parameter invalid
		ASE_InvalidMode, // hardware is in a bad mode or used in a bad mode
		ASE_SPNotAdvancing, // hardware is not running when sample position is inquired
		ASE_NoClock, // sample clock or rate cannot be determined or is not present
		ASE_NoMemory // not enough memory for completing the request
	}
}