using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public abstract class DrawingObject
    {
        protected const Double EPSILON = 3.0;
        private DrawingState state;
        private Graphics graphics;
        public Pen Pen { get; set; }

        public DrawingObject()
        {
            ChangeState(PreviewState.GetInstance());
        }

        public abstract bool Intersect(Point testPoint);
        public abstract void Translate(int xAmount, int yAmount);

        public virtual void SetPenStyle(Color color, float width, DashStyle dashStyle)
        {
            this.Pen = new Pen(color, width)
            {
                DashStyle = dashStyle
            };
        }

        public virtual void Draw()
        {
            this.state.Draw(this);
        }

        public virtual void Render()
        {
        }

        public virtual void SetGraphics(Graphics graphics)
        {
            this.graphics = graphics;
        }

        public virtual Graphics GetGraphics()
        {
            return this.graphics;
        }

        public void ChangeState(DrawingState state)
        {
            this.state = state;
        }

        public void Select()
        {
            this.state.Select(this);
        }

        public void Deselect()
        {
            this.state.Deselect(this);
        }

        public virtual bool Add(DrawingObject drawingObject)
        {
            return false;  // default implementation is object not composite
        }

        public virtual bool Remove(DrawingObject drawingObject)
        {
            return false;  // default implementation is object not composite
        }
    }
}
