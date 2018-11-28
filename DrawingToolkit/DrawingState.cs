using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public abstract class DrawingState
    {
        public abstract void Draw(DrawingObject drawingObject);

        public virtual void Deselect(DrawingObject drawingObject)
        {
            // default implementation is no state transition
        }

        public virtual void Select(DrawingObject drawingObject)
        {
            // default implementation is no state transition
        }
    }
}
