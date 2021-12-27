using GraphProcessor;

public class MatrixBinaryFunctionNodeBase : BaseMatrixNode
{
    [NodeInput("InA")]
    public Matrix inputA;

    [NodeInput("InB")]
    public Matrix inputB;
}