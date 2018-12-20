using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class UndoCommand : ICommand
    {
        public string Name { get; set; }
        public ICanvas TargetCanvas { get; set; }
        public List<DrawingObject> selectedObjects { get; set; }
        public List<DrawingObject> previousObjects { get; set; }
        public List<DrawingObject> executedObjects { get; set; }

        public UndoCommand()
        {
            this.Name = "Undo Command";
            Debug.WriteLine(Name);
        }

        public UndoCommand(ICanvas canvas) : this()
        {
            TargetCanvas = canvas;
        }

        public void Execute()
        {
            if (TargetCanvas.UndoStack.Count > 0)
            {
                TargetCanvas.UndoStack.Pop().Unexecute();
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
