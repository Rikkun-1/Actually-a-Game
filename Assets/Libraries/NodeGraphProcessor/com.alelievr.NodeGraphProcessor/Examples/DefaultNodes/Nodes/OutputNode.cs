using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphProcessor;
using System.Linq;

[System.Serializable, NodeMenuItem("Custom/OutputNode")]
public class OutputNode : BaseNode
{
	[NodeInput(name = "In")]
    public float                input;

	public override string		name => "OutputNode";

	public override bool		deletable => true;

	protected override void Process()
	{
		// Do stuff
	}
}
