using Entitas;

[Game]
public sealed class WeaponComponent : IComponent
{
    public float  timeOfLastShot;
    public float  delayBetweenShots;
    public float  dispersal;
    public int    bulletDamage;
    public int    bulletSpeed;
    public int    bulletsPerShot;
    public string bulletPrefab;
}