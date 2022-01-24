using ProceduralToolkit;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Aiming : MonoBehaviour
{
    public Transform target;
    public Transform aimTargetIK;

    public Rig   bodyAim;
    public Rig   weaponAim;
    public float maxAimingAngle;
    public float speedOfAimingWeightDecreasing;

    private void Update()
    {
        if (!target) return;

        aimTargetIK.position = target.position;

        var targetDirection = aimTargetIK.position - transform.position;
        var angleToTarget   = Vector2.Angle(transform.forward.ToVector2XZ(), targetDirection.ToVector2XZ());

        if (angleToTarget > maxAimingAngle)
        {
            var weight =  1 - (angleToTarget - maxAimingAngle) / maxAimingAngle * speedOfAimingWeightDecreasing;
            bodyAim.weight   = weight;
            weaponAim.weight = weight;
            return;
        }

        bodyAim.weight   = 1;
        weaponAim.weight = 1;
    }
}