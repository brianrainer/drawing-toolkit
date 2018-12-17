using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public interface ICommand
    {
        String Name { get; set; }
        ICanvas TargetCanvas { get; set; }
        List<DrawingObject> selectedObjects { get; set; }
        List<DrawingObject> previousObjects { get; set; }
        List<DrawingObject> executedObjects { get; set; }

        void Execute();
        void Unexecute();
        void Reexecute();
    }
}
