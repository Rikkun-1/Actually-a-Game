public sealed class ShootAtEnemiesInSightSystems : Feature
{
    public ShootAtEnemiesInSightSystems(Contexts contexts)
    {
        Add(new TargetLostSystem(contexts));
        Add(new ShootAtEnemyInSightSystem(contexts));
    }
}