using Entitas;

[Game]
public sealed class CollisionComponent : IComponent
{
    public long firstID;
    public long secondID;
}