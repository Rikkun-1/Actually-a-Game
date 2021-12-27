using Source.View.Debug;

public sealed class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts)
    {
        Add(new DrawDebugSystems(contexts));
        Add(new UpdateUnityViewSystem(contexts));
        Add(new DestroyViewOnEntityDestroyedSystem(contexts));
    }
}