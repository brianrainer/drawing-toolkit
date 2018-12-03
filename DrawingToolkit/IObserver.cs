using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    interface IObserver
    {
        void Update(Point updatedPoint, int xAmount, int yAmount);
    }
}
