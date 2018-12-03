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
        
        private List<DrawingObject> SelectedObjectList;

        public SelectionTool()
        {
            this.Name = "Selection Tool";
            this.ToolTipText = "Selection Tool";
            this.Text = "Select";
            this.CheckOnClick = true;
            this.SelectedObjectList = new List<DrawingObject>();
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
            if (e.Button == MouseButtons.Left && canvas != null)
            {
                if (canvas.GetObjectAt(new Point(e.X,e.Y)) == null)
                {
                    canvas.DeselectAllObject();
                    SelectedObject = null;
                    SelectedObjectList.Clear();
                }
            }
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canvas != null)
            {
                StartPoint = new Point(e.X, e.Y);
                SelectedObject = canvas.GetObjectAt(StartPoint);
                if (SelectedObject != null)
                {
                    if (!SelectedObject.isSelected())
                    {
                        canvas.SelectObjectAt(StartPoint);
                        SelectedObjectList.Add(SelectedObject);
                    }
                    else
                    {
                        canvas.DeselectObjectAt(StartPoint);
                        SelectedObjectList.Remove(SelectedObject);
                        SelectedObject = null;
                    }
                }
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canvas != null)
            {
                foreach (DrawingObject obj in SelectedObjectList)
                {
                    if (obj != null)
                    {
                        obj.Translate(e.X - StartPoint.X, e.Y - StartPoint.Y);
                    }
                }
                StartPoint = new Point(e.X, e.Y);
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
        }
        
        public void ToolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.G) // group
            {
                DrawingObject Group = new GroupObject(SelectedObjectList);
                canvas.AddDrawingObject(Group);
                canvas.RemoveObjectsFromList(SelectedObjectList);
                SelectedObjectList.Clear();
                SelectedObjectList.Add(Group);
            }
            else if (e.Control && e.KeyCode == Keys.U) // ungroup
            {
                foreach (DrawingObject obj in SelectedObjectList)
                {
                    if (obj.isComposite())
                    {
                        canvas.RemoveDrawingObject(obj);
                        canvas.AddObjectsToList(obj.GetObjectList());
                    }
                }
                canvas.DeselectAllObject();
                SelectedObject = null;
                SelectedObjectList.Clear();
            }
            else if (e.Control && e.KeyCode == Keys.N) // next
            {

            }
            else if (e.Control && e.KeyCode == Keys.P) // prev
            {

            }
            else if (e.Control && e.KeyCode == Keys.L) //last
            {
                canvas.RemoveObjectsFromList(SelectedObjectList);
                canvas.AddObjectsToList(SelectedObjectList);
            }
            else if (e.Control && e.KeyCode == Keys.F) // first
            {
                canvas.RemoveObjectsFromList(SelectedObjectList);
                List<DrawingObject> OriginalList = canvas.GetObjectList();
                canvas.ClearObjectList();
                canvas.AddObjectsToList(SelectedObjectList);
                canvas.AddObjectsToList(OriginalList);
            }
            else if (e.Control && e.KeyCode == Keys.S) // show
            {

            }
            else if (e.Control && e.KeyCode == Keys.H) // hide
            {

            }
        }

        public void ToolKeyUp(object sender, KeyEventArgs e)
        {
        }
    }
}
