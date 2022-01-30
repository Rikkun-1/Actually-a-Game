using System.Collections.Generic;
using System.Linq;
using Entitas;

public class GetInteractiveEntityOnGridClickPositionSystem : ReactiveSystem<InputEntity>
{
    private readonly GameContext  _game;
    private readonly InputContext _input;

    public GetInteractiveEntityOnGridClickPositionSystem(Contexts contexts) : base(contexts.input)
    {
        _game  = contexts.game;
        _input = contexts.input;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.MouseGridClickPosition);
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasMouseGridClickPosition;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var e in entities)
        {
            var entitiesOnPosition = _game.GetEntitiesWithGridPosition(e.mouseGridClickPosition.value);
            var interactiveEntity = entitiesOnPosition.FirstOrDefault(entity => entity.isInteractive);

            if (interactiveEntity != null)
            {
                _input.ReplaceSelectedEntity(interactiveEntity.id.value);
            }
        }
    }
}