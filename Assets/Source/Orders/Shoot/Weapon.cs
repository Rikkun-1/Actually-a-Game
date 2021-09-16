public class Weapon
{
    public int    bulletDamage;
    public string bulletPrefab;
    public int    bulletSpeed;

    private Weapon()
    {
    }

    public Weapon(int bulletSpeed, int bulletDamage, string bulletPrefab)
    {
        this.bulletSpeed  = bulletSpeed;
        this.bulletDamage = bulletDamage;
        this.bulletPrefab = bulletPrefab;
    }
}