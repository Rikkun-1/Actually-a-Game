using Entitas;

[Game]
public sealed class LookAtDirectionOrderComponent : IComponent
{
    private readonly Angle _angle;

    public LookAtDirectionOrderComponent()
    {
        _angle = new Angle();
    }

    public float angle
    {
        get => _angle.value;
        set => _angle.value = value;
    }
}