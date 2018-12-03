using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class PreviewState : DrawingState
    {
        private PreviewState()
        {

        }

        private static DrawingState instance;
        public static DrawingState GetInstance()
        {
            if (instance == null)
            {
                instance = new PreviewState();
            }
            return instance;
        }

        public override void Draw(DrawingObject drawingObject)
        {
            drawingObject.SetPenStyle(Color.Red, 2.0f, System.Drawing.Drawing2D.DashStyle.Dot);
            drawingObject.SetBrushStyle(Color.PaleVioletRed);
            drawingObject.Render();
        }

        public override void Select(DrawingObject drawingObject)
        {
            drawingObject.ChangeState(EditState.GetInstance());
        }
    }
}
