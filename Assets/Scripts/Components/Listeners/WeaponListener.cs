public abstract class WeaponListener : EventListener, IWeaponListener
{ 
    public virtual void OnWeapon(GameEntity entity, Weapon weapon, WeaponVFX weaponView) {}
    
    protected override void Register()                 => gameEntity.AddWeaponListener(this);
    public override    void UnregisterEventListeners() => gameEntity.RemoveWeaponListener(this, false);
}