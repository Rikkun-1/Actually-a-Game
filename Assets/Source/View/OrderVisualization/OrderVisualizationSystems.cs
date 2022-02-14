public sealed class OrderVisualizationSystems : Feature
{
    public OrderVisualizationSystems(Contexts contexts)
    {
        Add(new VisualizeCursorSystem(contexts));
        Add(new VisualizeSelectedEntitySystem(contexts));
        Add(new VisualizeMoveToPositionOrderSystem(contexts));
        Add(new VisualizePathSystem(contexts));
        Add(new VisualizeLookAtDirectionOrderSystem(contexts));
        Add(new VisualizeLookAtPositionOrderSystem(contexts));
        Add(new VisualizeShootAtEntityOrderSystem(contexts));
    }
}