using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    class Circle : DrawingObject
    {
        public Point CenterPoint { get; set; }
        public float Radius { get; set; }

        public List<DrawingObject> Observers;

        public Circle()
        {
            this.Observers = new List<DrawingObject>();
        }

        public Circle(Point CenterPoint) : this()
        {
            this.CenterPoint = CenterPoint;
            Radius = 0;
        }

        public Circle(Point CenterPoint, Point radiusPoint) : this(CenterPoint)
        {
            Radius = (float)Math.Sqrt(Math.Pow(CenterPoint.X - radiusPoint.X, 2) + Math.Pow(CenterPoint.Y - radiusPoint.Y, 2));
        }

        public override void Render()
        {
            if (GetGraphics() != null)
            {
                GetGraphics().DrawEllipse(Pen, CenterPoint.X - Radius, CenterPoint.Y - Radius, Radius * 2, Radius * 2);
            }
        }

        public override bool Intersect(Point testPoint)
        {
            double testRadius = Math.Pow(testPoint.X - CenterPoint.X, 2) + Math.Pow(testPoint.Y - CenterPoint.Y, 2);
            double minRadius = Math.Pow(Radius - EPSILON, 2);
            double maxRadius = Math.Pow(Radius + EPSILON, 2);
            if (testRadius >= minRadius && testRadius <= maxRadius)
            {
                return true;
            }
            return false;
        }

        public override void Translate(int xAmount, int yAmount)
        {
            CenterPoint = new Point(CenterPoint.X + xAmount, CenterPoint.Y + yAmount);
        }

        public override void OnUpdate(DrawingObject sender, Point point)
        {
            this.Translate(point.X, point.Y);   
        }

        public override void OnChange(DrawingObject sender, Point point)
        {
            foreach (DrawingObject observer in Observers)
            {
                observer.OnUpdate(sender, point);
            }
        }

        public override void AddObserver(DrawingObject observer)
        {
            this.Observers.Add(observer);
        }

        public override void RemoveObserver(DrawingObject observer)
        {
            this.Observers.Remove(observer);
        }

    }
}
