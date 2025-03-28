using Core.GridService.Interface;
using UnityEngine;

public class GridClickTester : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = ReferenceLocator.Instance.CameraService.MainCamera.ScreenPointToRay(Input.mousePosition);

            // Define a flat plane at Y = 0 (or whatever your grid's Y is)
            Plane gridPlane = new Plane(Vector3.up, new Vector3(0f, 0.01f, 0f));

            if (gridPlane.Raycast(ray, out float enter))
            {
                Vector3 worldPos = ray.GetPoint(enter);

                var grid = ReferenceLocator.Instance.GridService.GetGrid();
                grid.GetXY(worldPos, out int x, out int y);

                if (grid.IsInBounds(x, y))
                {
                    Vector3 center = grid.GetWorldPosition(x, y) + new Vector3(10f, 0, 10f) * 0.5f;
                    Debug.DrawLine(center, center + Vector3.up * 2f, Color.green, 2f);
                    Debug.Log($"Plane hit → Grid cell: ({x}, {y})");
                }
                else
                {
                    Debug.Log("Hit outside of grid bounds.");
                }
            }
            else
            {
                Debug.Log("Ray did not hit the ground plane.");
            }
        }
    }
}