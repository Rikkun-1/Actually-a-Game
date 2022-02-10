using System;
using GraphProcessor;

[Serializable] [NodeMenuItem("Matrix/TacticalMapCreator")]
public class TacticalMapCreatorNode : BaseMatrixNode
{
    public enum Maps
    {
        DistanceFromThisPositionToAllPositions,
        AmountOfEnemiesThatCanBeSeenFromThisPosition
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
            Maps.DistanceFromThisPositionToAllPositions
                => TacticalMapCreator.DistanceFromThisToAllPositions(game, entity.gridPosition.value),

            Maps.AmountOfEnemiesThatCanBeSeenFromThisPosition
                => TacticalMapCreator.AmountOfEnemiesThatCanBeSeenFromThisPosition(game, entity.teamID.value),

            _ => throw new ArgumentOutOfRangeException()
        };
    }
}