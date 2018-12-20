using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public class PrevTool : ToolStripButton, ITool
    {
        public Cursor cursor => Cursors.Arrow;
        public ICanvas TargetCanvas { get; set; }
        public ICommand command { get; set; }

        public PrevTool()
        {
            Name = "Move Prev Tool";
            ToolTipText = "Move Prev";
            Text = "Previous";
            Debug.WriteLine(Name);
        }

        public PrevTool(ICanvas canvas) : this()
        {
            TargetCanvas = canvas;
        }

        protected override void OnClick(EventArgs e)
        {
            command = new MovePrevCommand(TargetCanvas);
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