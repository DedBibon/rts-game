namespace Camera
{
    using UnityEngine;

    public class CameraControlKeyboard : MonoBehaviour
    {
        public float zomeSpeed;
        public float moveSpeed;
        public float moveTime;
        
        public float maxPositionY;
        public float minPositionY;

        [SerializeField] private Camera cameraControl;
        
        [SerializeField] private Vector3 newPosition;


        private void Start()
        {
            newPosition = cameraControl.transform.position;
        }

        private void Update()
        {
            MovementInpute();
        }

        private void MovementInpute()
        {
            //Move WASD
            if (Input.GetKey(KeyCode.W))
                newPosition += (transform.forward * moveSpeed);

            if (Input.GetKey(KeyCode.S))
                newPosition += (transform.forward * -moveSpeed);
            
            if (Input.GetKey(KeyCode.D))
                newPosition += (transform.right * moveSpeed);

            if (Input.GetKey(KeyCode.A))
                newPosition += (transform.right * -moveSpeed);

            //Zoom scroling
            if (Input.mouseScrollDelta.y > 0 && cameraControl.transform.position.y < maxPositionY)
                newPosition += (transform.up * zomeSpeed);
            if (Input.mouseScrollDelta.y < 0 && cameraControl.transform.position.y > minPositionY)
                newPosition += (transform.up * -zomeSpeed);
            
            //Applye new position 
            cameraControl.transform.position =
                Vector3.Lerp(cameraControl.transform.position, newPosition, Time.deltaTime * moveTime)
                ;
        }
    }
}