using Entitas;

[Game]
public sealed class ShootOrderComponent : IOrderComponent, IRequiresTarget
{
    public Target target { get; set; }
}