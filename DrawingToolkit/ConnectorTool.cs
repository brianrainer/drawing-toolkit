using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DrawingToolkit
{
    public class ConnectorTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private ConnectorSegment connectorSegment;
        private DrawingObject StartObject, EndObject;

        public ConnectorTool()
        {
            this.Name = "Connector Tool";
            this.ToolTipText = "Connector Tool";
            this.Text = "Connector";
            this.CheckOnClick = true;
        }

        public Cursor cursor => Cursors.Arrow;

        public ICanvas TargetCanvas { get => GetCanvas(); set => this.SetCanvas(value); }

        private ICanvas GetCanvas()
        {
            return canvas;
        }

        private void SetCanvas(ICanvas value)
        {
            canvas = value;
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
            if (e.Button == MouseButtons.Left)
            {
                StartObject = GetCanvas().GetObjectAt(new Point(e.X,e.Y));
                if (StartObject != null)
                {
                    connectorSegment = new ConnectorSegment(StartObject.CenterPoint);
                }
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (StartObject != null && connectorSegment != null)
                {
                    GetCanvas().RemoveDrawingObject(connectorSegment);
                    connectorSegment = new ConnectorSegment(connectorSegment.StartPoint, new Point(e.X, e.Y));
                    GetCanvas().AddDrawingObject(connectorSegment);
                }
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                EndObject = GetCanvas().GetObjectAt(new Point(e.X, e.Y));
                if (EndObject != null && StartObject != null &&
                    connectorSegment != null &&
                    EndObject != StartObject && EndObject != connectorSegment)
                {
                    GetCanvas().RemoveDrawingObject(connectorSegment);
                    connectorSegment = new ConnectorSegment(connectorSegment.StartPoint, EndObject.CenterPoint);
                    StartObject.AddObserver(connectorSegment);
                    EndObject.AddObserver(connectorSegment);
                    connectorSegment.AddObserver(StartObject);
                    connectorSegment.AddObserver(EndObject);
                    GetCanvas().AddDrawingObject(connectorSegment);
                    GetCanvas().DeselectAllObject();
                    connectorSegment.Select();
                }
                else
                {
                    GetCanvas().RemoveDrawingObject(connectorSegment);
                }
                connectorSegment = null;
                StartObject = null;
                EndObject = null;
            }
        }
    }
}
