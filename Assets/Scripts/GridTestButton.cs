using System;
using Core.CameraService.Enum;
using Core.CameraService.Interface;
using Core.CameraService.Service;
using Core.GridService.Interface;
using UnityEngine;

public class GridTestButton : MonoBehaviour
{
    private ICameraService _cameraService;
    private void Awake()
    {
        _cameraService = ReferenceLocator.Instance.CameraService;
    }

    public async void OnClick_ShowGrid()
    {
        var gridService = ReferenceLocator.Instance.GridService;
        var visualizer = ReferenceLocator.Instance.GridVisualizerService;
        await _cameraService.SwitchToCamera(CamerasEnum.BattleCamera.ToString());
        gridService.InitializeGrid(new Vector3(-10, 0, -10)); // Position it to fit your ground
        await visualizer.ShowGrid();
    }
}