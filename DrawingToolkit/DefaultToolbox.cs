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
        private ITool currentActiveTool;

        public ITool CurrentActiveTool { get => this.currentActiveTool; set => this.currentActiveTool = value; }

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
                        this.currentActiveTool = (ITool)button;
                        if (ToolSelected != null)
                        {
                            ToolSelected(this.currentActiveTool);
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
                if (item != this.currentActiveTool)
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
