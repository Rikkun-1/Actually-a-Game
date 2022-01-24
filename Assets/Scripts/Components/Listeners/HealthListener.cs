using Entitas;
using TMPro;
using UnityEngine;

public class HealthListener : MonoBehaviour, IEventListener, IHealthListener
{
    public delegate void HealthChangedAction(int newHealth, int newMaxHealth);

    public event HealthChangedAction OnHealthChanged;
    
    private GameEntity      _entity;
    
    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddHealthListener(this);
        
        if (_entity.hasHealth) OnHealth(_entity, _entity.health.currentHealth, _entity.health.maxHealth);
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveHealthListener(this, false);
    }
    
    public void OnHealth(GameEntity entity, int currentHealth, int maxHealth)
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
