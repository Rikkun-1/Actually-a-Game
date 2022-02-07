using UnityEngine;

public class OrthographicCameraController: MonoBehaviour
{
    public float maxTranslationMagnitude;
    public float moveSpeed;
    public float rotationSpeed;
    public float sizeChangeSpeed;

    public          float minimumCameraSize;
    public          float maximumCameraSize;
    [Min(1)] public float defaultCameraSize;
    
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
        var zoomAcceleratedMoveSpeed = moveSpeed * _camera.orthographicSize / defaultCameraSize;

        var move =  zoomAcceleratedMoveSpeed * Time.unscaledDeltaTime;

        var angle         = transform.rotation.eulerAngles.y;
        var forwardVector = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
        var rightVector   = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.right;
        
        var verticalMove   = forwardVector * (Input.GetAxisRaw("Vertical") * move);
        var horizontalMove = rightVector * (Input.GetAxisRaw("Horizontal") * move);

        var translation = verticalMove + horizontalMove;
        translation = Vector3.ClampMagnitude(translation, maxTranslationMagnitude);
        transform.Translate(translation, Space.World);
    }
    
    private void HandleZooming()
    {
        var orthographicSizeDelta = Input.mouseScrollDelta.y * sizeChangeSpeed * Time.unscaledDeltaTime;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize + orthographicSizeDelta, 
                                               minimumCameraSize,
                                               maximumCameraSize);
    }
}