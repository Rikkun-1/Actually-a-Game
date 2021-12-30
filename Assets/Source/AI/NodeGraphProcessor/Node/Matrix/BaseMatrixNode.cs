using System;
using GraphProcessor;

[Serializable]
public abstract class BaseMatrixNode : BaseNode
{
    [NodeOutput("Out")]
    public Matrix output;

    public override bool isRenamable => true;
}