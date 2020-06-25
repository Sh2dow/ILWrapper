namespace ILWrapper.Enums
{
	/// <summary>
	/// Type of palette in compressed image types.
	/// </summary>
	public enum PaletteType
	{
		/// <summary>
		/// No palette used: image is decompressed.
		/// </summary>
		NONE = 0x400,

		/// <summary>
		/// Palette of type RGB24 is used.
		/// </summary>
		RGB24 = 0x401,

		/// <summary>
		/// Palette of type RGB32 is used.
		/// </summary>
		RGB32 = 0x402,

		/// <summary>
		/// Palette of type RGBA32 is used.
		/// </summary>
		RGBA32 = 0x403,

		/// <summary>
		/// Palette of type BGR24 is used.
		/// </summary>
		BGR24 = 0x404,

		/// <summary>
		/// Palette of type BGR32 is used.
		/// </summary>
		BGR32 = 0x405,

		/// <summary>
		/// Palette of type BGRA32 is used.
		/// </summary>
		BGRA32 = 0x406,
	}
}
