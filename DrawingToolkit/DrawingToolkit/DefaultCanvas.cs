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
        private List<DrawingObject> DrawingObjectList;

        public DefaultCanvas()
        {
            this.DrawingObjectList = new List<DrawingObject>();

            this.DoubleBuffered = true;
            this.BackColor = System.Drawing.Color.White;
            this.Dock = DockStyle.Fill;

            this.Paint += DefaultCanvas_Paint;
            this.MouseUp += DefaultCanvas_MouseUp;
            this.MouseDown += DefaultCanvas_MouseDown;
            this.MouseMove += DefaultCanvas_MouseMove;
            this.MouseClick += DefaultCanvas_MouseClick;
            this.MouseDoubleClick += DefaultCanvas_MouseDoubleClick;
        }

        private void DefaultCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach(DrawingObject drawingObject in DrawingObjectList)
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

        private void DefaultCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.currentActiveTool != null)
            {
                this.currentActiveTool.ToolMouseClick(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.currentActiveTool != null)
            {
                this.currentActiveTool.ToolMouseDoubleClick(sender, e);
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
            this.DrawingObjectList.Add(drawingObject);
            this.Repaint();
        }

        public void RemoveDrawingObject(DrawingObject drawingObject)
        {
            this.DrawingObjectList.Remove(drawingObject);
            this.Repaint();
        }

        public DrawingObject GetObjectAt(Point e)
        {
            foreach (DrawingObject drawingObject in DrawingObjectList)
            {
                if (drawingObject.Intersect(e))
                {
                    return drawingObject;
                }
            }
            return null;
        }

        public void Repaint()
        {
            this.Invalidate();
            this.Update();
        }

        public DrawingObject SelectObjectAt(Point e)
        {
            DrawingObject drawingObject = GetObjectAt(e);
            if (drawingObject != null)
            {
                drawingObject.Select();
            }
            return drawingObject;
        }

        public void DeselectObjectAt(Point e)
        {
            DrawingObject drawingObject = GetObjectAt(e);
            if (drawingObject != null)
            {
                drawingObject.Deselect();
            }
        }

        public void DeselectAllObject()
        {
            foreach (DrawingObject drawingObject in DrawingObjectList)
            {
                drawingObject.Deselect();
            }
        }

        public List<DrawingObject> GetObjectList()
        {
            return DrawingObjectList;
        }
    }
}
