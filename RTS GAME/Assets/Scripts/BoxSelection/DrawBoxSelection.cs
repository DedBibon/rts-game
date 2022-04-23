using UnityEngine;

namespace BoxSelection
{
    public static class DrawBoxSelection
    {
        private static Texture2D _texture2D;

        private static Texture2D GetTexture2D()
        {
            if (_texture2D) return _texture2D;
            
            _texture2D = new Texture2D(1, 1);
            _texture2D.SetPixel(0, 0, Color.white);
            _texture2D.Apply();

            return _texture2D;
        }
        
        public static void DrawRect(Rect rect, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(rect, GetTexture2D());
            GUI.color = Color.white;
        }

        //Get screen info for rect
        public static Rect GetScreenRect(Vector2 screenPosition1, Vector2 screenPosition2)
        {
            screenPosition1.y = Screen.height - screenPosition1.y;
            screenPosition2.y = Screen.height - screenPosition2.y;
            var topLeft = Vector2.Min(screenPosition1, screenPosition2);
            var bottomRight = Vector2.Max(screenPosition1, screenPosition2);
            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }
        
        //GUI border for box selection
        public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
        {
            DrawRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
            DrawRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
            DrawRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
            DrawRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
        }
        
        //Create 3D bounds based on a 2D rect.
        public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
        {
            var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
            var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
            var min = Vector3.Min(v1, v2);
            var max = Vector3.Max(v1, v2);
            min.z = camera.nearClipPlane;
            max.z = camera.farClipPlane;

            var bounds = new Bounds();
            bounds.SetMinMax(min, max);
            return bounds;
        }
    }
}