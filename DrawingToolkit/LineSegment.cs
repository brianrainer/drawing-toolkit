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
            this.Name = "Line";
        }

        public LineSegment(Point startpoint) : this()
        {
            StartPoint = startpoint;
        }

        public LineSegment(Point startpoint, Point endpoint) : this(startpoint)
        {
            EndPoint = endpoint;
        }

        public override void Render()
        {
            if (GetGraphics() != null)
            {
                GetGraphics().DrawLine(GetPen(), StartPoint, EndPoint);
            }
        }

        public override bool Intersect(Point testPoint)
        {
            bool IsInsideBound =
                   testPoint.X <= Math.Max(StartPoint.X, EndPoint.X) &&
                   testPoint.X >= Math.Min(StartPoint.X, EndPoint.X) &&
                   testPoint.Y <= Math.Max(StartPoint.Y, EndPoint.Y) &&
                   testPoint.Y >= Math.Min(StartPoint.Y, EndPoint.Y);

            if (IsInsideBound)
            {
                double slope = GetSlope();
                if (slope == 1000000000 || slope == 0)
                {
                    return true;
                }

                double shift = EndPoint.Y - slope * EndPoint.X;
                double y_line = slope * testPoint.X + shift;
                if (Math.Abs(y_line - testPoint.Y) < EPSILON)
                {
                    return true;
                }
            }
            return false;
        }

        public double GetSlope()
        {
            if (Math.Abs(StartPoint.X - EndPoint.X) == 0)
            {
                return 1000000000;
            }
            return (EndPoint.Y - StartPoint.Y) / (double)(EndPoint.X - StartPoint.X);
        }

        public override void Translate(int xAmount, int yAmount)
        {
            StartPoint = new Point(StartPoint.X + xAmount, StartPoint.Y + yAmount);
            EndPoint = new Point(EndPoint.X + xAmount, EndPoint.Y + yAmount);
            OnChange(xAmount, yAmount);
        }
    }
}
