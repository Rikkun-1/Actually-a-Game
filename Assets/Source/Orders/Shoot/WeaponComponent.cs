using Entitas;

[Game]
public sealed class WeaponComponent : IComponent
{
    public int    bulletDamage;
    public string bulletPrefab;
    public int    bulletSpeed;
}