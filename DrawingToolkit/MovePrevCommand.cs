using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class MovePrevCommand : ICommand
    {
        public String Name { get; set; }
        public ICanvas TargetCanvas { get; set; }
        public List<DrawingObject> selectedObjects { get; set; }
        public List<DrawingObject> previousObjects { get; set; }
        public List<DrawingObject> executedObjects { get; set; }

        public MovePrevCommand()
        {
            this.Name = "Move Prev";
        }

        public MovePrevCommand(ICanvas canvas): this()
        {
            TargetCanvas = canvas;
            selectedObjects = canvas.GetSelectedObject();
            previousObjects = new List<DrawingObject>(canvas.GetObjectList());
        }

        public void Execute()
        {
            List<int> IndexList = new List<int>();
            foreach (DrawingObject obj in selectedObjects)
            {
                IndexList.Add(obj.Index);
            }

            TargetCanvas.RemoveObjectsFromList(selectedObjects);
            for (int i = 0; i < selectedObjects.Count; i++)
            {
                TargetCanvas.AddDrawingObjectAtIndex(IndexList[i] - 1, selectedObjects[i]);
            }

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
