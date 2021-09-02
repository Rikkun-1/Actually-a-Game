public sealed class EachFrameExecutionSystems : Feature
{
    public EachFrameExecutionSystems(Contexts contexts)
    {
        Add(new DrawWalkableTilesSystem(contexts));
        Add(new DrawPathsSystem(contexts));
    }
}