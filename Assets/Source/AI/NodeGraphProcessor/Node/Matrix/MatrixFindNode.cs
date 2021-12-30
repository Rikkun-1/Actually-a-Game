using System;
using GraphProcessor;

[Serializable] [NodeMenuItem("Matrix/MatrixFind")]
public class MatrixFindNode : BaseMatrixNode
{
    public enum Find
    {
        MIN,
        MAX
    }

    [NodeOutput("X")]
    public int x;

    [NodeOutput("Y")]
    public int y;

    [NodeOutput("Value")]
    public int value;

    public Find find;

    [NodeInput("In")]
    public Matrix input;

    protected override void Process()
    {
        if (input is null) return;

        switch (find)
        {
            case Find.MIN:
                (x, y, value) = input.Min(val => val > 0);
                break;
            case Find.MAX:
                (x, y, value) = input.Max(val => val > 0);
                break;
            default: throw new ArgumentOutOfRangeException();
        }

        output = input;
    }
}