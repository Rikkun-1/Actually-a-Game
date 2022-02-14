using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphProcessor;
using System.Linq;

[System.Serializable, NodeMenuItem("Operations/Sub")]
public class SubNode : BaseNode
{
	[NodeInput(name = "A")]
    public float                inputA;
	[NodeInput(name = "B")]
    public float                inputB;

	[NodeOutput(name = "Out")]
	public float				output;

	public override string		name => "Sub";

	protected override void Process()
	{
	    output = inputA - inputB;
	}
}
