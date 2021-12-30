using Entitas;

[Game]
public sealed class BulletComponent : IComponent
{
    public int  damage;
    public long shooterID;
}