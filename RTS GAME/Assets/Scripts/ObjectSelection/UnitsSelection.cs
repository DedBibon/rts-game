using UnityEngine;
using BoxSelection;
using CameraControl;

namespace ObjectSelection
{
    public class UnitsSelection : MonoBehaviour
    {
        public float thickness;
        private bool _isSelecting;
        private static Vector3 _mousePositing;
        
        private void Update()
        {
            //Start box selection
            if (Input.GetMouseButtonDown(0))
            {
                _isSelecting = true;
                _mousePositing = Input.mousePosition;
                
                //Deactivation of the camera control script
                gameObject.GetComponent<CameraControlKeyboard>().enabled = false;
            }
            
            //End box selection
            if (!Input.GetMouseButtonUp(0)) return;
            
            _isSelecting = false;
            
            //Activation of thecamera contol script
            gameObject.GetComponent<CameraControlKeyboard>().enabled = true;
        }

        private void OnGUI()
        {
            if (!_isSelecting) return;
            
            //Draw rect box selection
            var rect = DrawBoxSelection.GetScreenRect(_mousePositing, Input.mousePosition);
            DrawBoxSelection.DrawRect(rect, new Color(0.5f, 1f, 0.4f, 0.2f));
            DrawBoxSelection.DrawScreenRectBorder(rect, thickness, new Color(0.5f, 1f, 0.4f));
        }
        
    }
}