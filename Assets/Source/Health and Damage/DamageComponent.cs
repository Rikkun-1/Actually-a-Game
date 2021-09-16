using System.Collections.Generic;
using Entitas;

[Game]
public sealed class DamageComponent : IComponent
{
    public List<Damage> damageList;
}

public class Damage
{
    public long damageDealerID;
    public int  damage;

    Damage()
    {
    }
    
    public Damage(long damageDealerID, int damage)
    {
        this.damageDealerID = damageDealerID;
        this.damage         = damage;
    }
}