using System;
using GraphProcessor;

[Serializable] [NodeMenuItem("Matrix/Matrix")]
public class MatrixNode : BaseMatrixNode
{
    [NodeInput("In")]
    public Matrix input;

    protected override void Process()
    {
        output = input;
    }
}