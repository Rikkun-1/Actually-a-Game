using Entitas;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthListener : MonoBehaviour, IEventListener, IHealthListener
{
    public delegate void HealthChangedAction(int newHealth);

    public event HealthChangedAction OnHealthChanged;
    
    private TextMeshProUGUI _healthTextField;
    private GameEntity      _entity;
    
    public void RegisterEventListeners(IEntity entity)
    {
        _healthTextField = GetComponent<TextMeshProUGUI>();

        _entity = (GameEntity)entity;
        _entity.AddHealthListener(this);
        
        if (_entity.hasHealth) OnHealth(_entity, _entity.health.value);
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveHealthListener(this, false);
    }
    
    public void OnHealth(GameEntity entity, int value)
    {
        _healthTextField.text = value.ToString();

        OnHealthChanged?.Invoke(value);
    }
}
