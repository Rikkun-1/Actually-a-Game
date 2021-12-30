using System.Collections.Generic;
using Entitas;

[Game]
public sealed class DamageComponent : IComponent
{
    public List<Damage> damageList;
}