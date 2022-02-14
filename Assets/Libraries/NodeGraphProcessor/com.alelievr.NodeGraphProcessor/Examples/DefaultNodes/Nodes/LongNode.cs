using System;
using GraphProcessor;

[Serializable] [NodeMenuItem("Primitives/Long")]
public class LongNode : BaseNode
{
    [NodeOutput("Out")]
    public float output;

    [NodeInput("In")]
    public float input;

    public override string name => "Float";

    protected override void Process() => output = input;
}