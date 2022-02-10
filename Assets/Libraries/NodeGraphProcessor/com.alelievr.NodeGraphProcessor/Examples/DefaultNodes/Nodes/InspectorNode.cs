using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphProcessor;
using System.Linq;

[System.Serializable, NodeMenuItem("Custom/InspectorNode")]
public class InspectorNode : BaseNode
{
	[NodeInput(name = "In")]
    public float                input;

	[NodeOutput(name = "Out")]
	public float				output;

	[ShowInInspector]
	public bool additionalSettings;
	[ShowInInspector]
	public string additionalParam;

	public override string		name => "InspectorNode";

	protected override void Process()
	{
	    output = input * 42;
	}
}
