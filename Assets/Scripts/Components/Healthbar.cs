using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthListener))]
public class Healthbar : MonoBehaviour
{
    private int _maxHealth;
    private int _targetHealth;
    
    public Image      targetHealthBar;
    public GameObject takenPrefab;

    private void Start()
    {
        GetComponent<HealthListener>().OnHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(int newHealth, int newMaxHealth)
    {
        _maxHealth = newMaxHealth;
        
        newHealth = Mathf.Clamp(newHealth, 0, _maxHealth);

        var parentWidth = GetComponent<RectTransform>().rect.width;
        
        var x     = parentWidth * newHealth / _maxHealth;
        var width = parentWidth * (_targetHealth - newHealth) / _maxHealth;

        Instantiate(takenPrefab, transform)
           .GetComponent<TakenHealthScript>()
           .Init(x, width);
        
        _targetHealth = newHealth;
    }

    private void Update()
    {
        targetHealthBar.fillAmount = (float)_targetHealth / _maxHealth;
        
        if (_targetHealth <= 0)
        {
            StartCoroutine(HideHealthBar());    
        }
    }

    private IEnumerator HideHealthBar()
    {
        var wait = new WaitForSecondsRealtime(3);
        yield return wait;
        
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}