using System.Linq;
using Entitas;
using ProceduralToolkit;
using UnityEngine;

public class TargetVisibleSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _entities;
    private readonly IGroup<GameEntity> _possibleTargets;

    public TargetVisibleSystem(Contexts contexts)
    {
        _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TeamID,
                                                             GameMatcher.Vision)
                                                      .NoneOf(GameMatcher.ShootAtEntityOrder));

        _possibleTargets = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.TeamID,
                                                                    GameMatcher.Health));
    }

    public void Execute()
    {
        foreach (var e in _entities.GetEntities())
        {
            var myPosition = e.worldPosition.value;
            var possibleTargets =
                _possibleTargets.GetEntities()
                                .OrderBy(entity => Vector2.Distance(myPosition, entity.worldPosition.value));

            foreach (var targetEntity in possibleTargets)
            {
                if (!targetEntity.hasUnityView) continue;
                if (e.teamID.value == targetEntity.teamID.value) continue;

                var raycastOrigin = e.worldPosition.value.ToVector3XZ();
                raycastOrigin.y = 0.25f;

                var targetPosition = targetEntity.worldPosition.value.ToVector3XZ();
                targetPosition.y = 0.25f;

                var raycastDirection = targetPosition - raycastOrigin;

                //var maxDistance = Mathf.Min(Vector3.Distance(raycastOrigin, targetPosition), e.vision.distance);
                var maxDistance = Vector3.Distance(raycastOrigin, targetPosition);
                //Debug.DrawRay(raycastOrigin, raycastDirection, Color.magenta);

                var raycastHits = Physics.RaycastAll(raycastOrigin, raycastDirection, maxDistance);

                if (raycastHits.Length == 1)
                {
                    e.ReplaceShootAtEntityOrder(targetEntity.id.value);
                }
            }
        }
    }
}