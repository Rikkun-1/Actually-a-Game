public static class BulletHelper
{
    public static bool IsSelfHit(GameEntity suffererEntity, GameEntity bulletEntity)
    {
        return bulletEntity.bullet.shooterID == suffererEntity.id.value;
    }

    public static bool IsHitByTeammate(GameEntity suffererEntity, GameEntity bulletEntity)
    {
        return suffererEntity.hasTeamID &&
               bulletEntity.bullet.shooterTeamID == suffererEntity.teamID.value;
    }
}