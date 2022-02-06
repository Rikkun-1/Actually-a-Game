public sealed class EachFrameExecutionSystems : Feature
{
    public EachFrameExecutionSystems(Contexts contexts)
    {
        // Add(new DrawDebugSystems(contexts));
        
        Add(new InputSystems(contexts));
        
        Add(new ImmediateOrderExecutionSystems(contexts));
        Add(new OrderVisualizationSystems(contexts));
        
        Add(new GameEventSystems(contexts));
    }
}