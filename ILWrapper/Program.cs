using System;
using System.Collections.Generic;
using System.IO;
using ILWrapper.Enums;



namespace ILWrapper
{
	class Program
	{
		public static void Main(string[] args)
		{
			string dds = "DDS.dds";
			string png = "PNG.png";

			var image = new Image();

			image.Load(dds);

			image.Save("TEST.png", ImageType.PNG);

			int aaa = 0;
		}
	}
}
