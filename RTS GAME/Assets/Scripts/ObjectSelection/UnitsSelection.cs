using UnityEngine;
using BoxSelection;
using CameraControl;

namespace ObjectSelection
{
    public class UnitsSelection : MonoBehaviour
    {
        public float thickness;
        public LayerMask clickable;
        public LayerMask ground;

        private bool _isSelecting;
        
        private static Vector3 _mousePositing;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isSelecting = true;
                _mousePositing = Input.mousePosition;

                gameObject.GetComponent<CameraControlKeyboard>().enabled = false;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isSelecting = false;
                
                gameObject.GetComponent<CameraControlKeyboard>().enabled = true;
       
                // Camera myCam = Camera.main;
                // RaycastHit hit;
                // Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
                // if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
                // {
                //     UnitManager.Selected(hit.collider.gameObject);
                // }
            }

            if (_isSelecting)
            {
                // foreach (var obj in FindObjectsOfType<UnitComponent>())
                // {
                //     
                //     if (Input.GetKey(KeyCode.LeftShift))
                //     {
                //         if (IsWithinSelectionBounds(obj.gameObject))
                //          UnitManager.Selected(obj.gameObject);
                //     }
                //     else
                //     {
                //         if (IsWithinSelectionBounds(obj.gameObject))
                //             UnitManager.Selected(obj.gameObject);
                //         else
                //             UnitManager.DeSelected(obj.gameObject);
                //     }
                // }
            }
        }

        private void OnGUI()
        {
            if (!_isSelecting) return;

            var rect = DrawBoxSelection.GetScreenRect(_mousePositing, Input.mousePosition);
            DrawBoxSelection.DrawRect(rect, new Color(0.5f, 1f, 0.4f, 0.2f));
            DrawBoxSelection.DrawScreenRectBorder(rect, thickness, new Color(0.5f, 1f, 0.4f));
        }

        //Checking the entry of the object into the box selection
        private static bool IsWithinSelectionBounds(GameObject gameObject)
        {
            var camera = Camera.main;
            var viewportBounds =
                DrawBoxSelection.GetViewportBounds(camera, _mousePositing, Input.mousePosition);

            return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
        }
    }
}