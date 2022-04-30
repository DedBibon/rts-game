// using UnityEngine;
//
// public class UnitsSelection : MonoBehaviour
// {
//     //Статус вибору юнітів, позиція миші в просторі та прискорення
//     private bool _isSelecting;
//     private static Vector3 _mousePositing;
//     public float thickness;
//
//     public LayerMask clickable;
//     public LayerMask ground;
//
//     private void Update()
//     {
//         //Початок вибору
//         if (Input.GetMouseButtonDown(0))
//         {
//             _isSelecting = true;
//             _mousePositing = Input.mousePosition;
//         }
//
//         //Кінець вибору
//         if (Input.GetMouseButtonUp(0))
//         {
//             _isSelecting = false;
//
//             //Система променів для прорахунку кліку по об'єкту (вибір юніта ЛКМ)
//             Camera myCam = Camera.main;
//             RaycastHit hit;
//             Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
//             if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
//             {
//                 UnitManager.Selected(hit.collider.gameObject);
//             }
//         }
//
//         if (_isSelecting)
//         {
//             
//             foreach (var obj in FindObjectsOfType<UnitComponent>())
//             {
//                 
//                 if (Input.GetKey(KeyCode.LeftShift))
//                 {
//                     if (IsWithinSelectionBounds(obj.gameObject))
//                      UnitManager.Selected(obj.gameObject);
//                 }
//                 else
//                 {
//                     if (IsWithinSelectionBounds(obj.gameObject))
//                         UnitManager.Selected(obj.gameObject);
//                     else
//                         UnitManager.DeSelected(obj.gameObject);
//                 }
//             }
//         }
//     }
//
//     private void OnGUI()
//     {
//         if (_isSelecting)
//         {
//             var rect = DrawBoxSelection.GetScreenRect(_mousePositing, Input.mousePosition);
//             DrawBoxSelection.DrawReact(rect, new Color(0.5f, 1f, 0.4f, 0.2f));
//             DrawBoxSelection.DrawScreenRectBorder(rect, thickness, new Color(0.5f, 1f, 0.4f));
//         }
//     }
//
//     //Метод по визначенні об'єкту в зоні видіденню
//     private static bool IsWithinSelectionBounds(GameObject gameObject)
//     {
//         var camera = Camera.main;
//         var viewportBounds =
//             DrawBoxSelection.GetViewportBounds(camera, _mousePositing, Input.mousePosition);
//
//         return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
//     }
// }