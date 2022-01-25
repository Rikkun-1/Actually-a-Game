using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TakenHealthScript : MonoBehaviour
{
    [Min(0)] public float moveSpeed;
    [Min(0)] public float distanceMultiplier;

    private RectTransform _rectTransform;
    private float         _height;
    private float         _startY;
    private float         _destructionY;
    private Image[]       _images;
    
    public void Init(float x, float width)
    {
        _images        = GetComponentsInChildren<Image>();
        _rectTransform = GetComponent<RectTransform>();

        _height = _rectTransform.rect.height;
        
        _startY       = _rectTransform.anchoredPosition.y;
        _destructionY = _startY - _height * distanceMultiplier;
        
        _rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, x, width);
    }
    
    private void Update()
    {
        var newAnchoredPosition = _rectTransform.anchoredPosition.WithYDecreasedBy(moveSpeed * Time.unscaledDeltaTime);
        _rectTransform.anchoredPosition = newAnchoredPosition;
        
        var currentY = newAnchoredPosition.y;
        
        SetImagesAlpha(currentY);

        if (currentY < _destructionY)
        {
            Destroy(gameObject);
        }
    }

    private void SetImagesAlpha(float currentY)
    {
        var percent = (currentY - _startY) / (_destructionY - _startY);

        foreach (var image in _images)
        {
            var color = image.color;
            color.a     = 1 - percent;
            image.color = color;
        }
    }
}