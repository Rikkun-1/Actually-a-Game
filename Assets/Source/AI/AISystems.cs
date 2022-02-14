public sealed class AISystems : Feature
{
    public AISystems(Contexts contexts)
    {
        Add(new ProcessAISystem(contexts));
    }
}