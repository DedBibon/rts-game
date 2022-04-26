using UnityEngine;
using BoxSelection;
using CameraControl;
using Units;

namespace ObjectSelection
{
    public class UnitsSelection : MonoBehaviour
    {
        [SerializeField] private Team team;

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
            
            //Check the contents of the object in the selection area
            CheckSelectionObject();

            //End box selection
            if (!Input.GetMouseButtonUp(0)) 
                return;
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
                }
                else
                {
                    if (IsWithinSelectionBounds(list.gameObject))
                        objWay.AddUnits(list.gameObject);
                    else
                        objWay.DelUnits(list.gameObject);
                }
                objWay.InfoSelection();
            }
        }

        public Team GetTeam => team;

        public enum Team
        {
            Green,
            Red
        }
    }
}