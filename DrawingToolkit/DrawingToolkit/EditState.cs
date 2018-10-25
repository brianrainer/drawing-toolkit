using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class EditState : DrawingState
    {
        private EditState()
        {

        }

        private static DrawingState instance;
        public static DrawingState GetInstance()
        {
            if (instance == null)
            {
                instance = new EditState();
            }
            return instance;
        }

        public override void Draw(DrawingObject drawingObject)
        {
            drawingObject.RenderOnEditState();
        }

        public override void Deselect(DrawingObject drawingObject)
        {
            drawingObject.ChangeState(StaticState.GetInstance());
        }
    }
}
