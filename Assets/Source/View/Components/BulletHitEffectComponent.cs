using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game] [Unique] [IgnoreSave]
public sealed class BulletHitEffectComponent : IComponent
{
    public ParticleSystem bulletHitPrefab;
    public ParticleSystem bulletHitCachedInstance;
}