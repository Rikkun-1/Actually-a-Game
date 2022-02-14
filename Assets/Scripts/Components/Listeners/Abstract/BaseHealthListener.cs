using UnityEngine;

[AddComponentMenu("")] // hide in component menu
public abstract class BaseHealthListener: BaseEventListener, IHealthListener
{
    public abstract void OnHealth(GameEntity entity, int currentHealth, int maxHealth);
    protected override void Register()                 => gameEntity.AddHealthListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveHealthListener(this, false);
}
