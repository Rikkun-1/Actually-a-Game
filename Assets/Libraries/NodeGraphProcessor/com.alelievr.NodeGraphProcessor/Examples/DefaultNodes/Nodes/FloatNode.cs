using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphProcessor;

[System.Serializable, NodeMenuItem("Primitives/Float")]
public class FloatNode : BaseNode
{
    [NodeOutput("Out")]
	public float		output;
	
    [NodeInput("In")]
	public float		input;

	public override string name => "Float";

	protected override void Process() => output = input;
}