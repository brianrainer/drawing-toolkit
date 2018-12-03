using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public abstract class DrawingObject : IObserver, IObservable
    {
        protected const Double EPSILON = 3.0;
        public Guid ID { get; set; }
        protected DrawingState state;
        private Graphics graphics;
        public Pen Pen { get; set; }
        public Brush Brush { get; set; }
        public Point CenterPoint { get; set; }
        public List<DrawingObject> Observers { get; set; }

        public DrawingObject()
        {
            ID = Guid.NewGuid();
            ChangeState(PreviewState.GetInstance());
        }

        public abstract bool Intersect(Point testPoint);
        public abstract void Translate(int xAmount, int yAmount);

        public virtual void SetPenStyle(Color color, float width, DashStyle dashStyle)
        {
            Pen = new Pen(color, width)
            {
                DashStyle = dashStyle
            };
        }

        public virtual void SetBrushStyle(Color color)
        {
            Brush = new SolidBrush(color);
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

        public virtual void ChangeState(DrawingState state)
        {
            this.state = state;
        }

        public virtual bool isSelected()
        {
            return state == EditState.GetInstance();
        }

        protected virtual bool isNear(Point a, Point b)
        {
            if (Math.Abs(a.X-b.X)<=EPSILON && Math.Abs(a.Y - b.Y) <= EPSILON)
            {
                return true;
            }
            return false;
        }

        public virtual void Select()
        {
            this.state.Select(this);
        }

        public virtual void Deselect()
        {
            this.state.Deselect(this);
        }

        public virtual bool isComposite()
        {
            return false; // default
        }

        public virtual void Add(DrawingObject drawingObject)
        {
        }

        public virtual void Remove(DrawingObject drawingObject)
        {
        }

        public virtual List<DrawingObject> GetObjectList()
        {
            return new List<DrawingObject>();
        }

        public virtual void Update(Point updatedPoint, int xAmount, int yAmount)
        {
        }

        public virtual void OnChange(int xAmount, int yAmount)
        {
        }

        public virtual void AddObserver(DrawingObject observer)
        {
        }

        public virtual void RemoveObserver(DrawingObject observer)
        {
        }
    }
}
