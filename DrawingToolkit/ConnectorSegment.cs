using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class ConnectorSegment : DrawingObject
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public ConnectorSegment()
        {
            this.Name = "Connector";
        }

        public ConnectorSegment(Point startpoint) : this()
        {
            StartPoint = startpoint;
        }

        public ConnectorSegment(Point startpoint, Point endpoint) : this(startpoint)
        {
            EndPoint = endpoint;
        }

        public override void Render()
        {
            if (GetGraphics() != null)
            {
                GetGraphics().DrawLine(this.GetPen(), StartPoint, EndPoint);
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
            if (Math.Abs(EndPoint.X-StartPoint.X) == 0)
            {
                return 1000000000;
            }
            return (EndPoint.Y - StartPoint.Y) / (double)(EndPoint.X - StartPoint.X);
        }

        public override void Translate(int xAmount, int yAmount)
        {
            // do nothing
        }

        public override void UpdateObserver(DrawingObject sender, int xAmount, int yAmount)
        {
            List<Tuple<Point, DrawingObject>> TmpRemove = new List<Tuple<Point, DrawingObject>>();
            List<Tuple<Point, DrawingObject>> TmpAdd = new List<Tuple<Point, DrawingObject>>();

            foreach (Tuple<Point, DrawingObject> tupl in GetObserverList())
            {
                Point contactPoint = tupl.Item1;
                DrawingObject obj = tupl.Item2;
                if (obj == sender)
                {
                    if (IsNear(contactPoint, StartPoint))
                    {
                        StartPoint = new Point(StartPoint.X + xAmount, StartPoint.Y + yAmount);
                        contactPoint = StartPoint;
                    }
                    else if (IsNear(contactPoint, EndPoint))
                    {
                        EndPoint = new Point(EndPoint.X + xAmount, EndPoint.Y + yAmount);
                        contactPoint = EndPoint;
                    }
                    else
                    {
                        // calculate angle, scale, then move point start or end to respective point
                    }
                    TmpRemove.Add(tupl);
                    TmpAdd.Add(new Tuple<Point, DrawingObject>(contactPoint, obj));
                }
            }
            foreach (Tuple<Point, DrawingObject> tupl in TmpRemove)
            {
                RemoveObserver(tupl);
            }
            foreach (Tuple<Point, DrawingObject> tupl in TmpAdd)
            {
                AddObserver(tupl.Item2, tupl.Item1);
            }
        }

        public override void OnChange(int xAmount, int yAmount)
        {
            // do nothing
        }
    }
}
