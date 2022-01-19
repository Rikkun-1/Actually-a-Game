using UnityEngine;

public static class ShootHelper
{
    public static void Shoot(GameEntity shooter, WeaponComponent weapon)
    {
        if (!GunIsReady(weapon) || !shooter.hasUnityView) return;

        weapon.weaponView ??= shooter.unityView.gameObject.GetComponentInChildren<Weapon>();

        for (var i = 0; i < weapon.bulletsPerShot; i++)
        {
            CreateBullet(shooter, weapon, GetDeviatedVelocity(weapon));
        }

        weapon.weaponView.PlayShootEffects();

        weapon.timeOfLastShot = GameTime.timeFromStart;
    }

    private static bool GunIsReady(WeaponComponent weapon)
    {
        return GameTime.timeFromStart > weapon.timeOfLastShot + weapon.delayBetweenShots;
    }

    private static Vector3 GetDeviatedVelocity(WeaponComponent weapon)
    {
        var velocity = weapon.weaponView.barrelEnd.forward.normalized * weapon.bulletSpeed;
        velocity.x += Random.Range(-weapon.dispersal, weapon.dispersal);
        velocity.y += Random.Range(-weapon.dispersal, weapon.dispersal);
        velocity.z += Random.Range(-weapon.dispersal, weapon.dispersal);

        return velocity;
    }

    private static void CreateBullet(GameEntity shooter, WeaponComponent weapon, Vector3 velocity)
    {
        var bullet = EntityCreator.CreateGameEntity();
        bullet.AddBullet(weapon.bulletDamage, shooter.id.value, shooter.teamID.value);
        bullet.AddViewPrefab(weapon.bulletPrefab);
        bullet.AddWorldPosition(weapon.weaponView.barrelEnd.position);
        bullet.AddVelocity(velocity);
    }
}