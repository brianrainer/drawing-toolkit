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
        private List<DrawingObject> drawingObjectList;

        public GroupObject(List<DrawingObject> drawingObjects)
        {
            drawingObjectList = new List<DrawingObject>(drawingObjects);
        }

        public override bool isComposite()
        {
            return true;
        }

        public override List<DrawingObject> GetObjectList()
        {
            return drawingObjectList;
        }

        public override void ChangeState(DrawingState state)
        {
            if (drawingObjectList != null && drawingObjectList.Count > 0)
            {
                foreach (DrawingObject obj in drawingObjectList)
                {
                    obj.ChangeState(state);
                }
                this.state = state;
            }
        }

        public override void Draw()
        {
            if (drawingObjectList != null && drawingObjectList.Count > 0)
            {
                foreach (DrawingObject obj in drawingObjectList)
                {
                    obj.SetGraphics(this.GetGraphics());
                    obj.Draw();
                }
            }
        }

        public override bool Intersect(Point testPoint)
        {
            if (drawingObjectList != null && drawingObjectList.Count > 0)
            {
                foreach (DrawingObject obj in drawingObjectList)
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
            if (drawingObjectList != null && drawingObjectList.Count > 0)
            {
                foreach (DrawingObject obj in drawingObjectList)
                {
                    obj.Translate(xAmount, yAmount);
                }
            }
        }

        public override void Add(DrawingObject drawingObject)
        {
            this.drawingObjectList.Add(drawingObject);
        }

        public override void Remove(DrawingObject drawingObject)
        {
            this.drawingObjectList.Remove(drawingObject);
        }

        public override void Select()
        {
            if (drawingObjectList != null && drawingObjectList.Count > 0)
            {
                foreach (DrawingObject obj in drawingObjectList)
                {
                    obj.Select();
                }
            }
        }

        public override void Deselect()
        {
            if (drawingObjectList != null && drawingObjectList.Count > 0)
            {
                foreach (DrawingObject obj in drawingObjectList)
                {
                    obj.Deselect();
                }
            }
        }
    }
}
