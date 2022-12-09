using UnityEngine;

namespace NE.CameraManager
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] public float panSpeed = 10f;
        [SerializeField] public float zoomSpeed = 500f;
        [SerializeField] public float panBorderThickness = 10f;
        [SerializeField] public Vector2 panLimit = new(20, 20);
        public static float minZoom = 10f;
        public static float maxZoom = 40f;

        private float _minYPos = minZoom;
        private const float CAMERA_Y_UPDATE_SPEED = 25f;
        
        private void Update()
        {
            HandleCameraMovement();
        }

        private void HandleCameraMovement()
        {
            Vector3 cameraPos = transform.position;
            cameraPos = HandleMouseScroll(cameraPos);
            cameraPos = HandlePanSlide(cameraPos);
            
            //allow to define map limit
            cameraPos.x = Mathf.Clamp(cameraPos.x, -panLimit.x, panLimit.x);
            cameraPos.z = Mathf.Clamp(cameraPos.z, -panLimit.y, panLimit.y);
            cameraPos.y = Mathf.Clamp(cameraPos.y, _minYPos, maxZoom);
            transform.position = cameraPos;
        }

        private Vector3 HandlePanSlide(Vector3 cameraPos)
        {
            if (Input.mousePosition.y >= Screen.height - panBorderThickness)
                cameraPos.z += panSpeed * Time.deltaTime;
            
            if (Input.mousePosition.y <= panBorderThickness)
                cameraPos.z -= panSpeed * Time.deltaTime;
            
            if (Input.mousePosition.x >= Screen.width - panBorderThickness)
                cameraPos.x += panSpeed * Time.deltaTime;
            
            if (Input.mousePosition.x <= panBorderThickness)
                cameraPos.x -= panSpeed * Time.deltaTime;

            return cameraPos;
        }

        private Vector3 HandleMouseScroll(Vector3 cameraPos)
        {
            
            
            if (Input.GetAxis("Mouse ScrollWheel") > 0f && CanZoom()) 
                cameraPos.y -= zoomSpeed * Time.deltaTime;
            
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                cameraPos.y += zoomSpeed * Time.deltaTime;
            
            if (!CanZoom())
                cameraPos.y += CAMERA_Y_UPDATE_SPEED * Time.deltaTime;
            else
                _minYPos = minZoom;
            
            return cameraPos;
        }
        
        // TODO Fix the zoom glitter
        private bool CanZoom()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit) && hit.distance <= minZoom)
            {
                _minYPos = transform.position.y;
                return false;
            }

            return true;
        }
    }
}
