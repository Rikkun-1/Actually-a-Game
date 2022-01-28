using ProceduralToolkit;
using UnityEngine;

public static class ShootHelper
{
    public static void Shoot(Vector2 shootingPosition, float shootingAngle, Weapon weapon)
    {
        var bulletSpeed = weapon.bulletSpeed;
        var velocity    = Quaternion.Euler(0, shootingAngle, 0) * Vector3.forward * bulletSpeed;

        var e = EntityCreator.CreateGameEntity();
        e.AddBullet(weapon.bulletDamage);
        e.AddViewPrefab(weapon.bulletPrefab);
        e.AddWorldPosition(shootingPosition);
        e.AddVelocity(velocity.ToVector2XZ());
    }
}
