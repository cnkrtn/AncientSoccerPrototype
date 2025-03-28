#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Core.GridService.Interface;
using Core.GridService.Data;

[ExecuteAlways]
public class GridDebugDrawer : MonoBehaviour
{
    private IGridService _gridService;

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || ReferenceLocator.Instance == null)
            return;

        _gridService = ReferenceLocator.Instance.GridService;
        var grid = _gridService.GetGrid();
        if (grid == null) return;

        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                Vector3 worldPos = grid.GetWorldPosition(x, y) + Vector3.up * 0.5f;

                Handles.color = Color.white;
                Handles.Label(worldPos, $"{x},{y}");
            }
        }
    }
}
#endif