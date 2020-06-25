namespace ILWrapper.Enums
{
	/// <summary>
	/// Type of compressor used on an image.
	/// </summary>
	public enum CompressorUsed
	{
		/// <summary>
		/// No compressor is used.
		/// </summary>
		NONE = 0x701,

		/// <summary>
		/// RLE compressior used.
		/// </summary>
		RLE = 0x702,

		/// <summary>
		/// ZLIB compressior used.
		/// </summary>
		ZLIB = 0x704,
	}
}
