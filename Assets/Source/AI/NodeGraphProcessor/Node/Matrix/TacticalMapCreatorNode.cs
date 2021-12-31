using System;
using GraphProcessor;

[Serializable] [NodeMenuItem("Matrix/TacticalMapCreator")]
public class TacticalMapCreatorNode : BaseMatrixNode
{
    public enum Maps
    {
        DISTANCE_FROM_THIS_POSITION_TO_ALL_POSITIONS,
        AMOUNT_OF_ENEMIES_THAT_CAN_BE_SEEN_FROM_THIS_POSITION
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
            Maps.DISTANCE_FROM_THIS_POSITION_TO_ALL_POSITIONS
                => TacticalMapCreator.DistanceFromThisToAllPositions(game, entity.gridPosition.value),

            Maps.AMOUNT_OF_ENEMIES_THAT_CAN_BE_SEEN_FROM_THIS_POSITION
                => TacticalMapCreator.AmountOfEnemiesThatCanBeSeenFromThisPosition(game, entity.teamID.value),

            _ => throw new ArgumentOutOfRangeException()
        };
    }
}