namespace ILWrapper.Enums
{
	/// <summary>
	/// Type of utilizing memory when managing images.
	/// </summary>
	public enum MemoryType
	{
		/// <summary>
		/// Utilize the fastest speed in cost of memory.
		/// </summary>
		Speed = 0x660,

		/// <summary>
		/// Utilize the least amount of memory in cost of speed.
		/// </summary>
		Memory = 0x661,

		/// <summary>
		/// Utilize based on processor priority.
		/// </summary>
		Any = 0x662,
	}
}
