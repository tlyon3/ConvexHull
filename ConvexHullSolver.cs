using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using _1_convex_hull;
using System.Linq;

namespace _2_convex_hull {
	class ConvexHullSolver {
		System.Drawing.Graphics g;
		System.Windows.Forms.PictureBox pictureBoxView;

		public ConvexHullSolver(System.Drawing.Graphics g, System.Windows.Forms.PictureBox pictureBoxView) {
			this.g = g;
			this.pictureBoxView = pictureBoxView;
		}

		public void Refresh() {
			// Use this especially for debugging and whenever you want to see what you have drawn so far
			pictureBoxView.Refresh();
		}

		public void Pause(int milliseconds) {
			// Use this especially for debugging and to animate your algorithm slowly
			pictureBoxView.Refresh();
			System.Threading.Thread.Sleep(milliseconds);
		}

		public void Solve(List<System.Drawing.PointF> pointList) {
			// TODO: Insert your code here

			//order points by x value
			pointList = pointList.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();//O(nlogn)

			Hull solution = getHull(pointList,0,"start");
			//Console.WriteLine("Solution:");
			//printPointInformation(solution.getPoints());
			Pen redPen = new Pen(Color.Red, 2.0f);
			drawLines(solution.getPoints(), redPen);
			Refresh();
		}

		public void drawLines(List<PointF> points, Pen pen) {
			PointF[] pointArray = points.ToArray();
			g.DrawLines(pen, pointArray);
			g.DrawLine(pen, pointArray[0], pointArray[points.Count - 1]);
		}

		public Hull getHull(List<PointF> pointList, int recurLevel, String side) {
			if (pointList.Count <= 2) {
				Hull result = new Hull(pointList);
				result.setRightMost(pointList[pointList.Count-1]);
				return result;
			}

			List<List<PointF>> sets = divideSet(pointList);

			Hull leftHull = getHull(sets[0], recurLevel+1, side+=" left");
			Hull rightHull = getHull(sets[1], recurLevel+1, side+=" right");
			Console.WriteLine(recurLevel + " " + side);
			Console.WriteLine("\tSize of left: " + leftHull.getPoints().Count);
			Console.WriteLine("\tSize of right: " + rightHull.getPoints().Count);
			return merge(leftHull, rightHull);
		}

		public Hull merge(Hull left, Hull right) {
			bool leftSideChanged = true;
			bool rightSideChanged = true;
			int rightMostInLeft = left.getRightMostIndex();
			int leftUpperIndex = rightMostInLeft;
			int rightUpperIndex = 0;
			//get upper common tangent\
			while (leftSideChanged || rightSideChanged) {
				int newIndex = getLeftUpper(left, right, leftUpperIndex, rightUpperIndex);
				if (leftUpperIndex == newIndex) {
					leftSideChanged = false;
				}
				else {
					leftUpperIndex = newIndex;
					leftSideChanged = true;
				}

				newIndex = getRightUpper(left, right, leftUpperIndex, rightUpperIndex);
				if (rightUpperIndex == newIndex) {
					rightSideChanged = false;
				}
				else {
					rightSideChanged = true;
					rightUpperIndex = newIndex;
				}
			}
			int leftLowerIndex = rightMostInLeft;
			int rightLowerIndex = 0;

			leftSideChanged = true;
			rightSideChanged = true;

			//get lower common tangent
			while (leftSideChanged || rightSideChanged) {
				int newIndex = getLeftLower(left, right, leftLowerIndex, rightLowerIndex);
				if (newIndex == leftLowerIndex) {
					leftSideChanged = false;
				}
				else {
					leftLowerIndex = newIndex;
					leftSideChanged = true;
				}

				newIndex = getRightLower(left, right, leftLowerIndex, rightLowerIndex);
				if (newIndex == rightLowerIndex) {
					rightSideChanged = false;
				}
				else {
					rightSideChanged = true;
					rightLowerIndex = newIndex;
				}

			}

			List<PointF> points = join(left, right, leftUpperIndex, rightUpperIndex, leftLowerIndex, rightLowerIndex);
			Hull result = new Hull(points);
			return result;
		}

		private int getLeftUpper(Hull left, Hull right, int leftIndex, int rightIndex){
			List<PointF> leftPoints = left.getPoints();
			for (int i = 0; i < leftPoints.Count;i++){
				Double oldSlope = calculateSlope(left.getPoints()[leftIndex], right.getPoints()[rightIndex]);
				int newIndex = left.getPrevIndex(leftIndex);
				Double newSlope = calculateSlope(left.getPoints()[newIndex], right.getPoints()[rightIndex]);
				if(newSlope <= oldSlope){
					leftIndex = newIndex;
				}
			}
			return leftIndex;
		}

		private int getRightUpper(Hull left, Hull right, int leftIndex, int rightIndex){
			List<PointF> rightPoints = right.getPoints();
			for (int i = 0; i < rightPoints.Count;i++){
				Double oldSlope = calculateSlope(left.getPoints()[leftIndex], right.getPoints()[rightIndex]);
				int newIndex = right.getNextIndex(rightIndex);
				Double newSlope = calculateSlope(left.getPoints()[leftIndex], right.getPoints()[newIndex]);
				if(newSlope >= oldSlope){
					rightIndex = newIndex;
				}
			}
			return rightIndex;
		}

		private int getLeftLower(Hull left, Hull right, int leftIndex, int rightIndex){
			List<PointF> leftPoints = left.getPoints();
			for (int i = 0; i < leftPoints.Count; i++) {
				Double oldSlope = calculateSlope(left.getPoints()[leftIndex], right.getPoints()[rightIndex]);
				int newIndex = left.getNextIndex(leftIndex);
				Double newSlope = calculateSlope(left.getPoints()[newIndex], right.getPoints()[rightIndex]);
				if (newSlope >= oldSlope) {
					leftIndex = newIndex;
				}
			}
			return leftIndex;
		}

		private int getRightLower(Hull left, Hull right, int leftIndex, int rightIndex){
			List<PointF> rightPoints = right.getPoints();
			for (int i = 0; i < rightPoints.Count; i++) {
				Double oldSlope = calculateSlope(left.getPoints()[leftIndex], right.getPoints()[rightIndex]);
				int newIndex = right.getPrevIndex(rightIndex);
				Double newSlope = calculateSlope(left.getPoints()[leftIndex], right.getPoints()[newIndex]);
				if (newSlope <= oldSlope) {
					rightIndex = newIndex;
				}
			}
			return rightIndex;
		}

		private List<PointF> join(Hull left, Hull right, int leftUpper, int rightUpper, int leftLower, int rightLower){
			List<PointF> result = new List<PointF>();
			List<PointF> leftPoints = left.getPoints();
			List<PointF> rightPoints = right.getPoints();

			Console.WriteLine("--------------------");
			Console.WriteLine("Joining left(" + left.getPoints().Count + ")");
			for (int i = 0; i < leftPoints.Count;i++){
				Console.WriteLine("\t" + left.printPointInfo(i));
			}
			Console.WriteLine("With Right(" + right.getPoints().Count + ")");
			for (int i = 0; i < rightPoints.Count; i++) {
				Console.WriteLine("\t" + right.printPointInfo(i));
			}
			Console.WriteLine("Left upper: " + leftUpper + left.printPointInfo(leftUpper));
			Console.WriteLine("Right upper: " + rightUpper + right.printPointInfo(rightUpper));
			Console.WriteLine("Left lower: " + leftLower + left.printPointInfo(leftLower));
			Console.WriteLine("Right lower: " + rightLower + right.printPointInfo(rightLower));

			for (int i = 0; i <= leftUpper;i++){
				Console.WriteLine("\tAdded: [" + leftPoints[i].X + ", " + leftPoints[i].Y + "]");
				result.Add(leftPoints[i]);
			}
			for (int i = rightUpper; i <= rightLower;i++){
				Console.WriteLine("\tAdded: [" + rightPoints[i].X + ", " + rightPoints[i].Y + "]--");
				result.Add(rightPoints[i]);
			}
			int index = leftLower;
			while(index != 0){
				result.Add(leftPoints[index]);
				index = left.getNextIndex(index);
			}
			return result;
		}

		private int getIndexForPoint(PointF point, Hull hull){
			List<PointF> points = hull.getPoints();
			for (int i = 0; i < points.Count;i++){
				if(points[i].Equals(point)){
					return i;
				}
			}
			return -100;
		}

		//return a list of a list of points. 
		//The first list will be the left side. 
		//The second will be the right side
		public List<List<System.Drawing.PointF>> divideSet(List<PointF> points) {
			List<PointF> leftSide = points.Take(points.Count / 2).ToList();
			List<PointF> rightSide = points.Skip(points.Count / 2).ToList();
			List<List<PointF>> result = new List<List<PointF>>();
			result.Add(leftSide);
			result.Add(rightSide);
			return result;
		}

		public Double calculateSlope(PointF left, PointF right) {
			return ((right.Y - left.Y) / (right.X - left.X));
		}

		private void printPointInformation(List<PointF> pointList) {
			foreach (PointF point in pointList) {
				Console.WriteLine("[" + point.X + "," + point.Y + "]");
			}
		}
	}
}
