namespace NAudio.Wave.Asio
{
	internal enum ASIOSampleType
	{
		ASIOSTInt16MSB = 0,
		ASIOSTInt24MSB = 1, // used for 20 bits as well
		ASIOSTInt32MSB = 2,
		ASIOSTFloat32MSB = 3, // IEEE 754 32 bit float
		ASIOSTFloat64MSB = 4, // IEEE 754 64 bit double float
		ASIOSTInt32MSB16 = 8, // 32 bit data with 16 bit alignment
		ASIOSTInt32MSB18 = 9, // 32 bit data with 18 bit alignment
		ASIOSTInt32MSB20 = 10, // 32 bit data with 20 bit alignment
		ASIOSTInt32MSB24 = 11, // 32 bit data with 24 bit alignment
		ASIOSTInt16LSB = 16,
		ASIOSTInt24LSB = 17, // used for 20 bits as well
		ASIOSTInt32LSB = 18,
		ASIOSTFloat32LSB = 19, // IEEE 754 32 bit float, as found on Intel x86 architecture
		ASIOSTFloat64LSB = 20, // IEEE 754 64 bit double float, as found on Intel x86 architecture
		ASIOSTInt32LSB16 = 24, // 32 bit data with 18 bit alignment
		ASIOSTInt32LSB18 = 25, // 32 bit data with 18 bit alignment
		ASIOSTInt32LSB20 = 26, // 32 bit data with 20 bit alignment
		ASIOSTInt32LSB24 = 27, // 32 bit data with 24 bit alignment
		ASIOSTDSDInt8LSB1 = 32, // DSD 1 bit data, 8 samples per byte. First sample in Least significant bit.
		ASIOSTDSDInt8MSB1 = 33, // DSD 1 bit data, 8 samples per byte. First sample in Most significant bit.
		ASIOSTDSDInt8NER8 = 40, // DSD 8 bit data, 1 sample per byte. No Endianness required.
	}
}