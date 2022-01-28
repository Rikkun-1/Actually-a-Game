public class Weapon
{
    public int    bulletSpeed;
    public int    bulletDamage;
    public string bulletPrefab;

    Weapon()
    {
    }
    
    public Weapon(int bulletSpeed, int bulletDamage, string bulletPrefab)
    {
        this.bulletSpeed  = bulletSpeed;
        this.bulletDamage = bulletDamage;
        this.bulletPrefab = bulletPrefab;
    }
}