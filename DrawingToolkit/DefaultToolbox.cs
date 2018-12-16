using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public class DefaultToolbox : ToolStrip, IToolbox
    {
        public ITool CurrentActiveTool { get; set; }

        public event ToolSelectedEventHandler ToolSelected;

        public void AddSeparator()
        {
            this.Items.Add(new ToolStripSeparator());
        }

        public void AddTool(ITool tool)
        {
            if (tool is ToolStripButton)
            {
                ToolStripButton toggleButton = (ToolStripButton)tool;

                if (toggleButton.CheckOnClick)
                {
                    toggleButton.CheckedChanged += toggleButton_CheckedChanged;
                }

                this.Items.Add(toggleButton);
            }
        }

        public void RemoveTool(ITool tool)
        {
            foreach(ToolStripItem item in this.Items)
            {
                if (item is ITool)
                {
                    if (item.Equals(tool))
                    {
                        this.Items.Remove(item);
                    }
                }
            }
        }

        private void toggleButton_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is ToolStripButton)
            {
                ToolStripButton button = (ToolStripButton)sender;
                if (button.Checked)
                {
                    if (button is ITool)
                    {
                        this.CurrentActiveTool = (ITool)button;
                        if (ToolSelected != null)
                        {
                            ToolSelected(this.CurrentActiveTool);
                        }
                        UncheckInactiveToggleButtons();
                    }
                }
            }
        }

        private void UncheckInactiveToggleButtons()
        {
            foreach(ToolStripItem item in this.Items)
            {
                if (item != this.CurrentActiveTool)
                {
                    if (item is ToolStripButton)
                    {
                        ((ToolStripButton)item).Checked = false;
                    }
                }
            }
        }
    }
}
