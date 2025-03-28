using Core.CameraService.Enum;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera/Camera Prefab Entry")]
public class CameraPrefabEntry : ScriptableObject
{
    public CamerasEnum cameraId;
    public GameObject cameraPrefab;
}