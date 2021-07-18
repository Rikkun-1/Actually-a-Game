using UnityEngine;
using Entitas;

public sealed class RootSystems : Feature  {

    public RootSystems(Contexts contexts)
    {
        Add(new CreatePlayerSystem(contexts));
        Add(new HealthSystems(contexts));
    }
}

public sealed class HealthSystems : Feature  {

    public HealthSystems(Contexts contexts)
    {
        Add(new LogHealthSystem(contexts));
        Add(new Health2Systems(contexts));
    }
}

public sealed class Health2Systems : Feature  {

    public Health2Systems(Contexts contexts)
    {
        Add(new LogHealthSystem(contexts));
        Add(new LogHealthSystem(contexts));
    }
}

