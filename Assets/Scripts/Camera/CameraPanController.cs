using System.Collections;
using Core.GridService.Interface;
using Unity.Cinemachine;
using UnityEngine;

public class CameraPanController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minZoomY = 5f;
    [SerializeField] private float maxZoomY = 25f;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 90f;

    [Header("Clamp Area")]
    [SerializeField] private Vector2 clampX = new Vector2(-50, 50);
    [SerializeField] private Vector2 clampZ = new Vector2(-50, 50);

    [SerializeField] private CinemachineCamera virtualCam;
    [SerializeField] private Transform _followTarget;

    private CinemachineFollow _transposer;
    private IGridService _gridService;

    private void OnEnable()
    {
        _gridService = ReferenceLocator.Instance.GridService;
        _transposer = virtualCam.GetComponentInChildren<CinemachineFollow>();

        if (_followTarget == null || virtualCam == null || _transposer == null)
        {
            Debug.LogError("CameraPanController: Missing required references.");
            enabled = false;
            return;
        }

        StartCoroutine(WaitForGridAndCenterCamera());
    }

    private IEnumerator WaitForGridAndCenterCamera()
    {
       
        while (_gridService.GetGrid() == null)
            yield return null;

        virtualCam.Follow = _followTarget;
        _followTarget.SetParent(virtualCam.transform.parent);

        var grid = _gridService.GetGrid();
      
        var gridMid = grid.GetWorldPosition(10, 10); // center of the grid
        _followTarget.position = gridMid;
        
        Debug.Log("Grid Origin: " + grid.OriginPosition);
        Debug.Log("Target Grid WorldPos (10,10): " + grid.GetWorldPosition(10, 10));
    }

    private void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveDir += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) moveDir += Vector3.back;
        if (Input.GetKey(KeyCode.A)) moveDir += Vector3.left;
        if (Input.GetKey(KeyCode.D)) moveDir += Vector3.right;

        Vector3 move = Quaternion.Euler(0, _followTarget.eulerAngles.y, 0) * moveDir.normalized;
        _followTarget.position += new Vector3(move.x, 0f, move.z) * moveSpeed * Time.deltaTime;

        ClampPosition();
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Vector3 offset = _transposer.FollowOffset;
            offset.y -= scroll * zoomSpeed;
            offset.y = Mathf.Clamp(offset.y, minZoomY, maxZoomY);
            _transposer.FollowOffset = offset;
        }
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _followTarget.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            _followTarget.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void ClampPosition()
    {
        Vector3 pos = _followTarget.position;
        pos.x = Mathf.Clamp(pos.x, clampX.x, clampX.y);
        pos.z = Mathf.Clamp(pos.z, clampZ.x, clampZ.y);
        _followTarget.position = pos;
    }
}
