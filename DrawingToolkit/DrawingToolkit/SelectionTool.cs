﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingToolkit
{
    public class SelectionTool : ToolStripButton, ITool
    {
        private ICanvas canvas;
        private DrawingObject SelectedObject;
        private Point StartPoint;
        
        private List<DrawingObject> SelectedObjectList;

        public SelectionTool()
        {
            this.Name = "Selection Tool";
            this.ToolTipText = "Selection Tool";
            this.Text = "Select";
            this.CheckOnClick = true;
            this.SelectedObjectList = new List<DrawingObject>();
        }

        public Cursor cursor => Cursors.Arrow;

        public ICanvas TargetCanvas { get => GetCanvas(); set => SetCanvas(value); }

        private ICanvas GetCanvas()
        {
            return this.canvas;
        }

        private void SetCanvas(ICanvas value)
        {
            this.canvas = value;
        }

        public void ToolMouseClick(object sender, MouseEventArgs e)
        {
        }

        public void ToolMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canvas != null)
            {
                StartPoint = new Point(e.X, e.Y);
                SelectedObject = canvas.GetObjectAt(StartPoint);
                if (SelectedObject != null)
                {
                    if (SelectedObject.isSelected())
                    {
                        canvas.DeselectObjectAt(StartPoint);
                        SelectedObjectList.Remove(SelectedObject);
                        SelectedObject = null;
                    }
                    else
                    {
                        canvas.SelectObjectAt(StartPoint);
                        SelectedObjectList.Add(SelectedObject);
                    }
                }
            }
        }

        public void ToolMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canvas != null)
            {
                StartPoint = new Point(e.X, e.Y);
            }
        }

        public void ToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && canvas != null)
            {
                foreach (DrawingObject obj in SelectedObjectList)
                {
                    if (obj != null)
                    {
                        obj.Translate(e.X - StartPoint.X, e.Y - StartPoint.Y);
                    }
                }
                StartPoint = new Point(e.X, e.Y);
            }
        }

        public void ToolMouseUp(object sender, MouseEventArgs e)
        {
        }
    }
}
