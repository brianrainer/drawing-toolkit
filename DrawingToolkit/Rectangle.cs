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
        }

        public Rectangle(Point StartPoint) : this()
        {
            this.StartPoint = StartPoint;
        }

        public Rectangle(Point StartPoint, Point EndPoint) : this(StartPoint)
        {
            this.EndPoint = EndPoint;
        }

        public override void Render()
        {
            if(GetGraphics() != null)
            {
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
            if ((testPoint.X >= Math.Min(StartPoint.X,EndPoint.X) && 
                testPoint.X <= Math.Max(StartPoint.X,EndPoint.X) && 
                (Math.Abs(testPoint.Y-StartPoint.Y) < EPSILON || 
                Math.Abs(testPoint.Y-EndPoint.Y) < EPSILON)) || 
                (testPoint.Y >= Math.Min(StartPoint.Y,EndPoint.Y) && 
                testPoint.Y <= Math.Max(StartPoint.Y,EndPoint.Y) && 
                (Math.Abs(testPoint.X-StartPoint.X) < EPSILON || 
                Math.Abs(testPoint.X-EndPoint.X) < EPSILON)))
            {
                return true;
            }
            return false;
        }

        public override void Translate(int xAmount, int yAmount)
        {
            StartPoint = new Point(StartPoint.X + xAmount, StartPoint.Y + yAmount);
            EndPoint = new Point(EndPoint.X + xAmount, EndPoint.Y + yAmount);
        }
    }
}
