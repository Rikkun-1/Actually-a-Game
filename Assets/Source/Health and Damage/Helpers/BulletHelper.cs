public static class BulletHelper
{
    public static bool IsSelfHit(GameEntity suffererEntity, GameEntity bulletEntity)
    {
        return bulletEntity.bullet.shooterID == suffererEntity.id.value;
    }
    
    public static bool IsHitByTeammate(GameEntity suffererEntity, GameEntity bulletEntity)
    {
        var shooter = Contexts.sharedInstance.game.GetEntityWithId(bulletEntity.bullet.shooterID);
        
        return suffererEntity.hasTeamID && 
               shooter.hasTeamID &&
               shooter.teamID.value == suffererEntity.teamID.value;
    }
}