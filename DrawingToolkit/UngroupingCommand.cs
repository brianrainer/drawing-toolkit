using System;
using System.Collections.Generic;
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
        }

        public UngroupingCommand(ICanvas canvas) : this()
        {
            TargetCanvas = canvas;
            previousObjects = new List<DrawingObject>(canvas.GetObjectList());
        }

        public UngroupingCommand(ICanvas canvas, List<DrawingObject> selected) : this(canvas)
        {
            selectedObjects = selected;
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
        }

        public void Unexecute()
        {
            TargetCanvas.ClearObjectList();
            TargetCanvas.AddObjectsToListBack(this.previousObjects);
            TargetCanvas.UpdateListIndex();
        }

        public void Reexecute()
        {
            TargetCanvas.ClearObjectList();
            TargetCanvas.AddObjectsToListBack(this.executedObjects);
            TargetCanvas.UpdateListIndex();
        }
    }
}
