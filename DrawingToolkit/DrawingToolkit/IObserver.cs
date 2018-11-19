﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    interface IObserver
    {
        void OnUpdate(DrawingObject Sender, Point point);
    }
}
