using ProceduralToolkit;
using Source;
using UnityEngine;

public static class ShootHelper
{
    public static void Shoot(Vector2 shootingPosition, float shootingAngle, WeaponComponent weapon, long shooterID, int shooterTeamID)
    {
        var velocity = Quaternion.Euler(0, shootingAngle, 0) * Vector3.forward * weapon.bulletSpeed;

        var e = EntityCreator.CreateGameEntity();
        e.AddBullet(weapon.bulletDamage, shooterID, shooterTeamID);
        e.AddViewPrefab(weapon.bulletPrefab);
        e.AddWorldPosition(shootingPosition);
        e.AddVelocity(velocity.ToVector2XZ());
    }
}