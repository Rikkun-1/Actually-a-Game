using Entitas;
using Entitas.CodeGeneration.Attributes;
using Newtonsoft.Json;

[Game]
[Event(EventTarget.Self)]
public sealed class WeaponComponent : IComponent
{
    public Weapon    weapon;
    [JsonIgnore]
    public WeaponVFX weaponView;
}