using Source.AI;

public sealed class PlanningPhaseSystems : Feature
{
    public PlanningPhaseSystems(Contexts contexts)
    {
        Add(new AISystems(contexts));
    }
}