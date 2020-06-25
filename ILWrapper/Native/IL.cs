using System;
using System.Runtime.InteropServices;



namespace ILWrapper.Native
{
	internal static class IL
	{
		private const string Lib = "DevIL.dll";

		#region Main

		[DllImport(Lib, EntryPoint = "ilDisable", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Disable(uint mode);

		[DllImport(Lib, EntryPoint = "ilEnable", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Enable(uint mode);

		[DllImport(Lib, EntryPoint = "ilHint", CallingConvention = CallingConvention.StdCall)]
		public static extern void Hint(uint target, uint mode);

		[DllImport(Lib, EntryPoint = "ilInit", CallingConvention = CallingConvention.StdCall)]
		public static extern void Init();

		[DllImport(Lib, EntryPoint = "ilPopAttrib", CallingConvention = CallingConvention.StdCall)]
		public static extern void PopAttribute();

		[DllImport(Lib, EntryPoint = "ilPushAttrib", CallingConvention = CallingConvention.StdCall)]
		public static extern void PushAttribute(uint attrib);

		[DllImport(Lib, EntryPoint = "ilShutDown", CallingConvention = CallingConvention.StdCall)]
		public static extern void ShutDown();

		#endregion

		#region Is

		[DllImport(Lib, EntryPoint = "ilIsDisabled", CallingConvention = CallingConvention.StdCall)]
		public static extern bool IsDisabled(uint mode);

		[DllImport(Lib, EntryPoint = "ilIsEnabled", CallingConvention = CallingConvention.StdCall)]
		public static extern bool IsEnabled(uint mode);

		[DllImport(Lib, EntryPoint = "ilIsValid", CallingConvention = CallingConvention.StdCall)]
		public static extern bool IsValid(uint type, [In][MarshalAs(UnmanagedType.LPStr)] string file);

		[DllImport(Lib, EntryPoint = "ilIsValidL", CallingConvention = CallingConvention.StdCall)]
		public static extern bool IsValid(uint type, IntPtr ptr, uint size);

		#endregion

		#region DXTC

		[DllImport(Lib, EntryPoint = "ilCompressDXT", CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr CompressDXT(IntPtr ptr, uint w, uint h, uint d, uint format, ref uint size);

		[DllImport(Lib, EntryPoint = "ilDxtcDataToImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool DXTCToImage();

		[DllImport(Lib, EntryPoint = "ilDxtcDataToSurface", CallingConvention = CallingConvention.StdCall)]
		public static extern bool DXTCToSurface();

		[DllImport(Lib, EntryPoint = "ilFlipSurfaceDxtcData", CallingConvention = CallingConvention.StdCall)]
		public static extern void FlipSurfaceDXTC();

		[DllImport(Lib, EntryPoint = "ilGetDXTCData", CallingConvention = CallingConvention.StdCall)]
		public static extern uint GetDXTCData(IntPtr ptr, uint size, uint format);

		[DllImport(Lib, EntryPoint = "ilInvertSurfaceDxtcDataAlpha", CallingConvention = CallingConvention.StdCall)]
		public static extern bool InvertSurfaceDXTCAlpha();

		#endregion

		#region Image

		[DllImport(Lib, EntryPoint = "ilBindImage", CallingConvention = CallingConvention.StdCall)]
		public static extern void BindImage(uint id);

		[DllImport(Lib, EntryPoint = "ilClearImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool ClearImage();

		[DllImport(Lib, EntryPoint = "ilCloneCurImage", CallingConvention = CallingConvention.StdCall)]
		public static extern uint CloneCurvedImage();

		[DllImport(Lib, EntryPoint = "ilConvertImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool ConvertImage(uint format, uint type);

		[DllImport(Lib, EntryPoint = "ilCopyImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool CopyImage(uint id);

		[DllImport(Lib, EntryPoint = "ilCreateSubImage", CallingConvention = CallingConvention.StdCall)]
		public static extern uint CreateSubImage(uint type, uint id);

		[DllImport(Lib, EntryPoint = "ilDeleteImage", CallingConvention = CallingConvention.StdCall)]
		public static extern void DeleteImage(uint id);

		[DllImport(Lib, EntryPoint = "ilDeleteImages", CallingConvention = CallingConvention.StdCall)]
		public static extern void DeleteImages(UIntPtr ptr, uint[] ids);

		[DllImport(Lib, EntryPoint = "ilGenImage", CallingConvention = CallingConvention.StdCall)]
		public static extern uint GenerateImage();

		[DllImport(Lib, EntryPoint = "ilGenImages", CallingConvention = CallingConvention.StdCall)]
		public static extern void GenerateImages(UIntPtr ptr, uint[] ids);

		[DllImport(Lib, EntryPoint = "ilActiveImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool GetActiveImage(uint id);

		[DllImport(Lib, EntryPoint = "ilDefaultImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool GetDefaultImage();

		[DllImport(Lib, EntryPoint = "ilImageToDxtcData", CallingConvention = CallingConvention.StdCall)]
		public static extern bool ImageToDXTC(uint format);

		[DllImport(Lib, EntryPoint = "ilIsImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool IsImage(uint id);

		[DllImport(Lib, EntryPoint = "ilLoadImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool LoadImage([In][MarshalAs(UnmanagedType.LPStr)] string file);

		[DllImport(Lib, EntryPoint = "ilOverlayImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool OverlayImage(uint id, int x, int y, int z);

		[DllImport(Lib, EntryPoint = "ilSaveImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool SaveImage([In][MarshalAs(UnmanagedType.LPStr)] string file);

		#endregion

		#region Function

		[DllImport(Lib, EntryPoint = "ilCompressFunc", CallingConvention = CallingConvention.StdCall)]
		public static extern bool CompressFunction(uint mode);

		[DllImport(Lib, EntryPoint = "ilFormatFunc", CallingConvention = CallingConvention.StdCall)]
		public static extern bool FormatFunction(uint mode);

		[DllImport(Lib, EntryPoint = "ilOriginFunc", CallingConvention = CallingConvention.StdCall)]
		public static extern bool OriginFunction(uint mode);

		[DllImport(Lib, EntryPoint = "ilTypeFunc", CallingConvention = CallingConvention.StdCall)]
		public static extern bool TypeFunction(uint mode);

		#endregion

		#region Palette

		[DllImport(Lib, EntryPoint = "ilApplyPal", CallingConvention = CallingConvention.StdCall)]
		public static extern bool ApplyPalette([In][MarshalAs(UnmanagedType.LPStr)] string file);

		[DllImport(Lib, EntryPoint = "ilConvertPal", CallingConvention = CallingConvention.StdCall)]
		public static extern bool ConvertPalette(uint format);

		[DllImport(Lib, EntryPoint = "ilGetPalette", CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr GetPalette();

		[DllImport(Lib, EntryPoint = "ilLoadPal", CallingConvention = CallingConvention.StdCall)]
		public static extern bool LoadPalette([In][MarshalAs(UnmanagedType.LPStr)] string file);

		[DllImport(Lib, EntryPoint = "ilSavePal", CallingConvention = CallingConvention.StdCall)]
		public static extern bool SavePalette([In][MarshalAs(UnmanagedType.LPStr)] string file);

		#endregion

		#region Modify

		[DllImport(Lib, EntryPoint = "ilApplyProfile", CallingConvention = CallingConvention.StdCall)]
		public static extern bool ApplyProfile(IntPtr @in, IntPtr @out);

		[DllImport(Lib, EntryPoint = "ilBlit", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Blit(uint id, int dx, int dy, int dz, uint sx, uint sy, uint sz, uint w, uint h, uint d);

		[DllImport(Lib, EntryPoint = "ilClampNTSC", CallingConvention = CallingConvention.StdCall)]
		public static extern bool ClampNTSC();

		[DllImport(Lib, EntryPoint = "ilClearColour", CallingConvention = CallingConvention.StdCall)]
		public static extern void ClearColor(float r, float g, float b, float a);

		[DllImport(Lib, EntryPoint = "ilCopyPixels", CallingConvention = CallingConvention.StdCall)]
		public static extern uint CopyPixels(uint x, uint y, uint z, uint w, uint h, uint d, uint format, uint type, IntPtr ptr);

		[DllImport(Lib, EntryPoint = "ilModAlpha", CallingConvention = CallingConvention.StdCall)]
		public static extern void ModulateAlpha(double alpha);

		[DllImport(Lib, EntryPoint = "ilSaveData", CallingConvention = CallingConvention.StdCall)]
		public static extern bool SaveRawData([In][MarshalAs(UnmanagedType.LPStr)] string file);

		#endregion

		#region Get

		[DllImport(Lib, EntryPoint = "ilActiveFace", CallingConvention = CallingConvention.StdCall)]
		public static extern bool GetActiveFace(uint id);

		[DllImport(Lib, EntryPoint = "ilActiveLayer", CallingConvention = CallingConvention.StdCall)]
		public static extern bool GetActiveLayer(uint id);

		[DllImport(Lib, EntryPoint = "ilActiveMipmap", CallingConvention = CallingConvention.StdCall)]
		public static extern bool GetActiveMipmap(uint id);

		[DllImport(Lib, EntryPoint = "ilGetAlpha", CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr GetAlpha(uint type);

		[DllImport(Lib, EntryPoint = "ilGetData", CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr GetData();

		[DllImport(Lib, EntryPoint = "ilGetError", CallingConvention = CallingConvention.StdCall)]
		public static extern uint GetError();

		[DllImport(Lib, EntryPoint = "ilGetInteger", CallingConvention = CallingConvention.StdCall)]
		internal static extern int GetInteger(uint mode);

		[DllImport(Lib, EntryPoint = "ilGetString", CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr GetString(uint value);

		#endregion

		#region Set

		[DllImport(Lib, EntryPoint = "ilSetAlpha", CallingConvention = CallingConvention.StdCall)]
		public static extern bool SetAlpha(double alpha);

		[DllImport(Lib, EntryPoint = "ilSetData", CallingConvention = CallingConvention.StdCall)]
		public static extern bool SetData(IntPtr ptr);

		[DllImport(Lib, EntryPoint = "ilSetDuration", CallingConvention = CallingConvention.StdCall)]
		public static extern bool SetDuration(uint duration);

		[DllImport(Lib, EntryPoint = "ilSetInteger", CallingConvention = CallingConvention.StdCall)]
		public static extern void SetInteger(uint mode, int value);

		[DllImport(Lib, EntryPoint = "ilKeyColour", CallingConvention = CallingConvention.StdCall)]
		public static extern void SetKeyColor(float r, float g, float b, float a);

		[DllImport(Lib, EntryPoint = "ilSetPixels", CallingConvention = CallingConvention.StdCall)]
		public static extern void SetPixels(int x, int y, int z, uint w, uint h, uint d, uint format, uint type, IntPtr ptr);

		[DllImport(Lib, EntryPoint = "ilSetString", CallingConvention = CallingConvention.StdCall)]
		public static extern void SetString(uint mode, [In][MarshalAs(UnmanagedType.LPStr)] string value);

		#endregion

		#region Type

		[DllImport(Lib, EntryPoint = "ilDetermineType", CallingConvention = CallingConvention.StdCall)]
		public static extern uint DetermineType([In][MarshalAs(UnmanagedType.LPStr)] string file);

		[DllImport(Lib, EntryPoint = "ilDetermineTypeL", CallingConvention = CallingConvention.StdCall)]
		public static extern uint DetermineType(IntPtr ptr, uint size);

		[DllImport(Lib, EntryPoint = "ilLoad", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Load(uint type, [In][MarshalAs(UnmanagedType.LPStr)] string file);

		[DllImport(Lib, EntryPoint = "ilLoadL", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Load(uint type, IntPtr ptr, uint size);

		[DllImport(Lib, EntryPoint = "ilLoadData", CallingConvention = CallingConvention.StdCall)]
		public static extern bool LoadData([In][MarshalAs(UnmanagedType.LPStr)] string file, uint w, uint h, uint d, byte bpp);

		[DllImport(Lib, EntryPoint = "ilLoadDataL", CallingConvention = CallingConvention.StdCall)]
		public static extern bool LoadData(IntPtr ptr, uint size, uint w, uint h, uint d, byte bpp);

		[DllImport(Lib, EntryPoint = "ilSave", CallingConvention = CallingConvention.StdCall)]
		public static extern bool SaveImage(uint type, [In][MarshalAs(UnmanagedType.LPStr)] string file);

		[DllImport(Lib, EntryPoint = "ilSaveL", CallingConvention = CallingConvention.StdCall)]
		public static extern uint SaveImage(uint type, IntPtr ptr, uint size);

		[DllImport(Lib, EntryPoint = "ilTypeFromExt", CallingConvention = CallingConvention.StdCall)]
		public static extern uint TypeFromExtension([In][MarshalAs(UnmanagedType.LPStr)] string file);

		#endregion

		#region Texture

		[DllImport(Lib, EntryPoint = "ilSurfaceToDxtcData", CallingConvention = CallingConvention.StdCall)]
		public static extern bool SurfaceToDXTC(uint format);

		[DllImport(Lib, EntryPoint = "ilTexImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool TextureImage(uint w, uint h, uint d, byte bpp, uint format, uint type, IntPtr ptr);

		[DllImport(Lib, EntryPoint = "ilTexImageDxtc", CallingConvention = CallingConvention.StdCall)]
		public static extern bool TextureImageDXTC(int w, int h, int d, uint format, IntPtr ptr);

		#endregion
	}
}
