using System;
using GraphProcessor;
using UnityEngine.Rendering;

[Serializable] [NodeMenuItem("Matrix/MatrixCompareFunction")]
public class MatrixCompareFunctionNode : BaseMatrixBinaryFunctionNode
{
    public CompareFunction compareFunction = CompareFunction.LessEqual;

    protected override void Process()
    {
        if (inputA is null || inputB is null) return;

        output = compareFunction switch
        {
            CompareFunction.Disabled     => inputA,
            CompareFunction.Never        => inputA * 0,
            CompareFunction.Less         => inputA <  inputB,
            CompareFunction.Equal        => inputA == inputB,
            CompareFunction.LessEqual    => inputA <= inputB,
            CompareFunction.Greater      => inputA >  inputB,
            CompareFunction.NotEqual     => inputA != inputB,
            CompareFunction.GreaterEqual => inputA >= inputB,
            CompareFunction.Always       => inputA.ForEach(_ => 1),
            _                            => throw new ArgumentOutOfRangeException()
        };
    }
}