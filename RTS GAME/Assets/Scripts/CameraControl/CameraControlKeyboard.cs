using UnityEngine;

namespace CameraControl
{
    public class CameraControlKeyboard : MonoBehaviour
    {
        //Pointer on camera for limite zooming
        [SerializeField]private Camera cameraControl;

        public Camera CameraControl => cameraControl;
        
        //Move + zoom
        public float zoomSpeed;
        public float moveSpeed;
        public float moveTime;

        //Rotation
        public float sensitivity;
        private Quaternion _newRotation;
        private bool _isRotation;
        
        //Limit
        public float maxPositionY;
        public float minPositionY;
        
        private Vector3 _newPosition;

        private void Start()
        {
            //Starting incision position in space 
            _newPosition = transform.position;
            _newRotation = transform.rotation;
        }

        private void Update()
        {
            //Rotation event
            if (Input.GetMouseButtonDown(2))
                _isRotation = true;
            if (Input.GetMouseButtonUp(2))
                _isRotation = false;
            
            MovementInpute();
        }

        private void MovementInpute()
        {
            //Rotation
            if (Input.GetAxis("Mouse X") > 0 && _isRotation)
                _newRotation *= Quaternion.Euler(Vector3.up * sensitivity);

            if (Input.GetAxis("Mouse X") < 0 && _isRotation)
                _newRotation *= Quaternion.Euler(Vector3.up * -sensitivity);
            
            //Move WASD
            if (Input.GetKey(KeyCode.W))
                _newPosition += (transform.forward * moveSpeed);

            if (Input.GetKey(KeyCode.S))
                _newPosition += (transform.forward * -moveSpeed);

            if (Input.GetKey(KeyCode.D))
                _newPosition += (transform.right * moveSpeed);

            if (Input.GetKey(KeyCode.A))
                _newPosition += (transform.right * -moveSpeed);

            //Zoom scroling
            if (Input.mouseScrollDelta.y > 0 && cameraControl.transform.position.y < maxPositionY)
                _newPosition += (transform.up * zoomSpeed);
            if (Input.mouseScrollDelta.y < 0 && cameraControl.transform.position.y > minPositionY)
                _newPosition += (transform.up * -zoomSpeed);

            //Apply new position 
            transform.position =
                Vector3.Lerp(transform.position, _newPosition, Time.deltaTime * moveTime);
            
            //Apply new rotation
            transform.rotation =
                Quaternion.Lerp(transform.rotation, _newRotation, Time.deltaTime * moveTime);
        }
    }
}