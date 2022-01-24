using UnityEngine;

public static class ShootHelper
{
    public static void Shoot(GameEntity shooter, WeaponComponent weapon)
    {
        if (!GunIsReady(weapon) || !shooter.hasUnityView) return;
        
        PlaceVFX(shooter, weapon);
        InternalShoot(shooter, weapon);
    }

    private static void InternalShoot(GameEntity shooter, WeaponComponent weapon)
    {
        for (var i = 0; i < weapon.weapon.bulletsPerShot; i++)
        {
            CreateBullet(shooter, weapon, GetDeviatedVelocity(weapon));
        }

        weapon.weapon.timeOfLastShot = GameTime.timeFromStart;
        shooter.UpdateWeapon();
    }

    private static void PlaceVFX(GameEntity shooter, WeaponComponent weapon)
    {
        weapon.weaponView ??= shooter.unityView.gameObject.GetComponentInChildren<WeaponVFX>();
        weapon.weaponView.PlayShootEffects();
    }

    private static bool GunIsReady(WeaponComponent weapon)
    {
        return GameTime.timeFromStart > weapon.weapon.timeOfLastShot + weapon.weapon.delayBetweenShots;
    }

    private static Vector3 GetDeviatedVelocity(WeaponComponent weapon)
    {
        var dispersal = weapon.weapon.dispersal;

        var velocity = weapon.weaponView.barrelEnd.forward.normalized * weapon.weapon.bulletSpeed;
        velocity += new Vector3().Randomize(dispersal, dispersal);

        return velocity;
    }

    private static void CreateBullet(GameEntity shooter, WeaponComponent weapon, Vector3 velocity)
    {
        var bullet = EntityCreator.CreateGameEntity();
        bullet.AddBullet(weapon.weapon.bulletDamage, shooter.id.value, shooter.teamID.value);
        bullet.AddViewPrefab(weapon.weapon.bulletPrefab);
        bullet.AddWorldPosition(weapon.weaponView.barrelEnd.position);
        bullet.AddVelocity(velocity);
    }
}