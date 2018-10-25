using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public class SelectionTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private DrawingObject SelectedObject;
        private Point StartPoint;

        public SelectionTool()
        {
            this.Name = "Selection Tool";
            this.ToolTipText = "Selection Tool";
            this.Text = "Select";
            this.CheckOnClick = true;
        }

        public Cursor cursor => Cursors.Arrow;

        public ICanvas TargetCanvas { get => GetCanvas(); set => SetCanvas(value); }

        private ICanvas GetCanvas()
        {
            return this.canvas;
        }

        private void SetCanvas(ICanvas value)
        {
            this.canvas = value;
        }

        public void ToolMouseClick(object sender, MouseEventArgs e)
        {
        }

        public void ToolMouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            StartPoint = new Point(e.X, e.Y);

            if (e.Button == MouseButtons.Left && canvas != null)
            {
                canvas.DeselectAllObject();
                SelectedObject = canvas.SelectObjectAt(StartPoint);
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canvas != null)
            {
                if (SelectedObject != null)
                {
                    SelectedObject.Translate(e.X - StartPoint.X, e.Y - StartPoint.Y);
                    StartPoint = new Point(e.X, e.Y);
                }
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
        }
    }
}
