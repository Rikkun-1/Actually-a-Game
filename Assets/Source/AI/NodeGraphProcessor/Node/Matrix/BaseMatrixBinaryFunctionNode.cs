using GraphProcessor;

public abstract class BaseMatrixBinaryFunctionNode : BaseMatrixNode
{
    [NodeInput("InA")]
    public Matrix inputA;

    [NodeInput("InB")]
    public Matrix inputB;
}