public sealed class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts)
    {
        Add(new UpdateUnityViewSystem(contexts));
        Add(new ColorizeTeamsSystem(contexts));
        Add(new DestroyViewOnEntityDestroyedSystem(contexts));
    }
}