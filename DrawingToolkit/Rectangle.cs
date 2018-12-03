using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class Rectangle : DrawingObject
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public Rectangle()
        {
            Observers = new List<DrawingObject>();
        }

        public Rectangle(Point StartPoint) : this()
        {
            this.StartPoint = StartPoint;
        }

        public Rectangle(Point StartPoint, Point EndPoint) : this(StartPoint)
        {
            this.EndPoint = EndPoint;
            this.CenterPoint = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
        }

        public override void Render()
        {
            if(GetGraphics() != null)
            {
                GetGraphics().FillRectangle(
                    this.Brush,
                    Math.Min(StartPoint.X, EndPoint.X),
                    Math.Min(StartPoint.Y, EndPoint.Y),
                    Math.Abs(StartPoint.X - EndPoint.X),
                    Math.Abs(StartPoint.Y - EndPoint.Y)
                );
                GetGraphics().DrawRectangle(
                    Pen,
                    Math.Min(StartPoint.X, EndPoint.X),
                    Math.Min(StartPoint.Y, EndPoint.Y),
                    Math.Abs(StartPoint.X - EndPoint.X),
                    Math.Abs(StartPoint.Y - EndPoint.Y)
                );
            }
        }

        public override bool Intersect(Point testPoint)
        {
            bool isOnBorder = 
               (testPoint.X >= Math.Min(StartPoint.X, EndPoint.X) &&
                testPoint.X <= Math.Max(StartPoint.X, EndPoint.X) &&
               (Math.Abs(testPoint.Y - StartPoint.Y) < EPSILON ||
                Math.Abs(testPoint.Y - EndPoint.Y) < EPSILON)) || 
               (testPoint.Y >= Math.Min(StartPoint.Y, EndPoint.Y) &&
                testPoint.Y <= Math.Max(StartPoint.Y, EndPoint.Y) &&
               (Math.Abs(testPoint.X - StartPoint.X) < EPSILON ||
                Math.Abs(testPoint.X - EndPoint.X) < EPSILON ));

            bool isInside = 
                testPoint.X >= Math.Min(StartPoint.X, EndPoint.X) &&
                testPoint.X <= Math.Max(StartPoint.X, EndPoint.X) &&
                testPoint.Y >= Math.Min(StartPoint.Y, EndPoint.Y) &&
                testPoint.Y <= Math.Max(StartPoint.Y, EndPoint.Y) ;

            return isInside;
        }

        public override void Translate(int xAmount, int yAmount)
        {
            OnChange(xAmount, yAmount);
            StartPoint = new Point(StartPoint.X + xAmount, StartPoint.Y + yAmount);
            EndPoint = new Point(EndPoint.X + xAmount, EndPoint.Y + yAmount);
            CenterPoint = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
        }

        public override void Update(Point updatedPoint, int xAmount, int yAmount)
        {
            if (isNear(updatedPoint, CenterPoint))
            {
                StartPoint = new Point(StartPoint.X + xAmount, StartPoint.Y + yAmount);
                EndPoint = new Point(EndPoint.X + xAmount, EndPoint.Y + yAmount);
                CenterPoint = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
            }
        }

        public override void OnChange(int xAmount, int yAmount)
        {
            foreach (DrawingObject observer in Observers)
            {
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
