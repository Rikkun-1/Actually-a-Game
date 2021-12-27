using System;
using GraphProcessor;

[Serializable] [NodeMenuItem("Matrix/MatrixFindNode")]
public class MatrixFindNode : BaseMatrixNode
{
    [NodeOutput("X")]
    public int x;

    [NodeOutput("Y")]
    public int y;

    [NodeOutput("Value")]
    public int value;

    [NodeInput("In")]
    public Matrix input;

    protected override void Process()
    {
        if (input is null) return;

        (x, y, value) = input.Max();
        output        = input;
    }
}