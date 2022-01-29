using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Unique]
public sealed class BulletHitEffectComponent : IComponent
{
    public ParticleSystem bulletHitPrefab;
    public ParticleSystem bulletHitCachedInstance;
}