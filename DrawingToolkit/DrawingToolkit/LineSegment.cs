using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DrawingToolkit
{
    public class LineSegment : DrawingObject
    {
        private const double EPSILON = 3.0;

        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public LineSegment()
        {
            SetPenStyle(Color.Black, 2.0f, DashStyle.Solid);
        }

        public LineSegment(Point startpoint) : this()
        {
            this.StartPoint = startpoint;
        }

        public LineSegment(Point startpoint, Point endpoint) : this(startpoint)
        {
            this.EndPoint = endpoint;
        }

        public void Render()
        {
            if (GetGraphics() != null)
            {
                GetGraphics().DrawLine(Pen, StartPoint, EndPoint);
            }
        }

        public override bool Intersect(Point testPoint)
        {
            double slope = GetSlope();
            double shift = EndPoint.Y - slope * EndPoint.X;
            double y_line = slope * testPoint.X + shift;

            if (Math.Abs(y_line - testPoint.Y) < EPSILON)
            {
                return true;
            }
            return false;
        }

        public double GetSlope()
        {
            return (EndPoint.Y - StartPoint.Y) / (double)(EndPoint.X - StartPoint.X);
        }

        public override void RenderOnPreviewState()
        {
            SetPenStyle(Color.Red, 2.0f, DashStyle.Dot);
            Render();
        }

        public override void RenderOnEditState()
        {
            SetPenStyle(Color.Blue, 2.0f, DashStyle.Solid);
            Render();
        }

        public override void RenderOnStaticState()
        {
            SetPenStyle(Color.Black, 2.0f, DashStyle.Solid);
            Render();
        }

        public override void Translate(int xAmount, int yAmount)
        {
            StartPoint = new Point(StartPoint.X + xAmount, StartPoint.Y + yAmount);
            EndPoint = new Point(EndPoint.X + xAmount, EndPoint.Y + yAmount);
        }
    }
}
