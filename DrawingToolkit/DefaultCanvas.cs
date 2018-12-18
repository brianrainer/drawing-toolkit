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
    public class DefaultCanvas : Control, ICanvas
    {
        private ITool currentActiveTool;
        private List<DrawingObject> DrawingObjectList;
        private List<DrawingObject> SelectedObjectList;
        private ICommand command;

        public TextBox TextBox { get; set; }
        public Stack<ICommand> UndoStack { get; set; }
        public Stack<ICommand> RedoStack { get; set; }

        public DefaultCanvas()
        {
            this.DrawingObjectList = new List<DrawingObject>();
            this.SelectedObjectList = new List<DrawingObject>();
            this.UndoStack = new Stack<ICommand>();
            this.RedoStack = new Stack<ICommand>();

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

        public void RefreshTextBox()
        {
            if (this.TextBox == null)
            {
                this.TextBox = new TextBox()
                {
                    Multiline = true,
                    ReadOnly = true
                };
            }
            
            this.TextBox.Text = "Drawing Object List\n\n";
            foreach (DrawingObject obj in DrawingObjectList)
            {
                this.TextBox.AppendText(obj.Index + " " + obj.Name + "\n\n");
            }

            this.TextBox.Refresh();
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
            if (e.Control && e.KeyCode == Keys.Z) // undo
            {
                if (UndoStack.Count != 0)
                {
                    command = UndoStack.Pop();
                    Debug.WriteLine(command.Name);
                    command.Unexecute();
                }
            }
            else if (e.Control && e.KeyCode == Keys.Y) // redo
            {
                if (RedoStack.Count != 0)
                {
                    command = RedoStack.Pop();
                    Debug.WriteLine(command.Name);
                    command.Reexecute();
                }
            }
            else if (SelectedObjectList.Count != 0)
            {
                if (e.Control && e.KeyCode == Keys.G) // group
                {
                    command = new GroupingCommand(this);
                    command.Execute();
                }
                else if (e.Control && e.KeyCode == Keys.U) // ungroup
                {
                    command = new UngroupingCommand(this);
                    command.Execute();
                }
                else if (e.Control && e.KeyCode == Keys.N) // next
                {
                    command = new MoveNextCommand(this);
                    command.Execute();
                }
                else if (e.Control && e.KeyCode == Keys.P) // prev
                {
                    command = new MovePrevCommand(this);
                    command.Execute();
                }
                else if (e.Control && e.KeyCode == Keys.L) //last
                {
                    command = new MoveLastCommand(this);
                    command.Execute();
                }
                else if (e.Control && e.KeyCode == Keys.F) // first
                {
                    command = new MoveFirstCommand(this);
                    command.Execute();
                }
                else if (e.Control && e.KeyCode == Keys.H) // hide
                {
                    command = new HideCommand(this);
                    command.Execute();
                }
            }
            this.Repaint();
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
            this.UpdateListIndex();
        }

        public void AddDrawingObjectAtIndex(int index, DrawingObject drawingObject)
        {
            if (index < 0)
            {
                List<DrawingObject> tmpList = new List<DrawingObject>();
                tmpList.Add(drawingObject);
                foreach (DrawingObject obj in DrawingObjectList)
                {
                    tmpList.Add(obj);
                }
                DrawingObjectList = new List<DrawingObject>(tmpList);
            }
            else if (index >= DrawingObjectList.Count)
            {
                this.DrawingObjectList.Add(drawingObject);
            }
            else
            {
                this.DrawingObjectList.Insert(index, drawingObject);
            }
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
                if (drawingObject.IsShown() &&  drawingObject.Intersect(e))
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

        public void SelectObjectAt(Point e)
        {
            DrawingObject drawingObject = GetObjectAt(e);
            if (drawingObject != null)
            {
                drawingObject.Select();
                SelectedObjectList.Add(drawingObject);
            }
        }

        public void SelectAllObject()
        {
            foreach (DrawingObject obj in DrawingObjectList)
            {
                obj.Select();
            }
        }

        public void DeselectObjectAt(Point e)
        {
            DrawingObject drawingObject = GetObjectAt(e);
            if (drawingObject != null)
            {
                drawingObject.Deselect();
                SelectedObjectList.Remove(drawingObject);
            }
        }

        public void DeselectAllObject()
        {
            foreach (DrawingObject drawingObject in DrawingObjectList)
            {
                drawingObject.Deselect();
            }
            SelectedObjectList.Clear();
        }

        public void UpdateListIndex()
        {
            for(int i=0;i<DrawingObjectList.Count; i++)
            {
                DrawingObjectList[i].Index = i;
            }

            this.UpdateSelectedByIndex();
            this.RefreshTextBox();
        }

        public List<DrawingObject> GetSelectedObject()
        {
            return SelectedObjectList;
        }

        public void AddSelectedObject(DrawingObject drawingObject)
        {
            SelectedObjectList.Add(drawingObject);
        }

        public void RemoveSelectedObject(DrawingObject drawingObject)
        {
            SelectedObjectList.Remove(drawingObject);
        }

        public void UpdateSelectedByIndex()
        {
            SelectedObjectList = SelectedObjectList.OrderBy(o => o.Index).ToList();
        }
    }
}
