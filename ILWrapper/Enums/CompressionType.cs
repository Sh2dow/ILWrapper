namespace ILWrapper.Enums
{
	/// <summary>
	/// Type of compression applied to an image.
	/// </summary>
	public enum CompressionType
	{
		/// <summary>
		/// Use compressed based on processor priorities.
		/// </summary>
		Any = 0x662,

		/// <summary>
		/// Use compression.
		/// </summary>
		Use = 0x666,

		/// <summary>
		/// Do not use compression.
		/// </summary>
		None = 0x667,
	}
}
