﻿using System.Collections.Generic;
using Entitas;

[Game]
public sealed class DamageComponent : IComponent
{
    public List<Damage> damageList;
}

public class Damage
{
    public int  damage;
    public long damageDealerID;

    public Damage()
    {
    }

    public Damage(long damageDealerID, int damage)
    {
        this.damageDealerID = damageDealerID;
        this.damage         = damage;
    }
}