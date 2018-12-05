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
        public Point CenterPoint { get; set; }

        public Circle()
        {
            Radius = 0;
        }

        public Circle(Point CenterPoint) : this()
        {
            this.CenterPoint = CenterPoint;
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
                    GetBrush(), 
                    CenterPoint.X - Radius, 
                    CenterPoint.Y - Radius, 
                    Radius * 2, 
                    Radius * 2
                );
                GetGraphics().DrawEllipse(
                    GetPen(), 
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
            CenterPoint = new Point(CenterPoint.X + xAmount, CenterPoint.Y + yAmount);
            OnChange(xAmount, yAmount);
        }
    }
}
