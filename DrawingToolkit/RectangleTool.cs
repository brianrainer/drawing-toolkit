using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public class RectangleTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private Rectangle rectangle;

        public RectangleTool()
        {
            this.Name = "Rectangle Tool";
            this.ToolTipText = "Rectangle Tool";
            this.Text = "Rectangle";
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
                rectangle = new Rectangle(new System.Drawing.Point(e.X, e.Y));
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                canvas.RemoveDrawingObject(rectangle);
                Point EndPoint = new Point(e.X, e.Y);
                rectangle = new Rectangle(rectangle.StartPoint, EndPoint);
                canvas.AddDrawingObject(rectangle);
                canvas.UpdateListIndex();
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point EndPoint = new Point(e.X, e.Y);
                canvas.RemoveDrawingObject(rectangle);
                canvas.UpdateListIndex();

                if (Math.Abs(rectangle.StartPoint.X - EndPoint.X) > rectangle.GetEpsilon() || 
                    Math.Abs(rectangle.StartPoint.Y - EndPoint.Y) > rectangle.GetEpsilon() )
                {
                    rectangle = new Rectangle(rectangle.StartPoint, EndPoint);
                    command = new DrawCommand(canvas, rectangle);
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
