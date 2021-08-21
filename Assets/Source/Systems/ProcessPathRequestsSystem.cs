using System.Collections.Generic;
using System.Linq;
using Entitas;
using Roy_T.AStar.Paths;
using Roy_T.AStar.Primitives;

public class ProcessPathRequestsSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    private PathFinder pathfinder = new PathFinder();
    private GameEntity _gridHolder;

    public ProcessPathRequestsSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.PathRequest.Added());
    }   

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPathRequest;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        _gridHolder = _contexts.game.GetEntities(GameMatcher.PathfindingGrid)
                               .ToList()
                               .SingleEntity();
        
        foreach (var e in entities)
        {
            var from = e.pathRequest.From;
            var to = e.pathRequest.To;

            var start = new GridPosition(from.x, from.y);
            var end = new GridPosition(to.x, to.y);
            
            var path = pathfinder.FindPath(start, end, _gridHolder.pathfindingGrid.Value);
            
            e.ReplacePath(path, 0);
        }
    }
}