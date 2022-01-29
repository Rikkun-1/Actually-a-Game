public static class BulletCollisionHelper
{
    public static (GameEntity bullet, GameEntity sufferer) SplitToBulletAndSufferer(GameEntity firstEntity,
                                                                                    GameEntity secondEntity)
    {
        return firstEntity.hasBullet
                   ? (firstEntity, secondEntity)
                   : (secondEntity, firstEntity);
    }
}