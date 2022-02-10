using Shouldly;

public class describe_collision_systems : entitas_tests
{
    private void describe_delete_collision_system()
    {
        GameEntity e = null;
        
        before = () =>
        {
            Setup();
            systems.Add(new DeleteCollisionsSystem(contexts));
            e = CreateEntity();
        };

        it["destroy entity with collision"] = () =>
        {
            e.AddCollision(0, 0);
            systems.Cleanup();
            e.isDestroyed.ShouldBeTrue();
        };

        it["does not destroy entity without collision"] = () =>
        {
            systems.Cleanup();
            e.isDestroyed.ShouldBeFalse();
        };
    }
}
