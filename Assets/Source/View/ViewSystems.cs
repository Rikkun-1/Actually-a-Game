public sealed class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts)
    {
        Add(new PlaceBulletHitEffectsSystem(contexts));
        Add(new UpdateUnityViewSystem(contexts));
        Add(new ColorizeTeamsSystem(contexts));
        Add(new VisualizeViewDestructionSystem(contexts));
        Add(new PushBodiesThatHitByBullet(contexts));
        Add(new RotateWallsViewByTypeSystem(contexts));

        Add(new DeleteViewOnEntityDeletedSystem(contexts));
    }
}