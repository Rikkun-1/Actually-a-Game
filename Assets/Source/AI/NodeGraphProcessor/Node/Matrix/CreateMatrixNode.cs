using System;
using GraphProcessor;

[Serializable] [NodeMenuItem("Matrix/CreateMatrix")]
public class CreateMatrixNode : BaseMatrixNode
{
    [NodeInput("Width")] [ShowAsDrawer]
    public int width;

    [NodeInput("Height")] [ShowAsDrawer]
    public int height;

    [NodeInput("Initial value")] [ShowAsDrawer]
    public int initialValue;

    protected override void Process()
    {
        output = new Matrix(width, height, initialValue);
    }
}