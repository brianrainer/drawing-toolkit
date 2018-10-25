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
            SetPenStyle(Color.Black, 2.0f, DashStyle.Solid);
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
            if (testPoint.X >= StartPoint.X && testPoint.X <= EndPoint.X && testPoint.Y >= StartPoint.Y && testPoint.Y <= EndPoint.Y)
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


        public override void RenderOnPreviewState()
        {
            SetPenStyle(Color.Red, 2.0f, DashStyle.Dot);
            Draw();
        }

        public override void RenderOnEditState()
        {
            SetPenStyle(Color.Blue, 2.0f, DashStyle.Solid);
            Draw();
        }

        public override void RenderOnStaticState()
        {
            SetPenStyle(Color.Black, 2.0f, DashStyle.Solid);
            Draw();
        }

        public override bool Add(DrawingObject drawingObject)
        {
            return false;
        }

        public override bool Remove(DrawingObject drawingObject)
        {
            return false;
        }
    }
}
