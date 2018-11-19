﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    interface IObservable
    {
        void OnChange(DrawingObject sender, Point point);
        void AddObserver(DrawingObject observer);
        void RemoveObserver(DrawingObject observer);
    }
}
