using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

using ILWrapper.Enums;



namespace ILWrappe
{
	class Program
	{
		public static System.Drawing.Image DDStoPNG(string file)
		{
			var buffer = File.ReadAllBytes(file);
			var image = new ILWrapper.Image(buffer);

			using var ms = new MemoryStream();
			image.Save(ms, ImageType.PNG);
			return System.Drawing.Image.FromStream(ms);
		}

		public static void Main(string[] args)
		{
			string dds = "DDS.dds";
			string png = "PNG.png";

			var image = DDStoPNG(dds);



			int aaa = 0;
		}
	}
}
