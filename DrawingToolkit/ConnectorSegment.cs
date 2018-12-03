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
            Observers = new List<DrawingObject>();
        }

        public ConnectorSegment(Point startpoint) : this()
        {
            StartPoint = startpoint;
        }

        public ConnectorSegment(Point startpoint, Point endpoint) : this(startpoint)
        {
            EndPoint = endpoint;
            CenterPoint = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y)/2);
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
            // do nothing
        }

        public override void Update(Point updatedPoint, int xAmount, int yAmount)
        {
            if (isNear(StartPoint,updatedPoint))
            {
                StartPoint = new Point(StartPoint.X + xAmount, StartPoint.Y + yAmount);
                CenterPoint = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
            }
            if (isNear(EndPoint, updatedPoint))
            {
                EndPoint = new Point(EndPoint.X + xAmount, EndPoint.Y + yAmount);
                CenterPoint = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
            }
        }

        public override void OnChange(int xAmount, int yAmount)
        {
            foreach (DrawingObject observer in Observers)
            {
                observer.Update(StartPoint, xAmount, yAmount);
                observer.Update(EndPoint, xAmount, yAmount);
                observer.Update(CenterPoint, xAmount, yAmount);
            }
        }

        public override void AddObserver(DrawingObject observer)
        {
            Observers.Add(observer);
        }

        public override void RemoveObserver(DrawingObject observer)
        {
            Observers.Remove(observer);
        }

    }
}
