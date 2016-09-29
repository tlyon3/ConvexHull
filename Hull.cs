using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace _1_convex_hull {
	public class Hull {
		List<PointF> points;
		PointF rightMostPoint;
		Dictionary<PointF, int> pointIndexDict;

		public PointF getNext(PointF point){
			int currentIndex = pointIndexDict[point];
			int newIndex = (currentIndex + 1) % points.Count;
			return points[newIndex];
		}

		public int getNextIndex(int currentIndex){
			if(currentIndex == this.points.Count - 1){
				return 0;
			}
			else {
				return currentIndex + 1;
			}
		}

		public PointF getPrev(PointF point){
			try {
				int currentIndex = pointIndexDict[point];
				int newIndex = (currentIndex - 1) % points.Count;
				if (newIndex < 0) {
					newIndex = ~newIndex + 1;
				}
				return points[newIndex];
			}
			catch(KeyNotFoundException ex){
				Console.WriteLine(ex.Message);
				return new PointF(0.0f,0.0f);
			}
		}

		public int getPrevIndex(int currentIndex){
			if(currentIndex == 0){
				return this.points.Count - 1;
			}
			else{
				return currentIndex - 1;
			}
		}

		public Hull() {
		}

		public List<PointF> getPoints(){
			return this.points;
		}

		public Hull(List<PointF> points){
			this.pointIndexDict = new Dictionary<PointF, int>();
			this.points = points;
			//for (int i = 0; i < points.Count;i++){
			//	pointIndexDict.Add(points[i], i);
			//}
		}

		public void setRightMost(PointF point){
			this.rightMostPoint = point;
		}

		public PointF getRightMost(){
			return this.rightMostPoint;
		}

		public PointF getRightMostPoint(){
			PointF rightMost = new PointF();
			foreach(PointF point in this.points){
				if(point.X > rightMost.X){
					rightMost = point;
				}
			}
			return rightMost;
		}

		public int getRightMostIndex(){
			int max = 0;
			for (int i = 0; i < points.Count;i++){
				if(points[i].X > points[max].X){
					max = i;
				}	
			}
			return max;
		}
		public String printPointInfo(int index){
			return "[" + points[index].X + ", " + points[index].Y + "]";
		}

		public int getLeftMostIndex(){
			int max = 0;
			for (int i = 0; i < points.Count; i++) {
				if (points[i].X < points[max].X) {
					max = i;
				}
			}
			return max;
		}
	}
}
