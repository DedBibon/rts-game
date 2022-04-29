using UnityEngine;
using BoxSelection;
using CameraControl;
using Units;

namespace ObjectSelection
{
    public class UnitsControl : MonoBehaviour
    {
        //Units Selection group
        [SerializeField] private Team groupControl;
        
        //Selection box settings (default settings)
        [SerializeField] private float widthBoard = 1;
        [SerializeField] private Color colorRect = new Color(0.5f, 1f, 0.4f, 0.2f);
        [SerializeField] private Color bordColor = new Color(0.5f, 1f, 0.4f);
        
        private bool _isSelecting;
        private static Vector3 _mousePositing;
        
        private void Update()
        {
            //Check at the beginning/end of the selection
            SelectionCheck();

            //Check for selection of objects
            CheckSelectionObject();
        }

        //Runtime code for GUI
        private void OnGUI()
        {
            if (!_isSelecting) return;

            //Draw rect box selection
            var rect = DrawBoxSelection.GetScreenRect(_mousePositing, Input.mousePosition);
            DrawBoxSelection.DrawRect(rect, colorRect);
            DrawBoxSelection.DrawScreenRectBorder(rect, widthBoard, bordColor);
        }


        private void SelectionCheck()
        {
            //Start box selection
            if (Input.GetMouseButtonDown(0))
            {
                _isSelecting = true;
                _mousePositing = Input.mousePosition;

                //Deactivation of the camera control script
                gameObject.GetComponent<CameraControlKeyboard>().enabled = false;
            }

            if (!Input.GetMouseButtonUp(0))
                return;
            _isSelecting = false;

            //Activation of thecamera contol script
            gameObject.GetComponent<CameraControlKeyboard>().enabled = true;
        }

        private static bool IsWithinSelectionBounds(GameObject gameObject)
        {
            var camera = Camera.main;
            var viewportBounds =
                DrawBoxSelection.GetViewportBounds(camera, _mousePositing, Input.mousePosition);

            return camera != null &&
                   viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
        }

        private void CheckSelectionObject()
        {
            if (!_isSelecting)
                return;

            var objWay = gameObject.GetComponent<UnitManager>();
            foreach (var list in objWay.AllUnitsPlayer)
            {
                //Choice with Shift and without
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (IsWithinSelectionBounds(list.gameObject))
                        objWay.AddUnits(list.gameObject);
                    if (IsWithinSelectionBounds(list.gameObject) && objWay.ConntainsInfo(list.gameObject))
                        objWay.DelUnits(list.gameObject);
                }
                else
                {
                    if (IsWithinSelectionBounds(list.gameObject))
                        objWay.AddUnits(list.gameObject);
                    else
                        objWay.DelUnits(list.gameObject);
                }
                
                //Debug in console
                objWay.InfoSelection();
            }
        }

        public Team GetGroupControl => groupControl;

        public enum Team
        {
            Green,
            Red
        }
    }
}