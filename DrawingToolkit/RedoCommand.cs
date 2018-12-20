using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class RedoCommand : ICommand
    {
        public string Name { get; set; }
        public ICanvas TargetCanvas { get; set; }
        public List<DrawingObject> selectedObjects { get; set; }
        public List<DrawingObject> previousObjects { get; set; }
        public List<DrawingObject> executedObjects { get; set; }

        public RedoCommand()
        {
            this.Name = "Undo Command";
            Debug.WriteLine(Name);
        }

        public RedoCommand(ICanvas canvas) : this()
        {
            TargetCanvas = canvas;
        }

        public void Execute()
        {
            if (TargetCanvas.RedoStack.Count > 0)
            {
                TargetCanvas.RedoStack.Pop().Reexecute();
            }
        }

        public void Reexecute()
        {
        }

        public void Unexecute()
        {
        }
    }
}
