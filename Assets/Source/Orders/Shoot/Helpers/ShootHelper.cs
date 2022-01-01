using ProceduralToolkit;
using Source;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

/// <summary>
/// Some extension methods for <see cref="Random"/> for creating a few more kinds of random stuff.
/// </summary>
public static class RandomExtensions
{
    /// <summary>
    ///   Generates normally distributed numbers. Each operation makes two Gaussians for the price of one, and apparently they can be cached or something for better performance, but who cares.
    /// </summary>
    /// <param name="r"></param>
    /// <param name = "mu">Mean of the distribution</param>
    /// <param name = "sigma">Standard deviation</param>
    /// <returns></returns>
    public static float NextGaussian(float mu = 0, float sigma = 1)
    {
        var u1 = Random.Range(0f, 1f);
        var u2 = Random.Range(0f, 1f);

        var randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                            Mathf.Sin(2.0f * Mathf.PI * u2);

        var randNormal = mu + sigma * randStdNormal;

        return randNormal;
    }
}

public static class ShootHelper
{
    public static void Shoot(Vector2 shootingPosition, float shootingAngle, WeaponComponent weapon, long shooterID, int shooterTeamID)
    {
        if (!(GameTime.timeFromStart > weapon.timeOfLastShot + weapon.delayBetweenShots)) return;

        for (var i = 0; i < weapon.bulletsPerShot; i++)
        {
            var angleDispersal = RandomExtensions.NextGaussian(sigma: weapon.dispersal / 2);

            // var dispersal = Random.Range(-weapon.dispersal, weapon.dispersal);
            var velocity = Quaternion.Euler(0, shootingAngle + angleDispersal, 0) * Vector3.forward * weapon.bulletSpeed ;

            var e = EntityCreator.CreateGameEntity();
            e.AddBullet(weapon.bulletDamage, shooterID, shooterTeamID);
            e.AddViewPrefab(weapon.bulletPrefab);
            e.AddWorldPosition(shootingPosition);
            e.AddVelocity(velocity.ToVector2XZ());
        }

        weapon.timeOfLastShot = GameTime.timeFromStart;
    }
}