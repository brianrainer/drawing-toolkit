using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public class ExampleToolbarItem : ToolStripButton, IToolbarItem
    {
        private ICommand command;

        public ExampleToolbarItem()
        {
            this.Name = "Example";
            this.ToolTipText = "Example ToolbarItem";
            this.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.Click += ExampleToolbarItem_Click;
        }

        private void ExampleToolbarItem_Click(object sender, EventArgs e)
        {
            if (command != null)
            {
                this.command.Execute();
            }
        }

        public void SetCommand(ICommand command)
        {
            this.command = command;
        }
    }
}
