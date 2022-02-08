public sealed class OrderVisualizationSystems : Feature
{
    public OrderVisualizationSystems(Contexts contexts)
    {
        Add(new VisualizeCursorSystem(contexts));
        Add(new VisualizeSelectedEntitySystem(contexts));
        Add(new VisualizeMoveToPositionOrderSystem(contexts));
        Add(new VisualizePathSystem(contexts));
        Add(new VisualizeLookOrderSystem(contexts));
        Add(new VisualizeShootAtEntityOrderSystem(contexts));
    }
}