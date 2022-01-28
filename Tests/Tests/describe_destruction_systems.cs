using Shouldly;

public class describe_destruction_systems : entitas_tests
{
    private void describe_destroy_system()
    {
        GameEntity e = null;
        
        before = () =>
        {
            Setup();
            systems.Add(new DestroySystem(contexts));
            e = CreateEntity();
        };

        it["destroys entities flagged as destroyed"] = () =>
        {
            e.isDestroyed = true;
            systems.Cleanup();
            e.isEnabled.ShouldBeFalse();
        };

        it["does not destroy entities not flagged as destroyed"] = () =>
        {
            e.isDestroyed = false;
            systems.Cleanup();
            e.isEnabled.ShouldBeTrue();
        };

        it["does not destroy indestructible entities flagged as destroyed"] = () =>
        {
            e.isDestroyed      = true;
            e.isIndestructible = true;
            systems.Cleanup();
            e.isEnabled.ShouldBeTrue();
        };
    }

    private void describe_remove_destroyed_for_indestructible()
    {
        GameEntity e = null;

        before = () =>
        {
            Setup();
            systems.Add(new RemoveDestroyedForIndestructibleSystem(contexts));
            e = CreateEntity();
        };

        it["removes destroyed for indestructible"] = () =>
        {
            e.isDestroyed      = true;
            e.isIndestructible = true;
            systems.Cleanup();
            e.isDestroyed.ShouldBeFalse(); 
        };
        
        it["does not remove destroyed for not indestructible"] = () =>
        {
            e.isDestroyed      = true;
            e.isIndestructible = false;
            systems.Cleanup();
            e.isDestroyed.ShouldBeTrue(); 
        };
    }
}