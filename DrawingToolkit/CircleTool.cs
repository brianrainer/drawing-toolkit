using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public class CircleTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private Circle ellipse;

        public CircleTool()
        {
            this.Name = "Circle Tool";
            this.ToolTipText = "Circle Tool";
            this.Text = "Circle";
            this.CheckOnClick = true;
        }

        public Cursor cursor => Cursors.Arrow;
        public ICanvas TargetCanvas { get => GetCanvas(); set => this.SetCanvas(value); }
        public ICommand command { get; set; }

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
                ellipse = new Circle(new System.Drawing.Point(e.X, e.Y));
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                canvas.RemoveDrawingObject(ellipse);
                Point EndPoint = new Point(e.X, e.Y);
                ellipse = new Circle(ellipse.CenterPoint, EndPoint);
                canvas.AddDrawingObject(ellipse);
                canvas.UpdateListIndex();
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point EndPoint = new Point(e.X, e.Y);
                canvas.RemoveDrawingObject(ellipse);

                if (Math.Abs(ellipse.CenterPoint.X - EndPoint.X) > ellipse.GetEpsilon() 
                    || Math.Abs(ellipse.CenterPoint.Y - EndPoint.Y) > ellipse.GetEpsilon())
                {
                    ellipse = new Circle(ellipse.CenterPoint, EndPoint);
                    command = new DrawCommand(canvas, ellipse);
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
