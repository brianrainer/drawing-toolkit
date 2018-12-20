using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public partial class MainWindow : Form
    {
        private ICanvas canvas;
        private IToolbox toolbox;

        public MainWindow()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            #region canvas
            this.canvas = new DefaultCanvas();
            this.toolStripContainer1.ContentPanel.Controls.Add((Control)this.canvas);
            #endregion

            #region toolbox
            this.toolbox = new DefaultToolbox();
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add((Control)this.toolbox);
            this.toolbox.AddTool(new SelectionTool());
            this.toolbox.AddSeparator();
            this.toolbox.AddTool(new LineTool());
            this.toolbox.AddTool(new RectangleTool());
            this.toolbox.AddTool(new CircleTool());
            this.toolbox.AddTool(new ConnectorTool());
            this.toolbox.AddSeparator();
            this.toolbox.AddTool(new PrevTool(canvas));
            this.toolbox.AddTool(new NextTool(canvas));
            this.toolbox.AddTool(new FirstTool(canvas));
            this.toolbox.AddTool(new LastTool(canvas));
            this.toolbox.AddSeparator();
            this.toolbox.AddTool(new UndoTool(canvas));
            this.toolbox.AddTool(new RedoTool(canvas));
            this.toolbox.ToolSelected += toolBox_ToolSelected;
            #endregion

            #region textbox
            this.textBox1.Multiline = true;
            this.textBox1.ReadOnly = true;
            this.textBox1.TabStop = false;
            this.canvas.TextBox = this.textBox1;
            #endregion
        }

        private void toolBox_ToolSelected(ITool tool)
        {
            if (this.canvas != null)
            {
                this.canvas.DeselectAllObject();
                this.canvas.SetActiveTool(tool);
                tool.TargetCanvas = this.canvas;
            }
        }
    }
}
