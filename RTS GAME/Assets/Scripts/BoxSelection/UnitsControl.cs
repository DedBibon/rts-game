using System;
using UnityEngine;
using BoxSelection;
using CameraControl;
using Units;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace ObjectSelection
{
    public class UnitsControl : MonoBehaviour
    {
        //Units Selection group
        [SerializeField] private Team groupControl;

        [SerializeField] private LayerMask clickable;

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
            var objWay = gameObject.GetComponent<UnitManager>();
            
            //Click select
            if (Input.GetMouseButtonUp(0))
            {
                var cameraControl = gameObject.GetComponent<CameraControlKeyboard>().CameraControl;
                var ray = cameraControl.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, clickable))
                    objWay.AddUnits(hit.collider.gameObject);
            }
            
            if (!_isSelecting)
                return;
            
            //Choice with Shift and without (box selection)
            foreach (var list in objWay.AllUnitsPlayer)
            {
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