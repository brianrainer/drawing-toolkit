using System;
using System.Collections.Generic;
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
        }

        public HideCommand(ICanvas canvas) : this()
        {
            TargetCanvas = canvas;
        }

        public HideCommand(ICanvas canvas, List<DrawingObject> selected) : this(canvas)
        {
            selectedObjects = selected;
            executedObjects = new List<DrawingObject>(selected);
        }

        public void Execute()
        {
            foreach (DrawingObject obj in selectedObjects)
            {
                obj.Hide();
            }
            selectedObjects.Clear();
        }

        public void Reexecute()
        {
            foreach (DrawingObject obj in executedObjects)
            {
                obj.Hide();
            }
        }

        public void Unexecute()
        {
            foreach(DrawingObject obj in executedObjects)
            {
                obj.Show();
            }
        }
    }
}
