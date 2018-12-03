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
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public LineSegment()
        {
            Observers = new List<DrawingObject>();
        }

        public LineSegment(Point startpoint) : this()
        {
            StartPoint = startpoint;
        }

        public LineSegment(Point startpoint, Point endpoint) : this(startpoint)
        {
            EndPoint = endpoint;
            CenterPoint = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
        }

        public override void Render()
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

        public override void Translate(int xAmount, int yAmount)
        {
            StartPoint = new Point(StartPoint.X + xAmount, StartPoint.Y + yAmount);
            EndPoint = new Point(EndPoint.X + xAmount, EndPoint.Y + yAmount);
            CenterPoint = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
        }
    }
}
