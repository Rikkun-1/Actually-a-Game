using Entitas;
using UnityEngine;

[Physics]
public sealed class BulletHitComponent : IComponent
{
    public long       bulletEntityID;
    public long       colliderEntityID;
    public RaycastHit raycastHit;
}