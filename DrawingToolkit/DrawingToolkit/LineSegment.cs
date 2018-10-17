using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DrawingToolkit
{
    public class LineSegment : DrawingObject
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        private Pen pen;

        public LineSegment()
        {
            this.pen = new Pen(Color.Black, 2.0f)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
            };
        }

        public LineSegment(Point startpoint) : this()
        {
            this.StartPoint = startpoint;
        }

        public LineSegment(Point startpoint, Point endpoint) : this(startpoint)
        {
            this.EndPoint = endpoint;
        }

        public override void Draw()
        {
            if (this.GetGraphics() != null)
            {
                this.GetGraphics().DrawLine(pen, this.StartPoint, this.EndPoint);
            }
        }
    }
}
