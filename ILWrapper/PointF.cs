namespace ILWrapper
{
	public struct PointF
	{
		private float x;
		private float y;

		public PointF(float x, float y) { this.x = x; this.y = y; }
		public PointF(System.Drawing.PointF point) { this.x = point.X; this.y = point.Y; }
		public System.Drawing.PointF ToSystemPoint() => new System.Drawing.PointF(this.x, this.y);
	}
}
