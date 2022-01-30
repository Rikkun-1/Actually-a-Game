using Entitas;
using ProceduralToolkit;
using UnityEngine;

public class DrawSelectedInteractiveEntitySystem : IExecuteSystem
{
    private readonly InputContext _input;
    private readonly GameContext  _game;

    public DrawSelectedInteractiveEntitySystem(Contexts contexts)
    {
        _input = contexts.input;
        _game  = contexts.game;
    }

    public void Execute()
    {
        if (!_input.hasSelectedEntity) return;
        
        var selectedEntity = _game.GetEntityWithId(_input.selectedEntity.gameEntityID);

        if (selectedEntity != null)
        {
            DebugE.DrawWireCircleXZ(selectedEntity.worldPosition.value, 0.5f, Color.green);
        }
    }
}