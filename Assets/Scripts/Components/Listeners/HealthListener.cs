using System;
using Entitas;
using UnityEngine;

public class HealthListener : MonoBehaviour, IEventListener, IHealthListener
{
    public delegate void HealthChanged(int newHealth, int newMaxHealth);

    public event HealthChanged OnHealthChanged;

    private GameEntity _entity;

    private void Start()
    {
        if (_entity.hasHealth) OnHealth(_entity, _entity.health.currentHealth, _entity.health.maxHealth);
    }
    
    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddHealthListener(this);
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
