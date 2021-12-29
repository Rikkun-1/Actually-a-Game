using Entitas;
using ProceduralToolkit;
using UnityEngine;

public class TargetNotVisibleSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly GameContext        _game;

    public TargetNotVisibleSystem(Contexts contexts)
    {
        _game = contexts.game;
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ShootAtEntityOrder,
                                                             GameMatcher.Vision));
    }

    public void Execute()
    {
        foreach (var e in _entities.GetEntities())
        {
            var targetEntityID = e.shootAtEntityOrder.targetID;
            var targetEntity   = _game.GetEntityWithId(targetEntityID);

            if (targetEntity == null)
            {
                TargetLost(e);
                continue;
            }

            if (!targetEntity.hasUnityView) continue;

            var raycastOrigin = e.worldPosition.value.ToVector3XZ();
            raycastOrigin.y = 0.25f;

            var targetPosition = targetEntity.worldPosition.value.ToVector3XZ();
            targetPosition.y = 0.25f;

            var raycastDirection = targetPosition - raycastOrigin;

            //var maxDistance = Mathf.Min(Vector3.Distance(raycastOrigin, targetPosition), e.vision.distance);
            var maxDistance = Vector3.Distance(raycastOrigin, targetPosition);
            //Debug.DrawRay(raycastOrigin, raycastDirection, Color.magenta);

            var raycastHits = Physics.RaycastAll(raycastOrigin, raycastDirection, maxDistance);

            if (raycastHits.Length != 1)
            {
                TargetLost(e);
            }
        }
    }

    private static void TargetLost(GameEntity e)
    {
        e.RemoveShootAtEntityOrder();
        e.RemoveLookAtEntityOrder();
    }
}