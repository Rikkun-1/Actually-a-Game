using UnityEngine;
using GraphProcessor;

[System.Serializable, NodeMenuItem("Custom/Vertical")]
public class VerticalNode : BaseNode
{
	[NodeInput, Vertical]
    public float                input;

	[NodeOutput, Vertical]
	public float				output;
	[NodeOutput, Vertical]
	public float				output2;
	[NodeOutput, Vertical]
	public float				output3;

	public override string		name => "Vertical";

	protected override void Process()
	{
	    output = input * 42;
	}
}
