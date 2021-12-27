using System;
using GraphProcessor;

[Serializable]
public class BaseMatrixNode : BaseNode
{
    [NodeOutput("Out")]
    public Matrix output;

    public override bool isRenamable => true;
}