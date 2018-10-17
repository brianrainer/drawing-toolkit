using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class Rectangle : DrawingObject
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        private Pen pen;

        public Rectangle()
        {
            this.pen = new Pen(Color.Black, 2.0f)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
            };
        }

        public Rectangle(Point StartPoint) : this()
        {
            this.StartPoint = StartPoint;
        }

        public Rectangle(Point StartPoint, Point EndPoint) : this(StartPoint)
        {
            this.EndPoint = EndPoint;
        }

        public override void Draw()
        {
            if(this.GetGraphics() != null)
            {
                this.GetGraphics().DrawRectangle(
                    pen, 
                    Math.Min(StartPoint.X, EndPoint.X), 
                    Math.Min(StartPoint.Y, EndPoint.Y), 
                    Math.Abs(StartPoint.X - EndPoint.X), 
                    Math.Abs(StartPoint.Y - EndPoint.Y)
                    );
            }
        }
    }
}
