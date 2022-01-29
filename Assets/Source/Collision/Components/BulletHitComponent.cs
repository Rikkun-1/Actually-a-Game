using Entitas;
using UnityEngine;

[Game]
public sealed class BulletHitComponent : IComponent
{
    public long       bulletEntityID;
    public long       colliderEntityID;
    public RaycastHit raycastHit;
}