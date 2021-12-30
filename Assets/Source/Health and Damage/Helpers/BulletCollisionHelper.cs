public static class BulletCollisionHelper
{
    public static (GameEntity bullet, GameEntity sufferer) SplitToBulletAndSufferer(GameEntity firstEntity, 
                                                                                    GameEntity secondEntity)
    {
        return firstEntity.hasBullet 
                   ? (firstEntity, secondEntity) 
                   : (secondEntity, firstEntity);
    }
    
    public static bool IsCollisionBetweenBulletAndOtherEntity(GameEntity firstEntity, GameEntity secondEntity)
    {
        if (firstEntity == null || secondEntity == null)     return false;
        if (firstEntity.hasBullet == secondEntity.hasBullet) return false;
        return true;
    }
}