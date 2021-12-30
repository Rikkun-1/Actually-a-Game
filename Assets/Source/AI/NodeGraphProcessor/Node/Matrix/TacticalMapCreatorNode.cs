using System;
using GraphProcessor;

[Serializable] [NodeMenuItem("Matrix/TacticalMapCreator")]
public class TacticalMapCreatorNode : BaseMatrixNode
{
    public enum Maps
    {
        CREATE_DISTANCE_FROM_THIS_POSITION_TO_ALL_POSITIONS_MAP,
        CREATE_AMOUNT_OF_ENEMIES_THAT_CAN_BE_SEEN_FROM_THIS_POSITION_MAP
    }

    [NodeInput("Entity ID")] [ShowAsDrawer]
    public long entityID;

    public Maps map;

    protected override void Process()
    {
        var game   = Contexts.sharedInstance.game;
        var entity = game.GetEntityWithId(entityID);

        output = map switch
        {
            Maps.CREATE_DISTANCE_FROM_THIS_POSITION_TO_ALL_POSITIONS_MAP
                => TacticalMapCreator.CreateDistanceFromThisPositionToAllPositionsMap(game, entity.gridPosition.value),

            Maps.CREATE_AMOUNT_OF_ENEMIES_THAT_CAN_BE_SEEN_FROM_THIS_POSITION_MAP
                => TacticalMapCreator.CreateAmountOfEnemiesThatCanBeSeenFromThisPositionMap(game, entity.teamID.value),

            _ => throw new ArgumentOutOfRangeException()
        };
    }
}