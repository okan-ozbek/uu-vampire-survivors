using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        
        [SerializeField] private Transform player;

        [SerializeField] private Vector2 deadzoneSize;
        [SerializeField] private float smoothSpeed = 15f;
        
        private Vector3 _offset;
        private Vector3 _targetPosition;

        private void Start()
        {
            _offset = transform.position - player.position;
        }
        
        private void FixedUpdate()
        {
            _targetPosition = player.position + _offset;
        }
        
        private void LateUpdate()
        {
            Vector3 cameraPosition = transform.position;
        
            if (Mathf.Abs(_targetPosition.x - cameraPosition.x) > deadzoneSize.x)
            {
                cameraPosition.x = _targetPosition.x - Mathf.Sign(_targetPosition.x - cameraPosition.x) * deadzoneSize.x;   
            }
        
            if (Mathf.Abs(_targetPosition.y - cameraPosition.y) > deadzoneSize.y)
            {
                cameraPosition.y = _targetPosition.y - Mathf.Sign(_targetPosition.y - cameraPosition.y) * deadzoneSize.y;
            }
        
            transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothSpeed * Time.deltaTime);
        }
        
        private void OnDrawGizmos()
        {
            if (player == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(deadzoneSize.x * 2, deadzoneSize.y * 2, 0));
        }
    }
}