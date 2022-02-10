using System;
using Entitas;
using UnityEngine;

public class WeaponListener : MonoBehaviour, IEventListener, IWeaponListener
{ 
    public event Action<Weapon, WeaponVFX> OnWeaponChanged;
    
    private GameEntity _entity;

    private void Start()
    {
        if (_entity.hasWeapon) OnWeapon(_entity, _entity.weapon.weapon, _entity.weapon.weaponView);
    }
    
    public void RegisterEventListeners(IEntity entity)
    {
        _entity = (GameEntity)entity;
        _entity.AddWeaponListener(this);
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
