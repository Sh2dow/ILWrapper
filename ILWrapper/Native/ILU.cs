using System;
using System.Runtime.InteropServices;



namespace ILWrapper.Native
{
	internal static class ILU
	{
		public const string Lib = "ILU.dll";

		#region Main

		[DllImport(Lib, EntryPoint = "iluAlienify", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Alienify();

		[DllImport(Lib, EntryPoint = "iluBuildMipmaps", CallingConvention = CallingConvention.StdCall)]
		public static extern bool BuildMipMaps();

		[DllImport(Lib, EntryPoint = "iluCompareImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool CompareImages(uint otherImage);

		[DllImport(Lib, EntryPoint = "iluConvolution", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Convolution(int[] matrix, int scale, int bias);

		[DllImport(Lib, EntryPoint = "iluCrop", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Crop(uint offsetX, uint offsetY, uint offsetZ, uint width, uint height, uint depth);

		[DllImport(Lib, EntryPoint = "iluImageParameter", CallingConvention = CallingConvention.StdCall)]
		public static extern void ImageParameter(uint pName, uint param);

		[DllImport(Lib, EntryPoint = "iluInit", CallingConvention = CallingConvention.StdCall)]
		public static extern void Init();

		[DllImport(Lib, EntryPoint = "iluSetLanguage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool SetLanguage(uint language);

		[DllImport(Lib, EntryPoint = "iluWave", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Wave(float angle);

		#endregion

		#region Color

		[DllImport(Lib, EntryPoint = "iluBlurAvg", CallingConvention = CallingConvention.StdCall)]
		public static extern bool BlurAverage(uint iterations);

		[DllImport(Lib, EntryPoint = "iluBlurGaussian", CallingConvention = CallingConvention.StdCall)]
		public static extern bool BlurGaussian(uint iterations);

		[DllImport(Lib, EntryPoint = "iluColoursUsed", CallingConvention = CallingConvention.StdCall)]
		public static extern uint ColorsUsed();

		[DllImport(Lib, EntryPoint = "iluContrast", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Contrast(float contrast);

		[DllImport(Lib, EntryPoint = "iluEmboss", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Emboss();

		[DllImport(Lib, EntryPoint = "iluEqualize", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Equalize();

		[DllImport(Lib, EntryPoint = "iluGammaCorrect", CallingConvention = CallingConvention.StdCall)]
		public static extern bool GammaCorrect(float gamma);

		[DllImport(Lib, EntryPoint = "iluNoisify", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Noisify(float tolerance);

		[DllImport(Lib, EntryPoint = "iluPixelize", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Pixelize(uint pixelSize);

		[DllImport(Lib, EntryPoint = "iluReplaceColour", CallingConvention = CallingConvention.StdCall)]
		public static extern bool ReplaceColor(byte red, byte green, byte blue, float tolerance);

		[DllImport(Lib, EntryPoint = "iluSaturate1f", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Saturate(float saturation);

		[DllImport(Lib, EntryPoint = "iluSaturate4f", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Saturate(float red, float green, float blue, float saturation);

		[DllImport(Lib, EntryPoint = "iluSharpen", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Sharpen(float factor, uint iterations);

		[DllImport(Lib, EntryPoint = "iluSwapColours", CallingConvention = CallingConvention.StdCall)]
		public static extern bool SwapColors();

		#endregion

		#region Edge

		[DllImport(Lib, EntryPoint = "iluEdgeDetectE", CallingConvention = CallingConvention.StdCall)]
		public static extern bool EdgeDetectE();

		[DllImport(Lib, EntryPoint = "iluEdgeDetectP", CallingConvention = CallingConvention.StdCall)]
		public static extern bool EdgeDetectP();

		[DllImport(Lib, EntryPoint = "iluEdgeDetectS", CallingConvention = CallingConvention.StdCall)]
		public static extern bool EdgeDetectS();

		#endregion

		#region Get

		[DllImport(Lib, EntryPoint = "iluErrorString", CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr GetErrorString(uint error);

		[DllImport(Lib, EntryPoint = "iluGetString", CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr GetString(uint name);

		[DllImport(Lib, EntryPoint = "iluGetInteger", CallingConvention = CallingConvention.StdCall)]
		public static extern int GetInteger(uint mode);

		#endregion

		#region Reflect

		[DllImport(Lib, EntryPoint = "iluFlipImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool FlipImage();

		[DllImport(Lib, EntryPoint = "iluInvertAlpha", CallingConvention = CallingConvention.StdCall)]
		public static extern bool InvertAlpha();

		[DllImport(Lib, EntryPoint = "iluMirror", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Mirror();

		[DllImport(Lib, EntryPoint = "iluNegative", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Negative();

		#endregion

		#region Rotate

		[DllImport(Lib, EntryPoint = "iluRegionfv", CallingConvention = CallingConvention.StdCall)]
		public static extern void RegionF(PointF[] points, uint num);

		[DllImport(Lib, EntryPoint = "iluRegioniv", CallingConvention = CallingConvention.StdCall)]
		public static extern void RegionI(Point[] points, uint num);

		[DllImport(Lib, EntryPoint = "iluRotate", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Rotate(float angle);

		[DllImport(Lib, EntryPoint = "iluRotate3D", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Rotate3D(float x, float y, float z, float angle);

		#endregion

		#region Scale

		[DllImport(Lib, EntryPoint = "iluEnlargeCanvas", CallingConvention = CallingConvention.StdCall)]
		public static extern bool EnlargeCanvas(uint width, uint height, uint depth);

		[DllImport(Lib, EntryPoint = "iluEnlargeImage", CallingConvention = CallingConvention.StdCall)]
		public static extern bool EnlargeImage(uint xDim, uint yDim, uint zDim);

		[DllImport(Lib, EntryPoint = "iluScale", CallingConvention = CallingConvention.StdCall)]
		public static extern bool Scale(uint width, uint height, uint depth);

		[DllImport(Lib, EntryPoint = "iluScaleAlpha", CallingConvention = CallingConvention.StdCall)]
		public static extern bool ScaleAlpha(float scale);

		[DllImport(Lib, EntryPoint = "iluScaleColours", CallingConvention = CallingConvention.StdCall)]
		public static extern bool ScaleColors(float red, float green, float blue);

		#endregion
	}
}
