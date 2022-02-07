using UnityEngine;

public class OrthographicCameraController: MonoBehaviour
{
    public          float moveSpeed;
    public          float rotationSpeed;
    public          float sizeChangeSpeed;
    [Min(1)] public float defaultSize;
    
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
    }
    
    private void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleZooming();
    }
    
    private void HandleRotation()
    {
        var rotationDelta = Input.GetAxisRaw("YAxis") * rotationSpeed * Time.unscaledDeltaTime;
        transform.Rotate(Vector3.up, rotationDelta, Space.World);
    }
    
    private void HandleMovement()
    {
        var zoomAcceleratedMoveSpeed = moveSpeed * _camera.orthographicSize / defaultSize;

        var move =  zoomAcceleratedMoveSpeed * Time.unscaledDeltaTime;

        var angle         = transform.rotation.eulerAngles.y;
        var forwardVector = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
        var rightVector   = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.right;
        
        var verticalMove   = forwardVector * (Input.GetAxisRaw("Vertical") * move);
        var horizontalMove = rightVector * (Input.GetAxisRaw("Horizontal") * move);

        transform.Translate(verticalMove + horizontalMove, Space.World);
    }
    
    private void HandleZooming()
    {
        var orthographicSizeDelta = Input.mouseScrollDelta.y * sizeChangeSpeed * Time.unscaledDeltaTime;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize + orthographicSizeDelta, 1, float.MaxValue);
    }
}