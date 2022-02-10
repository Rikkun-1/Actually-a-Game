using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Event(EventTarget.Self)]
public sealed class WeaponComponent : IComponent
{
    public Weapon    weapon;
    public WeaponVFX weaponView;
}