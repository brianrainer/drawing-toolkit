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
                Point contactPoint = new Point(e.X, e.Y);
                StartObject = GetCanvas().GetObjectAt(contactPoint);
                if (StartObject != null)
                {
                    connectorSegment = new ConnectorSegment(contactPoint);
                }
                else
                {
                    connectorSegment = null;
                }
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point contactPoint = new Point(e.X, e.Y);
                if (connectorSegment != null)
                {
                    GetCanvas().RemoveDrawingObject(connectorSegment);
                    connectorSegment = new ConnectorSegment(connectorSegment.StartPoint, contactPoint);
                    GetCanvas().AddDrawingObject(connectorSegment);
                }
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point contactPoint = new Point(e.X, e.Y);
                GetCanvas().RemoveDrawingObject(connectorSegment);
                EndObject = GetCanvas().GetObjectAt(contactPoint);
                //if (EndObject != null && StartObject != null && connectorSegment != null && EndObject != StartObject && EndObject != connectorSegment)
                if (EndObject != null && EndObject != StartObject)
                {
                    connectorSegment = new ConnectorSegment(connectorSegment.StartPoint, contactPoint);

                    StartObject.AddObserver(connectorSegment, connectorSegment.StartPoint);
                    EndObject.AddObserver(connectorSegment, contactPoint);

                    connectorSegment.AddObserver(StartObject, connectorSegment.StartPoint);
                    connectorSegment.AddObserver(EndObject, contactPoint);

                    GetCanvas().AddDrawingObject(connectorSegment);
                    connectorSegment.Select();
                    GetCanvas().DeselectAllObject();
                }
                connectorSegment = null;
                StartObject = null;
                EndObject = null;
            }
        }
    }
}
