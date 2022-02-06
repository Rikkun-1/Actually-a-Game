﻿public sealed class ExecuteShootOrderSystems : Feature
{
    public ExecuteShootOrderSystems(Contexts contexts)
    {
        Add(new ExecuteShootAtDirectionOrderSystem(contexts));
        Add(new ExecuteShootAtPositionOrderSystem(contexts));
        Add(new ExecuteShootAtEntityOrderSystem(contexts));
    }
}