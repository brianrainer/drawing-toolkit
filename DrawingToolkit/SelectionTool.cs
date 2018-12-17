using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void UpdateList()
        {
            SelectedObjectList = SelectedObjectList.OrderBy(o => o.Index).ToList();
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
                    if (!SelectedObject.IsSelected())
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
                UpdateList();
                ICommand command = new GroupingCommand(canvas, SelectedObjectList);
                canvas.UndoStack.Push(command);
                canvas.RedoStack.Clear();
                command.Execute();
            }
            else if (e.Control && e.KeyCode == Keys.U) // ungroup
            {
                UpdateList();
                ICommand command = new UngroupingCommand(canvas, SelectedObjectList);
                canvas.UndoStack.Push(command);
                canvas.RedoStack.Clear();
                command.Execute();
            }
            else if (e.Control && e.KeyCode == Keys.N) // next
            {
                UpdateList();

                List<int> IndexList = new List<int>();
                foreach (DrawingObject obj in SelectedObjectList)
                {
                    IndexList.Add(obj.Index);
                }

                canvas.RemoveObjectsFromList(SelectedObjectList);
                for (int i=0; i<SelectedObjectList.Count; i++)
                {
                    canvas.AddDrawingObjectAtIndex(IndexList[i]+1, SelectedObjectList[i]);
                }
                canvas.UpdateListIndex();
            }
            else if (e.Control && e.KeyCode == Keys.P) // prev
            {
                UpdateList();

                List<int> IndexList = new List<int>();
                foreach (DrawingObject obj in SelectedObjectList)
                {
                    IndexList.Add(obj.Index);
                }

                canvas.RemoveObjectsFromList(SelectedObjectList);
                for (int i=0; i<SelectedObjectList.Count; i++)
                {
                    canvas.AddDrawingObjectAtIndex(IndexList[i]-1, SelectedObjectList[i]);
                }

                canvas.UpdateListIndex();
            }
            else if (e.Control && e.KeyCode == Keys.L) //last
            {
                UpdateList();
                canvas.RemoveObjectsFromList(SelectedObjectList);
                canvas.AddObjectsToListBack(SelectedObjectList);
                canvas.UpdateListIndex();
            }
            else if (e.Control && e.KeyCode == Keys.F) // first
            {
                UpdateList();
                canvas.RemoveObjectsFromList(SelectedObjectList);
                canvas.AddObjectsToListFirst(SelectedObjectList);
                canvas.UpdateListIndex();
            }
            else if (e.Control && e.KeyCode == Keys.D) // Delete
            {
                ICommand command = new HideCommand(canvas, SelectedObjectList);
                canvas.UndoStack.Push(command);
                canvas.RedoStack.Clear();
                command.Execute();
            }
            else if (e.Control && e.KeyCode == Keys.Z) // undo
            {
                if (canvas.UndoStack.Count != 0)
                {
                    ICommand TopCommand = canvas.UndoStack.Pop();
                    canvas.RedoStack.Push(TopCommand);
                    TopCommand.Unexecute();
                }
            }
            else if (e.Control && e.KeyCode == Keys.Y) // redo
            {
                if (canvas.RedoStack.Count != 0)
                {
                    ICommand TopCommand = canvas.RedoStack.Pop();
                    canvas.UndoStack.Push(TopCommand);
                    TopCommand.Reexecute();
                }
            }
        }

        public void ToolKeyUp(object sender, KeyEventArgs e)
        {
        }
    }
}
