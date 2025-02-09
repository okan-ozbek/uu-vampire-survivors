using System.Collections;
using Configs;
using UnityEngine;
using Utility;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField] private Transform player;
        [SerializeField] private Vector2 deadzoneSize;
        
        [Header("Camera Movement Settings")]
        [SerializeField] private float smoothSpeed = 15f;
        [SerializeField] private float resetSpeed = 150;
        
        [Header("Camera Zoom Settings")]
        [SerializeField] private float defaultZoom = 5f;
        
        private Vector3 _offset;
        private Vector3 _targetPosition;
        private Camera _camera;

        private void OnEnable()
        {
            CameraConfig.OnZoomOut += HandleZoom;
            CameraConfig.OnZoomIn += HandleZoom;
            CameraConfig.OnZoomReset += HandleZoomReset;
        }
        
        private void OnDisable()
        {
            CameraConfig.OnZoomOut -= HandleZoom;
            CameraConfig.OnZoomIn -= HandleZoom;
            CameraConfig.OnZoomReset -= HandleZoomReset;
        }

        private void Start()
        {
            _camera = Camera.main;
            if (_camera)
            {
                _camera.orthographicSize = defaultZoom;
            }
            
            _offset = transform.position - player.position;
        }
        
        private void FixedUpdate()
        {
            _targetPosition = player.position + _offset;
        }
        
        private void LateUpdate()
        {
            Vector3 cameraPosition = transform.position;

            cameraPosition = FollowTransform(cameraPosition);
            cameraPosition = ResetCameraPosition(cameraPosition);
            
            transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothSpeed * Time.deltaTime);
        }

        private Vector3 FollowTransform(Vector3 cameraPosition)
        {
            if (Mathf.Abs(_targetPosition.x - cameraPosition.x) > deadzoneSize.x)
            {
                cameraPosition.x = _targetPosition.x - Mathf.Sign(_targetPosition.x - cameraPosition.x) * deadzoneSize.x;   
            }
        
            if (Mathf.Abs(_targetPosition.y - cameraPosition.y) > deadzoneSize.y)
            {
                cameraPosition.y = _targetPosition.y - Mathf.Sign(_targetPosition.y - cameraPosition.y) * deadzoneSize.y;
            }

            return cameraPosition;
        }

        private Vector3 ResetCameraPosition(Vector3 cameraPosition)
        {
            if (PlayerInput.MovementDirection == Vector2.zero)
            {
                cameraPosition = Vector3.Lerp(cameraPosition, _targetPosition, resetSpeed * Time.deltaTime);
            }

            return cameraPosition;
        }

        private void HandleZoom(float zoom, float duration)
        {
            StartCoroutine(HandleZoomCoroutine(zoom, duration));
        }

        private void HandleZoomReset(float duration)
        {
            StartCoroutine(HandleZoomCoroutine(defaultZoom, duration));
        }
        
        private IEnumerator HandleZoomCoroutine(float zoom, float duration)
        {
            float time = 0f;
            float startZoom = _camera.orthographicSize;
            
            while (time < duration)
            {
                _camera.orthographicSize = Mathf.Lerp(startZoom, zoom, time / duration);
                time += Time.deltaTime;
                
                yield return null;
            }
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