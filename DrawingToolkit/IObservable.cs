using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    interface IObservable
    {
        void OnChange(int xAmount, int yAmount);
        void AddObserver(DrawingObject observer, Point contactPoint);
        void RemoveObserver(Tuple<Point, DrawingObject> Object);
    }
}
