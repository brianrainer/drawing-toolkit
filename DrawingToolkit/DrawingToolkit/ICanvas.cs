using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolkit
{
    public interface ICanvas
    {
        String Name { get; set; }
        ITool GetActiveTool();
        void SetActiveTool(ITool tool);

        void AddDrawingObject(DrawingObject drawingObject);
        void RemoveDrawingObject(DrawingObject drawingObject);
        DrawingObject GetObjectAt(Point e);
        DrawingObject SelectObjectAt(Point e);
        void DeselectObjectAt(Point e);
        List<DrawingObject> GetObjectList();
        void DeselectAllObject();

        void Repaint();
    }
}
