using System.Threading.Tasks;
using UnityEngine;
using Core.CameraService.Interface;
using Unity.Cinemachine;

namespace Core.CameraService.Service
{
    public class CameraService : ICameraService
    {
        
        private Camera _mainCamera;
        private CameraManager _cameraManager;
        private string _activeCameraKey;
        
        public CinemachineCamera ActiveVirtualCamera => _cameraManager.GetActiveVirtualCamera();
        public Camera MainCamera => _mainCamera;

        public CameraService(Camera mainCamera, CameraManager cameraManager)
        {
            _mainCamera = mainCamera;
            _cameraManager = cameraManager;

            if (_mainCamera == null)
                Debug.LogError("CameraService: Main Camera is null.");
            if (_cameraManager == null)
                Debug.LogError("CameraService: Camera Manager is null.");
        }

        public async Task Inject()
        {
            await _cameraManager.LoadAllCamerasAsync();
        }

        public Vector3 GetMouseWorldPosition(LayerMask groundMask)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
                return hit.point;
            return Vector3.negativeInfinity;
        }

        public void SetFollowTarget(Transform target)
        {
            _cameraManager.GetActiveVirtualCamera().Follow = target;
        }

        public async Task SwitchToCamera(string key)
        {
            if (_cameraManager.HasCamera(key))
            {
                _cameraManager.DeactivateAll();
                _cameraManager.ActivateCamera(key);
                _activeCameraKey = key;
            }
            else
            {
                Debug.LogError($"CameraService: No camera found for key '{key}'.");
            }

            await Task.CompletedTask;
        }

       
    }
}