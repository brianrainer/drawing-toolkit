using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class HideCommand : ICommand
    {
        public string Name { get; set; }
        public ICanvas TargetCanvas { get; set; }
        public List<DrawingObject> selectedObjects { get; set; }
        public List<DrawingObject> previousObjects { get; set; }
        public List<DrawingObject> executedObjects { get; set; }

        public HideCommand()
        {
            this.Name = "Hide Command";
            Debug.WriteLine(Name);
        }

        public HideCommand(ICanvas canvas) : this()
        {
            TargetCanvas = canvas;
            selectedObjects = canvas.GetSelectedObject();
            executedObjects = new List<DrawingObject>(selectedObjects);
        }

        public void Execute()
        {
            foreach (DrawingObject obj in selectedObjects)
            {
                obj.Hide();
            }
            selectedObjects.Clear();
            TargetCanvas.UndoStack.Push(this);
            TargetCanvas.RedoStack.Clear();
        }

        public void Reexecute()
        {
            foreach (DrawingObject obj in executedObjects)
            {
                obj.Hide();
            }
            TargetCanvas.UndoStack.Push(this);
        }

        public void Unexecute()
        {
            foreach(DrawingObject obj in executedObjects)
            {
                obj.Show();
            }
            TargetCanvas.RedoStack.Push(this);
        }
    }
}
