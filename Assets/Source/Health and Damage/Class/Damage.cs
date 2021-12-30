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