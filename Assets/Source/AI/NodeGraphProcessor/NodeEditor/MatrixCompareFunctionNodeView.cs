using GraphProcessor;

[NodeCustomEditor(typeof(MatrixCompareFunctionNode))]
internal class MatrixCompareFunctionNodeView : MatrixBinaryFunctionNodeViewBase
{
    protected override float GetColorDivider()
    {
        return normalize
                   ? Matrix.GetMaximumOfAll(new[] { nodeTarget.inputA, nodeTarget.inputB, nodeTarget.output })
                   : 1f;
    }
}