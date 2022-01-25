using UnityEngine;

public class OrthographicCameraController: MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float sizeChangeSpeed;
    [Min(1)] public float defaultSize;
    
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
    }
    
    private void Update()
    {
        var angle = transform.rotation.eulerAngles;
        
        angle.y += Input.GetAxisRaw("YAxis") * rotationSpeed * Time.unscaledDeltaTime;

        transform.rotation= Quaternion.Euler(angle);

        var forwardVector = Quaternion.AngleAxis(angle.y, Vector3.up) * Vector3.forward;
        var rightVector   = Quaternion.AngleAxis(angle.y, Vector3.up) * Vector3.right;
        
        var position = transform.position;
        
        var acceleratedMoveSpeed = moveSpeed * _camera.orthographicSize / defaultSize;

        var vertical   = Input.GetAxisRaw("Vertical") * acceleratedMoveSpeed * Time.unscaledDeltaTime;
        var horizontal = Input.GetAxisRaw("Horizontal") * acceleratedMoveSpeed * Time.unscaledDeltaTime;
        
        position += forwardVector * vertical;
        position += rightVector * horizontal;

        transform.position = position;

        var orthographicSize = _camera.orthographicSize;
        orthographicSize         += Input.mouseScrollDelta.y * sizeChangeSpeed * Time.unscaledDeltaTime;
        _camera.orthographicSize =  Mathf.Clamp(orthographicSize, 1, float.MaxValue);
    }
}
