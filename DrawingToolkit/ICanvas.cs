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
        void AddObjectsToListBack(LinkedList<DrawingObject> drawingObjectList);
        void AddObjectsToListFirst(LinkedList<DrawingObject> drawingObjectList);
        void RemoveDrawingObject(DrawingObject drawingObject);
        void RemoveObjectsFromList(LinkedList<DrawingObject> drawingObjectList);
        void ClearObjectList();

        DrawingObject GetObjectAt(Point e);
        LinkedList<DrawingObject> GetObjectList();

        DrawingObject SelectObjectAt(Point e);
        LinkedList<DrawingObject> SelectAllObject();

        void DeselectObjectAt(Point e);
        void DeselectAllObject();

        void Repaint();
    }
}
