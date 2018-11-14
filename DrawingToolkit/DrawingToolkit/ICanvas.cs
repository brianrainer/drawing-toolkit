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
        void AddObjectsToList(List<DrawingObject> drawingObjectList);
        void RemoveDrawingObject(DrawingObject drawingObject);
        void RemoveObjectsFromList(List<DrawingObject> drawingObjectList);

        DrawingObject GetObjectAt(Point e);
        List<DrawingObject> GetObjectList();

        DrawingObject SelectObjectAt(Point e);
        List<DrawingObject> SelectAllObject();

        void DeselectObjectAt(Point e);
        void DeselectAllObject();

        void Repaint();
    }
}
