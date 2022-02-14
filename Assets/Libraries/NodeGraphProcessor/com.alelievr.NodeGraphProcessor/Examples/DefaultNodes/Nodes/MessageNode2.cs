using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphProcessor;
using System.Linq;

[System.Serializable, NodeMenuItem("Custom/MessageNode2")]
public class MessageNode2 : BaseNode
{
	[NodeInput(name = "In")]
    public float                input;

	[NodeOutput(name = "Out")]
	public float				output;

	public override string		name => "MessageNode2";

	protected override void Process()
	{
	    output = input * 42;
	}
}
