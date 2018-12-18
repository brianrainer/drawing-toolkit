using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public interface ITool
    {
        String Name { get; set; }
        Cursor cursor { get; }
        ICanvas TargetCanvas { get; set; }
        ICommand command { get; set; }

        void ToolMouseDown(object sender, MouseEventArgs e);
        void ToolMouseUp(object sender, MouseEventArgs e);
        void ToolMouseMove(object sender, MouseEventArgs e);
        void ToolMouseClick(object sender, MouseEventArgs e);
        void ToolMouseDoubleClick(object sender, MouseEventArgs e);
        void ToolKeyDown(object sender, KeyEventArgs e);
        void ToolKeyUp(object sender, KeyEventArgs e);
    }
}
