	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GraphProcessor;
using System.Linq;

[System.Serializable, NodeMenuItem("Custom/Drawer Field Test")]
public class DrawerFieldTestNode : BaseNode
{

	[NodeInput(name = "Vector 4"), ShowAsDrawer]
	public Vector4 vector4;

	[NodeInput("Vector 3"), ShowAsDrawer]
	public Vector3 vector3;

	[NodeInput("Vector 2"), ShowAsDrawer]
	public Vector2 vector2;

	[NodeInput("Float"), ShowAsDrawer]
	public float floatInput;

	[NodeInput("Vector 3 Int"), ShowAsDrawer]
	public Vector3Int vector3Int;

	[NodeInput("Vector 2 Int"), ShowAsDrawer]
	public Vector2Int vector2Int;

	[NodeInput("Int"), ShowAsDrawer]
	public int intInput;

	[NodeInput(name = "Empty")]
	public int intInput2;

	[NodeInput("String"), ShowAsDrawer]
	public string stringInput;

	[NodeInput("Color"), ShowAsDrawer]
	new public Color color;

	[NodeInput("Game Object"), ShowAsDrawer]
	public GameObject gameObject;

	[NodeInput("Animation Curve"), ShowAsDrawer]
	public AnimationCurve animationCurve;

	[NodeInput("Rigidbody"), ShowAsDrawer]
	public Rigidbody rigidbody;

	[NodeInput("Layer Mask"), ShowAsDrawer]
	public LayerMask layerMask;

	public override string name => "Drawer Field Test";

	protected override void Process() {}
}