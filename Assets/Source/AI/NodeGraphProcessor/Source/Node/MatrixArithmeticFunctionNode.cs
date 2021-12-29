using System;
using GraphProcessor;

[Serializable] [NodeMenuItem("Matrix/MatrixArithmeticFunction")]
public class MatrixArithmeticFunctionNode : MatrixBinaryFunctionNodeBase
{
    public ArithmeticFunction arithmeticFunction = ArithmeticFunction.Add;

    protected override void Process()
    {
        if (inputA is null || inputB is null) return;

        output = arithmeticFunction switch
        {
            ArithmeticFunction.Add => inputA + inputB,
            ArithmeticFunction.Sub => inputA - inputB,
            ArithmeticFunction.Mul => inputA * inputB,
            ArithmeticFunction.Div => inputA / inputB,
            _                      => throw new ArgumentOutOfRangeException()
        };
    }
}