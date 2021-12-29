using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphProcessor;
using System.Linq;

[System.Serializable, NodeMenuItem("Conditional/Switch")]
public class SwitchNode : BaseNode
{
	[NodeInput(name = "In")]
    public float                input;

	[NodeOutput(name = "Out")]
	public float				output;

	public override string		name => "Switch";

	protected override void Process()
	{
	    output = input * 42;
	}
}
