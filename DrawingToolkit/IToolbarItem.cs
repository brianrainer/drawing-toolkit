﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public interface IToolbarItem
    {
        String Name { set; get; }
        void SetCommand(ICommand command);
    }
}