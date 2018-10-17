using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public class DefaultCanvas : Control, ICanvas
    {
        private ITool currentActiveTool;
        private List<DrawingObject> drawingObjectList;

        public DefaultCanvas()
        {
            this.drawingObjectList = new List<DrawingObject>();

            this.DoubleBuffered = true;
            this.BackColor = System.Drawing.Color.White;
            this.Dock = DockStyle.Fill;

            this.Paint += DefaultCanvas_Paint;
            this.MouseUp += DefaultCanvas_MouseUp;
            this.MouseDown += DefaultCanvas_MouseDown;
            this.MouseMove += DefaultCanvas_MouseMove;
        }

        private void DefaultCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach(DrawingObject drawingObject in drawingObjectList)
            {
                drawingObject.SetGraphics(e.Graphics);
                drawingObject.Draw();
            }
        }

        private void DefaultCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.currentActiveTool != null)
            {
                this.currentActiveTool.ToolMouseUp(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.currentActiveTool != null)
            {
                this.currentActiveTool.ToolMouseDown(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.currentActiveTool != null)
            {
                this.currentActiveTool.ToolMouseMove(sender, e);
                this.Repaint();
            }
        }

        public ITool GetActiveTool()
        {
            return this.currentActiveTool;
        }

        public void SetActiveTool(ITool tool)
        {
            this.currentActiveTool = tool;
        }

        public void AddDrawingObject(DrawingObject drawingObject)
        {
            this.drawingObjectList.Add(drawingObject);
            this.Repaint();
        }

        public void RemoveDrawingObject(DrawingObject drawingObject)
        {
            this.drawingObjectList.Remove(drawingObject);
            this.Repaint();
        }

        public DrawingObject GetDrawingObject(Point e)
        {
            throw new NotImplementedException();
        }

        public void Repaint()
        {
            this.Invalidate();
            this.Update();
        }
    }
}
