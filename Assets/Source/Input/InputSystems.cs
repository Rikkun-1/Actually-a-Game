public sealed class InputSystems : Feature
{
    public InputSystems(Contexts contexts)
    {
        Add(new ProcessMouseInputSystem(contexts));
        Add(new GetInteractiveEntityOnGridClickPositionSystem(contexts));
        
        Add(new AddSelectedOrderComponentToEntitySystem(contexts));
        
        Add(new DrawSelectedInteractiveEntitySystem(contexts));
    }
}