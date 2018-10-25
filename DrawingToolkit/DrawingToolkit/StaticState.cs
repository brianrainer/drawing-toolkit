using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class StaticState : DrawingState
    {
        private StaticState()
        {

        }

        private static DrawingState instance;
        public static DrawingState GetInstance()
        {
            if (instance == null)
            {
                instance = new StaticState();
            }
            return instance;
        }

        public override void Draw(DrawingObject drawingObject)
        {
            drawingObject.RenderOnStaticState();
        }

        public override void Select(DrawingObject drawingObject)
        {
            drawingObject.ChangeState(EditState.GetInstance());
        }
    }
}
