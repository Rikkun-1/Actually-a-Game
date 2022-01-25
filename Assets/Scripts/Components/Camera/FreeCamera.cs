using UnityEngine;
using UnityEngine.Rendering;

public class FreeCamera : MonoBehaviour
{
    public float lookSpeedMouse = 4.0f;

    public float moveSpeed = 10.0f;

    public float turbo = 10.0f;

    private float _inputRotateAxisX, _inputRotateAxisY;
    private float _inputVertical,    _inputHorizontal, _inputYAxis;
    private bool  _leftShiftBoost,   _leftShift,       _fire1;

    private void Update()
    {
        // If the debug menu is running, we don't want to conflict with its inputs.
        if (DebugManager.instance.displayRuntimeUI)
            return;

        UpdateInputs();

        var moved = _inputRotateAxisX != 0.0f || _inputRotateAxisY != 0.0f || _inputVertical != 0.0f || _inputHorizontal != 0.0f || _inputYAxis != 0.0f;
        if (moved)
        {
            var rotationX    = transform.localEulerAngles.x;
            var newRotationY = transform.localEulerAngles.y + _inputRotateAxisX;

            // Weird clamping code due to weird Euler angle mapping...
            var newRotationX = rotationX - _inputRotateAxisY;
            if (rotationX <= 90.0f && newRotationX >= 0.0f)
                newRotationX = Mathf.Clamp(newRotationX, 0.0f, 90.0f);
            if (rotationX >= 270.0f)
                newRotationX = Mathf.Clamp(newRotationX, 270.0f, 360.0f);

            transform.localRotation = Quaternion.Euler(newRotationX, newRotationY, transform.localEulerAngles.z);

            var moveSpeed = Time.unscaledDeltaTime * this.moveSpeed;
            if (_fire1 || _leftShiftBoost && _leftShift)
                moveSpeed *= turbo;
            transform.position += transform.forward * moveSpeed * _inputVertical;
            transform.position += transform.right * moveSpeed * _inputHorizontal;
            transform.position += Vector3.up * moveSpeed * _inputYAxis;
        }
    }

    private void UpdateInputs()
    {
        _inputRotateAxisX = 0.0f;
        _inputRotateAxisY = 0.0f;
        _leftShiftBoost   = false;
        _fire1            = false;

        if (Input.GetMouseButton(1))
        {
            _leftShiftBoost   = true;
            _inputRotateAxisX = Input.GetAxisRaw("Mouse X") * lookSpeedMouse;
            _inputRotateAxisY = Input.GetAxisRaw("Mouse Y") * lookSpeedMouse;
        }

        _leftShift = Input.GetKey(KeyCode.LeftShift);
        _fire1     = Input.GetAxisRaw("Fire1") > 0.0f;

        _inputVertical   = Input.GetAxisRaw("Vertical");
        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        _inputYAxis      = Input.GetAxisRaw("YAxis");
    }
}