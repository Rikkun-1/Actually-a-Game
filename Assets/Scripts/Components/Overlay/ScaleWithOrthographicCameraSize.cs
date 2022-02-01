using UnityEngine;

public class ScaleWithOrthographicCameraSize : MonoBehaviour
{
    public Vector3 minScale;
    public Vector3 maxScale;
    public float   defaultCameraOrthographicSize;
    
    private Camera        _camera;
    private RectTransform _rectTransform;

    private Vector3 _defaultScale;

    private void Start()
    {
        _camera        = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
        _defaultScale  = _rectTransform.localScale;
    }

    private void Update()
    {
        var newScale = _defaultScale * _camera.orthographicSize / defaultCameraOrthographicSize;
        newScale.x = Mathf.Clamp(newScale.x, minScale.x, maxScale.x);
        newScale.y = Mathf.Clamp(newScale.y, minScale.y, maxScale.y);
        
        _rectTransform.localScale = newScale;
        transform.forward         = _camera.transform.forward;
    }
}