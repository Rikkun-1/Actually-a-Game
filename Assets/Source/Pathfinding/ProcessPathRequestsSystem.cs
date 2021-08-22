using System.Collections.Generic;
using System.Linq;
using Entitas;
using Roy_T.AStar.Paths;
using Roy_T.AStar.Primitives;

public class ProcessPathRequestsSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts   _contexts;
    private          GameEntity _gridHolder;

    private readonly PathFinder _pathfinder = new PathFinder();

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
            var from = e.pathRequest.from;
            var to   = e.pathRequest.to;

            var start = new GridPosition(from.x, from.y);
            var end   = new GridPosition(to.x,   to.y);

            var path = _pathfinder.FindPath(start, end, _gridHolder.pathfindingGrid.value);

            e.ReplacePath(path, 0);
        }
    }
}