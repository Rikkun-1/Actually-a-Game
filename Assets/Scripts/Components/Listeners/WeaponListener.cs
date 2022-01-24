using Entitas;
using UnityEngine;

public class WeaponListener : MonoBehaviour, IEventListener, IWeaponListener
{
    public delegate void WeaponChanged(Weapon weapon, WeaponVFX weaponView);

    public event WeaponChanged OnWeaponChanged;
    
    private GameEntity _entity;
    
    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddWeaponListener(this);

        if (_entity.hasWeapon) OnWeapon(_entity, _entity.weapon.weapon, _entity.weapon.weaponView);
    }

    public void UnregisterEventListeners()
    {
        _entity.RemoveWeaponListener(this, false);
    }

    public void OnWeapon(GameEntity entity, Weapon weapon, WeaponVFX weaponView)
    {
        OnWeaponChanged?.Invoke(weapon, weaponView);
    }
}
