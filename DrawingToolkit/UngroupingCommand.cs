using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class UngroupingCommand : ICommand
    {
        public string Name { get; set; }
        public ICanvas TargetCanvas { get; set; }
        public List<DrawingObject> selectedObjects { get; set; }
        public List<DrawingObject> previousObjects { get; set; }
        public List<DrawingObject> executedObjects { get; set; }

        public UngroupingCommand()
        {
            this.Name = "Ungrouping Command";
            Debug.WriteLine(Name);
        }

        public UngroupingCommand(ICanvas canvas) : this()
        {
            TargetCanvas = canvas;
            selectedObjects = canvas.GetSelectedObject();
            previousObjects = new List<DrawingObject>(canvas.GetObjectList());
        }

        public void Execute()
        {
            foreach (DrawingObject obj in selectedObjects)
            {
                if (obj.IsComposite())
                {
                    TargetCanvas.RemoveDrawingObject(obj);
                    TargetCanvas.AddObjectsToListBack(obj.GetCompositeObjects());
                }
            }
            TargetCanvas.DeselectAllObject();
            TargetCanvas.UpdateListIndex();
            selectedObjects.Clear();
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
