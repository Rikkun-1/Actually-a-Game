using System.Collections.Generic;
using System.Linq;
using Entitas;

public class RecalculatePathSystem : ReactiveSystem<GameEntity>
{
    private readonly IGroup<GameEntity> _entitiesWithPath;
    
    public RecalculatePathSystem(Contexts contexts) : base(contexts.game)
    {
        _entitiesWithPath = contexts.game.GetGroup(GameMatcher.Path);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.PathfindingGrid.Added());    
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPathfindingGrid;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in _entitiesWithPath.GetEntities())
        {
            var lastDestination = e.path.waypoints.Last();

            var start = e.gridPosition.value;
            
            e.ReplacePathRequest(start, lastDestination);
            
            e.RemovePath();
        }
    }
}