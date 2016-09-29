using System;
using System.Drawing;
namespace _1_convex_hull {
	public class Line {
		private System.Drawing.PointF right;
		private System.Drawing.PointF left;

		public Line() {
			right = new PointF(0.0f, 0.0f);
			left = new PointF(0.0f, 0.0f);
		}

		public Line(PointF right, PointF left){
			this.right = right;
			this.left = left;
		}

		public PointF getRight(){
			return this.right;
		}

		public PointF getLeft(){
			return this.left;
		}

		public float getSlope(){
			return ((right.Y - left.Y) / (right.X - left.X));
		}
	}
}
