using System.Threading.Tasks;
using UnityEngine;
using Unity.Cinemachine;

namespace Core.CameraService.Interface
{
    public interface ICameraService
    {
        Task Inject();
        Camera MainCamera { get; }
        CinemachineCamera ActiveVirtualCamera { get; }
        Vector3 GetMouseWorldPosition(LayerMask groundMask);
        void SetFollowTarget(Transform target);
        Task SwitchToCamera(string key);
    }
}