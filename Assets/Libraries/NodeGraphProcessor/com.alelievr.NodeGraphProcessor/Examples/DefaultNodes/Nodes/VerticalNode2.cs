using UnityEngine;
using GraphProcessor;

[System.Serializable, NodeMenuItem("Custom/Vertical 2")]
public class VerticalNode2 : BaseNode
{
	[NodeInput, Vertical]
    public float                input;

	[NodeInput]
    public float                input2;

	[NodeOutput, Vertical]
	public float				output;

	[NodeOutput]
	public float				output2;

	public override string		name => "Vertical 2";

	protected override void Process()
	{
	    output = input * 42;
	}
}
