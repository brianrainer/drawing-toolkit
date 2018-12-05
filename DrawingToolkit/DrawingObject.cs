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
        private Graphics G;

        protected const Double EPSILON = 3.0;
        protected DrawingState State;
        private Pen P;
        private Brush B;

        public string Name { get; set; }
        public Guid ID { get; set; }

        private LinkedList<DrawingObject> CompositeObjects;
        private LinkedList<Tuple<Point, DrawingObject>> Observers;

        public DrawingObject()
        {
            ID = Guid.NewGuid();
            ChangeState(PreviewState.GetInstance());
            CompositeObjects = new LinkedList<DrawingObject>();
            Observers = new LinkedList<Tuple<Point, DrawingObject>>();
        }

        public abstract bool Intersect(Point testPoint);
        public abstract void Translate(int xAmount, int yAmount);

        public virtual void SetPen(Color color, float width, DashStyle dashStyle)
        {
            P = new Pen(color, width)
            {
                DashStyle = dashStyle
            };
        }

        public virtual Pen GetPen()
        {
            return P;
        }

        public virtual void SetBrushStyle(Color color)
        {
            B = new SolidBrush(color);
        }

        public virtual Brush GetBrush()
        {
            return B;
        }

        public virtual void Draw()
        {
            State.Draw(this);
        }

        public virtual void Render()
        {
            // default no render
        }


        public virtual void SetGraphics(Graphics graphics)
        {
            G = graphics;
        }

        public virtual Graphics GetGraphics()
        {
            return G;
        }

        public virtual void ChangeState(DrawingState state)
        {
            State = state;
        }

        public virtual bool IsSelected()
        {
            return State == EditState.GetInstance();
        }

        protected virtual bool IsNear(Point a, Point b)
        {
            if (Math.Abs(a.X-b.X)<=EPSILON && Math.Abs(a.Y - b.Y) <= EPSILON)
            {
                return true;
            }
            return false;
        }

        public virtual void Select()
        {
            State.Select(this);
        }

        public virtual void Deselect()
        {
            State.Deselect(this);
        }

        public virtual bool IsComposite()
        {
            return false; // default is non composite
        }

        public virtual void AddComposite(DrawingObject drawingObject)
        {
            CompositeObjects.AddLast(drawingObject);
        }

        public virtual void RemoveComposite(DrawingObject drawingObject)
        {
            CompositeObjects.Remove(drawingObject);
        }

        public virtual LinkedList<DrawingObject> GetCompositeObjects()
        {
            return CompositeObjects;
        }

        public virtual LinkedList<Tuple<Point,DrawingObject>> GetObserverList()
        {
            return Observers;
        }

        public virtual void UpdateObserver(DrawingObject sender, int xAmount, int yAmount)
        {
            this.Translate(xAmount, yAmount); // default implementation for observable
        }

        public virtual void OnChange(int xAmount, int yAmount)
        {
            foreach (Tuple<Point, DrawingObject> tupl in GetObserverList())
            {
                tupl.Item2.UpdateObserver(this, xAmount, yAmount);
            }
        }

        public virtual void AddObserver(DrawingObject observer, Point contactPoint)
        {
            Observers.AddLast(new Tuple<Point, DrawingObject>(contactPoint, observer));
        }

        public virtual void RemoveObserver(Tuple<Point, DrawingObject> Object)
        {
            Observers.Remove(Object);
        }
    }
}
