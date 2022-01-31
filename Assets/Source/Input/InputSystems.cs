public sealed class InputSystems : Feature
{
    public InputSystems(Contexts contexts)
    {
        Add(new GetGridClickSystem(contexts));
        Add(new GetInteractiveEntityOnGridClickPositionSystem(contexts));
        
        Add(new AddSelectedOrderComponentToEntitySystem(contexts));
        
        Add(new DrawSelectedInteractiveEntitySystem(contexts));
    }
}