using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class DrawCommand : ICommand
    {
        public string Name { get; set; }
        public ICanvas TargetCanvas { get; set; }
        public List<DrawingObject> selectedObjects { get; set; }
        public List<DrawingObject> previousObjects { get; set; }
        public List<DrawingObject> executedObjects { get; set; }

        private DrawingObject drawingObject;

        public DrawCommand()
        {
            this.Name = "Draw Command";
            Debug.WriteLine(Name);
        }

        public DrawCommand(ICanvas canvas) : this()
        {
            TargetCanvas = canvas;
        }

        public DrawCommand(ICanvas canvas, DrawingObject drawingObject) : this(canvas)
        {
            this.drawingObject = drawingObject;
        }

        public void Execute()
        {
            TargetCanvas.AddDrawingObject(drawingObject);
            TargetCanvas.DeselectAllObject();
            TargetCanvas.AddSelectedObject(drawingObject);
            drawingObject.Select();
            TargetCanvas.UpdateListIndex();
            TargetCanvas.UndoStack.Push(this);
            TargetCanvas.RedoStack.Clear();
        }

        public void Reexecute()
        {
            TargetCanvas.AddDrawingObject(drawingObject);
            TargetCanvas.RemoveSelectedObject(drawingObject);
            TargetCanvas.UpdateListIndex();
            TargetCanvas.UndoStack.Push(this);
        }

        public void Unexecute()
        {
            TargetCanvas.RemoveDrawingObject(drawingObject);
            TargetCanvas.RemoveSelectedObject(drawingObject);
            TargetCanvas.UpdateListIndex();
            TargetCanvas.RedoStack.Push(this);
        }
    }
}
