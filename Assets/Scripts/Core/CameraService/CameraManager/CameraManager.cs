using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Core.CameraService.Keys;

namespace Core.CameraService
{
    public class CameraManager : MonoBehaviour
    {
        private Dictionary<string, CinemachineCamera> _cameras = new();

        public async Task LoadAllCamerasAsync()
        {
            AsyncOperationHandle<CameraCollection> handle = Addressables.LoadAssetAsync<CameraCollection>(CameraServiceKeys.KEY_CAMERA_COLLECTION);
            await handle.Task;

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError("CameraManager: Failed to load CameraCollection.");
                return;
            }

            CameraCollection collection = handle.Result;

            foreach (var entry in collection.entries)
            {
                if (entry.cameraPrefab == null)
                {
                    Debug.LogError($"CameraManager: cameraPrefab for '{entry.cameraId}' is null.");
                    continue;
                }

                GameObject instance = Instantiate(entry.cameraPrefab, transform);
                if (instance.TryGetComponent(out CinemachineCamera virtualCam))
                {
                    _cameras.Add(entry.cameraId.ToString(), virtualCam);
                    virtualCam.gameObject.SetActive(false);
                    Debug.Log($"CameraManager: Loaded camera '{entry.cameraId}'.");
                }
                else
                {
                    Debug.LogError($"CameraManager: Prefab for '{entry.cameraId}' lacks a CinemachineVirtualCamera.");
                }
            }
        }

        public bool HasCamera(string key) => _cameras.ContainsKey(key);

        public void DeactivateAll()
        {
            foreach (var cam in _cameras.Values)
                cam.gameObject.SetActive(false);
        }

        public void ActivateCamera(string key)
        {
            if (_cameras.TryGetValue(key, out var cam))
                cam.gameObject.SetActive(true);
        }

        public CinemachineCamera GetActiveVirtualCamera()
        {
            foreach (var cam in _cameras.Values)
            {
                if (cam.gameObject.activeSelf)
                    return cam;
            }
            return null;
        }
    }
}