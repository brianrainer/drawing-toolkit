using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class Circle : DrawingObject
    {
        public float Radius { get; set; }

        public Circle()
        {
            Observers = new List<DrawingObject>();
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
                GetGraphics().FillEllipse(
                    this.Brush, 
                    CenterPoint.X - Radius, 
                    CenterPoint.Y - Radius, 
                    Radius * 2, 
                    Radius * 2
                );
                GetGraphics().DrawEllipse(
                    Pen, 
                    CenterPoint.X - Radius, 
                    CenterPoint.Y - Radius, 
                    Radius * 2, 
                    Radius * 2
                );
            }
        }

        public override bool Intersect(Point testPoint)
        {
            double testRadius = Math.Pow(testPoint.X - CenterPoint.X, 2) + Math.Pow(testPoint.Y - CenterPoint.Y, 2);
            double minRadius = Math.Pow(Radius - EPSILON, 2);
            double maxRadius = Math.Pow(Radius + EPSILON, 2);

            bool isOnBorder = testRadius >= minRadius && testRadius <= maxRadius;
            bool isInside = testRadius <= maxRadius;

            return isInside;
        }

        public override void Translate(int xAmount, int yAmount)
        {
            OnChange(xAmount, yAmount);
            CenterPoint = new Point(CenterPoint.X + xAmount, CenterPoint.Y + yAmount);
        }

        public override void Update(Point updatedPoint, int xAmount, int yAmount)
        {
            if (isNear(updatedPoint, CenterPoint))
            {
                CenterPoint = new Point(CenterPoint.X + xAmount, CenterPoint.Y + yAmount);
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
