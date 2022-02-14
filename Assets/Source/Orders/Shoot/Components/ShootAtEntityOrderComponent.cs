[Game]
public sealed class ShootAtEntityOrderComponent : IOrderComponent, IRequiresTargetID
{
    public long targetID { get; set; }
}