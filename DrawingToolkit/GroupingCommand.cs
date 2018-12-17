using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class GroupingCommand : ICommand
    {
        public String Name { get; set; }
        public ICanvas TargetCanvas { get; set; }
        public List<DrawingObject> selectedObjects { get; set; }
        public List<DrawingObject> previousObjects { get; set; }
        public List<DrawingObject> executedObjects { get; set; }

        public GroupingCommand()
        {
            this.Name = "Grouping Command";
        }

        public GroupingCommand(ICanvas canvas) : this()
        {
            this.TargetCanvas = canvas;
            this.previousObjects = new List<DrawingObject>(TargetCanvas.GetObjectList());
        }

        public GroupingCommand(ICanvas canvas, List<DrawingObject> selected) : this(canvas)
        {
            this.selectedObjects = selected;
        }

        public void Execute()
        {
            DrawingObject Group = new GroupObject(selectedObjects);
            TargetCanvas.AddDrawingObject(Group);
            TargetCanvas.RemoveObjectsFromList(selectedObjects);
            selectedObjects.Clear();
            selectedObjects.Add(Group);
            TargetCanvas.UpdateListIndex();
            this.executedObjects = new List<DrawingObject>(TargetCanvas.GetObjectList());
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
