public sealed class EachFrameExecutionSystems : Feature
{
    public EachFrameExecutionSystems(Contexts contexts)
    {
        Add(new DrawDebugSystems(contexts));
        Add(new DrawPathsSystem(contexts));
        
        Add(new InputSystems(contexts));
        Add(new GameEventSystems(contexts));
    }
}