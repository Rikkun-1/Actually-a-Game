public sealed class TargetVisibilitySystems : Feature
{
    public TargetVisibilitySystems(Contexts contexts)
    {
        Add(new TargetNotVisibleSystem(contexts));
        Add(new TargetVisibleSystem(contexts));
    }
}