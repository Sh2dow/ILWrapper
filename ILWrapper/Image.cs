using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ILWrapper.Enums;
using ILWrapper.Native;



namespace ILWrapper.Drawing
{
	public sealed class Image : IDisposable, IEquatable<Image>
	{
		private bool _disposed = false;
		private uint _id;
		private byte[] _buffer;

		private ImageType _image_type;
		private ImageFormat _format;
		private CompressedDataFormat _dxtc;
		private PrimitiveType _data_type;
		private PaletteType _pal_type;
		private ImageFormat _pal_format;
		private OriginLocation _origin;
		private CubeMapOrientation _cubemap;
		private int _width;
		private int _height;
		private int _depth;
		private int _bytespp;
		private int _bitspp;
		private int _channel;
		private int _pal_bytespp;
		private int _pal_columns;
		private int _face_count;
		private int _array;
		private int _mipmaps;
		private int _layers;

		public int Size => this._buffer?.Length ?? -1;
		public ImageType ImageType => this._image_type;
		public ImageFormat Format => this._format;
		public CompressedDataFormat DXTCFormat => this._dxtc;
		public PrimitiveType DataType => this._data_type;
		public PaletteType PaletteType => this._pal_type;
		public ImageFormat PaletteFormat => this._pal_format;
		public OriginLocation Origin => this._origin;
		public int Width => this._width;
		public int Height => this._height;
		public int Depth => this._depth;
		public int BytesPerPixel => this._bytespp;
		public int BitsPerPixel => this._bitspp;
		public int ChannelCount => this._channel;
		public int PaletteBytesPerPixel => this._pal_bytespp;
		public int PaletteColumnCount => this._pal_columns;
		public int FaceCount => this._face_count;
		public int ImageArrayCount => this._array;
		public int MipMapCount => this._mipmaps;
		public int LayerCount => _layers;
		public bool HasDXTCData => this._dxtc != CompressedDataFormat.NONE;
		public bool HasPaletteData => this._pal_type != PaletteType.NONE;
		public bool IsCubeMap => this._cubemap != CubeMapOrientation.None && this._cubemap != CubeMapOrientation.Sphere;
		public bool IsSphereMap => this._cubemap == CubeMapOrientation.Sphere;

		public static Image Default => new Image();

		public Image() { }
		public Image(string file) => this.Load(file);
		internal Image(uint id) => this._id = id;
		~Image()
		{
			this.Dispose(false);
		}

		private void Initialize()
		{
			this._id = IL.GenerateImage();
			IL.BindImage(this._id);
		}
		private void Release()
		{
			IL.DeleteImage(this._id);
			IL.ShutDown();
		}

		public bool Load(string file)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!File.Exists(file)) throw new FileNotFoundException($"File {file} does not exist");
			var buffer = File.ReadAllBytes(file);

			return this.LoadSettings(buffer);
		}

		public bool Load(Stream stream)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!stream.CanRead) throw new IOException("Stream passed cannot be used to read data");

			var count = (int)(stream.Length - stream.Position);
			var buffer = new byte[count];
			stream.Read(buffer, 0, count);

			return this.LoadSettings(buffer);
		}

		public bool Load(Stream stream, int length)
		{


			return false;
		}

		public bool Load(Stream stream, int position, int length)
		{


			return true;
		}

		public bool Load(byte[] buffer)
		{
			var array = new byte[buffer.Length];
			Array.Copy(buffer, 0, array, 0, buffer.Length);
			return this.LoadSettings(array);
		}

		public bool Load(byte[] buffer, int position, int count)
		{
			if (buffer == null || buffer.Length == 0)
			{

				throw new Exception("Buffer cannot be null or empty");

			}

			if (position < 0 || position >= buffer.Length)
			{
			
				throw new Exception("Position should be a non-negative value and less than buffer length");
			
			}

			if (count <= 0 || position + count > buffer.Length)
			{

				throw new Exception("Count should be a non-negative value and not exceed buffer length");

			}

			var array = new byte[count];
			Array.Copy(buffer, position, array, 0, count);
			return this.LoadSettings(array);
		}

		private bool LoadSettings(byte[] buffer)
		{
			this._buffer = buffer;
			this.Initialize();

			unsafe
			{

				fixed (byte* ptr = &this._buffer[0])
				{

					var intptr_t = new IntPtr(ptr);
					this._image_type = (ImageType)IL.DetermineType(intptr_t, (uint)this.Size);
					if (!IL.Load((uint)this.ImageType, intptr_t, (uint)this.Size))
					{

						return false;

					}

				}

			}

			this.GetImageInfo();

			return false;
		}

		private void GetImageInfo()
		{
			this.GetFormat();
			this.GetDXTC();
			this.GetDataType();
			this.GetPalType();
			this.GetPalFormat();
			this.GetOrigin();
			this.GetWidth();
			this.GetHeight();
			this.GetDepth();
			this.GetBytesPP();
			this.GetBitsPP();
			this.GetChannel();
			this.GetPalBytesPP();
			this.GetPalColumns();
			this.GetFaces();
			this.GetArray();
			this.GetMipMaps();
			this.GetLayers();
			this.GetCubeMap();
		}
		private void GetFormat() => this._format = (ImageFormat)IL.GetInteger((uint)ILInteger.ImageFormat);
		private void GetDXTC() => this._dxtc = (CompressedDataFormat)IL.GetInteger((uint)ILInteger.DXTCFormat);
		private void GetDataType() => this._data_type = (PrimitiveType)IL.GetInteger((uint)ILInteger.ImageType);
		private void GetPalType() => this._pal_type = (PaletteType)IL.GetInteger((uint)ILInteger.PaletteType);
		private void GetPalFormat() => this._pal_format = (ImageFormat)IL.GetInteger((uint)ILInteger.PaletteFormat);
		private void GetOrigin() => this._origin = (OriginLocation)IL.GetInteger((uint)ILInteger.ImageOrigin);
		private void GetWidth() => this._width = IL.GetInteger((uint)ILInteger.ImageWidth);
		private void GetHeight() => this._height = IL.GetInteger((uint)ILInteger.ImageHeight);
		private void GetDepth() => this._depth = IL.GetInteger((uint)ILInteger.ImageDepth);
		private void GetBytesPP() => this._bytespp = IL.GetInteger((uint)ILInteger.ImageBytesPerPixel);
		private void GetBitsPP() => this._bitspp = IL.GetInteger((uint)ILInteger.ImageBitsPerPixel);
		private void GetChannel() => this._channel = IL.GetInteger((uint)ILInteger.ImageChannels);
		private void GetPalBytesPP() => this._pal_bytespp = IL.GetInteger((uint)ILInteger.ImagePaletteBytesPerPixel);
		private void GetPalColumns() => this._pal_columns = IL.GetInteger((uint)ILInteger.ImagePaletteColumnCount);
		private void GetFaces() => this._face_count = IL.GetInteger((uint)ILInteger.ImageFaceCount) + 1;
		private void GetArray() => this._array = IL.GetInteger((uint)ILInteger.ImageArrayCount) + 1;
		private void GetMipMaps() => this._mipmaps = IL.GetInteger((uint)ILInteger.ImageMipMapCount) + 1;
		private void GetLayers() => this._layers = IL.GetInteger((uint)ILInteger.ImageLayerCount) + 1;
		private void GetCubeMap() => this._cubemap = (CubeMapOrientation)IL.GetInteger((uint)ILInteger.CubeFlags);


		public static bool IsValid(Image image)
		{
			return !(image is null) && image._id != 0;
		}

		public bool Equals(Image other)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public override string ToString()
		{
			return String.Empty;
		}

		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			
		}
	}
}
