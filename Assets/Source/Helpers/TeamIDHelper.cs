using System.Collections.Generic;
using System.Linq;

public static class TeamIDHelper
{
    public static List<int> GetPossibleTeamIDs(GameContext game)
    {
        var entitiesWithTeam = game.GetGroup(GameMatcher.TeamID);

        return entitiesWithTeam.GetEntities()
                               .Select(e => e.teamID.value)
                               .Distinct()
                               .ToList();
    }

    public static List<int> GetEnemyTeamIDs(GameContext game, int entityTeamID)
    {
        var enemyTeamIDs = new List<int>();
        enemyTeamIDs.AddRange(TeamIDHelper.GetPossibleTeamIDs(game)
                                          .Where(id => id != entityTeamID));
        return enemyTeamIDs;
    }
}
