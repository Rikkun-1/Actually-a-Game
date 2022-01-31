using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using ProceduralToolkit;
using UnityEngine;

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

        if (selectedOrderComponent is IRequiresVector2IntPosition requiresVector2IntPosition)
        {
            requiresVector2IntPosition.position = mouseGridClickPosition;
            DebugE.DrawWireQuadXZ(mouseGridClickPosition.ToVector3XZ(), Quaternion.identity, Vector2.one, Color.magenta, 15f);
            return;
        }

        if (selectedOrderComponent is IRequiresVector2Position requiresVector2Position)
        {
            requiresVector2Position.position = mouseGridClickPosition;
            DebugE.DrawWireQuadXZ(mouseGridClickPosition.ToVector3XZ(), Quaternion.identity, Vector2.one, Color.magenta, 15f);
            return;
        }
        
        if (selectedOrderComponent is IRequiresDirection requiresDirection)
        {
            var direction = mouseGridClickPosition - selectedEntity.gridPosition.value;
            requiresDirection.angle = direction.ToAngle();
            Debug.DrawRay(selectedEntity.worldPosition.value, direction.ToVector3XZ(), Color.magenta, 15f);
            return;
        }
        
        if (selectedOrderComponent is IRequiresTargetID requiresTargetID)
        {
            requiresTargetID.targetID = _game.GetEntitiesWithGridPosition(mouseGridClickPosition)
                                             .First(e => e.isPlayer).id.value;
        }
    }
}