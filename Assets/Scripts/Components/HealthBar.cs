using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int   maxHealth;
    
    //reduce speed
    public float targetChangeSpeed;
    public float delayedChangeSpeed;
    
    public float delayedHealth;
    public float targetHealth;

    public Image delayedHealthBar;
    public Image targetHealthBar;

    private void Start()
    {
        delayedHealth = maxHealth;
        targetHealth  = maxHealth;
        
        GetComponentInParent<HealthListener>().OnHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(int newHealth)
    {
        targetHealth = newHealth;
    }

    private void Update()
    {
        delayedHealth = Mathf.MoveTowards(delayedHealth, targetHealth, Time.unscaledDeltaTime * delayedChangeSpeed);

        targetHealthBar.fillAmount = Mathf.MoveTowards(targetHealthBar.fillAmount, targetHealth / maxHealth, Time.unscaledDeltaTime * targetChangeSpeed / 100);
        
        delayedHealthBar.fillAmount = delayedHealth / maxHealth;

        if (delayedHealth <= 0)
        {
            HideHealthBar();    
        }
    }

    private void HideHealthBar()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}