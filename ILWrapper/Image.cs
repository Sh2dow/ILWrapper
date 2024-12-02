using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ILWrapper.Enums;
using ILWrapper.Native;



namespace ILWrapper
{
	[DebuggerDisplay("ImageType: {ImageType} | Width: {Width} | Height: {Height}")]
	public sealed class Image
	{
		#region Fields

		private bool _disposed = false;
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

		#endregion

		#region Properties

		public static Image Default => new Image();
		public bool IsValid => this.Size > 0;
		public int Size => this._buffer?.Length ?? -1;
		public ImageType ImageType => this._image_type;
		public ImageFormat Format => this._format;
		public CompressedDataFormat DXTCFormat => this._dxtc;
		public PrimitiveType DataType => this._data_type;
		public PaletteType PaletteType => this._pal_type;
		public ImageFormat PaletteFormat => this._pal_format;
		public OriginLocation Origin => this._origin;
		public int Area => this.Width * this.Height;
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

		#endregion

		#region Main

		public Image() { }
		public Image(string file) => this.Load(file);
		public Image(byte[] buffer) => this.Load(buffer);
		public Image(Image image) => image.CopyTo(this);
		// public Image(System.Drawing.Image image)
		// {
		// 	using var ms = new MemoryStream((image.Width * image.Height) << 1);
		// 	image.Save(ms, image.RawFormat);
		// 	ms.Position = 0;
		// 	this.Load(ms);
		// }

		private uint Initialize()
		{
			var id = IL.GenerateImage();
			IL.BindImage(id);
			return id;
		}
		private void Release(uint id)
		{
			IL.DeleteImage(id);
			IL.ShutDown();
		}
		private ErrorType ImageToNative(uint id, bool gettype)
		{
			unsafe
			{

				fixed (byte* ptr = &this._buffer[0])
				{

					var intptr_t = new IntPtr(ptr);
					if (gettype) this._image_type = (ImageType)IL.DetermineType(intptr_t, (uint)this.Size);
					if (!IL.Load((uint)this._image_type, intptr_t, (uint)this.Size))
					{

						var error = (ErrorType)IL.GetError();
						return error;

					}
					else return ErrorType.NoError;
					
				}

			}
		}

		public byte[] GetBuffer()
		{
			if (this._buffer == null) return null;
			var result = new byte[this.Size];
			Array.Copy(this._buffer, result, this.Size);
			return result;
		}

		#endregion

		#region Load

		public void Load(string file)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!File.Exists(file)) throw new FileNotFoundException($"File {file} does not exist");
			var buffer = File.ReadAllBytes(file);

			this.LoadSettings(buffer);
		}

		public void Load(string file, ImageType type)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!File.Exists(file)) throw new FileNotFoundException($"File {file} does not exist");
			var buffer = File.ReadAllBytes(file);

			this.LoadSettings(buffer, type);
		}

		public void Load(Stream stream)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead) throw new IOException("Stream passed cannot be used to read data");

			var count = (int)(stream.Length - stream.Position);

			if (count == 0)
			{

				throw new ArgumentException("Stream position is set at the end, unable to read data");

			}

			var buffer = new byte[count];
			stream.Read(buffer, 0, count);

			this.LoadSettings(buffer);
		}

		public void Load(Stream stream, ImageType type)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead) throw new IOException("Stream passed cannot be used to read data");

			var count = (int)(stream.Length - stream.Position);

			if (count == 0)
			{

				throw new ArgumentException("Stream position is set at the end, unable to read data");

			}

			var buffer = new byte[count];
			stream.Read(buffer, 0, count);

			this.LoadSettings(buffer, type);
		}

		public void Load(Stream stream, int count)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead) throw new IOException("Stream passed cannot be used to read data");
			
			if (count <= 0 || stream.Position + count < stream.Length)
			{

				throw new Exception("Number of bytes read should be a non-negative value and not exceed buffer length");

			}

			var buffer = new byte[count];
			stream.Read(buffer, 0, count);

			this.LoadSettings(buffer);
		}

		public void Load(Stream stream, int count, ImageType type)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead) throw new IOException("Stream passed cannot be used to read data");

			if (count <= 0 || stream.Position + count < stream.Length)
			{

				throw new Exception("Number of bytes read should be a non-negative value and not exceed buffer length");

			}

			var buffer = new byte[count];
			stream.Read(buffer, 0, count);

			this.LoadSettings(buffer, type);
		}

		public void Load(byte[] buffer)
		{
			var array = new byte[buffer.Length];
			Array.Copy(buffer, 0, array, 0, buffer.Length);
			this.LoadSettings(array);
		}

		public void Load(byte[] buffer, ImageType type)
		{
			var array = new byte[buffer.Length];
			Array.Copy(buffer, 0, array, 0, buffer.Length);
			this.LoadSettings(array, type);
		}

		public void Load(byte[] buffer, int position, int count)
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

				throw new Exception("Number of bytes read should be a non-negative value and not exceed buffer length");

			}

			var array = new byte[count];
			Array.Copy(buffer, position, array, 0, count);
			this.LoadSettings(array);
		}

		public void Load(byte[] buffer, int position, int count, ImageType type)
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

				throw new Exception("Number of bytes read should be a non-negative value and not exceed buffer length");

			}

			var array = new byte[count];
			Array.Copy(buffer, position, array, 0, count);
			this.LoadSettings(array, type);
		}

		private void LoadSettings(byte[] buffer)
		{
			this._buffer = buffer;
			var id = this.Initialize();

			var error = this.ImageToNative(id, true);

			if (error != ErrorType.NoError)
			{

				this.Release(id);
				throw new ILWrapperException(error);

			}

			this.GetImageInfo();
			this.Release(id);
		}

		private void LoadSettings(byte[] buffer, ImageType type)
		{
			this._buffer = buffer;
			this._image_type = type;
			var id = this.Initialize();

			var error = this.ImageToNative(id, false);

			if (error != ErrorType.NoError)
			{

				this.Release(id);
				throw new ILWrapperException(error);

			}

			this.GetImageInfo();
			this.Release(id);
		}

		#endregion

		#region Save

		public void Save(string file)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!this.IsValid) throw new ILWrapperException("Image has never been loaded");

			File.WriteAllBytes(file, this._buffer);
		}

		public void Save(string file, ImageType type)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!this.IsValid) throw new ILWrapperException("Image has never been loaded");

			if (type == this._image_type)
			{

				File.WriteAllBytes(file, this._buffer);

			}
			else
			{

				var array = this.SaveSettings(type);
				File.WriteAllBytes(file, array);

			}
		}

		public void Save(Stream stream)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!this.IsValid) throw new ILWrapperException("Image has never been loaded");

			stream.Write(this._buffer, 0, this.Size);
		}

		public void Save(Stream stream, ImageType type)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!this.IsValid) throw new ILWrapperException("Image has never been loaded");

			if (type == this._image_type)
			{

				stream.Write(this._buffer, 0, this.Size);

			}
			else
			{

				var array = this.SaveSettings(type);
				stream.Write(array, 0, array.Length);

			}
		}

		public void Save(byte[] buffer)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!this.IsValid) throw new ILWrapperException("Image has never been loaded");

			if (buffer == null)
			{

				throw new ArgumentNullException(nameof(buffer));

			}

			if (buffer.Length < this._buffer.Length)
			{

				throw new ArgumentException("Length of buffer passed is smaller than image size");

			}

			Array.Copy(this._buffer, 0, buffer, 0, this.Size);
		}

		public void Save(byte[] buffer, ImageType type)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!this.IsValid) throw new ILWrapperException("Image has never been loaded");

			if (buffer == null)
			{

				throw new ArgumentNullException(nameof(buffer));

			}

			var array = this.SaveSettings(type);

			if (buffer.Length < array.Length)
			{

				throw new ArgumentException("Length of buffer passed is smaller than image size");

			}

			Array.Copy(array, 0, buffer, 0, array.Length);
		}

		public void Save(byte[] buffer, int position)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!this.IsValid) throw new ILWrapperException("Image has never been loaded");

			if (buffer == null)
			{

				throw new ArgumentNullException(nameof(buffer));

			}

			if (position < 0 || position >= this.Size)
			{

				throw new Exception("Position should be a non-negative value and less than buffer length");

			}

			if (position + this.Size > buffer.Length)
			{

				throw new Exception("Array passed does not have enough space for image buffer");

			}

			Array.Copy(this._buffer, 0, buffer, position, this.Size);
		}

		public void Save(byte[] buffer, int position, ImageType type)
		{
			if (this._disposed) throw new ObjectDisposedException("Image has been disposed");
			if (!this.IsValid) throw new ILWrapperException("Image has never been loaded");

			if (buffer == null)
			{

				throw new ArgumentNullException(nameof(buffer));

			}

			if (position < 0 || position >= this.Size)
			{

				throw new Exception("Position should be a non-negative value and less than buffer length");

			}

			var array = this.SaveSettings(type);

			if (position + array.Length > buffer.Length)
			{

				throw new Exception("Array passed does not have enough space for image buffer");

			}

			Array.Copy(array, 0, buffer, position, array.Length);
		}

		private byte[] SaveSettings(ImageType type)
		{
			var id = this.Initialize();

			var error = this.ImageToNative(id, false);
			
			if (error != ErrorType.NoError)
			{
			
				this.Release(id);
				throw new ILWrapperException(error);
			
			}

			uint size = IL.SaveImage((uint)type, IntPtr.Zero, 0);

			if (size == 0)
			{

				error = (ErrorType)IL.GetError();
				this.Release(id);
				throw new ILWrapperException(error);

			}

			var array = new byte[size];

			unsafe
			{

				fixed (byte* ptr = &array[0])
				{

					if (IL.SaveImage((uint)type, new IntPtr(ptr), size) == 0)
					{

						error = (ErrorType)IL.GetError();
						this.Release(id);
						throw new ILWrapperException(error);

					}

				}

			}

			this.Release(id);
			return array;
		}

		#endregion

		#region Misc

		public void CopyTo(Image dest)
		{
			dest._array = this._array;
			dest._bitspp = this._bitspp;
			dest._bytespp = this._bytespp;
			dest._channel = this._channel;
			dest._cubemap = this._cubemap;
			dest._data_type = this._data_type;
			dest._depth = this._depth;
			dest._dxtc = this._dxtc;
			dest._face_count = this._face_count;
			dest._format = this._format;
			dest._height = this._height;
			dest._image_type = this._image_type;
			dest._layers = this._layers;
			dest._mipmaps = this._mipmaps;
			dest._origin = this._origin;
			dest._pal_bytespp = this._pal_bytespp;
			dest._pal_columns = this._pal_columns;
			dest._pal_format = this._pal_format;
			dest._pal_type = this._pal_type;
			dest._width = this._width;
			dest._buffer = new byte[this.Size];
			Array.Copy(this._buffer, 0, dest._buffer, 0, this.Size);
		}

		public Image Clone()
		{
			var result = new Image();
			this.CopyTo(result);
			return result;
		}

		// public System.Drawing.Image ToManagedImage()
		// {
		// 	if (!this.IsValid) throw new ArgumentNullException("Image was never loaded and has no buffer");
		// 	using var ms = new MemoryStream(this._buffer);
		// 	return System.Drawing.Image.FromStream(ms);
		// }

		#endregion

		#region GetInfo

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

		#endregion

		#region Check

		public static bool IsValidExportExtension(string extension)
		{
			return GetExportExtensions().ToList().Contains(extension.ToUpperInvariant());
		}

		public static bool IsValidImportExtension(string extension)
		{
			return GetImportExtensions().ToList().Contains(extension.ToUpperInvariant());
		}

		public static bool FileHasValidExportExtension(string file)
		{
			var extension = Path.GetExtension(file)[1..].ToUpperInvariant();
			return GetExportExtensions().ToList().Contains(extension);
		}

		public static bool FileHasValidImportExtension(string file)
		{
			var extension = Path.GetExtension(file)[1..].ToUpperInvariant();
			return GetImportExtensions().ToList().Contains(extension);
		}

		public static IEnumerable<string> GetExportExtensions()
		{
			var intptr_t = IL.GetString((uint)ILString.ExportExtensions);
			if (intptr_t == IntPtr.Zero)
			{

				var error = (ErrorType)IL.GetError();
				throw new ILWrapperException(error);

			}
			
			string str = Marshal.PtrToStringAnsi(intptr_t);
			string[] array = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			for (int i = 0; i < array.Length; ++i) yield return array[i].ToUpperInvariant();
		}

		public static IEnumerable<string> GetImportExtensions()
		{
			var intptr_t = IL.GetString((uint)ILString.ImportExtensions);
			if (intptr_t == IntPtr.Zero)
			{

				var error = (ErrorType)IL.GetError();
				throw new ILWrapperException(error);

			}

			string str = Marshal.PtrToStringAnsi(intptr_t);
			string[] array = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			
			for (int i = 0; i < array.Length; i++)
			{

				str = array[i].ToUpperInvariant();
				yield return str == "DCMDDS" ? "DCM" : str;

			}
		}

		#endregion

		#region Override

		public override bool Equals(object obj) => obj is Image image && this == image;

		public static bool operator ==(Image img1, Image img2)
		{
			if (img1.Size != img2.Size) return false;
			
			bool result = true;

			for (int i = 0; i < img1.Size; ++i) result &= img1._buffer[i] == img2._buffer[i];

			return result;
		}

		public static bool operator !=(Image img1, Image img2) => !(img1 == img2);

		public override int GetHashCode()
		{
			int result = 1;

			for (int i = 0; i < this.Size; ++i) result = HashCode.Combine(result, this._buffer[i]);

			return result;
		}

		public override string ToString()
		{
			return $"Type: {this.ImageType} | Format: {this.Format}";
		}

		#endregion
	}
}
