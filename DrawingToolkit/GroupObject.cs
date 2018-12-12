using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public class GroupObject : DrawingObject
    {
        public GroupObject(LinkedList<DrawingObject> drawingObjects)
        {
            foreach (DrawingObject obj in drawingObjects)
            {
                AddComposite(obj);
            }
        }

        public override bool IsComposite()
        {
            return true;
        }

        public override void ChangeState(DrawingState state)
        {
            if (GetCompositeObjects() != null)
            {
                foreach (DrawingObject obj in GetCompositeObjects())
                {
                    obj.ChangeState(state);
                }
                this.State = state;
            }
        }

        public override void Draw()
        {
            if (GetCompositeObjects() != null)
            {
                foreach (DrawingObject obj in GetCompositeObjects())
                {
                    obj.SetGraphics(this.GetGraphics());
                    obj.Draw();
                }
            }
        }

        public override bool Intersect(Point testPoint)
        {
            if (GetCompositeObjects() != null)
            {
                foreach (DrawingObject obj in GetCompositeObjects())
                {
                    if (obj.Intersect(testPoint))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override void Translate(int xAmount, int yAmount)
        {
            if (GetCompositeObjects() != null)
            {
                foreach (DrawingObject obj in GetCompositeObjects())
                {
                    obj.Translate(xAmount, yAmount);
                }
            }
        }

        public override void Select()
        {
            if (GetCompositeObjects() != null)
            {
                foreach (DrawingObject obj in GetCompositeObjects())
                {
                    obj.Select();
                }
            }
        }

        public override void Deselect()
        {
            if (GetCompositeObjects().Count > 0)
            {
                foreach (DrawingObject obj in GetCompositeObjects())
                {
                    obj.Deselect();
                }
            }
        }

        public override bool IsSelected()
        {
            if (GetCompositeObjects().Count > 0)
            {
                foreach (DrawingObject obj in GetCompositeObjects())
                {
                    if (obj.IsSelected())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
