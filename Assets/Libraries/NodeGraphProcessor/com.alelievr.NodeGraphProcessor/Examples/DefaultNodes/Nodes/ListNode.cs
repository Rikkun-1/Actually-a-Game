using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphProcessor;
using System.Linq;

[System.Serializable, NodeMenuItem("Custom/List")]
public class ListNode : BaseNode
{
	[NodeOutput(name = "Out")]
	public Vector4				output;
	
	[NodeInput("In"), SerializeField]
	public Vector4				input;

	public List<GameObject>		objs = new List<GameObject>();

	public override string		name => "List";

	protected override void Process()
	{
		output = input;
	}
}
