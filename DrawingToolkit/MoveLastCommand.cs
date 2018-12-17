using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class MoveLastCommand : ICommand
    {
        public String Name { get; set; }
        public ICanvas TargetCanvas { get; set; }
        public List<DrawingObject> selectedObjects { get; set; }
        public List<DrawingObject> previousObjects { get; set; }
        public List<DrawingObject> executedObjects { get; set; }

        public MoveLastCommand()
        {
            this.Name = "Move to Last";
        }

        public MoveLastCommand(ICanvas canvas) : this()
        {
            TargetCanvas = canvas;
            selectedObjects = canvas.GetSelectedObject();
            previousObjects = new List<DrawingObject>(canvas.GetObjectList());
        }

        public void Execute()
        {
            TargetCanvas.RemoveObjectsFromList(selectedObjects);
            TargetCanvas.AddObjectsToListBack(selectedObjects);
            TargetCanvas.UpdateListIndex();
            executedObjects = new List<DrawingObject>(TargetCanvas.GetObjectList());
            TargetCanvas.UndoStack.Push(this);
            TargetCanvas.RedoStack.Clear();
        }

        public void Unexecute()
        {
            TargetCanvas.ClearObjectList();
            TargetCanvas.AddObjectsToListBack(this.previousObjects);
            TargetCanvas.UpdateListIndex();
            TargetCanvas.RedoStack.Push(this);
        }

        public void Reexecute()
        {
            TargetCanvas.ClearObjectList();
            TargetCanvas.AddObjectsToListBack(this.executedObjects);
            TargetCanvas.UpdateListIndex();
            TargetCanvas.UndoStack.Push(this);
        }
    }
}
