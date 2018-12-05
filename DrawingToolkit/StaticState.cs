using System;
using System.Collections.Generic;
using System.Drawing;
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
            drawingObject.SetPen(Color.Black, 2.0f, System.Drawing.Drawing2D.DashStyle.Solid);
            drawingObject.SetBrushStyle(Color.LightGreen);
            drawingObject.Render();
        }

        public override void Select(DrawingObject drawingObject)
        {
            drawingObject.ChangeState(EditState.GetInstance());
        }
    }
}
