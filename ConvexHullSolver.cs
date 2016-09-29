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
			pointList = pointList.OrderBy(p => p.X).ToList();//O(nlogn)

			Hull solution = getHull(pointList,0,"start");
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
			if (pointList.Count <= 3) {
				Hull result = new Hull(pointList);
				result.setRightMost(pointList[pointList.Count - 1]);
				return result;
			}
			else {

				List<List<PointF>> sets = divideSet(pointList);

				Hull leftHull = getHull(sets[0], recurLevel + 1, side += " left");
				Hull rightHull = getHull(sets[1], recurLevel + 1, side += " right");

				Console.WriteLine(recurLevel + " " + side);
				Console.WriteLine("\tSize of left: " + leftHull.getPoints().Count);
				Console.WriteLine("\tSize of right: " + rightHull.getPoints().Count);

				return merge(leftHull, rightHull);
			}
		}

		public Hull merge(Hull left, Hull right) {
			int rightMost = left.getRightMostIndex();
			int leftMost = right.getLeftMostIndex();

			int currentLeftIndex = rightMost;
			int currentRightIndex = leftMost;

			int upperLeft = -1;
			int upperRight = -1;
			int lowerLeft = -1;
			int lowerRight = -1;

			bool leftIndexChanged = false;
			bool rightIndexChanged = false;
			//iterate through at least once
			bool firstRight = true;
			bool firstLeft = true;

			//get upper common tangent
			while(leftIndexChanged || rightIndexChanged || firstLeft || firstRight){
				if(firstLeft || rightIndexChanged){
					firstLeft = false;
					upperLeft = getLeftUpper(left, right, currentLeftIndex, currentRightIndex);
					if(upperLeft == currentLeftIndex){
						leftIndexChanged = false;
						rightIndexChanged = false;
					}
					else {
						leftIndexChanged = true;
						currentLeftIndex = upperLeft;
					}
				}

				if(firstRight || leftIndexChanged){
					firstRight = false;
					upperRight = getRightUpper(left, right, currentLeftIndex, currentRightIndex);
					if(upperRight == currentRightIndex){
						leftIndexChanged = false;
						rightIndexChanged = false;
					}
					else {
						rightIndexChanged = true;
						currentRightIndex = upperRight;
					}
				}
			}

			//get lower common tangentt
			currentLeftIndex = rightMost;
			currentRightIndex = leftMost;

			leftIndexChanged = false;
			rightIndexChanged = false;
			//iterate through at least once
			firstRight = true;
			firstLeft = true;
			while (leftIndexChanged || rightIndexChanged || firstLeft || firstRight) {
				if (firstLeft || rightIndexChanged) {
					firstLeft = false;
					lowerLeft = getLeftLower(left, right, currentLeftIndex, currentRightIndex);
					if (lowerLeft == currentLeftIndex) {
						leftIndexChanged = false;
						rightIndexChanged = false;
					}
					else {
						leftIndexChanged = true;
						currentLeftIndex = lowerLeft;
					}
				}

				if (firstRight || leftIndexChanged) {
					firstRight = false;
					lowerRight = getRightLower(left, right, currentLeftIndex, currentRightIndex);
					if (lowerRight == currentRightIndex) {
						leftIndexChanged = false;
						rightIndexChanged = false;
					}
					else {
						rightIndexChanged = true;
						currentRightIndex = lowerRight;
					}
				}
			}

			//join points
			List<PointF> resultPoints = new List<PointF>();
			//add up to (and including) upperLeft
			for (int i = 0; i <= upperLeft;i++){
				resultPoints.Add(left.getPoints()[i]);
			}
			//add up to lowerRight
			for (int i = upperRight; i != lowerRight; i = right.getNextIndex(i)){
				resultPoints.Add(right.getPoints()[i]);
			}
			//add lowerRight
			resultPoints.Add(right.getPoints()[lowerRight]);
			//add from lowerLeft to beginning
			for (int i = lowerLeft; i != 0; i = left.getNextIndex(i)){
				resultPoints.Add(left.getPoints()[i]);
			}

			return new Hull(resultPoints);
		}
		private int getLeftUpper(Hull left, Hull right, int leftIndex, int rightIndex){ //O(n)
			List<PointF> leftPoints = left.getPoints();
			List<PointF> rightPoints = right.getPoints();
			while(calculateSlope(rightPoints[rightIndex], leftPoints[left.getPrevIndex(leftIndex)]) < 
			      calculateSlope(rightPoints[rightIndex], leftPoints[leftIndex])){
				leftIndex = left.getPrevIndex(leftIndex);
			}
			return leftIndex;
		}

		private int getRightUpper(Hull left, Hull right, int leftIndex, int rightIndex){ //O(n)
			List<PointF> rightPoints = right.getPoints();
			List<PointF> leftPoints = left.getPoints();
			while(calculateSlope(leftPoints[leftIndex], rightPoints[right.getNextIndex(rightIndex)]) > 
			      calculateSlope(leftPoints[leftIndex], rightPoints[rightIndex])){
				rightIndex = right.getNextIndex(rightIndex);
			}

			return rightIndex;
		}

		private int getLeftLower(Hull left, Hull right, int leftIndex, int rightIndex){ //O(n)
			List<PointF> leftPoints = left.getPoints();
			List<PointF> rightPoints = right.getPoints();
			while(calculateSlope(rightPoints[rightIndex], leftPoints[left.getNextIndex(leftIndex)]) > 
			      calculateSlope(rightPoints[rightIndex], leftPoints[leftIndex])){
				leftIndex = left.getNextIndex(leftIndex);
			}
			return leftIndex;
		}

		private int getRightLower(Hull left, Hull right, int leftIndex, int rightIndex){ //O(n)
			List<PointF> rightPoints = right.getPoints();
			List<PointF> leftPoints = left.getPoints();
			while(calculateSlope(leftPoints[leftIndex], rightPoints[right.getPrevIndex(rightIndex)]) < 
			      calculateSlope(leftPoints[leftIndex], rightPoints[rightIndex])){
				rightIndex = right.getNextIndex(rightIndex);
			}
			return rightIndex;
		}

		//private List<PointF> join(Hull left, Hull right, int leftUpper, int rightUpper, int leftLower, int rightLower){
		//	List<PointF> result = new List<PointF>();
		//	List<PointF> leftPoints = left.getPoints();
		//	List<PointF> rightPoints = right.getPoints();

		//	Console.WriteLine("--------------------");
		//	Console.WriteLine("Joining left(" + left.getPoints().Count + ")");
		//	for (int i = 0; i < leftPoints.Count;i++){
		//		Console.WriteLine("\t" + left.printPointInfo(i));
		//	}
		//	Console.WriteLine("With Right(" + right.getPoints().Count + ")");
		//	for (int i = 0; i < rightPoints.Count; i++) {
		//		Console.WriteLine("\t" + right.printPointInfo(i));
		//	}
		//	Console.WriteLine("Left upper: " + leftUpper + left.printPointInfo(leftUpper));
		//	Console.WriteLine("Right upper: " + rightUpper + right.printPointInfo(rightUpper));
		//	Console.WriteLine("Left lower: " + leftLower + left.printPointInfo(leftLower));
		//	Console.WriteLine("Right lower: " + rightLower + right.printPointInfo(rightLower));

		//	for (int i = 0; i <= leftUpper;i++){
		//		Console.WriteLine("\tAdded: [" + leftPoints[i].X + ", " + leftPoints[i].Y + "]");
		//		result.Add(leftPoints[i]);
		//	}
		//	for (int i = rightUpper; i <= rightLower;i++){
		//		Console.WriteLine("\tAdded: [" + rightPoints[i].X + ", " + rightPoints[i].Y + "]--");
		//		result.Add(rightPoints[i]);
		//	}
		//	int index = leftLower;
		//	while(index != 0){
		//		result.Add(leftPoints[index]);
		//		index = left.getNextIndex(index);
		//	}
		//	return result;
		//}

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
			return -((right.Y - left.Y) / (right.X - left.X));
		}

		private void printPointInformation(List<PointF> pointList) {
			foreach (PointF point in pointList) {
				Console.WriteLine("[" + point.X + "," + point.Y + "]");
			}
		}
	}
}
