using UnityEngine;
using GraphProcessor;

[System.Serializable, NodeMenuItem("Custom/Renamable")]
public class RenamableNode : BaseNode
{
    [NodeOutput("Out")]
	public float		output;
	
    [NodeInput("In")]
	public float		input;

	public override string name => "Renamable";

    public override bool isRenamable => true;

	protected override void Process() => output = input;
}