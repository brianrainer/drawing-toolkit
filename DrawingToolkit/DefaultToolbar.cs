using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public class DefaultToolbar : ToolStrip, IToolbar
    {
        public DefaultToolbar()
        {
            this.Dock = DockStyle.Top;
        }

        public void AddSeparator()
        {
            this.Items.Add(new ToolStripSeparator());
        }

        public void AddToolbarItem(IToolbarItem toolbarItem)
        {
            this.Items.Add((ToolStripItem)toolbarItem);
        }
    }
}
