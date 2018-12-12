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

            this.KeyDown += DefaultCanvas_KeyDown;
            this.KeyUp += DefaultCanvas_KeyUp;
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

        private void DefaultCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.currentActiveTool != null)
            {
                this.currentActiveTool.ToolKeyDown(sender, e);
                this.Repaint();
            }
        }

        private void DefaultCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.currentActiveTool != null)
            {
                this.currentActiveTool.ToolKeyUp(sender, e);
                this.Repaint();
            }
        }

        public void Repaint()
        {
            this.Invalidate();
            this.Update();
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

        public void AddObjectsToListBack(List<DrawingObject> drawingObjectList)
        {
            foreach (DrawingObject obj in drawingObjectList)
            {
                this.DrawingObjectList.Add(obj);
                this.Repaint();
            }
        }

        public void AddObjectsToListFirst(List<DrawingObject> drawingObjectList)
        {
            List<DrawingObject> tmpList = new List<DrawingObject>(drawingObjectList);
            foreach (DrawingObject obj in DrawingObjectList)
            {
                tmpList.Add(obj);
            }
            DrawingObjectList = new List<DrawingObject>(tmpList);
            this.Repaint();
        }

        public void RemoveDrawingObject(DrawingObject drawingObject)
        {
            this.DrawingObjectList.Remove(drawingObject);
            this.Repaint();
        }

        public void RemoveObjectsFromList(List<DrawingObject> drawingObjectList)
        {
            foreach(DrawingObject obj in drawingObjectList)
            {
                DrawingObjectList.Remove(obj);
                this.Repaint();
            }
        }

        public void ClearObjectList()
        {
            DrawingObjectList.Clear();
        }

        public DrawingObject GetObjectAt(Point e)
        {
            for(int i=DrawingObjectList.Count-1; i>=0; i--)
            {
                DrawingObject drawingObject = DrawingObjectList[i];
                if (drawingObject.Intersect(e))
                {
                    return drawingObject;
                }
            }
            return null;
        }

        public List<DrawingObject> GetObjectList()
        {
            return DrawingObjectList;
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

        public List<DrawingObject> SelectAllObject()
        {
            foreach (DrawingObject obj in DrawingObjectList)
            {
                obj.Select();
            }
            return DrawingObjectList;
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

        public void UpdateListIndex()
        {
            for(int i=0;i<DrawingObjectList.Count; i++)
            {
                DrawingObjectList[i].Index = i;
            }
        }
    }
}
