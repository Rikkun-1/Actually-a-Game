using Entitas;

[Game]
public sealed class WeaponComponent : IComponent
{
    public int    bulletDamage;
    public int    bulletSpeed;
    public string bulletPrefab;
}