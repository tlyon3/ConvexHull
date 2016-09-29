using System;
using System.Collections.Generic;
using System.Collections; 
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace _2_convex_hull
{
    public partial class FormMain : Form
    {

        abstract class PointGenerator
        {
            /// <summary>
            /// Generates a random point in a rectangle defined with the given width x height.
            /// Sub classes should implement a specific algorithm to determine point distribution.
            /// </summary>
            /// <param name="width">Width of the bounding rectangle</param>
            /// <param name="height">Height of the bounding rectangle</param>
            /// <returns>The generated random point</returns>
            public abstract PointF generatePointIn(int width, int height);
        }
        
        class GaussianPointGenerator : PointGenerator {

            // Uses this to generate the Guassian distribution
            // from http://msdn.microsoft.com/msdnmag/issues/06/09/TestRun/default.aspx
            // (http://msdn.microsoft.com/msdnmag/issues/06/09/TestRun/default.aspx?loc=&fig=true#fig9)
            class Gaussian
            {
                private Random r = new Random();
                private bool use_last_result = false; // flag for NextGaussian3()
                private double y2 = 0.0;  // secondary result for NextGaussian3()

                public double NextGaussian(double mean, double sd) // most efficient
                {
                    double x1, x2, w, y1 = 0.0;

                    if (use_last_result) // use answer from previous call?
                    {
                        y1 = y2;
                        use_last_result = false;
                    }
                    else
                    {
                        do
                        {
                            x1 = 2.0 * r.NextDouble() - 1.0;
                            x2 = 2.0 * r.NextDouble() - 1.0;
                            w = (x1 * x1) + (x2 * x2);
                        }
                        while (w >= 1.0); // are x1 and x2 inside unit circle?

                        w = Math.Sqrt((-2.0 * Math.Log(w)) / w);
                        y1 = x1 * w;
                        y2 = x2 * w;
                        use_last_result = true;
                    }

                    return mean + y1 * sd;
                }
            }
            Gaussian m_rand;

            public GaussianPointGenerator() {
                m_rand = new Gaussian();
            }

            public override PointF generatePointIn(int width, int height) {
                // mdj 1/8/07 generate random points with floats instead of ints to avoid having so many 
                // duplicates as per Dr. Ringger's comments from Fall 06.  Doubles would be better, but there's 
                // no built-in point data structure for doubles.  
                return new PointF((width / 2 + (float)m_rand.NextGaussian(0, width / 10)),
                                 (height / 2 + (float)m_rand.NextGaussian(0, height / 10)));
            }
        }

        class UniformPointGenerator : PointGenerator
        {
            private Random rand = new Random();

            /// <summary>
            /// Generates points that are uniformly distributed inside of the oval
            /// defined by the bounding rectangle passed in.
            /// </summary>
            /// <param name="width">Width of the bounding rectangle</param>
            /// <param name="height">Height of the bounding rectangle</param>
            /// <returns>Random point in an oval bound by the rectangle</returns>
            public override PointF generatePointIn(int width, int height) {
                double r, x, y;

                do {
                    //First generate points inside a circle
                    x = 2.0 * rand.NextDouble() - 1.0;
                    y = 2.0 * rand.NextDouble() - 1.0;

                    //Check radius
                    r = Math.Sqrt(x * x + y * y);
                } while( r > 1.0 );

                //Now convert to possibly-oval, larger bounds
                x *= width / 2 - 10;    //giving 5px border on each side
                y *= height / 2 - 10;
                //Translate to screen coords
                x += width / 2;
                y += height / 2;

                //Using float gives fewer duplicates than using int.
                //Double would be better but there is no Point-Double class.
                return new PointF((float)x, (float)y);
            }
        }
        
        Graphics m_g;
        PointGenerator pointGenerator;
        List<PointF> m_pointList;
        private Dictionary<float, PointF> UniquePoints;

        public FormMain()
        {
            InitializeComponent();
            pictureBoxView.Image = new Bitmap(pictureBoxView.Width, pictureBoxView.Height);
            m_g = Graphics.FromImage(pictureBoxView.Image);
            pointGenerator = new UniformPointGenerator();
            radioUniform.Checked = true;    //start with this as the default
            m_pointList = new List<PointF>();
            UniquePoints = new Dictionary<float, PointF>();
 
        }

        private PointF getRandomPoint()
        {
            //eam, 1/17/08 -- changed to use a Strategy Pattern for generating pts w/ different distributions
            return pointGenerator.generatePointIn(pictureBoxView.Image.Width, pictureBoxView.Image.Height);
        }

        private void generatePoints()
        {
            // create point list
            int numPoints = int.Parse(textBoxNumPoints.Text);
            m_pointList.Clear();
            UniquePoints.Clear();
            PointF NewlyCreatedPoint; 

            // make sure X value are unique.  Y values may contain duplicates by the way. 
            while (UniquePoints.Count < numPoints) 
            {
                NewlyCreatedPoint = getRandomPoint();   //get the next point to add
                if (!UniquePoints.ContainsKey(NewlyCreatedPoint.X))
                    UniquePoints.Add(NewlyCreatedPoint.X,NewlyCreatedPoint); 
            }

            // more convenient from here on out to use list. 
            foreach (float x in UniquePoints.Keys)
            {
                m_pointList.Add(UniquePoints[x]);
            }

            drawPoints();
            statusLabel.Text = "" + numPoints + " points Generated";
        }

        private void drawPoints()
        {
            m_g.Clear(Color.White);
            m_g.Flush();
            
            Pen pointColor = new Pen(Color.FromArgb(0, 0, 0));
            foreach (PointF point in m_pointList)
            {
                m_g.DrawEllipse(pointColor, new RectangleF(point, new Size(2, 2)));
                
            }
            pictureBoxView.Refresh(); // show our results
        }

        #region GUI Control
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            generatePoints();
        }

        private void textBoxNumPoints_Validating(object sender, CancelEventArgs e)
        {
            int result;
            if(!(int.TryParse(textBoxNumPoints.Text, out result)))
            {
                e.Cancel = true;
            }
        }
        
        private void buttonSolve_Click(object sender, EventArgs e)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
                ConvexHullSolver convexHullSolver = new ConvexHullSolver(m_g, pictureBoxView);
                convexHullSolver.Solve(m_pointList);
                pictureBoxView.Refresh();
            timer.Stop();
            statusLabel.Text = "Done.  Time taken: " + timer.Elapsed;

        }

        private void buttonClearToPoints_Click(object sender, EventArgs e)
        {
            drawPoints();
            statusLabel.Text = "Cleared to the original points.";
        }
        #endregion

        private void radioUniform_CheckedChanged(object sender, EventArgs e) {
            if( !(pointGenerator is UniformPointGenerator) ) {
                pointGenerator = new UniformPointGenerator();
            }
        }

        private void radioGaussian_CheckedChanged(object sender, EventArgs e) {
            if( !(pointGenerator is GaussianPointGenerator) ) {
                pointGenerator = new GaussianPointGenerator();
            }
        }
    }
}