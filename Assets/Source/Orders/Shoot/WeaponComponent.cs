using Entitas;

[Game]
public sealed class WeaponComponent : IComponent
{
    public int    bulletSpeed;
    public int    bulletDamage;
    public string bulletPrefab;
}