namespace ILWrapper
{
	public struct Point
	{
		private int x;
		private int y;

		public Point(int x, int y) { this.x = x; this.y = y; }
		public Point(System.Drawing.Point point) { this.x = point.X; this.y = point.Y; }
		public System.Drawing.PointF ToSystemPoint() => new System.Drawing.Point(this.x, this.y);
 	}
}
