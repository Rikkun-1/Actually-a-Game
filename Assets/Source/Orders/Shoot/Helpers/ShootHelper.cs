using ProceduralToolkit;
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
            CreateBullet(shooter, weapon, GetDeviatedVelocity(shooter.vision.directionAngle, weapon));
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

    private static Vector3 GetDeviatedVelocity(float angle, WeaponComponent weapon)
    {
        var dispersal = weapon.weapon.dispersal;

        var velocity = Quaternion.Euler(0, angle, 0) * Vector3.forward * weapon.weapon.bulletSpeed;
        velocity += new Vector3().Randomize(dispersal, dispersal);
        return velocity;
    }

    private static void CreateBullet(GameEntity shooter, WeaponComponent weapon, Vector3 velocity)
    {
        var bullet = GameEntityCreator.CreateEntity();
        bullet.AddBullet(weapon.weapon.bulletDamage, shooter.id.value, shooter.teamID.value);
        bullet.AddViewPrefab(weapon.weapon.bulletPrefab);

        var position      = shooter.worldPosition.value;
        var offset        = new Vector3(0.116f, 1.3f, 0.25f);
        var rotatedOffset = RotatePointAroundPivot(offset, 
                                                   Vector3.zero, 
                                                   new Vector3(0, shooter.vision.directionAngle, 0));
        
        bullet.AddWorldPosition(position + rotatedOffset);
        bullet.enablePreviousWorldPositionMemorization = true;
        bullet.AddVelocity(velocity);
    }
    
    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
        Vector3 dir = point - pivot;            // get point direction relative to pivot
        dir   = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot;                    // calculate rotated point
        return point;                           // return it
    }
}