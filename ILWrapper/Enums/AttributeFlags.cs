using System;



namespace ILWrapper.Enums
{
	/// <summary>
	/// 
	/// </summary>
	[Flags]
	public enum AttributeFlags
	{
		/// <summary>
		/// 
		/// </summary>
		Origin = 0x01,
		
		/// <summary>
		/// 
		/// </summary>
		File = 0x02,

		/// <summary>
		/// 
		/// </summary>
		Palette = 0x04,

		/// <summary>
		/// 
		/// </summary>
		Format = 0x08,

		/// <summary>
		/// 
		/// </summary>
		Type = 0x10,

		/// <summary>
		/// 
		/// </summary>
		Compress = 0x20,
		
		/// <summary>
		/// 
		/// </summary>
		LoadFail = 0x40,

		/// <summary>
		/// 
		/// </summary>
		FormatSpecific = 0x80,
		
		/// <summary>
		/// 
		/// </summary>
		All = 0xFFFFF
	}
}