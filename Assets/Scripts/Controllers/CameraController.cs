using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Vector2 deadzoneSize = new(2f, 2f);
        [SerializeField] private float smoothSpeed = 0.125f;

        private Vector3 _offset;

        private void Start()
        {
            _offset = transform.position - player.position;
        }

        private void LateUpdate()
        {
            Vector3 targetPosition = player.position + _offset;
            Vector3 cameraPosition = transform.position;

            if (Mathf.Abs(targetPosition.x - cameraPosition.x) > deadzoneSize.x)
            {
                cameraPosition.x = targetPosition.x - Mathf.Sign(targetPosition.x - cameraPosition.x) * deadzoneSize.x;
            }

            if (Mathf.Abs(targetPosition.y - cameraPosition.y) > deadzoneSize.y)
            {
                cameraPosition.y = targetPosition.y - Mathf.Sign(targetPosition.y - cameraPosition.y) * deadzoneSize.y;
            }

            transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothSpeed * Time.deltaTime);
        }
    }
}