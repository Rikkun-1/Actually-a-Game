using Entitas;

[Game]
public class LookDirectionOrderComponent : IComponent
{
    private int _angle;

    public int angle
    {
        get => _angle;
        set => _angle = value % 360;
    }
}