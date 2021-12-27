public sealed class GridSystems : Feature
{
    public GridSystems(Contexts contexts)
    {
        Add(new UpdateGridSizeSystem(contexts));
        Add(new DeleteEntitiesOutsideGridSystem(contexts));
    }
}