﻿namespace Source.Pathfinding.WalkabilityMap.Systems.Testing
{
    public sealed class WalkabilityMapTestingSystems : Feature
    {
        public WalkabilityMapTestingSystems(Contexts contexts)
        {
            Add(new TestGridNonWalkableSystem(contexts));
            Add(new TestGridWallsSystem(contexts));
        }
    }
}