using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ILWrapper.Enums;



namespace ILWrapper.Drawing
{
	public sealed class Image : IDisposable, IEquatable<Image>
	{
		private bool _disposed = false;
		private int _id;

		public static Image Default => new Image();





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
