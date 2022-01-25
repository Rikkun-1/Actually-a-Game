using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthListener))]
public class HealthBar : MonoBehaviour
{
    public  Image      healthBarImage;
    public  GameObject takenPrefab;
    
    private int   _previousHealth;
    private float _parentWidth;
    private bool  _isHidden;

    private void Start()
    {
        _parentWidth = GetComponent<RectTransform>().rect.width;

        GetComponent<HealthListener>().OnHealthChanged += UpdateHealthBar;
    }
    
    private void UpdateHealthBar(int newHealth, int newMaxHealth)
    {
        newHealth = Mathf.Clamp(newHealth, 0, newMaxHealth);
        
        VisualizeTakenFragment(newHealth, newMaxHealth);
        UpdateFillAmount(newHealth, newMaxHealth);
        DestroyIfZeroHealth(newHealth, newMaxHealth);
        
        _previousHealth = newHealth;
    }

    private void DestroyIfZeroHealth(int newHealth, int newMaxHealth)
    {
        if (newHealth <= 0 && newMaxHealth != 0)
        {
            StartCoroutine(HideHealthBar());
        }
        else if(_isHidden)
        {
            ShowHealthBar();
        }
    }

    private void UpdateFillAmount(int newHealth, int newMaxHealth)
    {
        healthBarImage.fillAmount = (float)newHealth / newMaxHealth;
    }

    private void VisualizeTakenFragment(int newHealth, int newMaxHealth)
    {
        var x     = _parentWidth * newHealth / newMaxHealth;
        var width = _parentWidth * (_previousHealth - newHealth) / newMaxHealth;

        Instantiate(takenPrefab, transform)
           .GetComponent<TakenHealthScript>()
           .Init(x, width);
    }

    private IEnumerator HideHealthBar()
    {
        var wait = new WaitForSecondsRealtime(3);
        yield return wait;
        
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        _isHidden = true;
    }
    
    private void ShowHealthBar()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        _isHidden = false;
    }
}