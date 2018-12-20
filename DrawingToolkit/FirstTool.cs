using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public class FirstTool : ToolStripButton, ITool
    {
        public Cursor cursor => Cursors.Arrow;
        public ICanvas TargetCanvas { get; set; }
        public ICommand command { get; set; }

        public FirstTool()
        {
            Name = "Move First Tool";
            ToolTipText = "Move First";
            Text = "First";
            Debug.WriteLine(Name);
        }

        public FirstTool(ICanvas canvas) : this()
        {
            TargetCanvas = canvas;
        }

        protected override void OnClick(EventArgs e)
        {
            command = new MoveFirstCommand(TargetCanvas);
            command.Execute();
        }

        public void ToolKeyDown(object sender, KeyEventArgs e)
        {
        }

        public void ToolKeyUp(object sender, KeyEventArgs e)
        {
        }

        public void ToolMouseClick(object sender, MouseEventArgs e)
        {
        }

        public void ToolMouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
        }
    }
}
