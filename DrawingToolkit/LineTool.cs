using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public class LineTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private LineSegment lineSegment;

        public LineTool()
        {
            this.Name = "Line Tool";
            this.ToolTipText = "Line Tool";
            this.CheckOnClick = true;
            this.Text = "Line";
        }

        public Cursor cursor => Cursors.Arrow;
        public ICommand command { get; set; }
        public ICanvas TargetCanvas { get => GetCanvas(); set => this.SetCanvas(value); }

        private ICanvas GetCanvas()
        {
            return canvas;
        }

        private void SetCanvas(ICanvas value)
        {
            canvas = value;
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lineSegment = new LineSegment(new Point(e.X, e.Y));
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                canvas.RemoveDrawingObject(lineSegment);
                Point endPoint = new Point(e.X, e.Y);
                lineSegment = new LineSegment(lineSegment.StartPoint, endPoint);
                canvas.AddDrawingObject(lineSegment);
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point endPoint = new Point(e.X, e.Y);
                canvas.RemoveDrawingObject(lineSegment);

                if (Math.Abs(lineSegment.StartPoint.X - endPoint.X) > lineSegment.GetEpsilon() || 
                    Math.Abs(lineSegment.StartPoint.Y - endPoint.Y) > lineSegment.GetEpsilon())
                {
                    lineSegment = new LineSegment(lineSegment.StartPoint, endPoint);
                    command = new DrawCommand(canvas, lineSegment);
                    command.Execute();
                }
            }
        }

        public void ToolMouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        public void ToolMouseClick(object sender, MouseEventArgs e)
        {
        }

        public void ToolKeyDown(object sender, KeyEventArgs e)
        {
        }

        public void ToolKeyUp(object sender, KeyEventArgs e)
        {
        }
    }
}
