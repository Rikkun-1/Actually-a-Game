using Entitas.Unity;
using UnityEngine;

[RequireComponent(typeof(EntityLink))]
[RequireComponent(typeof(CharacterLocomotion))]
public class AnimationUpdater : MonoBehaviour
{
    private CharacterLocomotion _characterLocomotion;
    private GameEntity          _entity;

    private void Start()
    {
        _entity              = (GameEntity)GetComponent<EntityLink>().entity;
        _characterLocomotion = GetComponent<CharacterLocomotion>();
    }

    private void Update()
    {
        if (_entity is { hasVelocity: true })
        {
            _characterLocomotion.CalculateAnimationVelocity(_entity.velocity.value);
        }
    }
}