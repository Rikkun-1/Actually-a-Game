using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

public class AddSelectedOrderComponentToEntitySystem : ReactiveSystem<InputEntity>
{
    private readonly GameContext  _game;
    private readonly InputContext _input;

    public AddSelectedOrderComponentToEntitySystem(Contexts contexts) : base(contexts.input)
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
        if (!_input.hasSelectedEntity || !_input.hasSelectedOrder) return;

        var selectedEntity = _game.GetEntityWithId(_input.selectedEntity.gameEntityID);
        if (selectedEntity == null) return;

        var selectedOrderName  = _input.selectedOrder.orderName;
        var selectedOrderIndex = Array.IndexOf(GameComponentsLookup.componentNames, selectedOrderName);
        var selectedOrderType  = GameComponentsLookup.componentTypes[selectedOrderIndex];

        var selectedOrderComponent = selectedEntity.CreateComponent(selectedOrderIndex, selectedOrderType);

        SetupComponent(selectedEntity, selectedOrderComponent);

        selectedEntity.ReplaceComponent(selectedOrderIndex, selectedOrderComponent);

        Clean();
    }

    private void Clean()
    {
        _input.RemoveSelectedOrder();
        _input.RemoveSelectedEntity();
        _input.RemoveMouseGridClickPosition();
    }

    private void SetupComponent(GameEntity selectedEntity, IComponent selectedOrderComponent)
    {
        var mouseGridClickPosition = _input.mouseGridClickPosition.value;

        switch (selectedOrderComponent)
        {
            case IRequiresVector2IntPosition requiresVector2IntPosition:
                {
                    requiresVector2IntPosition.position = mouseGridClickPosition;
                    break;
                }
            case IRequiresVector2Position requiresVector2Position:
                {
                    requiresVector2Position.position = mouseGridClickPosition;
                    break;
                }
            case IRequiresDirection requiresDirection:
                {
                    var direction = mouseGridClickPosition - selectedEntity.gridPosition.value;
                    requiresDirection.direction = direction;
                    break;
                }
            case IRequiresTargetID requiresTargetID:
                {
                    requiresTargetID.targetID = _game.GetEntitiesWithGridPosition(mouseGridClickPosition)
                                                     .First(e => e.isPlayer).id.value;
                    break;
                }
        }
    }
}