public sealed class DrawDebugSystems : Feature
{
    public DrawDebugSystems(Contexts contexts)
    {
        Add(new DrawPathsSystem(contexts));
        Add(new DrawWalkableTilesSystem(contexts));
    }
}