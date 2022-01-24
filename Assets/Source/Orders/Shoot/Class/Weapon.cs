public class Weapon
{
    public float  timeOfLastShot;
    public float  delayBetweenShots;
    public float  dispersal;
    public int    bulletDamage;
    public int    bulletSpeed;
    public int    bulletsPerShot;
    public string bulletPrefab;

    public Weapon(float timeOfLastShot, float delayBetweenShots, float dispersal, int bulletDamage, int bulletSpeed, int bulletsPerShot, string bulletPrefab)
    {
        this.timeOfLastShot    = timeOfLastShot;
        this.delayBetweenShots = delayBetweenShots;
        this.dispersal         = dispersal;
        this.bulletDamage      = bulletDamage;
        this.bulletSpeed       = bulletSpeed;
        this.bulletsPerShot    = bulletsPerShot;
        this.bulletPrefab      = bulletPrefab;
    }
}
